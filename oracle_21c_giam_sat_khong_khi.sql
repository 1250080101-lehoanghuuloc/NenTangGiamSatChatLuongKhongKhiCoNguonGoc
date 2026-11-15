SET DEFINE OFF;
-- =====================================================================
-- 1. BẢNG VAI TRÒ
-- =====================================================================
CREATE TABLE VaiTro (
    MaVaiTro    NUMBER(10) PRIMARY KEY,
    TenVaiTro   VARCHAR2(50) NOT NULL
);

-- =====================================================================
-- 2. BẢNG TÀI KHOẢN
-- =====================================================================
CREATE TABLE TaiKhoan (
    MaTaiKhoan   NUMBER(10) PRIMARY KEY,
    TenDangNhap  VARCHAR2(50) NOT NULL,
    MatKhauHash  VARCHAR2(200) NOT NULL,
    HoTen        NVARCHAR2(100),
    MaVaiTro     NUMBER(10) NOT NULL,
    KichHoat     NUMBER(1),
    CONSTRAINT FK_TaiKhoan_VaiTro FOREIGN KEY (MaVaiTro) REFERENCES VaiTro(MaVaiTro)
);

-- =====================================================================
-- 3. BẢNG TRẠM QUAN TRẮC
-- =====================================================================
CREATE TABLE Tram (
    MaTram       VARCHAR2(20) PRIMARY KEY,
    TenTram      NVARCHAR2(100) NOT NULL,
    ToaDo        VARCHAR2(40),
    Diachi       NVARCHAR2(200)
);

-- =====================================================================
-- 4. BẢNG PHƯƠNG PHÁP ĐO
-- =====================================================================
CREATE TABLE PhuongPhapDo (
    MaPhuongPhap  NUMBER(10) PRIMARY KEY,
    TenPhuongPhap NVARCHAR2(100) NOT NULL,
    MoTa          NVARCHAR2(300)
);

-- =====================================================================
-- 5. BẢNG CẢM BIẾN
-- =====================================================================
CREATE TABLE CamBien (
    MaCamBien     VARCHAR2(20) PRIMARY KEY,
    MaTram        VARCHAR2(20) NOT NULL,
    MaPhuongPhap  NUMBER(10) NOT NULL,
    LoaiChiSo     VARCHAR2(50) NOT NULL,
    DonViDo       VARCHAR2(20) NOT NULL,
    CONSTRAINT FK_CamBien_Tram FOREIGN KEY (MaTram) REFERENCES Tram(MaTram),
    CONSTRAINT FK_CamBien_PhuongPhap FOREIGN KEY (MaPhuongPhap) REFERENCES PhuongPhapDo(MaPhuongPhap)
);

-- =====================================================================
-- 6. BẢNG PHIÊN BẢN HIỆU CHUẨN
-- =====================================================================
CREATE TABLE PhienBanHieuChuan (
    MaPhienBan         VARCHAR2(20) PRIMARY KEY,
    MaCamBien          VARCHAR2(20) NOT NULL,
    ThoiDiemBatDau     TIMESTAMP NOT NULL,
    ThoiDiemHetHieuLuc TIMESTAMP,
    CONSTRAINT FK_HieuChuan_CamBien FOREIGN KEY (MaCamBien) REFERENCES CamBien(MaCamBien)
);

