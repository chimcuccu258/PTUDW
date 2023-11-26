DROP DATABASE QLVinpearl_63130803

CREATE DATABASE QLVinpearl_63130803
GO
USE QLVinpearl_63130803

CREATE TABLE DICHVU (
  maDV VARCHAR(10) PRIMARY KEY,
  tenDV NVARCHAR(255) NOT NULL,
  moTa NTEXT,
  anh VARCHAR(255) NOT NULL,
  maLoaiDV VARCHAR(10) NOT NULL,
  xepLoai FLOAT,
  sdtDV VARCHAR(20) NOT NULL,
  diaChiDV NVARCHAR(500) NOT NULL
);

CREATE TABLE CTHD (
  maHD VARCHAR(10),
  maVe VARCHAR(10),
  soLuong INT,
  giaTien MONEY,
  PRIMARY KEY (maHD, maVe)
);
CREATE TABLE VE (
  maVe VARCHAR(10) PRIMARY KEY,
  maDV VARCHAR(10),
  loaiVe BIT,
  giaTien MONEY
);

CREATE TABLE HOADON (
  maHD VARCHAR(10) PRIMARY KEY,
  maKH VARCHAR(10),
  maNV VARCHAR(10),
  ngayThanhToan DATETIME,
  SDT VARCHAR(20),
  email VARCHAR(255),
);

CREATE TABLE KHACHHANG (
  maKH VARCHAR(10) PRIMARY KEY,
  hoTenKH NVARCHAR(255),
  SDT VARCHAR(20),
  diaChi NVARCHAR(255),
  ngaySinh DATETIME,
  gioiTinh BIT,
  email VARCHAR(255),
  matKhau VARCHAR(255),
  anh VARCHAR(255),
  ResetPasswordCode VARCHAR(255),
  ResetPasswordCodeExpiration DATETIME
);

CREATE TABLE LOAIDV (
  maLoaiDV VARCHAR(10) PRIMARY KEY,
  tenLoai NVARCHAR(255)
);

CREATE TABLE LOAINV (
  maLoaiNV VARCHAR(10) PRIMARY KEY,
  tenLoai NVARCHAR(255),
  luongCoBan MONEY
);

CREATE TABLE NHANVIEN (
  maNV VARCHAR(10) PRIMARY KEY,
  maLoaiNV VARCHAR(10),
  hoTenNV NVARCHAR(255),
  diaChi NVARCHAR(255),
  ngaySinh DATETIME,
  sdt VARCHAR(20),
  gioiTinh BIT,
  anh VARCHAR(255),
  email VARCHAR(255),
  matKhau VARCHAR(255)
);

CREATE TABLE SOCA (
  maCa VARCHAR(10),
  maNV VARCHAR(10),
  soCa INT
  PRIMARY KEY (maCa, maNV)
);

CREATE TABLE CHUCNANG (
	maChucNang VARCHAR(10),
	tenChucNang NVARCHAR(255),
	PRIMARY KEY (maChucNang)
);

CREATE TABLE PHANQUYEN (
	maChucNang VARCHAR(10),
	maLoaiNV VARCHAR(10),
	ghiChu NVARCHAR(255),
	PRIMARY KEY (maChucNang, maLoaiNV)
);

--Ràng buộc FOREIGN KEY cho bảng PHANQUYEN:
ALTER TABLE PHANQUYEN
ADD CONSTRAINT fk_PHANQUYEN_CHUCNANG
FOREIGN KEY (maChucNang) REFERENCES CHUCNANG(maChucNang)
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE PHANQUYEN
ADD CONSTRAINT fk_PHANQUYEN_LOAINV
FOREIGN KEY (maLoaiNV) REFERENCES LOAINV(maLoaiNV)
ON UPDATE CASCADE
ON DELETE CASCADE;
--Ràng buộc FOREIGN KEY cho bảng cthd:

ALTER TABLE CTHD
ADD CONSTRAINT fk_CTHD_VE
FOREIGN KEY (maVe) REFERENCES VE(maVe)
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE CTHD
ADD CONSTRAINT fk_CTHD_HOADON
FOREIGN KEY (maHD) REFERENCES HOADON(maHD)
ON UPDATE CASCADE
ON DELETE CASCADE;
--//Ràng buộc FOREIGN KEY cho bảng hoadon:

ALTER TABLE HOADON
ADD CONSTRAINT fk_HOADON_KHACHHANG
FOREIGN KEY (maKH) REFERENCES KHACHHANG(maKH)
ON UPDATE CASCADE
ON DELETE CASCADE;

--//Ràng buộc FOREIGN KEY cho bảng nhanvien:

ALTER TABLE NHANVIEN
ADD CONSTRAINT fk_NHANVIEN_LOAINV
FOREIGN KEY (maLoaiNV) REFERENCES LOAINV(maLoaiNV)
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE VE
ADD CONSTRAINT fk_VE_DICHVU
FOREIGN KEY (maDV) REFERENCES DICHVU(maDV)
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE DICHVU
ADD CONSTRAINT fk_DICHVU_LOAIDV
FOREIGN KEY (maLoaiDV) REFERENCES LOAIDV (maLoaiDV)
ON UPDATE CASCADE
ON DELETE CASCADE;

--//Ràng buộc FOREIGN KEY cho bảng soca:

ALTER TABLE SOCA
ADD CONSTRAINT fk_SOCA_NHANVIEN
FOREIGN KEY (maNV) REFERENCES NHANVIEN(maNV)
ON UPDATE CASCADE
ON DELETE CASCADE;

ALTER TABLE HOADON ADD CONSTRAINT fk_HOADON_NHANVIEN
FOREIGN KEY (maNV) REFERENCES NHANVIEN(maNV)
ON UPDATE CASCADE
ON DELETE CASCADE;

-----------------------------------------------------
INSERT INTO CHUCNANG (maChucNang, tenChucNang) VALUES
('CN01', N'Danh Sách'),
('CN02', N'Thêm'),
('CN03', N'Sửa'),
('CN04', N'Xoá');

-----------------------------------------------------
INSERT INTO LOAIDV (maLoaiDV, tenLoai) VALUES
('LDV001', N'Nghỉ dưỡng'),
('LDV002', N'Ẩm thực'),
('LDV003', N'Khám phá & Hoạt động'),
('LDV004', N'Golf');
-------------------

