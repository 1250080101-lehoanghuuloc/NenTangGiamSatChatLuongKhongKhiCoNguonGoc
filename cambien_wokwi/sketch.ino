/*
  Thiet bi lam 3 viec:
    1) THU NHAN phep do tu cam bien (doc ADC, gan thoi diem do)
    2) KIEM TRA CHAT LUONG so bo (HOP_LE / NGHI_NGO / KHONG_HOP_LE kem ly do)
    3) CONG BO NGUON GOC: gui len MQTT ban tin gom gia tri + 4 thuoc tinh
       (trang thai hop le, phuong phap do, phien ban hieu chuan, do tre)
  Phan mem WinForms/Oracle nhan ban tin, tinh thoi diem nhan, kiem tra lai va luu.
*/
#include <WiFi.h>
#include <PubSubClient.h>
#include <time.h>

// ---------- Dinh nghia kieu du lieu (Dat len tren dau truoc khi dung) ----------
struct KetQua { 
  const char* trangThai; 
  String lyDo; 
};

// ---------- Cau hinh ----------
const char* WIFI_SSID = "Wokwi-GUEST";
const char* WIFI_PASS = "";
const char* MQTT_HOST = "broker.hivemq.com";
const uint16_t MQTT_PORT = 1883;
// DOI thanh chuoi rieng cua ban (broker cong khai, ai cung doc duoc)
const char* MQTT_TOPIC = "envmon-demo-abc123/tram01/phepdo";
const char* MA_TRAM = "TRAM01";

const unsigned long CHU_KY_MS = 2000;       // chu ky do (demo:  giay)
const long NGUONG_DO_TRE_S = 30;            // do tre > 30 s => nghi ngo

// Nut gia lap loi (giu nut khi den chu ky do)
const int PIN_BTN_DOT_BIEN = 25;   // gia tri tang dot bien
const int PIN_BTN_AM       = 26;   // gia tri am (-5)
const int PIN_BTN_TRE      = 27;   // du lieu den tre 60 giay
// LED trang thai
const int PIN_LED_HOP_LE = 16, PIN_LED_NGHI_NGO = 17, PIN_LED_KHONG_HOP_LE = 18;

// ---------- Du lieu cam bien ----------
struct HieuChuan { const char* ma; time_t tuLuc; };

// Khớp chính xác mã phiên bản với bảng PhienBanHieuChuan trong Oracle (từ 2026-01-01 00:00 UTC+7)
const HieuChuan HC_CB01[] = { { "PB_CB01_2026", 1767200400 } };
const HieuChuan HC_CB03[] = { { "PB_CB03_2026", 1767200400 } };

struct CamBien {
  const char* ma; const char* loaiChiSo; const char* donVi;
  const char* maPP; const char* tenPP;
  int pin; float giaTriToiDa;      // nguong vat ly
  float nguongDotBien;             // chenh lech toi da so voi lan truoc
  const HieuChuan* hc; int soHc;   // nullptr = chua co phien ban hieu chuan
  float truoc; bool coTruoc;
};

CamBien dsCamBien[] = {
  { "CB01", "PM2.5",  "ug/m3", "PP01", "Tan xao anh sang (Laser)", 34, 500,  100, HC_CB01, 1, 0, false },
  { "CB02", "PM10",   "ug/m3", "PP01", "Tan xao anh sang (Laser)", 35, 600,  150, nullptr, 0, 0, false }, 
  { "CB03", "CO2",    "ppm",   "PP03", "Hong ngoai khong phan tan (NDIR)", 32, 5000, 500, HC_CB03, 1, 0, false },
  { "CB04", "NhietDo","°C",    "PP05", "Cam bien dien tro nhiet", 33, 100,  20, nullptr, 0, 0, false }
};
const int SO_CB = sizeof(dsCamBien) / sizeof(dsCamBien[0]);

WiFiClient wifi;
PubSubClient mqtt(wifi);
unsigned long lanDoCuoi = 0;

// ---------- Tien ich ----------
String dinhDangGio(time_t t) {
  struct tm tmv; localtime_r(&t, &tmv);
  char buf[24]; strftime(buf, sizeof(buf), "%Y-%m-%d %H:%M:%S", &tmv);
  return String(buf);
}
String thoatChuoi(const String& s) {      // thoat dau " va \ de JSON hop le
  String r;
  for (size_t i = 0; i < s.length(); i++) { char c = s[i]; if (c == '"' || c == '\\') r += '\\'; r += c; }
  return r;
}
void hienLed(const char* trangThai) {
  digitalWrite(PIN_LED_HOP_LE, strcmp(trangThai, "HOP_LE") == 0);
  digitalWrite(PIN_LED_NGHI_NGO, strcmp(trangThai, "NGHI_NGO") == 0);
  digitalWrite(PIN_LED_KHONG_HOP_LE, strcmp(trangThai, "KHONG_HOP_LE") == 0);
}
void ketNoiWifi() {
  WiFi.begin(WIFI_SSID, WIFI_PASS);
  Serial.print("Dang ket noi WiFi");
  while (WiFi.status() != WL_CONNECTED) { delay(300); Serial.print("."); }
  Serial.println(" OK");
}
void dongBoGio() {
  configTime(7 * 3600, 0, "pool.ntp.org", "time.nist.gov");   // UTC+7
  Serial.print("Dong bo gio NTP");
  while (time(nullptr) < 1700000000) { delay(300); Serial.print("."); }
  Serial.println(" OK");
}
void ketNoiMqtt() {
  while (!mqtt.connected()) {
    String id = String("envmon-") + MA_TRAM + "-" + String((uint32_t)esp_random(), HEX);
    Serial.print("Ket noi MQTT... ");
    if (mqtt.connect(id.c_str())) Serial.println("OK");
    else { Serial.print("loi rc="); Serial.println(mqtt.state()); delay(2000); }
  }
}

