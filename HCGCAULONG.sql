create database HCGCAULONG
use HCGCAULONG
CREATE TABLE [Account]
(
	AccountID NVARCHAR(10) PRIMARY KEY,	-- Mã tài khoản tự động tăng
    TaiKhoan NVARCHAR(100) NOT NULL,			-- Tên người dùng
    MatKhau NVARCHAR(100) NOT NULL,				-- Mật khẩu
	HoVaTen NVARCHAR(200) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,		-- Địa chỉ email (không được trùng)
    NgayTao DATETIME DEFAULT GETDATE(),		-- Ngày tạo tài khoản
	Quyen NVARCHAR(50) NOT NULL DEFAULT 'User'
)

CREATE OR ALTER TRIGGER trg_InsertAccountID
ON [Account]
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @newAccountID NVARCHAR(10);
    
    -- Tìm ID cao nhất hiện có
    SELECT @newAccountID = 'ACC' + CAST(COALESCE(MAX(CAST(SUBSTRING(AccountID, 2, LEN(AccountID) - 1) AS INT)), 0) + 1 AS NVARCHAR(10))
    FROM [Account];

    -- Chèn các bản ghi mới với SanPhamID được tạo ra
    INSERT INTO [Account] (AccountID, TaiKhoan, MatKhau, HoVaTen, Email, NgayTao, Quyen)
    SELECT @newAccountID, TaiKhoan, MatKhau, HoVaTen, Email, NgayTao, Quyen
    FROM inserted;
END;

DROP TABLE IF EXISTS [Account];

INSERT INTO [Account] (TaiKhoan, MatKhau,HoVaTen, Email, Quyen)
VALUES 
(N'ahtuanday', N'ahtuanday',N'Hoang Anh Tuan', N'ahtuanday@gmail.com', N'Admin')

CREATE TABLE [Vot] (
    SanPhamID NVARCHAR(10) PRIMARY KEY,         -- Mã sản phẩm dưới dạng chuỗi
    TenSanPham NVARCHAR(200) NOT NULL,        -- Tên của vợt
    ThuongHieu NVARCHAR(50) NOT NULL,         -- Thương hiệu (Ví dụ: Yonex, Victor, ...)
    LoaiVot NVARCHAR(50) NOT NULL,            -- Loại vợt (Tấn Công, Công Thủ Toàn Diện, Phản Tạt - Phòng Thủ)
    ChatLieu NVARCHAR(100) NULL,               -- Chất liệu của vợt
    TrongLuong NVARCHAR(10) NOT NULL,         -- Trọng lượng vợt cầu lông (2U, 3U, 4U, 5U, F, 2F)
    ChuViCan NVARCHAR(10) NOT NULL,           -- Chu vi cán vợt (G1, G2, G3, G4, G5)
    SucCang NVARCHAR(20) NOT NULL,            -- Sức căng của vợt (17-19 lbs, 22-24 lbs, 28-32 lbs)
    ChieuDai NVARCHAR(10) NOT NULL,           -- Chiều dài vợt (665, 670, 675 mm)
    DiemCanBang NVARCHAR(20) NOT NULL,        -- Điểm cân bằng vợt (Nhẹ Đầu, Cân Bằng, Hơi Nặng Đầu, Nặng Đầu, Siêu Nặng Đầu)
    DoCung NVARCHAR(20) NOT NULL,             -- Độ cứng vợt cầu lông (Dẻo, Trung Bình, Cứng, Siêu Cứng)
    DangMatVot NVARCHAR(20) NOT NULL,         -- Dạng mặt vợt (Vuông, Bầu Dục)
    TrongLuongVung NVARCHAR(20) NOT NULL,     -- Trọng lượng vung của vợt (dưới 82 kg/cm2, 82-84 kg/cm2, ...)
    MauSac NVARCHAR(50) NULL,                 -- Màu sắc của vợt
    TroLuc NVARCHAR(50) NOT NULL,             -- Trợ lực (Không trợ lực, Trợ lực ít, Trợ lực vừa phải, Trợ lực cao)
    Gia DECIMAL(18, 0) NOT NULL,              -- Giá (Dưới 500.000, 500.000 - 1.000.000, ...)
    MoTa NVARCHAR(MAX) NOT NULL,              -- Mô tả chi tiết sản phẩm
    AnhSanPham NVARCHAR(MAX) NULL              -- Đường dẫn hoặc tên file ảnh sản phẩm
);