INSERT INTO DICHVU(maDV, tenDV, moTa, anh, maLoaiDV, xepLoai, sdtDV, diaChiDV) VALUES
('DV001', N'Vinpearl Condotel Beachfront Nha Trang', N'Vinpearl Condotel Beachfront Nha Trang với những căn hộ khách sạn có tầm nhìn hướng biển, không chỉ mang đến cho du khách sự tiện nghi, thoải mái, mà còn là cảm nhận trọn vẹn về những bãi biển đầy nắng gió của vịnh Nha Trang - một trong những vịnh biển đẹp của Nha Trang. ', 'dichvu_2.jpg', 'LDV001', 4.5, '0258 359 9099', N'78 - 80 Đường Trần Phú, Phường Lộc Thọ, Tp. Nha Trang, tỉnh Khánh Hòa'),
('DV002', N'Vinpearl Resort Nha Trang', N'Vinpearl Resort Nha Trang chào đón du khách bằng vẻ đẹp Á Đông thuần khiết với kiến trúc mang đậm phong cách Indochine sang trọng cùng bãi biển riêng tư hút mắt. Giữa vạn trải nghiệm sôi nổi, tại quần thể nghỉ dưỡng – giải trí biển hàng đầu khu vực của đảo Hòn Tre Nha Trang', 'dichvu_2.jpg', 'LDV001', 4, '258 359 8222', N'Đảo Hòn Tre, Tp. Nha Trang, Khánh Hòa, Việt Nam'),
('DV003', N'Vinpearl Luxury Nha Trang', N'Được sinh ra bởi khao khát tìm về chốn bình yên của tâm hồn, Vinpearl Luxury Nha Trang là điểm đến khó lòng bỏ qua với những du khách trân trọng từng giây phút an tĩnh tuyệt đối bên người mình yêu thương. Tọa lạc nơi bờ biển thiên đường, 84 căn biệt thự xinh đẹp nằm trải mình giữa những khu vườn nhiệt đới mướt mát, lắng nghe tiếng sóng rì rào khúc nhạc thư thái của thiên nhiên, tạo nên khung cảnh yên bình và đầm ấm như một “ốc đảo bình yên” cách xa khỏi chốn thị thành bận rộn.', 'dichvu_3.jpg', 'LDV001', 5, '258 359 8222', N'Đảo Hòn Tre, Tp. Nha Trang, Khánh Hòa, Việt Nam'),
('DV004', N'Vinpearl Discovery Golflink Nha Trang test', N'Tọa lạc trên thung lũng ở vị trí tuyệt đẹp ôm trọn điểm ngắm toàn cảnh sân golf 18 hố tiêu chuẩn quốc tế, Vinpearl Discovery Golflink Nha Trang là khu nghỉ dưỡng liền kề sân golf trên đảo đẹp bậc nhất Việt Nam. Đây chính là điểm đến lý tưởng cho những người ưa thích phong cách nghỉ dưỡng đẳng cấp gắn liền với các hoạt động thể thao giàu tính trí tuệ và đam mê mãnh liệt trong hành trình du lịch Nha Trang. Vinpearl Discovery Golflink Nha Trang được thiết kế đầy tính nghệ thuật, lấy cảm hứng từ phong cách kiến trúc Địa Trung Hải với mái vòm đặc trưng, 182 căn villa với bể bơi riêng biệt, trang bị đầy đủ nội thất, thiết kế hiện đại, cao cấp theo tiêu chuẩn quốc tế. Khu nghỉ dưỡng chắc chắn sẽ thỏa mãn những người yêu mến sự khoáng đạt.', 'dichvu_5.jpg', 'LDV001', 4.5, '0258 359 9099', N'Đảo Hòn Tre, Nha Trang, Khánh Hòa, Việt Nam'),
('DV005', N'Melia Vinpearl Empire Nha Trang', N'Chỉ cách sân bay 15 phút lái xe và cách danh thắng Ngũ Hành Sơn 800m, Vinpearl Resort & Spa Đà Nẵng tọa lạc nơi bãi biển Non Nước – \"Bãi biển hấp dẫn nhất hành tinh\" do tạp chí Forbes từng bình chọn. Trong bức tranh sơn thủy hữu tình, Vinpearl Resort & Spa Đà Nẵng tựa ốc đảo thanh bình với 122 căn biệt thự phong cách tân cổ điển được ôm ấp bởi hồ nước lượn quanh và khuôn viên rực rỡ hoa trái. Nơi đây lý tưởng để nghỉ dưỡng thảnh thơi và thuận tiện để khám phá các điểm đến nổi tiếng như Ngũ Hành Sơn, phố cổ Hội An, VinWonders Nam Hội An...\r\n\r\n', 'dichvu_6.jpg', 'LDV001', 5, '0258 359 9099', N'44 – 46 Đường Lê Thánh Tôn, phường Lộc Thọ, thành phố Nha Trang'),
('DV006', N'Vinpearl Resort & Spa Nha Trang Bay Bay', N'Trên “đảo thiên đường” Hòn Tre, Vinpearl Resort & Spa Nha Trang Bay với kiến trúc hình cánh cung trắng muốt luôn hút mắt với vẻ tinh khôi riêng biệt. Mỗi phòng nghỉ đều sở hữu view biển sống động đặc trưng vào lúc bình minh. Thiết kế khung cửa toàn kính bao quanh các căn biệt thự liền kề bờ cát trắng mịn mang tới trải nghiệm “thức giấc ngay giữa bãi biển riêng tư”. Trải nghiệm đặc trưng tại đây là một liệu trình thư giãn trên mặt hồ yên ả, thưởng thức bữa tối trong khung cảnh hoàng hôn tại nhà hàng Lagoon hay thả mình trên ghế lười xem bộ phim yêu thích tại Beach Cinema.', 'dichvu_7.jpg', 'LDV001', 4.5, '0258 359 9099', N'Đảo Hòn Tre, Nha Trang, Khánh Hòa, Việt Nam'),
('DV007', N'Vinpearl Discovery Rockside Nha Trang', N'Vinpearl Condotel Beachfront Nha Trang với những căn hộ khách sạn có tầm nhìn hướng biển, không chỉ mang đến cho du khách sự tiện nghi, thoải mái, mà còn là cảm nhận trọn vẹn về những bãi biển đầy nắng gió của vịnh Nha Trang - một trong những vịnh biển đẹp nhất hành tinh. Đây là một điểm đến mới “tất cả trong một” với trung tâm thương mại sầm uất, những nhà hàng sang trọng, bể bơi ngoài trời ngắm toàn cảnh vịnh biển, không gian hội họp đẳng cấp, nơi tụ hội của thành công.', 'dichvu_8.jpg', 'LDV001', 4.5, '(+84) 258 359 8', N'78 - 80 Đường Trần Phú, Phường Lộc Thọ, Tp. Nha Trang, tỉnh Khánh Hòa'),
('DV008', N'Melia Vinpearl Nha Trang Riverfont', N'Trên “đảo thiên đường” Hòn Tre, Vinpearl Resort & Spa Nha Trang Bay với kiến trúc hình cánh cung trắng muốt luôn hút mắt với vẻ tinh khôi riêng biệt. Mỗi phòng nghỉ đều sở hữu view biển sống động đặc trưng vào lúc bình minh. Thiết kế khung cửa toàn kính bao quanh các căn biệt thự liền kề bờ cát trắng mịn mang tới trải nghiệm “thức giấc ngay giữa bãi biển riêng tư”. Trải nghiệm đặc trưng tại đây là một liệu trình thư giãn trên mặt hồ yên ả, thưởng thức bữa tối trong khung cảnh hoàng hôn tại nhà hàng Lagoon hay thả mình trên ghế lười xem bộ phim yêu thích tại Beach Cinema.', 'dichvu_9.jpg', 'LDV001', 4.5, '258 359 8222', N'341 đường Trần Hưng Đạo, phường An Hải Bắc, quận Sơn Trà, Tp. Đà Nẵng'),
('DV009', N'Nha Trang Marriott Resort & Spa', N'Vinpearl Condotel Beachfront Nha Trang với những căn hộ khách sạn có tầm nhìn hướng biển, không chỉ mang đến cho du khách sự tiện nghi, thoải mái, mà còn là cảm nhận trọn vẹn về những bãi biển đầy nắng gió của vịnh Nha Trang - một trong những vịnh biển đẹp nhất hành tinh. Đây là một điểm đến mới “tất cả trong một” với trung tâm thương mại sầm uất, những nhà hàng sang trọng, bể bơi ngoài trời ngắm toàn cảnh vịnh biển, không gian hội họp đẳng cấp, nơi tụ hội của thành công.', 'dichvu_10.jpg', 'LDV001', 4.5, '0 111 222 333', N'Số 7 Trường Sa, Ngũ Hành Sơn, Nha Trang'),
('DV010', N'Melia Vinpearl Cua Sot Beach Resort', N'Vinpearl Discovery Greenhill Phú Quốc sở hữu các biệt thự nép mình dọc theo sườn núi với tầm nhìn khoáng đạt bao trọn cả đất trời. Hồ điều hòa nước ngọt rộng 17 ha nằm ở trung tâm khu nghỉ dưỡng tạo không khí trong lành và cảnh quan độc đáo, thích hợp với những vị khách yêu thích khám phá cảnh sắc thiên nhiên xen kẽ giữa đại dương bao la và núi đồi trùng điệp.\r\n\r\nMột trong những điểm nhấn của Vinpearl Discovery Greenhill Phú Quốc là các Clubhouse The Forest và Nerin thiết kế theo phong cách “tropical” ấn tượng với rừng cây nhiệt đới xanh mướt phủ quanh lối vào, tại đây du khách có thể thỏa thích check-in, thưởng thức những món ăn thượng hạng bao gồm cả đặc sản Phú Quốc và các loại đồ uống hấp dẫn.', 'dichvu_11.jpg', 'LDV001', 4.5, '0258 359 9099', N'Nha Trang, Khánh Hòa'),
('DV011', N'Vinpearl Landmark 81, Autograph Collection', N'Vinpearl Condotel Beachfront Nha Trang với những căn hộ khách sạn có tầm nhìn hướng biển, không chỉ mang đến cho du khách sự tiện nghi, thoải mái, mà còn là cảm nhận trọn vẹn về những bãi biển đầy nắng gió của vịnh Nha Trang - một trong những vịnh biển đẹp nhất hành tinh. Đây là một điểm đến mới “tất cả trong một” với trung tâm thương mại sầm uất, những nhà hàng sang trọng, bể bơi ngoài trời ngắm toàn cảnh vịnh biển, không gian hội họp đẳng cấp, nơi tụ hội của thành công.', 'dichvu_12.jpg', 'LDV001', 4.5, '0258 359 9099', N'Nha Trang Khánh Hòa'),
('DV012', N'VinHolidays Fiesta Nha Trang', N'Vinpearl Condotel Beachfront Nha Trang với những căn hộ khách sạn có tầm nhìn hướng biển, không chỉ mang đến cho du khách sự tiện nghi, thoải mái, mà còn là cảm nhận trọn vẹn về những bãi biển đầy nắng gió của vịnh Nha Trang - một trong những vịnh biển đẹp nhất hành tinh. Đây là một điểm đến mới “tất cả trong một” với trung tâm thương mại sầm uất, những nhà hàng sang trọng, bể bơi ngoài trời ngắm toàn cảnh vịnh biển, không gian hội họp đẳng cấp, nơi tụ hội của thành công.', 'dichvu_13.jpg', 'LDV001', 4.5, '0258 359 9099', N'Nha Trang Khánh Hòa'),
('DV013', N'Four Points by Sheraton Nha Trang', N'Vinpearl Condotel Beachfront Nha Trang với những căn hộ khách sạn có tầm nhìn hướng biển, không chỉ mang đến cho du khách sự tiện nghi, thoải mái, mà còn là cảm nhận trọn vẹn về những bãi biển đầy nắng gió của vịnh Nha Trang - một trong những vịnh biển đẹp nhất hành tinh. Đây là một điểm đến mới “tất cả trong một” với trung tâm thương mại sầm uất, những nhà hàng sang trọng, bể bơi ngoài trời ngắm toàn cảnh vịnh biển, không gian hội họp đẳng cấp, nơi tụ hội của thành công.', 'dichvu_14.jpg', 'LDV001', 4.5, '0258 359 9099', N'Nha Trang Khánh Hòa'),
('DV014', N'Sheraton Can Tho', N'Vinpearl Condotel Beachfront Nha Trang với những căn hộ khách sạn có tầm nhìn hướng biển, không chỉ mang đến cho du khách sự tiện nghi, thoải mái, mà còn là cảm nhận trọn vẹn về những bãi biển đầy nắng gió của vịnh Nha Trang - một trong những vịnh biển đẹp nhất hành tinh. Đây là một điểm đến mới “tất cả trong một” với trung tâm thương mại sầm uất, những nhà hàng sang trọng, bể bơi ngoài trời ngắm toàn cảnh vịnh biển, không gian hội họp đẳng cấp, nơi tụ hội của thành công.', 'dichvu_15.jpg', 'LDV001', 4.5, '0258 359 9099', N'Nha Trang Khánh Hòa'),
('DV015', N'Luke Bar', N'Tọa lạc bên bãi biển mang đầy màu sắc tươi trẻ, hiện đại, Luke Bar thu hút du khách với quầy bar sôi động cùng những ly cocktail hấp dẫn, nước ép, trái cây tươi miền nhiệt đới và các loại café thơm ngon... ', 'dichvu_16.jpg', 'LDV002', 5, '1900 6677', N'Water World, VinWonders Nha Trang'),
('DV016', N'Yummy Land', N'Nhà hàng fastfood Yummy Land phục vụ những món ăn nhanh kiểu Á như: cơm phần, mỳ xào hải sản… và kiểu Âu như burger, pizza hải sản… giúp du khách tái tạo năng lượng để tiếp tục vui chơi và khám phá thế giới giải trí đầy hấp dẫn của Vinwonders Nha Trang.\r\n\r\nVới sức chứa 250 chỗ, nhà hàng Yummy Land là sự lựa chọn hoàn hảo cho mọi du khách với thực đơn đa dạng, hương vị thơm ngon đặc sắc, lên món nhanh và không phải mất nhiều thời gian chờ đợi.', 'dichvu_17.jpg', 'LDV002', 5, '1900 6677', N'Adventure Land, VinWonders Nha Trang'),
('DV017', N'Lotteria Restaurent', N'Giới thiệu về Nhà hàng Lotteria\r\nChuỗi nhà hàng được đông đảo các bạn nhỏ yêu thích với các món ăn đặc trưng: gà rán, phomai que, khoai tây chiên…', 'dichvu_18.jpg', 'LDV002', 4.5, '1900 6677', N'Sea World, VinWonders Nha Trang | King Garden, Vinwonders Nha Trang'),
('DV018', N'Yummy Land Restaurent', N'Nhà hàng fastfood Yummy Land - nơi mang đến cho du khách những món ăn kiểu Á như: cơm phần, mỳ xào hải sản… và kiểu Âu như burger, pizza hải sản… để du khách nhanh chóng tái tạo năng lượng, tiếp tục vui chơi và khám phá thế giới giải trí đầy hấp dẫn của Vinwonders Nha Trang.\r\n\r\nVới sức chứa 250 chỗ ngồi, nhà hàng Yummy Land là sự lựa chọn hoàn hảo cho mọi du khách với thực đơn đa dạng, phục vụ nhanh chóng, tận tâm và không làm mất nhiều thời gian chờ đợi.', 'dichvu_19.jpg', 'LDV002', 4.5, '1900 6677', N'Water World, VinWonders Nha Trang'),
('DV019', N'Romy Cream', N'Quầy kem, nước giải khát & bánh ngọt Romy với menu đa dạng phù hợp với tất cả khách hàng, thêm nhiều lựa chọn cho cuộc chơi thêm hứng khởi tại King\ Garden, VinWonders Nha Trang.', 'dichvu_16.jpg', 'LDV002', 5, '1900 6677', N'Water World, VinWonders Nha Trang'),
('DV020', N'Lobby Bar - Vinpearl Discovery Rockside Nha Trang', N'Giới thiệu về Lobby Bar - Vinpearl Discovery Rockside Nha Trang\r\nLobby Bar nằm ngay tại sảnh chờ chính của khách sạn - một vị trí thuận lợi cho việc nghỉ chân và thư giãn sau hành trình vui chơi, du lịch Nha Trang. Nơi đây còn là địa điểm lý tưởng để gặp gỡ, chuyện trò với bạn bè, người thân bên ly thức uống thơm ngon, thức ăn nhẹ hấp dẫn và ngắm nhìn khung cảnh tuyệt đẹp của khu nghỉ dưỡng.', 'dichvu_21.jpg', 'LDV002', 5, '(+84) 258 359 8888', N'Vinpearl Discovery Rockside Nha Trang, Đảo Hòn Tre, Nha Trang, Khánh Hòa, Việt Nam'),
('DV021', N'Marina Restaurant', N'Tọa lạc tại tầng trệt của khách sạn Vinpearl Resort & Spa Nha Trang Bay, Nhà hàng Marina được bài trí pha trộn giữa nét đẹp hiện đại và truyền thống. Thực khách sẽ được phục vụ buổi tiệc Buffet đa dạng và phong phú từ các đặc sản Việt Nam và Quốc Tế được chế biến qua bàn tay của đầu bếp mang đẳng cấp 5* chắc chắn sẽ mang đến trải nghiệm tuyệt vời.', 'dichvu_22.jpg', 'LDV002', 5, '(+84) 258 359 8888', N'Vinpearl Resort & Spa Nha Trang Bay, Đảo Hòn Tre, Nha Trang, Khánh Hòa, Việt Nam'),
('DV022', N'Ozone Seafood Restaurant', N'Nhà hàng hải sản Ozone chinh phục những thực khách sành sỏi bằng nguồn hải sản tươi ngon, kết hợp với sự chăm chút chi tiết cho từng món ăn của các chuyên gia ẩm thực cùng với không gian kiến trúc độc đáo, phóng khoáng.\r\nKhi đến với Ozone, quý khách đừng quên thưởng thức những món ăn “sơn hào hải vị” tươi ngon cùng với 17 loại nước sốt trứ danh làm nên đẳng cấp nhà hàng hải sản 5*.', 'dichvu_23.jpg', 'LDV002', 5, '(+84) 258 359 8888', N'Imperial Club, Đảo Hòn Tre, Tp. Nha Trang, Tỉnh Khánh Hòa, Việt Nam'),
('DV023', N'Blue Lagoon Restaurant', N'Đến với khách sạn Nha Trang, Quý khách sẽ được thưởng thức những hương vị ẩm thực nổi tiếng trên khắp 4 phương trong không gian sang trọng, hiện đại và khung cảnh thơ mộng, nên thơ của Blue Lagoon Restaurant. ', 'dichvu_24.jpg', 'LDV002', 4.5, '(+84) 258 359 8598', N'Vinpearl Luxury Nha Trang, Đảo Hòn Tre, Tp. Nha Trang, Tỉnh Khánh Hòa, Việt Nam'),
('DV024', N'Huong Viet Restaurant', N'Huong Viet Restaurant được đặt tại tầng 1 tòa nhà đón tiếp, với sức chứa lên tới 120 khách. Nhà hàng mang tới cho bạn ẩm thực thuần Việt độc đáo, bao gồm nhiều món đặc sản Nha Trang, với không gian được thiết kế ấn tượng hoàn toàn bằng gỗ lim, có sự kết hợp hài hòa giữa phong cách kiến trúc truyền thống và lối kiến trúc hiện đại.', 'dichvu_25.jpg', 'LDV002', 4.5, '(+84) 258 359 8598', N'Vinpearl Luxury Nha Trang, Đảo Hòn Tre, Tp. Nha Trang, Tỉnh Khánh Hòa, Việt Nam'),
('DV025', N'Beach Bar - Vinpearl Luxury Nha Trang', N'Ở Beach Bar - Vinpearl Luxury Nha Trang, được thả mình trong làn nước mát tại resort Nha Trang, nhấp một ngụm cocktail mát lạnh bên bờ biển xanh cùng nắng gió chan hòa, còn gì khiến bạn cảm thấy thư thái hơn thế?', 'dichvu_26.jpg', 'LDV002', 5, '(+84) 258 359 8888', N'Vinpearl Luxury Nha Trang, Đảo Hòn Tre, Tp. Nha Trang, Tỉnh Khánh Hòa, Việt Nam'),
('DV026', N'Wave Bar', N'Tại Wave Bar, được đắm mình trong không gian sóng biển rì rào với những ly cocktail ngon tuyệt, cùng ngắm ánh hoàng hôn với người mình yêu sẽ là những khoảnh khắc tuyệt vời trong hành trình du lịch Nha Trang.', 'dichvu_27.jpg', 'LDV002', 5, '(+84) 258 359 8888', N'Vinpearl Luxury Nha Trang, Đảo Hòn Tre, Tp. Nha Trang, Tỉnh Khánh Hòa, Việt Nam'),
('DV027', N'Pool Bar - Vinpearl Resort Nha Trang', N'Nằm bên hồ bơi chính của khách sạn Vinpearl Resort Nha Trang, Pool Bar là nơi lý tưởng để quý khách có thể vừa thư giãn dưới làn nước trong xanh, vừa thưởng thức những ly cocktail hảo hạng, rượu và đồ ăn nhẹ.', 'dichvu_28.jpg', 'LDV002', 5, '(+84) 258 359 8598', N'(+84)258 359 8900'),
('DV028', N'Beachcomber - Vinpearl Resort Nha Trang', N'Tọa lạc tại Vinpearl Resort Nha Trang, Nhà hàng Beachcomber phục vụ ẩm thực theo phong cách biển và các món ăn quốc tế đặc sắc. Không gì tuyệt vời hơn khi thưởng thức ẩm thực trong không gian thơ mộng cạnh bãi biển và hồ bơi, một không gian thoáng đãng với gió biển khuấy động mọi giác quan của du khách.', 'dichvu_29.jpg', 'LDV002', 4.5, '(+84) 258 359 8888', N'Vinpearl Resort Nha Trang, Đảo Hòn Tre, Nha Trang, Khánh Hòa, Việt Nam'),
('DV029', N'Club Lounge', N'Club Lounge tọa lạc tại Vinpearl Resort & Spa Long Beach Nha Trang là nơi mang tới cho quý khách những buổi tối sôi động, mới lạ với những thức uống độc đáo, thức ăn nhẹ thơm ngon khiến những buổi tối trở nên \"chill\" hơn bao giờ hết.', 'dichvu_30.jpg', 'LDV002', 5, '(+84) 258 399 1888', N'Vinpearl Resort & Spa Long Beach Nha Trang, Lô D6B2 & D7A1, Khu 2, Bắc Bán đảo Cam Ranh, Huyện Cam Lâm, Tỉnh Khánh Hòa'),
('DV030', N'VinWonders Nha Trang', N'Bước qua cánh cổng của VinWonders Nha Trang là dấn thân vào một cuộc chu du kỳ thú vòng quanh thế giới của niềm vui, sự sôi động và cảm xúc thăng hoa bất tận ở 6 phân khu trò chơi đặc sắc! \r\n\r\nĐến với VinWonders Nha Trang, du khách sẽ được trải nghiệm Cáp treo Thiên đường dài hơn 3.300m tại một trong 29 vịnh biển đẹp nhất hành tinh, “Quẩy” tưng bừng tại Vịnh phao nổi hơn 4.200m2 lớn nhất thế giới! Ngất ngây trên độ cao 120m với Bánh xe bầu trời - Top 10 vòng xoay cao nhất thế giới. Du hành Alpine Coaster dài đến 1.865m - Đường trượt núi trên đảo đầu tiên tại Châu Á.\r\n\r\nKhám phá bộ sưu tập xương rồng lớn nhất Việt Nam cùng các kì hoa dị thảo 5 châu tại The World Garden. Thả mình trên Zipline 880m với Hattrick 03 kỷ lục Dài nhất, Dốc nhất, Cú nhảy tiếp đất cao nhất Việt Nam.', 'dichvu_32.jpg', 'LDV003', 5, '(+84) 258 359 8888', N'Đảo Hòn Tre, Vĩnh Nguyên, Thành phố Nha Trang, tỉnh Khánh Hoà'),
('DV031', N'Vinpearl Submarine Nha Trang\r\n', N'Là tàu ngầm trong suốt 360 độ đầu tiên trên thế giới cùng thiết kế thân vỏ tàu độc đáo 100% acrylic trong suốt, Vinpearl Submarine Nha Trang mang đến cho du khách trải nghiệm có 1-0-2 với tầm nhìn vô cực, hòa mình trọn vẹn vào không gian sâu thẳm và huyền diệu của đại dương', 'dichvu_33.jpg', 'LDV003', 5, '258 359 8222', N'Đảo Hòn Tre, Vĩnh Nguyên, Thành phố Nha Trang, Tỉnh Khánh Hoà'),
('DV032', N'Vinpearl Discovery CNTT Nha Trang', N'Vinpearl Resort Nha Trang chào đón du khách bằng vẻ đẹp Á Đông thuần khiết với kiến trúc mang đậm phong cách Indochine sang trọng cùng bãi biển riêng tư hút mắt. Giữa vạn trải nghiệm sôi nổi, tại quần thể nghỉ dưỡng – giải trí biển hàng đầu khu vực của đảo Hòn Tre, Vinpearl Resort Nha Trang là thiên đàng trú ẩn yên bình dành cho những ai yêu thích nghỉ dưỡng và chăm sóc sức khỏe, là nơi để phục hồi năng lượng cho những tâm hồn sôi nổi yêu giải trí và khám phá, nơi ghi dấu hạnh phúc bằng một đám cưới trong mơ, hay hội họp đẳng cấp để dẫn lối tới thành công.', 'dichvu_7.jpg', 'LDV001', 4.5, '0345324789', N'Nha TRang'),
('DV033', N'Vinpear Resort Nha Trang', N'Vinpearl Resort Nha Trang chào đón du khách bằng vẻ đẹp Á Đông thuần khiết với kiến trúc mang đậm phong cách Indochine sang trọng cùng bãi biển riêng tư hút mắt. Giữa vạn trải nghiệm sôi nổi, tại quần thể nghỉ dưỡng – giải trí biển hàng đầu khu vực của đảo Hòn Tre, Vinpearl Resort Nha Trang là thiên đàng trú ẩn yên bình dành cho những ai yêu thích nghỉ dưỡng và chăm sóc sức khỏe, là nơi để phục hồi năng lượng cho những tâm hồn sôi nổi yêu giải trí và khám phá, nơi ghi dấu hạnh phúc bằng một đám cưới trong mơ, hay hội họp đẳng cấp để dẫn lối tới thành công.\r\n                        ', 'dichvu_3.jpg', 'LDV002', 4.5, '043543234', N'Nha Trang'),
('DV035', N'Vinpearl Lovely Nha Trang', N'Vinpearl Resort Nha Trang chào đón du khách bằng vẻ đẹp Á Đông thuần khiết với kiến trúc mang đậm phong cách Indochine sang trọng cùng bãi biển riêng tư hút mắt. Giữa vạn trải nghiệm sôi nổi, tại quần thể nghỉ dưỡng – giải trí biển hàng đầu khu vực của đảo Hòn Tre, Vinpearl Resort Nha Trang là thiên đàng trú ẩn yên bình dành cho những ai yêu thích nghỉ dưỡng và chăm sóc sức khỏe, là nơi để phục hồi năng lượng cho những tâm hồn sôi nổi yêu giải trí và khám phá, nơi ghi dấu hạnh phúc bằng một đám cưới trong mơ, hay hội họp đẳng cấp để dẫn lối tới thành công.', 'pic1.jpg', 'LDV003', 4.5, '012321321', N'Nha Trang'),
('DV036', N'Vinpearl Earth Nha Trang', N'Vinpearl Resort Nha Trang chào đón du khách bằng vẻ đẹp Á Đông thuần khiết với kiến trúc mang đậm phong cách Indochine sang trọng cùng bãi biển riêng tư hút mắt. Giữa vạn trải nghiệm sôi nổi, tại quần thể nghỉ dưỡng – giải trí biển hàng đầu khu vực của đảo Hòn Tre, Vinpearl Resort Nha Trang là thiên đàng trú ẩn yên bình dành cho những ai yêu thích nghỉ dưỡng và chăm sóc sức khỏe, là nơi để phục hồi năng lượng cho những tâm hồn sôi nổi yêu giải trí và khám phá, nơi ghi dấu hạnh phúc bằng một đám cưới trong mơ, hay hội họp đẳng cấp để dẫn lối tới thành công.', 'test.jpg', 'LDV001', 5, '0345324789', N'Nha Trang'),
('DV037', N'Vinpearl Test Nha Trang', N'Vinpearl Resort Nha Trang chào đón du khách bằng vẻ đẹp Á Đông thuần khiết với kiến trúc mang đậm phong cách Indochine sang trọng cùng bãi biển riêng tư hút mắt. Giữa vạn trải nghiệm sôi nổi, tại quần thể nghỉ dưỡng – giải trí biển hàng đầu khu vực của đảo Hòn Tre, Vinpearl Resort Nha Trang là thiên đàng trú ẩn yên bình dành cho những ai yêu thích nghỉ dưỡng và chăm sóc sức khỏe, là nơi để phục hồi năng lượng cho những tâm hồn sôi nổi yêu giải trí và khám phá, nơi ghi dấu hạnh phúc bằng một đám cưới trong mơ, hay hội họp đẳng cấp để dẫn lối tới thành công.', 'chieunor.png', 'LDV001', 4.5, '0345324789', N'trai dat'),
('DV038', N'Vinpearl Alien NHa Trang', N'Vinpearl Resort Nha Trang chào đón du khách bằng vẻ đẹp Á Đông thuần khiết với kiến trúc mang đậm phong cách Indochine sang trọng cùng bãi biển riêng tư hút mắt. Giữa vạn trải nghiệm sôi nổi, tại quần thể nghỉ dưỡng – giải trí biển hàng đầu khu vực của đảo Hòn Tre, Vinpearl Resort Nha Trang là thiên đàng trú ẩn yên bình dành cho những ai yêu thích nghỉ dưỡng và chăm sóc sức khỏe, là nơi để phục hồi năng lượng cho những tâm hồn sôi nổi yêu giải trí và khám phá, nơi ghi dấu hạnh phúc bằng một đám cưới trong mơ, hay hội họp đẳng cấp để dẫn lối tới thành công.', 'pic3.jpg', 'LDV002', 4.5, '0345324789', N'Nha Trang'),
('DV039', N'Vinpearl Beautiful Nha Trang', N'Vinpearl Resort Nha Trang chào đón du khách bằng vẻ đẹp Á Đông thuần khiết với kiến trúc mang đậm phong cách Indochine sang trọng cùng bãi biển riêng tư hút mắt. Giữa vạn trải nghiệm sôi nổi, tại quần thể nghỉ dưỡng – giải trí biển hàng đầu khu vực của đảo Hòn Tre, Vinpearl Resort Nha Trang là thiên đàng trú ẩn yên bình dành cho những ai yêu thích nghỉ dưỡng và chăm sóc sức khỏe, là nơi để phục hồi năng lượng cho những tâm hồn sôi nổi yêu giải trí và khám phá, nơi ghi dấu hạnh phúc bằng một đám cưới trong mơ, hay hội họp đẳng cấp để dẫn lối tới thành công.', 'pic1.jpg', 'LDV003', 4.5, '0345324789', N'Nha TRang'),
('DV041', N'Vinpearl Golf Nha Trang', N'Kiệt tác sân golf 18 hố Vinpearl Golf Nha Trang do IMG Worldwide thiết kế, được bao trùm bởi vẻ đẹp thơ mộng của biển xanh bao la và sự hùng vĩ của núi non trùng điệp, đem lại những trải nghiệm golf hấp dẫn kết hợp cảnh quan tuyệt đẹp.\r\n\r\nChiều dài: 6787 yards, par 71\r\nLoại cỏ:\r\nFairways/Roughs/Tees: Seashore Paspalum - Sea isle 1\r\nGreens: Seashore Paspalum - Sea isle 2000', 'golf.jpg', 'LDV004', 5, '0258 359 0919', N'Nha Trang');