-- =====================================================================
-- 7. BẢNG PHÉP ĐO (DỮ LIỆU QUAN TRẮC & PROVENANCE)
-- =====================================================================
CREATE TABLE PhepDo (
    MaPhepDo       NUMBER(19) PRIMARY KEY,
    MaCamBien      VARCHAR2(20) NOT NULL,
    MaPhuongPhap   NUMBER(10) NOT NULL,
    MaPhienBan     VARCHAR2(20),
    GiaTriDo       NUMBER(12,4) NOT NULL,
    ThoiDiemDo     TIMESTAMP NOT NULL,
    ThoiDiemNhan   TIMESTAMP NOT NULL,
    DoTre          NUMBER(12,3),
    TrangThai      VARCHAR2(12) NOT NULL,
    LyDo           NVARCHAR2(500),
    CONSTRAINT FK_PhepDo_CamBien FOREIGN KEY (MaCamBien) REFERENCES CamBien(MaCamBien),
    CONSTRAINT FK_PhepDo_PhuongPhap FOREIGN KEY (MaPhuongPhap) REFERENCES PhuongPhapDo(MaPhuongPhap),
    CONSTRAINT FK_PhepDo_HieuChuan FOREIGN KEY (MaPhienBan) REFERENCES PhienBanHieuChuan(MaPhienBan)
);

SET DEFINE OFF;

-- Dọn sạch dữ liệu cũ theo thứ tự từ bảng con đến bảng cha để tránh vướng khóa ngoại
DELETE FROM PhienBanHieuChuan;
DELETE FROM CamBien;
DELETE FROM PhuongPhapDo;
DELETE FROM Tram;
DELETE FROM TaiKhoan;
DELETE FROM VaiTro;
COMMIT;

-- 1. Chèn Vai trò
INSERT INTO VaiTro (MaVaiTro, TenVaiTro) VALUES (1, 'Quản lý');
INSERT INTO VaiTro (MaVaiTro, TenVaiTro) VALUES (2, 'Người dùng');

-- 2. Chèn Tài khoản (admin / admin123)
INSERT INTO TaiKhoan (MaTaiKhoan, TenDangNhap, MatKhauHash, HoTen, MaVaiTro, KichHoat) 
VALUES (1, 'admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Quản trị viên', 1, 1);

-- 3. Chèn Trạm quan trắc mẫu
INSERT INTO Tram (MaTram, TenTram, ToaDo, Diachi) 
VALUES ('TR01', 'Trạm Quan trắc Quận 1', '10.7769, 106.7009', 'Sở TN&MT TP. Hồ Chí Minh');

-- 4. Chèn Phương pháp đo 
INSERT INTO PhuongPhapDo (MaPhuongPhap, TenPhuongPhap, MoTa) 
VALUES (1, 'Tán xạ ánh sáng (Laser Scattering)', 'Phương pháp quang học đo nồng độ bụi mịn PM2.5/PM10');
SET DEFINE OFF;
-- Phương pháp cảm biến điện hóa đo khí CO, NO2, SO2
INSERT INTO PhuongPhapDo (MaPhuongPhap, TenPhuongPhap, MoTa) 
VALUES (2, 'Cảm biến điện hóa (Electrochemical Sensor)', 'Đo nồng độ các khí độc hại như CO, NO2, SO2 dựa trên dòng điện sinh ra từ phản ứng oxy hóa khử');

-- Phương pháp hấp thụ hồng ngoại không phân tán (NDIR) đo CO2
INSERT INTO PhuongPhapDo (MaPhuongPhap, TenPhuongPhap, MoTa) 
VALUES (3, 'Hồng ngoại không phân tán (NDIR)', 'Phương pháp quang học chuyên dùng để đo nồng độ khí CO2 dựa trên mức độ hấp thụ bước sóng hồng ngoại');

-- Phương pháp bán dẫn (Metal Oxide Semiconductor - MOS) đo các hợp chất hữu dễ bay hơi (VOCs)
INSERT INTO PhuongPhapDo (MaPhuongPhap, TenPhuongPhap, MoTa) 
VALUES (4, 'Bán dẫn oxit kim loại (MOS)', 'Đo tổng lượng khí hữu cơ bay hơi (VOCs) và các loại khí dễ cháy dựa trên sự thay đổi điện trở bề mặt');

-- Phương pháp điện dung / điện trở nhiệt đo Nhiệt độ & Độ ẩm
INSERT INTO PhuongPhapDo (MaPhuongPhap, TenPhuongPhap, MoTa) 
VALUES (5, 'Cảm biến điện dung và điện trở (Capacitive & Resistive)', 'Đo nhiệt độ và độ ẩm không khí qua sự thay đổi điện dung và điện trở của vật liệu cảm biến');