CREATE OR ALTER TRIGGER trg_InsertSanPhamID
ON [Vot]
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @newSanPhamID NVARCHAR(10);
    
    -- Tìm ID cao nhất hiện có
    SELECT @newSanPhamID = 'V' + CAST(COALESCE(MAX(CAST(SUBSTRING(SanPhamID, 2, LEN(SanPhamID) - 1) AS INT)), 0) + 1 AS NVARCHAR(10))
    FROM [Vot];

    -- Chèn các bản ghi mới với SanPhamID được tạo ra
    INSERT INTO [Vot] (SanPhamID, TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
    SELECT @newSanPhamID, TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham
    FROM inserted;
END;

DROP TABLE IF EXISTS [Vot];

SELECT SanPhamID FROM [Vot] WHERE TenSanPham = N'Vợt Yonex Astrox 100';
SELECT SanPhamID, TenSanPham FROM Vot;


INSERT INTO [Vot] 
(TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Yonex Astrox 100', N'Yonex', N'Tấn Công', N'Graphite', N'4U', N'G4', N'26 lbs', N'675', N'Nặng Đầu', N'Cứng', N'Hình Chóp', N'75 - 80 kg/cm2', N'Xanh', N'Trợ lực vừa', 2500000, N'Vợt thích hợp cho những cú đánh mạnh mẽ.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\1.jpg')
INSERT INTO [Vot] 
(TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Victor Thruster K 770HT', N'Victor', N'Phản Tạt - Phòng Thủ', N'Graphite, Pyrofil', N'3U', N'G5', N'24', N'675', N'Hơi Nặng Đầu', N'Cứng', N'Vuông', N'82 - 84 kg/cm2', N'Xanh Dương/Trắng', N'Trợ lực cao', 2700000, N'Vợt dành cho lối chơi phản tạt và phòng thủ linh hoạt.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\2.jpg')
INSERT INTO [Vot] (TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Lining Aeronaut 9000', N'Lining', N'Công Thủ Toàn Diện', N'Military Grade Carbon Fiber', N'3U', N'G5', N'26', N'670', N'Cân Bằng', N'Trung Bình', N'Bầu dục', N'86 - 88 kg/cm2', N'Đen/Vàng', N'Trợ lực vừa phải', 2400000, N'Vợt có thiết kế tối ưu cho cả tấn công và phòng thủ.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\3.jpg')
INSERT INTO [Vot] (TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Mizuno JPX 8.1', N'Mizuno', N'Tấn Công', N'Hot Melt Graphite', N'4U', N'G4', N'29', N'675', N'Siêu Nặng Đầu', N'Cứng', N'Vuông', N'trên 88 kg/cm2', N'Xanh Lá', N'Trợ lực cao', 3500000, N'Vợt Mizuno JPX 8.1 phù hợp với người chơi tấn công mạnh.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\4.jpg')
INSERT INTO [Vot] (TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Proace Sweetspot 1000', N'Proace', N'Phản Tạt - Phòng Thủ', N'High Modulus Graphite', N'5U', N'G3', N'20', N'665', N'Nhẹ Đầu', N'Dẻo', N'Bầu dục', N'dưới 82 kg/cm2', N'Hồng/Trắng', N'Trợ lực ít', 1200000, N'Vợt siêu nhẹ, dễ dàng kiểm soát và linh hoạt.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\5.jpg')
INSERT INTO [Vot] (TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Kawasaki Honor S6', N'Kawasaki', N'Công Thủ Toàn Diện', N'High Modulus Graphite', N'3U', N'G2', N'24', N'670', N'Cân Bằng', N'Trung Bình', N'Vuông', N'82 - 84 kg/cm2', N'Xám', N'Trợ lực vừa phải', 1800000, N'Vợt phù hợp với người chơi thiên về lối chơi công thủ toàn diện.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\6.jpg')
INSERT INTO [Vot] (TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Felet Woven TK-888', N'Felet', N'Tấn Công', N'Ultra Carbon', N'2F', N'G4', N'30', N'675', N'Siêu Nặng Đầu', N'Siêu Cứng', N'Vuông', N'trên 88 kg/cm2', N'Đen/Đỏ', N'Trợ lực cao', 3800000, N'Vợt siêu cứng và nặng đầu cho những pha tấn công mạnh mẽ.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\7.jpg')
INSERT INTO [Vot] (TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Flypower Tornado 900', N'Flypower', N'Phản Tạt - Phòng Thủ', N'Graphite', N'4U', N'G5', N'22', N'670', N'Hơi Nặng Đầu', N'Trung Bình', N'Bầu dục', N'84 - 86 kg/cm2', N'Xanh Dương/Đen', N'Trợ lực vừa phải', 1600000, N'Vợt thích hợp cho người chơi phản tạt và phòng thủ.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\8.jpg')
INSERT INTO [Vot] (TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Kumpoo Power Shot Nano 2300', N'Kumpoo', N'Công Thủ Toàn Diện', N'Nano Carbon', N'3U', N'G3', N'25', N'675', N'Cân Bằng', N'Cứng', N'Vuông', N'86 - 88 kg/cm2', N'Vàng/Đen', N'Trợ lực cao', 2500000, N'Vợt phù hợp cho người chơi với phong cách công thủ toàn diện.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\9.jpg')
INSERT INTO [Vot] (TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Yonex Nanoflare 700', N'Yonex', N'Phản Tạt - Phòng Thủ', N'M40X, Super HMG', N'5U', N'G4', N'20', N'665', N'Nhẹ Đầu', N'Dẻo', N'Vuông', N'dưới 82 kg/cm2', N'Hồng/Đen', N'Trợ lực ít', 3200000, N'Vợt nhẹ đầu và trợ lực tốt, thích hợp cho phản tạt nhanh.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\10.jpg')
INSERT INTO [Vot] (TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Victor Auraspeed 100X', N'Victor', N'Phản Tạt - Phòng Thủ', N'High Resilience Graphite', N'4U', N'G5', N'27', N'670', N'Nhẹ Đầu', N'Cứng', N'Vuông', N'84 - 86 kg/cm2', N'Xanh/Đen', N'Trợ lực cao', 2800000, N'Vợt thích hợp cho phản tạt và phòng thủ tốc độ.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\11.jpg')
INSERT INTO [Vot] (TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Lining Turbo Charging 75', N'Lining', N'Công Thủ Toàn Diện', N'TB Nano, Military Grade Carbon Fiber', N'4U', N'G5', N'23', N'675', N'Cân Bằng', N'Trung Bình', N'Vuông', N'82 - 84 kg/cm2', N'Đỏ/Đen', N'Trợ lực vừa phải', 2000000, N'Vợt với công nghệ Turbo Charging hỗ trợ tốt cho cả tấn công và phòng thủ.', 'D:\CODE\HinhAnhSanPhamHCG\votyonex\12.jpg')
INSERT INTO [Vot] 
(TenSanPham, ThuongHieu, LoaiVot, ChatLieu, TrongLuong, ChuViCan, SucCang, ChieuDai, DiemCanBang, DoCung, DangMatVot, TrongLuongVung, MauSac, TroLuc, Gia, MoTa, AnhSanPham)
VALUES 
(N'Vợt Victor Auraspeed 90K', N'Victor', N'Công Thủ Toàn Diện', N'Graphite', N'3U', N'G5', N'28 lbs', N'675', N'Cân Bằng', N'Cứng', N'Vuông', N'82 - 84 kg/cm2', N'Xanh Đen', N'Trợ lực cao', 1500000, N'Vợt thích hợp cho người chơi linh hoạt.', 'D:\CODE\HinhAnhSanPhamHCG\votvictor\13.jpg')

CREATE TABLE [MUCDICH]
(
	MucDichID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDmucdich NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [MUCDICH];

INSERT INTO [MUCDICH] (MucDichID, NDmucdich)
VALUES (N'MD01', N'Luyện tập và Giải trí')
INSERT INTO [MUCDICH] (MucDichID, NDmucdich)
VALUES (N'MD02', N'Thi Đấu Phát triển kỹ thuật')
INSERT INTO [MUCDICH] (MucDichID, NDmucdich)
VALUES (N'MD03', N'Tăng cường sức mạnh khi tấn công')
INSERT INTO [MUCDICH] (MucDichID, NDmucdich)
VALUES (N'MD04', N'Cải thiện khả năng phòng thủ và phản xạ nhanh')
INSERT INTO [MUCDICH] (MucDichID, NDmucdich)
VALUES (N'MD05', N'Cân bằng giữa tấn công và phòng thủ')

CREATE TABLE [TRINHDO]
(
	TrinhDoID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDtrinhdo NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [TRINHDO];

INSERT INTO [TRINHDO] (TrinhDoID, NDtrinhdo)
VALUES (N'TD01', N'Mới Chơi')
INSERT INTO [TRINHDO] (TrinhDoID, NDtrinhdo)
VALUES (N'TD02', N'Trung Bình')
INSERT INTO [TRINHDO] (TrinhDoID, NDtrinhdo)
VALUES (N'TD03', N'Khá')
INSERT INTO [TRINHDO] (TrinhDoID, NDtrinhdo)
VALUES (N'TD04', N'Chuyên Nghiệp')

/*
CREATE TABLE [LUCCOTAY]
(
	LucCoTayID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDluccotay NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [LUCCOTAY];

INSERT INTO [LUCCOTAY] (LucCoTayID, NDluccotay)
VALUES (N'LCT01', N'Yếu')
INSERT INTO [LUCCOTAY] (LucCoTayID, NDluccotay)
VALUES (N'LCT02', N'Trung Bình')
INSERT INTO [LUCCOTAY] (LucCoTayID, NDluccotay)
VALUES (N'LCT03', N'Mạnh')


CREATE TABLE [PHONGCACHCHOI]
(
	PhongCachChoiID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDphongcachchoi NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [PHONGCACHCHOI];

INSERT INTO [PHONGCACHCHOI] (PhongCachChoiID, NDphongcachchoi)
VALUES (N'PCC01', N'Tấn công mạnh mẽ (smash nhiều)')
INSERT INTO [PHONGCACHCHOI] (PhongCachChoiID, NDphongcachchoi)
VALUES (N'PCC02', N'Phòng thủ và điều cầu linh hoạt')
INSERT INTO [PHONGCACHCHOI] (PhongCachChoiID, NDphongcachchoi)
VALUES (N'PCC03', N'Toàn Diện')
*/

CREATE TABLE [DOCUNG]
(
	DoCungID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDdocung NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [DOCUNG];

INSERT INTO [DOCUNG] (DoCungID, NDdocung)
VALUES (N'DC01', N'Mềm')
INSERT INTO [DOCUNG] (DoCungID, NDdocung)
VALUES (N'DC02', N'Trung Bình')
INSERT INTO [DOCUNG] (DoCungID, NDdocung)
VALUES (N'DC03', N'Cứng')

CREATE TABLE [SUCCANG]
(
	SucCangID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDsuccang NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [SUCCANG];

INSERT INTO [SUCCANG] (SucCangID, NDsuccang)
VALUES (N'SC01', N'17-19 lbs')
INSERT INTO [SUCCANG] (SucCangID, NDsuccang)
VALUES (N'SC02', N'22-24 lbs')

CREATE TABLE [TRONGLUONG]
(
	TrongLuongID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDtrongluong NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [TRONGLUONG];


INSERT INTO [TRONGLUONG] (TrongLuongID, NDtrongluong)
VALUES (N'TL01', N'2U (90-94g)')
INSERT INTO [TRONGLUONG] (TrongLuongID, NDtrongluong)
VALUES (N'TL02', N'3U (85-89g)')
INSERT INTO [TRONGLUONG] (TrongLuongID, NDtrongluong)
VALUES (N'TL03', N'4U (80-84g)')
INSERT INTO [TRONGLUONG] (TrongLuongID, NDtrongluong)
VALUES (N'TL04', N'5U (75-79g)')
INSERT INTO [TRONGLUONG] (TrongLuongID, NDtrongluong)
VALUES (N'TL05', N'F (70-74g)')
INSERT INTO [TRONGLUONG] (TrongLuongID, NDtrongluong)
VALUES (N'TL06', N'2F (65-69g)')

CREATE TABLE [CHUVI]
(
	ChuViID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDchuvi NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [CHUVI];

INSERT INTO [CHUVI] (ChuViID, NDchuvi)
VALUES (N'CV01', N'G5 (tay cầm siêu nhỏ')
INSERT INTO [CHUVI] (ChuViID, NDchuvi)
VALUES (N'CV02', N'G4 (tay cầm nhỏ')
INSERT INTO [CHUVI] (ChuViID, NDchuvi)
VALUES (N'CV03', N'G3 (tay cầm trung bình)')
INSERT INTO [CHUVI] (ChuViID, NDchuvi)
VALUES (N'CV04', N'G2 (tay cầm lớn)')
INSERT INTO [CHUVI] (ChuViID, NDchuvi)
VALUES (N'CV05', N'G1 (tay cầm siêu lớn)')

CREATE TABLE [CHATLIEU]
(
	ChatLieuID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDchatlieu NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [CHATLIEU];

INSERT INTO [CHATLIEU] (ChatLieuID, NDchatlieu)
VALUES (N'CL01', N'Chất liệu Carbon cao cấp (bền và nhẹ)')
INSERT INTO [CHATLIEU] (ChatLieuID, NDchatlieu)
VALUES (N'CL02', N'Chất liệu Graphite (phổ biến và bền)')
INSERT INTO [CHATLIEU] (ChatLieuID, NDchatlieu)
VALUES (N'CL03', N'Chất liệu Aluminium (rẻ và phù hợp cho người mới)')

CREATE TABLE [CHIEUDAI]
(
	ChieuDaiID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDchieudai NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [CHIEUDAI];

INSERT INTO [CHIEUDAI] (ChieuDaiID, NDchieudai)
VALUES (N'CD01', N'665 mm')
INSERT INTO [CHIEUDAI] (ChieuDaiID, NDchieudai)
VALUES (N'CD02', N'670 mm')
INSERT INTO [CHIEUDAI] (ChieuDaiID, NDchieudai)
VALUES (N'CD03', N'675 mm')

CREATE TABLE [TRONGLUONGVUNG]
(
	TrongLuongVungID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDtrongluongvung NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [TRONGLUONGVUNG];

INSERT INTO [TRONGLUONGVUNG] (TrongLuongVungID, NDtrongluongvung)
VALUES (N'TLV01', N'Dưới 82 kg/cm2')
INSERT INTO [TRONGLUONGVUNG] (TrongLuongVungID, NDtrongluongvung)
VALUES (N'TLV02', N'82-84 kg/cm2')
INSERT INTO [TRONGLUONGVUNG] (TrongLuongVungID, NDtrongluongvung)
VALUES (N'TLV03', N'84-86 kg/cm2')
INSERT INTO [TRONGLUONGVUNG] (TrongLuongVungID, NDtrongluongvung)
VALUES (N'TLV04', N'86-88 kg/cm2')
INSERT INTO [TRONGLUONGVUNG] (TrongLuongVungID, NDtrongluongvung)
VALUES (N'TLV05', N'Trên 88 kg/cm22')

CREATE TABLE [MATVOT]
(
	MatVotID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDmatvot NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [MATVOT];

INSERT INTO [MATVOT] (MatVotID, NDmatvot)
VALUES (N'MV01', N'Vuông')
INSERT INTO [MATVOT] (MatVotID, NDmatvot)
VALUES (N'MV02', N'Bầu Dục')

CREATE TABLE [TAICHINH]
(
	TaiChinhID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDtaichinh NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [TAICHINH];

INSERT INTO [TAICHINH] (TaiChinhID, NDtaichinh)
VALUES (N'TC01', N'Dưới 1 triệu')
INSERT INTO [TAICHINH] (TaiChinhID, NDtaichinh)
VALUES (N'TC02', N'Trên 1 triệu || Dưới 2 triệu')
INSERT INTO [TAICHINH] (TaiChinhID, NDtaichinh)
VALUES (N'TC03', N'Trên 2 triệu || Dưới 5 triệu')
INSERT INTO [TAICHINH] (TaiChinhID, NDtaichinh)
VALUES (N'TC04', N'Trên 5 triệu')

CREATE TABLE [HANGVOT]
(
	HangVotID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDhangvot NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [HANGVOT];

INSERT INTO [HANGVOT] (HangVotID, NDhangvot)
VALUES (N'HV01', N'Yonex')
INSERT INTO [HANGVOT] (HangVotID, NDhangvot)
VALUES (N'HV02', N'Victor')
INSERT INTO [HANGVOT] (HangVotID, NDhangvot)
VALUES (N'HV03', N'Lining')
INSERT INTO [HANGVOT] (HangVotID, NDhangvot)
VALUES (N'HV04', N'Kumpoo')
INSERT INTO [HANGVOT] (HangVotID, NDhangvot)
VALUES (N'HV05', N'Mizuno')
INSERT INTO [HANGVOT] (HangVotID, NDhangvot)
VALUES (N'HV06', N'Proace')
INSERT INTO [HANGVOT] (HangVotID, NDhangvot)
VALUES (N'HV07', N'Felet')
INSERT INTO [HANGVOT] (HangVotID, NDhangvot)
VALUES (N'HV08', N'Flypower')
INSERT INTO [HANGVOT] (HangVotID, NDhangvot)
VALUES (N'HV09', N'Kawasaki')

CREATE TABLE [PHANLOAIVOT]
(
	PhanLoaiVotID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDphanloaivot NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [PHANLOAIVOT];

INSERT INTO [PHANLOAIVOT] (PhanLoaiVotID, NDphanloaivot)
VALUES (N'PLV01', N'Vợt Tấn Công (Vợt Mạnh)')
INSERT INTO [PHANLOAIVOT] (PhanLoaiVotID, NDphanloaivot)
VALUES (N'PLV02', N'Vợt Phản Xạ (Vợt Linh Hoạt)')
INSERT INTO [PHANLOAIVOT] (PhanLoaiVotID, NDphanloaivot)
VALUES (N'PLV03', N'Vợt Đa Năng (Vợt Cân Bằng)')
INSERT INTO [PHANLOAIVOT] (PhanLoaiVotID, NDphanloaivot)
VALUES (N'PLV04', N'Vợt Dễ Điều Khiển (Vợt Hỗ Trợ Người Mới)')
INSERT INTO [PHANLOAIVOT] (PhanLoaiVotID, NDphanloaivot)
VALUES (N'PLV05', N'Vợt Chuyên Cho Phong Cách Phòng Thủ')

CREATE TABLE [PHANLOAINGUOI]
(
	PhanLoaiNguoiID NVARCHAR (10) PRIMARY KEY ,	-- Mã tài khoản tự động tăng
    NDphanloainguoi NVARCHAR(MAX) NOT NULL
);

DROP TABLE IF EXISTS [PHANLOAINGUOI];

INSERT INTO [PHANLOAINGUOI] (PhanLoaiNguoiID, NDphanloainguoi)
VALUES (N'PLN01', N'Người chơi mới bắt đầu')
INSERT INTO [PHANLOAINGUOI] (PhanLoaiNguoiID, NDphanloainguoi)
VALUES (N'PLN02', N'Người chơi trình độ trung cấp')
INSERT INTO [PHANLOAINGUOI] (PhanLoaiNguoiID, NDphanloainguoi)
VALUES (N'PLN03', N'Người chơi trình độ cao (chuyên nghiệp)')
INSERT INTO [PHANLOAINGUOI] (PhanLoaiNguoiID, NDphanloainguoi)
VALUES (N'PLN04', N'Người chơi yêu thích tấn công')
INSERT INTO [PHANLOAINGUOI] (PhanLoaiNguoiID, NDphanloainguoi)
VALUES (N'PLN05', N'Người chơi yêu thích phòng thủ và phản tạt')


CREATE TABLE [SuKien] (
    SuKienID NVARCHAR(10) PRIMARY KEY,
    MUCDICHID NVARCHAR(10),
    TRINHDOID NVARCHAR(10),
    LUCCOTAYID NVARCHAR(10),
    PHONGCACHCHOIID NVARCHAR(10),
    DOCUNGID NVARCHAR(10),
    SUCANGID NVARCHAR(10),
    TRONGLUONGID NVARCHAR(10),
    CHUVIID NVARCHAR(10),
    CHATLIEUID NVARCHAR(10),
    CHIEUDAIID NVARCHAR(10),
    TRONGLUONGVUNGID NVARCHAR(10),
    MATVOTID NVARCHAR(10),
    TAICHINHID NVARCHAR(10),
    HANGVOTID NVARCHAR(10),
    PHANLOAIID NVARCHAR(10),
	SANPHAMID NVARCHAR(10),
    FOREIGN KEY (MUCDICHID) REFERENCES MUCDICH(MucDichID),
    FOREIGN KEY (TRINHDOID) REFERENCES TRINHDO(TrinhDoID),
    FOREIGN KEY (LUCCOTAYID) REFERENCES LUCCOTAY(LucCoTayID),
    FOREIGN KEY (PHONGCACHCHOIID) REFERENCES PHONGCACHCHOI(PhongCachChoiID),
    FOREIGN KEY (DOCUNGID) REFERENCES DOCUNG(DoCungID),
    FOREIGN KEY (SUCANGID) REFERENCES SUCCANG(SucCangID),
    FOREIGN KEY (TRONGLUONGID) REFERENCES TRONGLUONG(TrongLuongID),
    FOREIGN KEY (CHUVIID) REFERENCES CHUVI(ChuViID),
    FOREIGN KEY (CHATLIEUID) REFERENCES CHATLIEU(ChatLieuID),
    FOREIGN KEY (CHIEUDAIID) REFERENCES CHIEUDAI(ChieuDaiID),
    FOREIGN KEY (TRONGLUONGVUNGID) REFERENCES TRONGLUONGVUNG(TrongLuongVungID),
    FOREIGN KEY (MATVOTID) REFERENCES MATVOT(MatVotID),
    FOREIGN KEY (TAICHINHID) REFERENCES TAICHINH(TaiChinhID),
    FOREIGN KEY (HANGVOTID) REFERENCES HANGVOT(HangVotID),
    FOREIGN KEY (PHANLOAIID) REFERENCES PHANLOAI(PhanLoaiID),
    FOREIGN KEY (SANPHAMID) REFERENCES Vot(SanPhamID)
);


DROP TABLE IF EXISTS [SuKien];

CREATE TABLE [TapLuat](
	LuatID NVARCHAR(10) PRIMARY KEY,
	NoiDung NVARCHAR(500) not null
)

DROP TABLE IF EXISTS [TapLuat];


INSERT [TapLuat] (LuatID , NoiDung) VALUES  (N'R001', N'DC01^SC01^TL01^CV01^CL01^CD01^TLV01^MV01>PLV01')
INSERT [TapLuat] (LuatID ,NoiDung) VALUES  (N'R002',N'DC02^SC02^TL02^CV02^CL02^CD02^TLV02^MV02>PLV02')
INSERT [TapLuat] (LuatID ,NoiDung) VALUES  (N'R007',N'DC03^SC02^TL03^CV03^CL03^CD03^TLV03^MV01>PLV03')
GO

INSERT [TapLuat] (LuatID , NoiDung) VALUES  (N'R003', N'MD01^TD01>PLN01')
INSERT [TapLuat] (LuatID , NoiDung) VALUES  (N'R004', N'MD02^TD02>PLN02')
INSERT [TapLuat] (LuatID , NoiDung) VALUES  (N'R008', N'MD03^TD03>PLN03')
GO

SELECT MAX(CAST(SUBSTRING(LuatID, 2, LEN(LuatID)) AS INT)) AS MaxId
FROM [TapLuat]
WHERE LuatID LIKE 'R%'
SELECT MAX(LuatID) FROM [TapLuat] WHERE LuatID LIKE 'R%'

INSERT [TapLuat] (LuatID , NoiDung) VALUES (N'R005', N'PLN01^PLV01^HV01^TC01>V1')
INSERT [TapLuat] (LuatID , NoiDung) VALUES (N'R006', N'PLN02^PLV02^HV02^TC02>V2')
INSERT [TapLuat] (LuatID , NoiDung) VALUES (N'R009', N'PLN03^PLV03^HV03^TC03>V3')