---
INSERT INTO VE (maVe, maDV, loaiVe, giaTien) VALUES
('VE000001', 'DV001', 0, 400000),
('VE000002', 'DV001', 1, 480000),

('VE000003', 'DV002', 0, 400000),
('VE000004', 'DV002', 1, 480000),

('VE000005', 'DV003', 0, 480000),
('VE000006', 'DV003', 1, 670000),

('VE000007', 'DV004', 0, 480000),
('VE000008', 'DV004', 1, 670000),

('VE000009', 'DV005', 0, 650000),
('VE000010', 'DV005', 1, 550000),

('VE000011', 'DV006', 0, 300000),
('VE000012', 'DV006', 1, 350000),

('VE000013', 'DV007', 0, 750000),
('VE000014', 'DV007', 1, 750000),

('VE000015', 'DV008', 0, 350000),
('VE000016', 'DV008', 1, 300000),

('VE000017', 'DV009', 0, 430000),
('VE000018', 'DV009', 1, 530000),

('VE000019', 'DV010', 0, 420000),
('VE000020', 'DV010', 1, 560000),

('VE000021', 'DV011', 0, 650000),
('VE000022', 'DV011', 1, 540000),

('VE000023', 'DV012', 0, 310000),
('VE000024', 'DV012', 1, 470000),

