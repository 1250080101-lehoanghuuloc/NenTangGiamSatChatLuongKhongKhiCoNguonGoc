# Nền tảng giám sát chất lượng không khí có nguồn gốc

Hệ thống thu nhận phép đo từ cảm biến, kiểm tra chất lượng sơ bộ, công bố **nguồn gốc dữ liệu** (phương pháp đo, phiên bản hiệu chuẩn, độ trễ, trạng thái hợp lệ) qua MQTT, sau đó phần mềm WinForms nhận bản tin và lưu vào Oracle.

- **Thiết bị (mô phỏng):** ESP32 chạy trên Wokwi, đọc ADC (biến trở giả lập cảm biến), kiểm tra sơ bộ, bật LED và publish JSON lên MQTT.
- **Phần mềm quản lý:** ứng dụng WinForms (.NET Framework 4.7.2) subscribe MQTT, lưu phép đo vào Oracle, quản lý trạm, cảm biến, hiệu chuẩn, tài khoản và xem dữ liệu.
- **CSDL:** Oracle 21c.

> Đề tài thuộc Khoa CNTT, Trường ĐH Tài nguyên và Môi trường TP.HCM (SV: Lê Hoàng Hữu Lộc, GVHD: Nguyễn Trọng Huynh).

---

## Cấu trúc thư mục

```
cambien_wokwi.zip
├── khongkhi/                        # Solution WinForms (khongkhi.slnx)
│   ├── packages/                    # NuGet packages
│   └── khongkhi/
│       ├── Frm/                     # Giao diện: đăng nhập, main, quản lý, xem dữ liệu, nguồn gốc
│       ├── Service/                 # Nghiệp vụ (Auth, ThuNhan, KiemTraChatLuong, MqttListener...)
│       ├── Repository/              # Truy cập Oracle (ADO.NET)
│       ├── DatabaseHelper.cs        # Chuỗi kết nối Oracle
│       └── Program.cs
├── cambien_wokwi/                   # Dự án Wokwi (ESP32)
│   ├── sketch.ino
│   ├── diagram.json
│   └── libraries.txt                # PubSubClient
└── oracle_21c_giam_sat_khong_khi.sql  # Tạo bảng, dữ liệu mẫu, trigger, procedure
```

## Công nghệ

| Thành phần | Chi tiết |
|---|---|
| Ứng dụng | C# WinForms, .NET Framework 4.7.2 |
| CSDL | Oracle 21c, `Oracle.ManagedDataAccess` 23.26 |
| MQTT (PC) | `MQTTnet` 4.3.1 |
| JSON | `Newtonsoft.Json` 13.0.4 |
| Thiết bị | ESP32 DevKit C v4 trên Wokwi, thư viện `PubSubClient` |
| Broker | `broker.hivemq.com:1883` (broker công khai) |

## Yêu cầu cài đặt

- Windows, Visual Studio 2022 trở lên (workload *.NET desktop development*) và .NET Framework 4.7.2 Developer Pack.
- Oracle Database 21c và công cụ chạy SQL (SQL Developer hoặc SQL*Plus).
- Kết nối Internet (MQTT broker công khai, Wokwi, NTP).

---

## Hướng dẫn chạy

> **Thứ tự bắt buộc: chạy project WinForms trước, rồi mới chạy Wokwi.**
> Broker công khai không lưu bản tin cũ. Nếu Wokwi gửi khi ứng dụng chưa subscribe, các bản tin đó sẽ mất.

### Bước 1. Tạo cơ sở dữ liệu Oracle

1. Mở SQL Developer, kết nối vào Oracle 21c.
2. Chạy toàn bộ file `oracle_21c_giam_sat_khong_khi.sql` bằng **Run Script (F5)**. File sẽ tạo bảng, sequence, view, trigger, procedure `SP_ThemPhepDo`, `SP_TaoPhienBanHieuChuan` và dữ liệu mẫu.
3. Tài khoản mẫu: `admin` / `admin123` (vai trò Quản lý).

**Bổ sung dữ liệu cho đủ 4 cảm biến.** Dữ liệu mẫu chỉ có `CB01`, trong khi sketch Wokwi gửi cả `CB01` đến `CB04`. Nếu thiếu, ứng dụng sẽ báo lỗi ràng buộc khóa ngoại khi lưu các cảm biến còn lại. Chạy thêm:

```sql
INSERT INTO CamBien (MaCamBien, MaTram, MaPhuongPhap, LoaiChiSo, DonViDo) VALUES ('CB02', 'TR01', 1, 'PM10',    'ug/m3');
INSERT INTO CamBien (MaCamBien, MaTram, MaPhuongPhap, LoaiChiSo, DonViDo) VALUES ('CB03', 'TR01', 3, 'CO2',     'ppm');
INSERT INTO CamBien (MaCamBien, MaTram, MaPhuongPhap, LoaiChiSo, DonViDo) VALUES ('CB04', 'TR01', 5, 'NhietDo', 'C');

-- Sketch gửi phiên bản PB_CB03_2026 cho CB03 (hiệu lực từ 2026-01-01)
INSERT INTO PhienBanHieuChuan (MaPhienBan, MaCamBien, ThoiDiemBatDau, ThoiDiemHetHieuLuc)
VALUES ('PB_CB03_2026', 'CB03', TIMESTAMP '2026-01-01 00:00:00', NULL);
COMMIT;
```

### Bước 2. Cấu hình kết nối Oracle

Mở `khongkhi/khongkhi/DatabaseHelper.cs` và sửa chuỗi kết nối cho khớp máy của bạn:

```csharp
private static string connectionString =
    "User Id=system;Password=12345;Data Source=localhost:1521/orcl21;";
```

Thay `User Id`, `Password` và service name (`orcl21`) nếu khác. Có thể kiểm tra tên service bằng lệnh `lsnrctl status`.

### Bước 3. Chạy project WinForms (làm trước)

1. Mở `khongkhi/khongkhi.slnx` bằng Visual Studio.
2. Chuột phải Solution, chọn **Restore NuGet Packages** (nếu thiếu package).
3. Nhấn **F5** (Start) để chạy.
4. Đăng nhập: `admin` / `admin123`.
5. Sau khi đăng nhập, form chính (`FrmMain`) tự kết nối broker và subscribe topic:

   ```
   envmon-demo-abc123/tram01/phepdo
   ```

   Nếu kết nối thất bại, ứng dụng hiện hộp thoại *"Lỗi kết nối MQTT Broker"*. Hãy kiểm tra Internet hoặc firewall (cổng 1883) trước khi sang bước sau.
6. **Giữ ứng dụng đang chạy.**

### Bước 4. Chạy Wokwi (làm sau)

1. Mở dự án Wokwi: <https://wokwi.com/projects/478822664188537857>
2. Nhấn nút **Start the simulation** (▶).
3. Quan sát **Serial Monitor**. Quá trình khởi động diễn ra theo thứ tự: nối WiFi `Wokwi-GUEST`, đồng bộ giờ NTP, kết nối MQTT. Sau đó thiết bị cứ mỗi **2 giây** đọc từng cảm biến và publish một bản tin JSON; Serial hiện dòng `-> da cong bo` khi gửi thành công.
4. Quay lại ứng dụng WinForms, vào **Xem dữ liệu** để thấy phép đo mới được lưu.

Hoặc kiểm tra trực tiếp trong Oracle:

```sql
SELECT MaPhepDo, MaCamBien, MaPhienBan, GiaTriDo, TrangThai, LyDo, ThoiDiemNhan
FROM PhepDo ORDER BY MaPhepDo DESC;
```

> Có thể dùng bản local thay cho link Wokwi: tạo dự án ESP32 mới trên Wokwi rồi chép nội dung `cambien_wokwi/sketch.ino`, `diagram.json`, `libraries.txt` vào.

---

## Cách mô phỏng và kiểm tra trên Wokwi

### Đấu nối

| Thành phần | Chân ESP32 | Ý nghĩa |
|---|---|---|
| Biến trở 1 | GPIO34 (ADC) | Cảm biến CB01 (PM2.5) |
| Biến trở 2 | GPIO35 (ADC) | Cảm biến CB02 (PM10) |
| Nút vàng | GPIO25 (INPUT_PULLUP) | Giữ nút: giá trị **tăng đột biến** (+200) |
| Nút đỏ | GPIO26 (INPUT_PULLUP) | Giữ nút: giá trị **âm** (-5) |
| Nút xanh | GPIO27 (INPUT_PULLUP) | Giữ nút: dữ liệu **đến trễ 60 giây** |
| LED xanh lá | GPIO16 | Trạng thái `HOP_LE` |
| LED vàng | GPIO17 | Trạng thái `NGHI_NGO` |
| LED đỏ | GPIO18 | Trạng thái `KHONG_HOP_LE` |

`CB03` (GPIO32) và `CB04` (GPIO33) có khai báo trong sketch nhưng chưa nối linh kiện trong sơ đồ Wokwi.