// ---------- 2) Kiem tra chat luong so bo ----------
KetQua kiemTraChatLuong(CamBien& cb, float v, time_t tgDo, time_t tgGui, const char* maPhienBan) {
  // Quy tac nguong vat ly: vi pham nghiem trong => KHONG_HOP_LE
  if (v < 0)              return { "KHONG_HOP_LE", "Gia tri am (" + String(v, 1) + ")" };
  if (v > cb.giaTriToiDa) return { "KHONG_HOP_LE", "Vuot nguong vat ly " + String(cb.giaTriToiDa, 0) };
  // Cac quy tac con lai: dau hieu bat thuong => NGHI_NGO
  String lyDo = "";
  long doTre = (long)(tgGui - tgDo);
  if (doTre > NGUONG_DO_TRE_S) lyDo += "Do tre " + String(doTre) + " s vuot nguong " + String(NGUONG_DO_TRE_S) + " s; ";
  if (cb.coTruoc && fabsf(v - cb.truoc) > cb.nguongDotBien)
    lyDo += "Dot bien so voi lan truoc (" + String(cb.truoc, 1) + " -> " + String(v, 1) + "); ";
  if (maPhienBan == nullptr) lyDo += "Chua co phien ban hieu chuan hieu luc; ";
  if (lyDo.length() > 0) return { "NGHI_NGO", lyDo };
  return { "HOP_LE", "" };
}

// ---------- 1) + 3) Thu nhan va cong bo ----------
void doVaCongBo(CamBien& cb) {
  // 1) Thu nhan
  time_t tgGui = time(nullptr);
  time_t tgDo = tgGui;
  float v = analogRead(cb.pin) * cb.giaTriToiDa / 4095.0f;
  if (digitalRead(PIN_BTN_TRE) == LOW)           tgDo = tgGui - 60;   // gia lap den tre
  if (digitalRead(PIN_BTN_AM) == LOW)            v = -5;              // gia lap gia tri am
  else if (digitalRead(PIN_BTN_DOT_BIEN) == LOW) v += 200;            // gia lap dot bien

  // Phien ban hieu chuan hieu luc tai thoi diem do (moi nhat co tuLuc <= tgDo)
  const char* maPhienBan = nullptr;
  for (int i = 0; i < cb.soHc; i++) if (cb.hc[i].tuLuc <= tgDo) maPhienBan = cb.hc[i].ma;

  // 2) Kiem tra chat luong
  KetQua kq = kiemTraChatLuong(cb, v, tgDo, tgGui, maPhienBan);
  hienLed(kq.trangThai);

  // 3) Cong bo nguon goc
  char json[640];
  snprintf(json, sizeof(json),
    "{\"maTram\":\"%s\",\"maCamBien\":\"%s\",\"loaiChiSo\":\"%s\",\"donVi\":\"%s\","
    "\"maPP\":\"%s\",\"phuongPhapDo\":\"%s\",\"phienBanHieuChuan\":%s%s%s,"
    "\"giaTri\":%.2f,\"tgDo\":\"%s\",\"tgGui\":\"%s\",\"doTreGiay\":%ld,"
    "\"trangThai\":\"%s\",\"lyDo\":\"%s\"}",
    MA_TRAM, cb.ma, cb.loaiChiSo, cb.donVi, cb.maPP, cb.tenPP,
    maPhienBan ? "\"" : "", maPhienBan ? maPhienBan : "null", maPhienBan ? "\"" : "",
    v, dinhDangGio(tgDo).c_str(), dinhDangGio(tgGui).c_str(), (long)(tgGui - tgDo),
    kq.trangThai, thoatChuoi(kq.lyDo).c_str());

  bool ok = mqtt.publish(MQTT_TOPIC, json);
  Serial.println(json);
  Serial.println(ok ? "  -> da cong bo" : "  -> LOI gui MQTT");

  // Ghi nho gia tri gan nhat
  if (strcmp(kq.trangThai, "KHONG_HOP_LE") != 0) { cb.truoc = v; cb.coTruoc = true; }
}

void setup() {
  Serial.begin(115200);
  pinMode(PIN_BTN_DOT_BIEN, INPUT_PULLUP);
  pinMode(PIN_BTN_AM, INPUT_PULLUP);
  pinMode(PIN_BTN_TRE, INPUT_PULLUP);
  pinMode(PIN_LED_HOP_LE, OUTPUT);
  pinMode(PIN_LED_NGHI_NGO, OUTPUT);
  pinMode(PIN_LED_KHONG_HOP_LE, OUTPUT);
  ketNoiWifi();
  dongBoGio();
  mqtt.setServer(MQTT_HOST, MQTT_PORT);
  mqtt.setBufferSize(1024);
}

void loop() {
  if (!mqtt.connected()) ketNoiMqtt();
  mqtt.loop();
  if (millis() - lanDoCuoi >= CHU_KY_MS) {
    lanDoCuoi = millis();
    for (int i = 0; i < SO_CB; i++) doVaCongBo(dsCamBien[i]);
  }
}