('VE000025', 'DV013', 0, 340000),
('VE000026', 'DV013', 1, 450000),

('VE000027', 'DV014', 0, 390000),
('VE000028', 'DV014', 1, 320000),

('VE000029', 'DV015', 0, 350000),
('VE000030', 'DV015', 1, 350000),

('VE000031', 'DV016', 0, 400000),
('VE000032', 'DV016', 1, 520000),

('VE000033', 'DV017', 1, 630000),
('VE000034', 'DV017', 0, 510000),

('VE000035', 'DV018', 0, 540000),
('VE000036', 'DV018', 1, 410000),

('VE000037', 'DV019', 0, 420000),
('VE000038', 'DV019', 1, 530000),

('VE000039', 'DV020', 1, 610000),
('VE000040', 'DV020', 0, 420000),

('VE000041', 'DV021', 0, 340000),
('VE000042', 'DV021', 1, 460000),

('VE000043', 'DV022', 0, 530000),
('VE000044', 'DV022', 1, 620000),

('VE000045', 'DV023', 0, 530000),
('VE000046', 'DV023', 1, 620000),

('VE000047', 'DV024', 1, 570000),
('VE000048', 'DV024', 0, 410000),

('VE000049', 'DV025', 0, 520000),
('VE000050', 'DV025', 1, 640000),