COMMIT;

-- 5. Chèn Cảm biến (gắn với Trạm TR01 và Phương pháp 1)
INSERT INTO CamBien (MaCamBien, MaTram, MaPhuongPhap, LoaiChiSo, DonViDo) 
VALUES ('CB01', 'TR01', 1, 'PM2.5', 'ug/m3');

-- 6. Chèn Phiên bản hiệu chuẩn cho cảm biến CB01
INSERT INTO PhienBanHieuChuan (MaPhienBan, MaCamBien, ThoiDiemBatDau, ThoiDiemHetHieuLuc) 
VALUES ('PB_CB01_2026', 'CB01', SYSTIMESTAMP - 30, NULL);

COMMIT;
select * from PhepDo;

CREATE SEQUENCE SEQ_PHEPDO
START WITH 1
INCREMENT BY 1
NOCACHE
NOCYCLE;

CREATE OR REPLACE VIEW V_ChiTietPhepDo AS
SELECT 
    p.MaPhepDo,
    t.MaTram,
    t.TenTram,
    cb.MaCamBien,
    cb.LoaiChiSo,
    cb.DonViDo,
    p.GiaTriDo,
    p.ThoiDiemDo,
    p.ThoiDiemNhan,
    p.DoTre,
    p.TrangThai,
    p.LyDo,
    p.MaPhienBan
FROM PhepDo p
JOIN CamBien cb ON p.MaCamBien = cb.MaCamBien
JOIN Tram t ON cb.MaTram = t.MaTram;

CREATE OR REPLACE TRIGGER TRG_PhepDo_AutoID
BEFORE INSERT ON PhepDo
FOR EACH ROW
BEGIN
    IF :NEW.MaPhepDo IS NULL THEN
        SELECT SEQ_PHEPDO.NEXTVAL INTO :NEW.MaPhepDo FROM DUAL;
    END IF;
END;

CREATE OR REPLACE TRIGGER TRG_KiemTraPhepDo
BEFORE INSERT ON PhepDo
FOR EACH ROW
BEGIN
    -- Kiểm tra nếu giá trị đo âm (lỗi phần cứng / cảm biến)
    IF :NEW.GiaTriDo < 0 THEN
        :NEW.TrangThai := 'KHONG_HOP_LE';
        :NEW.LyDo := NVL(:NEW.LyDo, 'Gia tri do am (loi cam bien)');
    -- Kiểm tra nếu giá trị PM2.5 / PM10 quá cao bất thường (đột biến)
    ELSIF :NEW.GiaTriDo > 500 THEN
        :NEW.TrangThai := 'NGHI_NGO';
        :NEW.LyDo := NVL(:NEW.LyDo, 'Gia tri vuot nguong bat thuong');
    END IF;
END;

CREATE OR REPLACE TRIGGER TRG_PhepDo_AutoID
BEFORE INSERT ON PhepDo
FOR EACH ROW
BEGIN
    IF :NEW.MaPhepDo IS NULL THEN
        SELECT SEQ_PHEPDO.NEXTVAL INTO :NEW.MaPhepDo FROM DUAL;
    END IF;
END;

SELECT TRIGGER_NAME, STATUS, TRIGGER_TYPE, TRIGGERING_EVENT 
FROM USER_TRIGGERS 
WHERE TABLE_NAME = 'PHEPDO';

CREATE OR REPLACE TRIGGER TRG_KiemTraPhepDo
BEFORE INSERT ON PhepDo
FOR EACH ROW
BEGIN
    IF :NEW.GiaTriDo < 0 THEN
        :NEW.TrangThai := 'KHONG_HOP_LE';
        :NEW.LyDo := NVL(:NEW.LyDo, 'Gia tri do am (loi cam bien)');
    ELSIF :NEW.GiaTriDo > 500 THEN
        :NEW.TrangThai := 'NGHI_NGO';
        :NEW.LyDo := NVL(:NEW.LyDo, 'Gia tri vuot nguong bat thuong');
    END IF;