### Quy tắc kiểm tra chất lượng sơ bộ (trên ESP32)

| Điều kiện | Kết quả |
|---|---|
| Giá trị `< 0` | `KHONG_HOP_LE` (giá trị âm) |
| Giá trị `>` ngưỡng vật lý của cảm biến | `KHONG_HOP_LE` (vượt ngưỡng vật lý) |
| Độ trễ `> 30 s` | `NGHI_NGO` |
| Chênh lệch so với lần trước `>` ngưỡng đột biến | `NGHI_NGO` |
| Chưa có phiên bản hiệu chuẩn hiệu lực (ví dụ CB02, CB04) | `NGHI_NGO` |
| Không vi phạm quy tắc nào | `HOP_LE` |

Thử nghiệm: giữ nút vàng/đỏ/xanh vào đúng lúc đến chu kỳ đo để xem LED và trạng thái thay đổi.

### Mẫu bản tin JSON

```json
{"maTram":"TRAM01","maCamBien":"CB01","loaiChiSo":"PM2.5","donVi":"ug/m3",
 "maPP":"PP01","phuongPhapDo":"Tan xao anh sang (Laser)","phienBanHieuChuan":"PB_CB01_2026",
 "giaTri":123.45,"tgDo":"2026-11-15 10:00:00","tgGui":"2026-11-15 10:00:00",
 "doTreGiay":0,"trangThai":"HOP_LE","lyDo":""}
```

---

## Chức năng của ứng dụng WinForms

- Đăng nhập (mật khẩu băm SHA-256), phân quyền theo vai trò: **Quản lý** thấy đủ menu, **Người dùng** chỉ xem dữ liệu.
- Quản lý trạm quan trắc, cảm biến, phiên bản hiệu chuẩn, tài khoản.
- Xem dữ liệu phép đo (view `V_ChiTietPhepDo`) và xem nguồn gốc dữ liệu.
- Nhận MQTT và lưu phép đo vào bảng `PhepDo`.

## Lưu ý quan trọng

- **Topic MQTT phải khớp 100%** giữa `sketch.ino` (`MQTT_TOPIC`) và code C# (`FrmMain.cs`, `MqttListenerService.cs`). Broker `broker.hivemq.com` là công khai, ai cũng đọc được. Nên đổi `envmon-demo-abc123` thành chuỗi riêng của bạn ở **cả hai nơi**.
- Chạy Wokwi lúc ứng dụng chưa mở thì bản tin bị mất, hãy mở ứng dụng trước.
- Trigger `TRG_KiemTraPhepDo` trong Oracle có thể ghi đè trạng thái (giá trị `< 0` thành `KHONG_HOP_LE`, `> 500` thành `NGHI_NGO`), nên trạng thái lưu trong DB có thể khác trạng thái ESP32 gửi.
- Ứng dụng có hai chỗ lắng nghe MQTT (`FrmMain` và `FrmXemDuLieu` qua `MqttListenerService`). Nếu thấy một bản tin bị lưu hai lần, kiểm tra lại hai luồng này.
- Trong `PhepDoRepository.cs` một số truy vấn dùng tên bảng kiểu `CAM_BIEN`, `PHUONG_PHAP_DO`, `PHIEN_BAN_HIEU_CHUAN`, trong khi script SQL tạo bảng `CamBien`, `PhuongPhapDo`, `PhienBanHieuChuan`. Nếu gặp lỗi `ORA-00942: table or view does not exist` thì cần thống nhất lại tên bảng.

## Xử lý sự cố

| Hiện tượng | Cách xử lý |
|---|---|
| `ORA-12514` / không kết nối được Oracle | Kiểm tra service name, user, password trong `DatabaseHelper.cs` và Listener |
| `ORA-02291` (khóa ngoại) khi lưu phép đo | Thiếu cảm biến hoặc phiên bản hiệu chuẩn trong DB, chạy phần bổ sung ở Bước 1 |
| Wokwi kẹt ở `Dang ket noi WiFi` hoặc `Dong bo gio NTP` | Đợi vài giây, nếu vẫn kẹt thì dừng và chạy lại mô phỏng |
| Wokwi `Ket noi MQTT... loi rc=-2` | Broker không truy cập được, thử lại sau |
| Ứng dụng không nhận dữ liệu | Kiểm tra topic khớp hai bên và đã chạy ứng dụng trước Wokwi |
| Thiếu thư viện khi build | Restore NuGet Packages cho solution |