('VE000051', 'DV026', 0, 400000),
('VE000052', 'DV026', 1, 450000),

('VE000053', 'DV027', 1, 390000),
('VE000054', 'DV027', 0, 390000),

('VE000055', 'DV028', 0, 350000),
('VE000056', 'DV028', 1, 480000),

('VE000057', 'DV029', 0, 400000),
('VE000058', 'DV029', 1, 450000),

('VE000059', 'DV030', 1, 370000),
('VE000060', 'DV030', 0, 420000),

('VE000061', 'DV031', 0, 430000),
('VE000062', 'DV031', 1, 470000),

('VE000063', 'DV032', 0, 430000),
('VE000064', 'DV032', 1, 470000),

('VE000065', 'DV033', 1, 450000),
('VE000066', 'DV033', 0, 420000),

('VE000069', 'DV035', 1, 610000),
('VE000070', 'DV035', 0, 490000),

('VE000071', 'DV036', 1, 560000),
('VE000072', 'DV036', 0, 350000),

('VE000073', 'DV037', 0, 300000),
('VE000074', 'DV037', 1, 450000),

('VE000075', 'DV038', 1, 480000),
('VE000076', 'DV038', 0, 420000),

('VE000077', 'DV039', 1, 480000),
('VE000078', 'DV039', 0, 450000),