END;

SELECT 
    MAPHEPDO, 
    MACAMBIEN, 
    MAPHIENBAN, 
    GIATRIDO, 
    TRANGTHAI, 
    LYDO, 
    ThoiDiemNhan 
FROM PhepDo 
ORDER BY MAPHEPDO DESC;
-- PROCEDURE
CREATE OR REPLACE PROCEDURE SP_ThemPhepDo (
    p_MaCamBien    IN VARCHAR2,
    p_MaPhuongPhap IN NUMBER,
    p_MaPhienBan   IN VARCHAR2,
    p_GiaTriDo     IN NUMBER,
    p_ThoiDiemDo   IN TIMESTAMP,
    p_ThoiDiemNhan IN TIMESTAMP,
    p_DoTre        IN NUMBER,
    p_TrangThai    IN VARCHAR2,
    p_LyDo         IN NVARCHAR2
) AS
BEGIN
    -- Thực hiện chèn dữ liệu (Trigger tự động tăng ID và kiểm tra hợp lệ sẽ chạy ở đây)
    INSERT INTO PhepDo (
        MaCamBien, MaPhuongPhap, MaPhienBan, GiaTriDo, 
        ThoiDiemDo, ThoiDiemNhan, DoTre, TrangThai, LyDo
    ) VALUES (
        p_MaCamBien, p_MaPhuongPhap, p_MaPhienBan, p_GiaTriDo, 
        p_ThoiDiemDo, p_ThoiDiemNhan, p_DoTre, p_TrangThai, p_LyDo
    );
    
    -- Xác nhận giao dịch thành công
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        -- Gặp lỗi bất kỳ thì hủy bỏ (Rollback) toàn bộ thay đổi
        ROLLBACK;
        RAISE_APPLICATION_ERROR(-20001, 'Lỗi lưu phép đo trong CSDL: ' || SQLERRM);
END;


CREATE OR REPLACE PROCEDURE SP_TaoPhienBanHieuChuan (
    p_MaPhienBan   IN VARCHAR2,
    p_MaCamBien    IN VARCHAR2,
    p_ThoiDiemMoi  IN TIMESTAMP
) AS
    v_Count NUMBER;
BEGIN
    -- Kiểm tra xem mã phiên bản đã tồn tại chưa
    SELECT COUNT(*) INTO v_Count FROM PhienBanHieuChuan WHERE MaPhienBan = p_MaPhienBan;
    IF v_Count > 0 THEN
        RAISE_APPLICATION_ERROR(-20002, 'Mã phiên bản hiệu chuẩn đã tồn tại!');
    END IF;

    -- Thao tác 1: Cập nhật thời gian kết thúc của phiên bản hiệu chuẩn cũ đang chạy
    UPDATE PhienBanHieuChuan 
    SET ThoiDiemHetHieuLuc = p_ThoiDiemMoi 
    WHERE MaCamBien = p_MaCamBien 
      AND ThoiDiemHetHieuLuc IS NULL;

    -- Thao tác 2: Chèn phiên bản hiệu chuẩn mới vào bảng
    INSERT INTO PhienBanHieuChuan (
        MaPhienBan, MaCamBien, ThoiDiemBatDau, ThoiDiemHetHieuLuc
    ) VALUES (
        p_MaPhienBan, p_MaCamBien, p_ThoiDiemMoi, NULL
    );

    -- Cả hai thao tác thành công thì Commit chính thức lưu vào CSDL
    COMMIT;
EXCEPTION
    WHEN OTHERS THEN
        -- Nếu xảy ra lỗi giữa chừng, tự động Rollback trả lại trạng thái cũ
        ROLLBACK;
        RAISE_APPLICATION_ERROR(-20003, 'Lỗi giao dịch hiệu chuẩn: ' || SQLERRM);
END;