('VE000081', 'DV041', 1, 560000),
('VE000082', 'DV041', 0, 120000);
----
INSERT INTO KHACHHANG(maKH, hoTenKH, SDT, diaChi, ngaySinh, gioiTinh, email, matKhau, anh) VALUES
('KH000001', N'Vũ Minh Nga', '0987654321', N'Nha Trang', '2002-11-09', 1, 'nga.vm.62@ntu.edu.com', '123', 'user.png'),
('KH000002', N'Nguyễn Hải Long', '0987654322', N'Nha Trang', '2002-11-03', 0, 'long.nh.60cntt@ntu.edu.vn', '12345', 'user.png'),
('KH000003', N'Trần Quyang Minh', '0385247684', N'Nha Trang', '2002-01-16', 1, 'minh.tq.62cntt@ntu.edu.vn', '12345', 'user.png'),
('KH000004', N'Nguyễn Thị Mai', '0123456789', N'Hanoi', '2000-05-25', 0, 'mai.nt.00@abc.com', 'password', 'user.png'),
('KH000005', N'Võ Văn An', '0987654321', N'Ho Chi Minh City', '1998-12-10', 1, 'an.vv.98@xyz.com', 'pass123', 'user.png'),
('KH000006', N'Lê Thị Hương', '0369876543', N'Da Nang', '1997-09-18', 0, 'huong.lt.97@gmail.com', 'abcdef', 'user.png'),
('KH000007', N'Trần Văn Long', '0345678901', N'Hanoi', '1995-03-08', 1, 'long.tv.95@yahoo.com', 'qwerty', 'user.png'),
('KH000008', N'Phạm Thị Phương', '0765432109', N'Can Tho', '1994-06-20', 0, 'phuong.ptp.94@hotmail.com', 'passpass', 'user.png'),
('KH000009', N'Hoàng Minh Tuấn', '0901234567', N'Hai Phong', '1993-01-30', 1, 'tuan.hm.93@gmail.com', 'mypassword', 'user.png'),
('KH000010', N'Mai Thanh Hà', '0987654321', N'Da Nang', '1991-08-15', 0, 'ha.mt.91@yahoo.com', '987654', 'user.png');

INSERT INTO KHACHHANG(maKH, hoTenKH, SDT, diaChi, ngaySinh, gioiTinh, email, matKhau, anh) VALUES
('KH000011', N'Nguyễn Thị Thảo', '0123456789', N'Hanoi', '1992-04-18', 0, 'thao.ntt.92@gmail.com', 'password123', 'user.png'),
('KH000012', N'Trần Văn Hùng', '0987654321', N'Ho Chi Minh City', '1996-07-22', 1, 'hung.tv.96@yahoo.com', 'abcdefg', 'user.png'),
('KH000013', N'Lê Anh Tuấn', '0369876543', N'Da Nang', '1994-11-15', 0, 'tuan.lat.94@hotmail.com', 'passpass', 'user.png'),
('KH000014', N'Phạm Thị Loan', '0345678901', N'Hanoi', '1993-03-08', 1, 'loan.pt.93@gmail.com', 'qwerty123', 'user.png'),
('KH000015', N'Đặng Văn Quang', '0765432109', N'Can Tho', '1995-06-20', 0, 'quang.dv.95@yahoo.com', 'pass123pass', 'user.png'),
('KH000016', N'Nguyễn Minh Hằng', '0901234567', N'Hai Phong', '1990-09-30', 1, 'hang.nm.90@gmail.com', 'mypassword', 'user.png'),
('KH000017', N'Vũ Văn Bình', '0987654321', N'Nha Trang', '1989-12-15', 0, 'binh.vv.89@yahoo.com', '987654abc', 'user.png'),
('KH000018', N'Trương Thị Lan', '0369876543', N'Da Nang', '1988-05-18', 1, 'lan.tt.88@hotmail.com', 'abcdefg123', 'user.png'),
('KH000019', N'Hoàng Văn Hòa', '0345678901', N'Hanoi', '1987-03-08', 0, 'hoa.hv.87@gmail.com', 'qwerty12345', 'user.png'),
('KH000020', N'Nguyễn Thị Ngọc', '0765432109', N'Can Tho', '1986-06-20', 1, 'ngoc.nt.86@yahoo.com', 'pass12345', 'user.png'),
('KH000021', N'Trần Văn Đức', '0901234567', N'Hai Phong', '1985-01-30', 0, 'duc.tv.85@gmail.com', 'mypassword123', 'user.png'),
('KH000022', N'Võ Thị Thủy', '0987654321', N'Nha Trang', '1984-08-15', 1, 'thuy.vt.84@yahoo.com', '987654321', 'user.png'),
('KH000023', N'Lê Văn Quân', '0369876543', N'Da Nang', '1983-09-18', 0, 'quan.lv.83@hotmail.com', 'abcdefg12345', 'user.png'),
('KH000024', N'Phạm Thị Hương', '0345678901', N'Hanoi', '1982-03-08', 1, 'huong.pt.82@gmail.com', 'qwertyui', 'user.png'),
('KH000025', N'Nguyễn Văn Sơn', '0765432109', N'Can Tho', '1981-06-20', 0, 'son.nv.81@yahoo.com', 'password12345', 'user.png'),
('KH000026', N'Trương Thị Mai', '0901234567', N'Hai Phong', '1980-01-30', 1, 'mai.tt.80@gmail.com', 'mypassword12345', 'user.png'),
('KH000027', N'Đặng Văn Đức', '0987654321', N'Nha Trang', '1979-08-15', 0, 'duc.dv.79@yahoo.com', '987654321abc', 'user.png'),
('KH000028', N'Vũ Thị Ngân', '0369876543', N'Da Nang', '1978-09-18', 1, 'ngan.vt.78@hotmail.com', 'abcdefg123ui', 'user.png'),
('KH000029', N'Nguyễn Văn Tâm', '0345678901', N'Hanoi', '1977-03-08', 0, 'tam.nv.77@gmail.com', 'qwertyuipass', 'user.png'),
('KH000030', N'Trần Thị Mai', '0765432109', N'Can Tho', '1976-06-20', 1, 'mai.tt.76@yahoo.com', 'password1234', 'user.png');

-- Trường hợp thay avatar của user về mặc định
UPDATE KHACHHANG
SET anh = 'user.png';

------------------
INSERT INTO LOAINV(maLoaiNV, tenLoai, luongCoBan) VALUES
('LNV001', N'Quản Lý', 12000000),
('LNV002', N'Lễ Tân', 12000000),
('LNV003', N'Kế toán', 12000000),
('LNV004', N'Bảo vệ', 10000000);
---

INSERT INTO NHANVIEN (maNV, maLoaiNV, hoTenNV, diaChi, ngaySinh, sdt, gioiTinh, anh, email, matKhau) VALUES
('NV001', 'LNV001', N'Admin', N'Hanoi', '2003-08-25', '0987654321', 0, 'pic2.jpg', 'admin@gmail.com', '12345'),
('NV002', 'LNV002', N'Phạm Thị Minh', N'Hanoi', '2000-02-14', '0901234567', 0, 'pic2.jpg', 'minhpham@gmail.com', '12345'),
('NV003', 'LNV002', N'Lê Đình Duy', N'Hai Phong', '1998-11-27', '0978123456', 1, 'pic2.jpg', 'duyld.98@gmail.com', '12345'),
('NV004', 'LNV003', N'Nguyễn Thị Linh', N'Hai Phong', '1997-08-10', '0967123456', 0, 'pic2.jpg', 'linhnt.97@gmail.com', '12345');

INSERT INTO NHANVIEN (maNV, maLoaiNV, hoTenNV, diaChi, ngaySinh, sdt, gioiTinh, anh, email, matKhau) VALUES
('NV005', 'LNV001', N'Trần Văn Anh', N'Ho Chi Minh City', '1995-04-03', '0912345678', 1, 'pic2.jpg', 'anh.tv.95@gmail.com', '12345'),
('NV006', 'LNV002', N'Nguyễn Thị Bảo', N'Da Nang', '1993-12-18', '0987654321', 0, 'pic2.jpg', 'bao.nt.93@yahoo.com', '12345'),
('NV007', 'LNV002', N'Lê Minh Chí', N'Can Tho', '1992-07-31', '0978123456', 1, 'pic2.jpg', 'chi.lm.92@gmail.com', '12345'),
('NV008', 'LNV003', N'Phạm Văn Đạt', N'Nha Trang', '1990-10-15', '0967123456', 0, 'pic2.jpg', 'dat.pv.90@gmail.com', '12345'),
('NV009', 'LNV001', N'Vũ Thị Êm', N'Hanoi', '1988-05-28', '0912345678', 1, 'pic2.jpg', 'em.vt.88@yahoo.com', '12345'),
('NV010', 'LNV002', N'Nguyễn Văn Phong', N'Ho Chi Minh City', '1986-02-12', '0987654321', 0, 'pic2.jpg', 'phong.nv.86@gmail.com', '12345'),
('NV011', 'LNV002', N'Lê Thị Gia', N'Da Nang', '1984-11-25', '0978123456', 1, 'pic2.jpg', 'gia.lt.84@gmail.com', '12345'),
('NV012', 'LNV003', N'Hoàng Văn Hùng', N'Can Tho', '1982-08-08', '0967123456', 0, 'pic2.jpg', 'hung.vh.82@yahoo.com', '12345'),
('NV013', 'LNV001', N'Trần Thị Ivy', N'Hai Phong', '1980-01-22', '0912345678', 1, 'pic2.jpg', 'ivy.tt.80@gmail.com', '12345'),
('NV014', 'LNV002', N'Võ Văn Jan', N'Nha Trang', '1978-06-05', '0987654321', 0, 'pic2.jpg', 'jan.vv.78@gmail.com', '12345'),
('NV015', 'LNV002', N'Lê Thị Kim', N'Hanoi', '1976-03-20', '0978123456', 1, 'pic2.jpg', 'kim.lt.76@yahoo.com', '12345'),
('NV016', 'LNV003', N'Nguyễn Văn Linh', N'Ho Chi Minh City', '1974-10-03', '0967123456', 0, 'pic2.jpg', 'linh.nv.74@gmail.com', '12345'),
('NV017', 'LNV001', N'Phạm Thị Mỹ', N'Da Nang', '1972-07-17', '0912345678', 1, 'pic2.jpg', 'my.pt.72@gmail.com', '12345'),
('NV018', 'LNV002', N'Trần Văn Ngọc', N'Can Tho', '1970-04-01', '0987654321', 0, 'pic2.jpg', 'ngoc.tv.70@gmail.com', '12345'),
('NV019', 'LNV002', N'Vũ Thị Oanh', N'Hai Phong', '1968-11-14', '0978123456', 1, 'pic2.jpg', 'oanh.vt.68@yahoo.com', '12345'),
('NV020', 'LNV003', N'Lê Văn Phương', N'Nha Trang', '1966-08-27', '0967123456', 0, 'pic2.jpg', 'phuong.lv.66@gmail.com', '12345'),
('NV021', 'LNV001', N'Nguyễn Thị Quỳnh', N'Hanoi', '1964-02-10', '0912345678', 1, 'pic2.jpg', 'quynh.nt.64@gmail.com', '12345'),
('NV022', 'LNV002', N'Võ Văn Rong', N'Ho Chi Minh City', '1962-11-25', '0987654321', 0, 'pic2.jpg', 'rong.vv.62@yahoo.com', '12345'),
('NV023', 'LNV002', N'Lê Thị Sương', N'Da Nang', '1960-07-09', '0978123456', 1, 'pic2.jpg', 'suong.lt.60@gmail.com', '12345'),
('NV024', 'LNV003', N'Nguyễn Văn Tuấn', N'Can Tho', '1958-04-23', '0967123456', 0, 'pic2.jpg', 'van.t.58@gmail.com', '12345'),
('NV025', 'LNV001', N'Trần Thị Uyên', N'Hai Phong', '1956-01-06', '0912345678', 1, 'pic2.jpg', 'uyen.tt.56@yahoo.com', '12345'),
('NV026', 'LNV002', N'Vũ Văn Vương', N'Nha Trang', '1954-08-19', '0987654321', 0, 'pic2.jpg', 'vuong.vv.54@gmail.com', '12345'),
('NV027', 'LNV002', N'Lê Thị Xuân', N'Hanoi', '1952-05-04', '0978123456', 1, 'pic2.jpg', 'xuan.lt.52@gmail.com', '12345'),
('NV028', 'LNV003', N'Nguyễn Văn Yên', N'Ho Chi Minh City', '1950-02-17', '0967123456', 0, 'pic2.jpg', 'yen.nv.50@yahoo.com', '12345'),
('NV029', 'LNV001', N'Phạm Thị Zara', N'Da Nang', '1948-09-02', '0912345678', 1, 'pic2.jpg', 'zara.pt.48@gmail.com','0987');

---
INSERT INTO PHANQUYEN(maChucNang, maLoaiNV, ghiChu) VALUES
('CN01','LNV001',N'Quyền xem'),
('CN02','LNV001',N'Quyền thêm'),
('CN03','LNV001',N'Quyền sửa'),
('CN04','LNV001',N'Quyền xoá'),

('CN01','LNV002',N'Quyền xem'),
('CN01','LNV004',N'Quyền xem'),

('CN01','LNV003',N'Quyền xem'),
('CN03','LNV003',N'Quyền sửa');

--INSERT INTO HOADON (maHD, maKH, maNV, ngayThanhToan, SDT, email) VALUES
--('HD000001', 'KH000001', 'NV001', '2023-11-22 14:11:10', '0092313213', 'duy.nh.62cntt@ntu.edu.vn'),
--('HD000002', 'KH000002', 'NV001', '2023-11-28 00:00:00', '0385247684', 'ly.dt.62cntt@ntu.edu.vn'),
--('HD000003', 'KH000002', 'NV001', '2023-11-28 00:00:00', '0385247684', 'ly.dt.62cntt@ntu.edu.vn'),
--('HD000004', 'KH000003', 'NV001', '2023-11-29 00:00:00', '0385247684', 'minh62cntt@ntu.edu.vn'),
--('HD000005', 'KH000003', 'NV001', '2023-11-29 00:00:00', '0385247684', 'minh62cntt@ntu.edu.vn'),
--('HD000006', 'KH000004', 'NV002', '2023-11-30 00:00:00', '0385247684', 'truong@gmail.com'),
--('HD000007', 'KH000008', 'NV003', '2023-12-01 00:00:00', '0704460748', 'duynho@gmail.com'),
--('HD000008', 'KH000009', 'NV003', '2023-12-02 00:00:00', '0347953691', 'luong@gmail.com'),
--('HD000009', 'KH000010', 'NV001', '2023-12-03 00:00:00', '0347953691', 'hong@gmail.com'),
--('HD000010', 'KH000011', 'NV001', '2023-12-04 00:00:00', '0123456789', 'hai.nguyen@gmail.com'),
--('HD000011', 'KH000012', 'NV001', '2023-12-05 00:00:00', '0987654321', 'lan.pham@gmail.com'),
--('HD000012', 'KH000013', 'NV001', '2023-12-06 00:00:00', '0909090909', 'binh.le@gmail.com'),
--('HD000013', 'KH000014', 'NV002', '2023-12-07 00:00:00', '0586164102', 'tung.do@gmail.com'),
--('HD000014', 'KH000015', 'NV003', '2023-12-08 00:00:00', '0385247684', 'duc.tran@gmail.com'),
--('HD000015', 'KH000016', 'NV001', '2023-12-09 00:00:00', '0123456789', 'anh.duc.nguyen@gmail.com'),
--('HD000016', 'KH000021', 'NV001', '2023-12-10 00:00:00', '0123456789', 'huong.nguyen@gmail.com'),
--('HD000017', 'KH000022', 'NV001', '2023-12-11 00:00:00', '0987654321', 'tu.tran@gmail.com'),
--('HD000018', 'KH000023', 'NV001', '2023-12-12 00:00:00', '0909090909', 'hong.ngo@gmail.com'),
--('HD000019', 'KH000026', 'NV001', '2023-12-13 00:00:00', '0123456789', 'son.pham@gmail.com'),
--('HD000020', 'KH000027', 'NV001', '2023-12-14 00:00:00', '0909090909', 'nam.nguyen@gmail.com'),
--('HD000021', 'KH000028', 'NV001', '2023-12-15 00:00:00', '0385247684', 'tram.ngo@gmail.com'),
--('HD000022', 'KH000029', 'NV001', '2023-12-16 00:00:00', '0586164102', 'yen.phan@gmail.com'),
--('HD000023', 'KH000030', 'NV004', '2023-12-17 00:00:00', '0123456789', 'duc.huynh@gmail.com'),
--('HD000024', 'KH000021', 'NV001', '2023-12-20 00:00:00', '0123456789', 'huong.nguyen@gmail.com'),
--('HD000025', 'KH000022', 'NV001', '2023-12-21 00:00:00', '0987654321', 'tu.tran@gmail.com'),
--('HD000026', 'KH000023', 'NV001', '2023-12-22 00:00:00', '0909090909', 'hong.ngo@gmail.com'),
--('HD000027', 'KH000026', 'NV001', '2023-12-23 00:00:00', '0123456789', 'son.pham@gmail.com'),
--('HD000028', 'KH000027', 'NV002', '2023-12-24 00:00:00', '0909090909', 'nam.nguyen@gmail.com'),
--('HD000029', 'KH000028', 'NV003', '2023-12-25 00:00:00', '0385247684', 'tram.ngo@gmail.com'),
--('HD000030', 'KH000029', 'NV001', '2023-12-26 00:00:00', '0586164102', 'yen.phan@gmail.com'),
--('HD000031', 'KH000030', 'NV001', '2023-12-26 00:00:00', '0123456789', 'duc.huynh@gmail.com');

--select * from HOADON

INSERT INTO SOCA(maCa, maNV, soCa) VALUES
('CA001', 'NV001', 20),
('CA002', 'NV002', 30),
('CA003', 'NV003', 20);
---
--INSERT INTO CTHD (maHD, maVe, soLuong, giaTien) VALUES
--('HD000001', 'VE000001', 1, 2044000),
--('HD000002', 'VE000002', 1, 3420000),
--('HD000003', 'VE000003', 5, 3500000),
--('HD000004', 'VE000004', 1, 3000000),
--('HD000005', 'VE000005', 3, 3000000),
--('HD000006', 'VE000006', 2, 1340000),
--('HD000007', 'VE000007', 1, 480000),
--('HD000008', 'VE000008', 3, 2010000),
--('HD000009', 'VE000009', 1, 650000),
--('HD000010', 'VE000010', 4, 2200000),
--('HD000011', 'VE000011', 2, 600000),
--('HD000012', 'VE000012', 1, 350000),
--('HD000013', 'VE000021', 1, 650000),
--('HD000014', 'VE000022', 3, 1620000),
--('HD000015', 'VE000031', 2, 920000),
--('HD000016', 'VE000001', 1, 400000),
--('HD000017', 'VE000002', 2, 960000),
--('HD000018', 'VE000013', 3, 2250000),
--('HD000019', 'VE000014', 2, 800000),
--('HD000020', 'VE000025', 1, 500000),
--('HD000021', 'VE000026', 4, 1920000),
--('HD000022', 'VE000037', 2, 900000),
--('HD000023', 'VE000038', 3, 1350000),
--('HD000024', 'VE000001', 2, 800000),
--('HD000025', 'VE000002', 1, 480000),
--('HD000026', 'VE000013', 4, 3000000),
--('HD000027', 'VE000014', 3, 2250000),
--('HD000028', 'VE000025', 2, 1000000),
--('HD000029', 'VE000013', 2, 1500000),
--('HD000030', 'VE000025', 1, 500000);
