using Libs.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Extensions
{
    public static class ModelBuilderExtentions
    {
        public static void SeedingData(this ModelBuilder modelBuilder)
        {
            var userRole = new IdentityRole
            {
                Id = "05f2400b-5471-466a-8b7e-27752367e4d6",
                Name = "User",
                NormalizedName = "USER"
            };

            var adminRole = new IdentityRole
            {
                Id = "10f2400b-5471-466a-8b7e-27752367e4d6",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            modelBuilder.Entity<IdentityRole>().HasData(userRole, adminRole);
            var admin = new User()
            {
                Id = "9ae1058d-b602-4025-ab1d-74e7bced8f3b",
                CreatedAt = new DateTime(2025, 10, 31, 20, 53, 44),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEFY87mzNg88TIJtuXRcRIeT0MXYto4NkcukxwFGpl+p5IHBJVqlPbyFx9UJIOmu7eA==",
                SecurityStamp = "3XVVZIW5RPRWT7MKN3Y6VRNTHXY2JGK5",
                ConcurrencyStamp = "6e66d8c1-89da-46df-bc24-ec54c7e7e7cf"
            };

            var user = new User()
            {
                Id = "8d581a98-361e-4333-a651-74e88ef572a4",
                CreatedAt = new DateTime(2025, 10, 31, 20, 54, 14),
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@gmail.com",
                NormalizedEmail = "USER@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEN8TWXW9pNZ+VVyeftOLixsSfyDOtPTZpv84QtbFESyzd6kZ0i70eIPvnvNBKX0Q9Q==",
                SecurityStamp = "DF7GIIY7UNBVCVLZD73QO6PGSVQXBSTW",
                ConcurrencyStamp = "f67e2437-61a2-4458-ac14-de7ab48158b6"
            };
            List<User> userList = new List<User>()
            {
                admin,
                user,
            };
            modelBuilder.Entity<User>().HasData(userList);
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    RoleId = adminRole.Id,
                    UserId = admin.Id
                },
                new IdentityUserRole<string>()
                {
                    RoleId = userRole.Id,
                    UserId = user.Id
                }
            };
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
            var loaiBangLais = new List<LoaiBangLai>()
            {
                new LoaiBangLai()
                {
                    Id = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a"),
                    TenLoai = "A1",
                    MoTa = "Xe mô tô hai bánh có dung tích xy lanh từ 50 cm3 đến dưới 175 cm3",
                    LoaiXe = "Xe máy",
                    ThoiGianThi = 19,
                    DiemToiThieu = 21,
                    isDeleted = false
                },
                new LoaiBangLai()
                {
                    Id = Guid.Parse("899712b1-bbe9-4fb6-8c7c-046b9de9e58b"),
                    TenLoai = "A2",
                    MoTa = "Xe mô tô dung tích xy lanh từ 175 cm3 trở lên",
                    LoaiXe = "Xe máy",
                    ThoiGianThi = 19,
                    DiemToiThieu = 23,
                    isDeleted = false
                },
                new LoaiBangLai()
                {
                    Id = Guid.Parse("25edbe66-cf05-43ca-ae57-a6dfe679563a"),
                    TenLoai = "A3",
                    MoTa = "Xe mô tô ba bánh",
                    LoaiXe = "Xe máy",
                    ThoiGianThi = 19,
                    DiemToiThieu = 23,
                    isDeleted = false
                },
                new LoaiBangLai()
                {
                    Id = Guid.Parse("84031e45-3634-445a-9edc-fe146cb5fd20"),
                    TenLoai = "A4",
                    MoTa = "Xe các loại máy kéo nhỏ có trọng tải đến 1000kg.",
                    LoaiXe = "Xe máy",
                    ThoiGianThi = 19,
                    DiemToiThieu = 23,
                    isDeleted = false
                },
                new LoaiBangLai()
                {
                    Id = Guid.Parse("de0f2329-c8c2-4736-9ea3-107239c304a1"),
                    TenLoai = "B1",
                    MoTa = "Ô tô số tự động chở người đến 9 chỗ ngồi, Ô tô tải dưới 3.500 kg.",
                    LoaiXe = "Xe oto",
                    ThoiGianThi = 20,
                    DiemToiThieu = 30,
                    isDeleted = false
                },
                new LoaiBangLai()
                {
                    Id = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848"),
                    TenLoai = "B2",
                    MoTa = "Ô tô 4 – 9 chỗ, ô tô chuyên dùng có trọng tải thiết kế dưới 3,5 tấn",
                    LoaiXe = "Xe oto",
                    ThoiGianThi = 22,
                    DiemToiThieu = 35,
                    isDeleted = false
                },
                new LoaiBangLai()
                {
                    Id = Guid.Parse("bfa465f9-2b2d-4f82-9242-b30b4af0a375"),
                    TenLoai = "C",
                    MoTa = "Ô tô chuyên dùng có trọng tải thiết kế từ 3500 kg trở lên. Máy kéo kéo một rơ moóc  từ 3500 kg trở lên.",
                    LoaiXe = "Xe oto",
                    ThoiGianThi = 24,
                    DiemToiThieu = 40,
                    isDeleted = false
                },
                new LoaiBangLai()
                {
                    Id = Guid.Parse("94d00b92-77fa-4efd-bc92-64da47a5bd1e"),
                    TenLoai = "SAETR",
                    MoTa = "1",
                    LoaiXe = "1",
                    ThoiGianThi = 2,
                    DiemToiThieu = 2,
                    isDeleted = true
                }
            };
            modelBuilder.Entity<LoaiBangLai>().HasData(loaiBangLais);

            // Seed MoPhongs (Simulations)
            var moPhongs = new List<MoPhong>()
            {
                new MoPhong()
                {
                    Id = Guid.Parse("de0f2329-c8c2-4736-9ea3-107239c304a1"),
                    NoiDung = "Người đi bộ sang đường bị khuất sau xe tải",
                    VideoUrl = "/videos/cau1.mp4",
                    DapAn = "10,11,12,13,14,15",
                    LoaiBangLaiId = Guid.Parse("de0f2329-c8c2-4736-9ea3-107239c304a1")
                },
                new MoPhong()
                {
                    Id = Guid.Parse("8e7344e6-7483-4912-bc52-26ccd1f4f3dc"),
                    NoiDung = "Nội dung 1",
                    VideoUrl = "/videos/823c2df0-3f5e-4dad-8ca2-663989bb3c37.mp4",
                    DapAn = "0.806122,1.060729,1.320611,1.563675,1.816014,2.078444",
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new MoPhong()
                {
                    Id = Guid.Parse("a465960a-1525-4140-96b3-284205133163"),
                    NoiDung = "Người đi bộ sang đường bị khuất sau xe tải",
                    VideoUrl = "/videos/cau1.mp4",
                    DapAn = "10,11,12,13,14,15",
                    LoaiBangLaiId = Guid.Parse("de0f2329-c8c2-4736-9ea3-107239c304a1")
                },
                new MoPhong()
                {
                    Id = Guid.Parse("1c32fc5d-51a6-4349-979d-c6d7b5d369ed"),
                    NoiDung = "Người đi bộ vượt đèn đỏ sang đường",
                    VideoUrl = "/videos/876b7aef-94dc-46e1-8d67-2b5f016e50ed.mp4",
                    DapAn = "18.357427,19.170872,20.296029,21.334759,22.189645,23.587884",
                    LoaiBangLaiId = Guid.Parse("de0f2329-c8c2-4736-9ea3-107239c304a1")
                }
            };
            modelBuilder.Entity<MoPhong>().HasData(moPhongs);

            // Seed BaiThis (Exams)
            var baiThis = new List<BaiThi>()
            {
                new BaiThi()
                {
                    Id = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    TenBaiThi = "Đề 4"
                }
            };
            modelBuilder.Entity<BaiThi>().HasData(baiThis);
            var chuDes = new List<ChuDe>()
            {
                new ChuDe()
                {
                    Id = Guid.Parse("78627311-083d-42f9-a921-168e88e5f187"),
                    TenChuDe = "Khái niệm và quy tắc",
                    MoTa = "Khái niệm và quy tắc",
                    ImageUrl = "/images/30f08b72-5724-4e94-94a5-c8211d05d547.png",
                    isDeleted = false
                },
                new ChuDe()
                {
                    Id = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    TenChuDe = "Câu hỏi điểm liệt",
                    MoTa = "Khái niệm và quy tắc giao thông",
                    ImageUrl = "/images/warning.png",
                    isDeleted = false
                },
                new ChuDe()
                {
                    Id = Guid.Parse("cdcfb0da-a22d-4891-bac4-1fea55614508"),
                    TenChuDe = "Kỹ thuật lái xe",
                    MoTa = "Kỹ thuật lái xe",
                    ImageUrl = "/images/laixe.png",
                    isDeleted = false
                },
                new ChuDe()
                {
                    Id = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    TenChuDe = "Văn hóa và đạo đức",
                    MoTa = "Văn hóa và đạo đức",
                    ImageUrl = "/images/vanhoa.png",
                    isDeleted = false
                },
                new ChuDe()
                {
                    Id = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    TenChuDe = "Nghiệp vụ vận tải",
                    MoTa = "Nghiệp vụ vận tải",
                    ImageUrl = "/images/vantai.png",
                    isDeleted = false
                },
                new ChuDe()
                {
                    Id = Guid.Parse("a5674e1d-af51-4a56-bdd8-758210677c1a"),
                    TenChuDe = "Biển báo đường bộ",
                    MoTa = "Biển báo đường bộ",
                    ImageUrl = "/images/bienbao.png",
                    isDeleted = false
                },
                new ChuDe()
                {
                    Id = Guid.Parse("a1720db9-1416-4e62-a922-e34364f67418"),
                    TenChuDe = "Sa hình",
                    MoTa = "Sa hình",
                    ImageUrl = "/images/sahinh.png",
                    isDeleted = false
                },
                new ChuDe()
                {
                    Id = Guid.Parse("1365e7dd-6325-4662-ba5a-fe0312d5519a"),
                    TenChuDe = "Cấu tạo và sửa chữa",
                    MoTa = "Cấu tạo và sửa chữa",
                    ImageUrl = "/images/cautao.png",
                    isDeleted = false
                }
            };
            modelBuilder.Entity<ChuDe>().HasData(chuDes);
            var cauHois = new List<CauHoi>()
            {
                new CauHoi()
                {
                    Id = Guid.Parse("5a28d77b-4387-49e6-8541-059f96459f50"),
                    NoiDung = "Người lái xe khách, xe buýt cần thực hiện những nhiệm vụ gì dưới đây?",
                    LuaChonA = "1. Luôn có ý thức về tính tổ chức, kỷ luật, thực hiện nghiêm biểu đồ xe chạy được phân công; thực hiện đúng hành trình, lịch trình, đón trả khách đúng nơi quy định; giúp đỡ hành khách đi xe, đặc biệt là những người khuyết tật, người già, trẻ em và phụ nữ có thai, có con nhỏ.",
                    LuaChonB = "2. Luôn có ý thức về tính tổ chức, kỷ luật, thực hiện linh hoạt biểu đồ xe chạy được phân công để tiết kiệm chi phí; thực hiện đúng hành trình, lịch trình khi có khách đi xe, đón trả khách ở những nơi thuận tiện cho hành khách đi xe.",
                    LuaChonC = "",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Lái xe khách, xe buýt thực hiện nghiêm biểu đồ chạy xe được phân công.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("3393dbff-9dee-4f39-9d0d-08219b69d1ed"),
                    NoiDung = "Người lái xe sử dụng đèn như thế nào khi lái xe trong khu đô thị và đông dân cư vào ban đêm?",
                    LuaChonA = "Bất cứ đèn nào miễn là mắt nhìn rõ phía trước.",
                    LuaChonB = "Chỉ bật đèn chiếu xa (đèn pha) khi không nhìn rõ đường.",
                    LuaChonC = "Đèn chiếu xa (đèn pha) khi đường vắng, đèn pha chiếu gần (đèn cốt) khi có xe đi ngược chiều.",
                    LuaChonD = "Đèn chiếu gần (đèn cốt).",
                    DapAnDung = 'D',
                    GiaiThich = "Trong đô thị sử dụng đèn chiếu gần.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("0e418bb1-c0a8-429c-964d-08d8b97149bf"),
                    NoiDung = "Thời gian làm việc của người lái xe ô tô không được lái xe liên tục quá bao nhiêu giờ trong trường hợp nào dưới đây?",
                    LuaChonA = "Không quá 4 giờ.",
                    LuaChonB = "Không quá 6 giờ.",
                    LuaChonC = "Không quá 8 giờ.",
                    LuaChonD = "Liên tục tùy thuộc vào sức khỏe và khả năng của người lái xe.",
                    DapAnDung = 'A',
                    GiaiThich = "Không lái xe liên tục quá 4 giờ.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("ca876d72-60f0-4666-ad3e-0aca1ef4df59"),
                    NoiDung = "Khi nhả hệ thống phanh dừng cơ khí điều khiển bằng tay (phanh tay)...",
                    LuaChonA = "Dùng lực tay phải kéo cần phanh tay về phía sau hết hành trình...",
                    LuaChonB = "Dùng lực tay phải bóp khóa hãm đẩy cần phanh tay về phía trước hết hành trình...",
                    LuaChonC = "Dùng lực tay phải đẩy cần phanh tay về phía trước hết hành trình...",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Thực hiện  phanh tay cần phải bóp khóa hãm đẩy cần phanh tay về phía trước.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("cdcfb0da-a22d-4891-bac4-1fea55614508"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("9b9167b2-d1ce-41c9-a47e-0c6fc37f1ee9"),
                    NoiDung = "Xe ô tô tham gia giao thông đường bộ phải bảo đảm các quy định...",
                    LuaChonA = "Kính chắn gió, kính cửa phải là loại kính an toàn...",
                    LuaChonB = "Có đủ đèn chiếu sáng gần và xa...",
                    LuaChonC = "Cả ý 1 và ý 2.",
                    LuaChonD = "",
                    DapAnDung = 'C',
                    GiaiThich = "",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("1365e7dd-6325-4662-ba5a-fe0312d5519a"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("33431925-d90e-443f-bb4d-0dc5a048394f"),
                    NoiDung = "Hành vi vượt xe tại các vị trí có tầm nhìn hạn chế, đường vòng, đầu dốc có bị nghiêm cấm hay không?",
                    LuaChonA = "Không bị nghiêm cấm.",
                    LuaChonB = "Không bị nghiêm cấm khi rất vội.",
                    LuaChonC = "Bị nghiêm cấm.",
                    LuaChonD = "Không bị nghiêm cấm khi khẩn cấp.",
                    DapAnDung = 'C',
                    GiaiThich = "",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("17df60b3-08c7-4804-97b1-13f6c07725a7"),
                    NoiDung = "Khi điều khiển xe mô tô tay ga xuống đường dốc dài...",
                    LuaChonA = "Giữ tay ga ở mức độ phù hợp, sử dụng phanh trước và phanh sau để giảm tốc độ.",
                    LuaChonB = "Nhả hết tay ga, tắt động cơ, sử dụng phanh trước và phanh sau để giảm tốc độ.",
                    LuaChonC = "Sử dụng phanh trước để giảm tốc độ kết hợp với tắt chìa khóa điện của xe.",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Xe mô tô xuống dốc dài cần sử dụng cả phanh trước và phanh sau để giảm tốc độ.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("cdcfb0da-a22d-4891-bac4-1fea55614508"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("713d0b90-a65b-46aa-a988-14e97dc5ca08"),
                    NoiDung = "Khi sơ cứu người bị tai nạn giao thông...",
                    LuaChonA = "Thực hiện cầm máu trực tiếp.",
                    LuaChonB = "Thực hiện cầm máu không trực tiếp (chặn động mạch).",
                    LuaChonC = "",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("9ac6b70c-a8d8-4f44-aab5-15f0f377bf80"),
                    NoiDung = "Trong hoạt động vận tải đường bộ, các hành vi nào dưới đây bị nghiêm cấm?",
                    LuaChonA = "Vận chuyển hàng nguy hiểm nhưng có giấy phép.",
                    LuaChonB = "Vận chuyển động vật hoang dã nhưng thực hiện đủ các quy định có liên quan.",
                    LuaChonC = "Vận chuyển hàng hóa cấm lưu thông; vận chuyển trái phép hàng nguy hiểm, động vật hoang dã.",
                    LuaChonD = "",
                    DapAnDung = 'C',
                    GiaiThich = "Nghiêm cấm vận chuyển hàng cấm lưu thông.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("8887645d-e66e-4136-a243-16df6cc623be"),
                    NoiDung = "Xe ô tô tham gia giao thông trên đường bộ phải có đủ các loại đèn gì dưới đây?",
                    LuaChonA = "Đèn chiếu sáng gần và xa.",
                    LuaChonB = "Đèn soi biển số, đèn báo hãm và đèn tín hiệu.",
                    LuaChonC = "Dàn đèn pha trên nóc xe.",
                    LuaChonD = "Cả ý 1 và ý 2.",
                    DapAnDung = 'D',
                    GiaiThich = "",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("1365e7dd-6325-4662-ba5a-fe0312d5519a"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("2eda2d78-132f-4100-bf02-1ab43e19e64e"),
                    NoiDung = "Xe mô tô và xe ô tô tham gia giao thông trên đường bộ phải bắt buộc có đủ bộ phận giảm thanh không?",
                    LuaChonA = "Không bắt buộc.",
                    LuaChonB = "Bắt buộc.",
                    LuaChonC = "Tùy từng trường hợp.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("1365e7dd-6325-4662-ba5a-fe0312d5519a"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("2e311460-e16f-44e1-981c-1c40c1268c1a"),
                    NoiDung = "Sử dụng rượu, bia khi lái xe, nếu bị phát hiện thì bị xử lý như thế nào?",
                    LuaChonA = "Chỉ bị nhắc nhở.",
                    LuaChonB = "Bị xử phạt hành chính hoặc có thể bị xử lý hình sự tùy theo mức độ vi phạm.",
                    LuaChonC = "Không bị xử lý hình sự.",
                    LuaChonD = null,
                    DapAnDung = 'B',
                    GiaiThich = "Sử dụng rượu, bia khi lái xe bị phạt hành chính hoặc xử lý hình sự.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("018d8cd1-13cc-42f9-a8e6-1c745ce6db87"),
                    NoiDung = "Người ngồi trên xe mô tô hai bánh, ba bánh, xe gắn máy khi tham gia giao thông có được mang, vác vật cồng kềnh hay không?",
                    LuaChonA = "Được mang, vác tùy trường hợp cụ thể.",
                    LuaChonB = "Không được mang, vác.",
                    LuaChonC = "Được mang, vác nhưng phải đảm bảo an toàn.",
                    LuaChonD = "Được mang, vác tùy theo sức khoẻ của bản thân.",
                    DapAnDung = 'B',
                    GiaiThich = "Xe mô tô không được mang vác vật cồng kềnh.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("459f6775-e179-4288-9c2d-1d725cf3b6d8"),
                    NoiDung = "Trong hoạt động vận tải đường bộ, các hành vi nào dưới đây bị nghiêm cấm?",
                    LuaChonA = "Vận chuyển hàng nguy hiểm nhưng có giấy phép.",
                    LuaChonB = "Vận chuyển động vật hoang dã nhưng thực hiện đủ các quy định có liên quan.",
                    LuaChonC = "Vận chuyển hàng hóa cấm lưu thông; vận chuyển trái phép hàng nguy hiểm, động vật hoang dã.",
                    LuaChonD = "",
                    DapAnDung = 'C',
                    GiaiThich = "Nghiêm cấm vận chuyển hàng cấm lưu thông.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("798d3390-105b-40e6-b030-23af2ccfa26a"),
                    NoiDung = "Trong các hành vi dưới đây, người lái xe mô tô có văn hóa giao thông phải ứng xử như thế nào?",
                    LuaChonA = "Điều khiển xe đi trên phần đường, làn đường có ít phương tiện tham gia giao thông, chỉ đội mũ bảo hiểm ở nơi có biển báo bắt buộc đội mũ bảo hiểm.",
                    LuaChonB = "Chấp hành quy định về tốc độ, đèn tín hiệu, biển báo hiệu, vạch kẻ đường khi lái xe; chấp hành hiệu lệnh, chỉ dẫn của người điều khiển giao thông; nhường đường cho người đi bộ, người già, trẻ em và người khuyết tật.",
                    LuaChonC = "Cả ý 1 và ý 2.",
                    LuaChonD = null,
                    DapAnDung = 'B',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("0c7fc479-bfa1-4f32-99c0-250934199496"),
                    NoiDung = "Người lái xe khách phải chấp hành những quy định nào dưới đây?",
                    LuaChonA = "Đón, trả khách đúng nơi quy định, không trở hành khách trên mui, trong khoang hành lý hoặc để hành khách đu bám bên ngoài xe.",
                    LuaChonB = "Không chở hàng nguy hiểm, hàng có mùi hôi thối hoặc động vật, hàng hóa khác có ảnh hưởng đến sức khỏe của hành khách.",
                    LuaChonC = "Chở hành khách trên mui; để hàng hóa trong khoang chở khách, chở quá số người theo quy định.",
                    LuaChonD = "Cả ý 1 và ý 2.",
                    DapAnDung = 'D',
                    GiaiThich = "Cả ý 1 và ý 2 đều đúng. Bởi vì không thể chở hành khách trên mui được, nên ý 3 sai.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("755996ec-0569-4a13-aeb4-28b38b33d0d3"),
                    NoiDung = "Theo hướng mũi tên, thứ tự các xe đi như thế nào là đúng quy tắc giao thông?",
                    LuaChonA = "Xe tải, xe công an, xe khách, xe con.",
                    LuaChonB = "Xe công an, xe khách, xe con, xe tải.",
                    LuaChonC = "Xe công an, xe con, xe tải, xe khách.",
                    LuaChonD = "Xe công an, xe tải, xe khách, xe con",
                    DapAnDung = 'D',
                    GiaiThich = "Thứ tự ưu tiên: Xe ưu tiên - Đường ưu tiên - Đường cùng cấp theo thứ tự bên phải trống - rẽ phải - đi thẳng - rẽ trái. 1. Xe công an: Xe ưu tiên; 2. Xe tải: Đường ưu tiên; 3. Xe khách: Đường không ưu tiên, bên phải trống; 4. Xe con: Đường không ưu tiên, bên phải vướng xe khách nên phải nhường.",
                    DiemLiet = false,
                    MediaUrl = "/images/h7.webp",
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("a1720db9-1416-4e62-a922-e34364f67418"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("c386c5d2-a62c-46d0-90fc-28c7b066a298"),
                    NoiDung = "Khi nhả hệ thống phanh dừng cơ khí điều khiển bằng tay (phanh tay), người lái xe cần phải thực hiện các thao tác nào?",
                    LuaChonA = "Dùng lực tay phải kéo cần phanh tay về phía sau hết hành trình; nếu khóa hãm bị kẹt cứng phải đẩy mạnh phanh tay về phía trước, sau đó bóp khóa hãm.",
                    LuaChonB = "Dùng lực tay phải bóp khóa hãm đẩy cần phanh tay về phía trước hết hành trình; nếu khóa hãm bị kẹt cứng phải kéo cần phanh tay về phía sau đồng thời bóp khóa hãm.",
                    LuaChonC = "Dùng lực tay phải đẩy cần phanh tay về phía trước hết hành trình; nếu khóa hãm bị kẹt cứng phải đẩy mạnh phanh tay về phía trước, sau đó bóp khóa hãm.",
                    LuaChonD = null,
                    DapAnDung = 'B',
                    GiaiThich = "Thực hiện  phanh tay cần phải bóp khóa hãm đẩy cần phanh tay về phía trước.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("cdcfb0da-a22d-4891-bac4-1fea55614508"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("78e0ce2c-e8a7-45a9-8999-2914161bc940"),
                    NoiDung = "Người lái xe và nhân viên phục vụ trên ô tô vận tải hành khách phải có những trách nhiệm gì theo quy định dưới đây?",
                    LuaChonA = "Kiểm tra các điều kiện bảo đảm an toàn của xe sau khi khởi hành; có trách nhiệm lái xe thật nhanh khi chậm giờ của khách.",
                    LuaChonB = "Kiểm tra các điều kiện bảo đảm an toàn của xe trước khi khởi hành; có thái độ văn minh, lịch sự, hướng dẫn hành khách ngồi đúng nơi quy định; kiểm tra việc sắp xếp, chằng buộc hành lý, bảo đảm an toàn.",
                    LuaChonC = "Có biện pháp bảo vệ tính mạng, sức khỏe, tài sản của hành khách đi xe, giữ gìn trật tự; vệ sinh trong xe; đóng cửa lên xuống của xe trước và trong khi xe chạy.",
                    LuaChonD = "Cả ý 2 và ý 3.",
                    DapAnDung = 'D',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("1a1040db-498c-4942-9542-29df91a1f15a"),
                    NoiDung = "Thời gian làm việc trong một ngày của người lái xe ô tô không được vượt quá bao nhiêu giờ trong trường hợp dưới đây?",
                    LuaChonA = "Không quá 8 giờ.",
                    LuaChonB = "Không quá 10 giờ.",
                    LuaChonC = "Không quá 12 giờ.",
                    LuaChonD = "Không hạn chế tùy thuộc vào sức khỏe và khả năng của người lái xe.",
                    DapAnDung = 'B',
                    GiaiThich = "Không làm việc 1 ngày của lái xe quá 10 giờ.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("b6400618-dbf7-4eff-9cc6-29ec22e49f30"),
                    NoiDung = "Khi khởi hành ô tô sử dụng hộp số cơ khí...",
                    LuaChonA = "Kiểm tra an toàn xung quanh xe ô tô...",
                    LuaChonB = "Kiểm tra an toàn xung quanh xe ô tô; đạp ly hợp (côn) hết hành trình...",
                    LuaChonC = "",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Khởi hành ô tô sử dụng hộp số đạp côn hết hành trình; vào số 1.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("cdcfb0da-a22d-4891-bac4-1fea55614508"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("9ecc10c6-4184-49b1-8417-2ccac03588de"),
                    NoiDung = "Trong trường hợp đặc biệt, để được lắp đặt, sử dụng còi, đèn không đúng với thiết kế của nhà sản xuất đối với từng loại xe cơ giới bạn phải đảm bảo yêu cầu nào dưới đây?",
                    LuaChonA = "Phải đảm bảo phụ tùng do đúng nhà sản xuất đó cung cấp.",
                    LuaChonB = "Phải được chấp thuận của cơ quan có thẩm quyền.",
                    LuaChonC = "Phải là xe đăng ký và hoạt động tại các khu vực có địa hình phức tạp.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Lắp đặt còi đèn không đúng thiết kế phải được chấp thuận của cơ quan có thẩm quyền.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("33ca0bba-4d98-429a-9aee-2ddcf4cd4778"),
                    NoiDung = "Người điều khiển phương tiện giao thông trên đường phố có được dừng xe, đỗ xe trên miệng cống thoát nước, miệng hầm của đường điện thoại, điện cao thế, chỗ dành riêng cho xe chữa cháy lấy nước hay không?",
                    LuaChonA = "Được dừng xe, đỗ xe trong trường hợp cần thiết.",
                    LuaChonB = "Không được dừng xe, đỗ xe.",
                    LuaChonC = "Được dừng xe, không được đỗ xe.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Không được dừng, đỗ xe trên miệng cống thoát nước.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("2008b29d-79af-4dad-83d8-30b5098afc26"),
                    NoiDung = "Trên làn đường dành cho ô tô có vũng nước lớn, có nhiều người đi xe mô tô trên làn đường bên cạnh, người lái xe ô tô xử lý như thế nào là có văn hóa giao thông?",
                    LuaChonA = "Cho xe chạy thật nhanh qua vũng nước.",
                    LuaChonB = "Giảm tốc độ cho xe chạy chậm qua vũng nước.",
                    LuaChonC = "Giảm tốc độ cho xe chạy chậm qua vũng nước.",
                    LuaChonD = "Giảm tốc độ cho xe chạy qua làn đường dành cho mô tô để tránh vũng nước.",
                    DapAnDung = 'C',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("aa5ad586-935e-42e0-9002-331a6e6f17e4"),
                    NoiDung = "Người điều khiển xe mô tô, ô tô, máy kéo trên đường mà trong máu hoặc hơi thở có nồng độ cồn có bị nghiêm cấm không?",
                    LuaChonA = "Bị nghiêm cấm.",
                    LuaChonB = "Không bị nghiêm cấm",
                    LuaChonC = "Không bị nghiêm cấm, nếu nồng độ cồn trong máu ở mức nhẹ, có thể điều khiển phương tiện tham gia giao thông.",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Trong máu hoặc hơi thở có nồng độ cồn bị nghiêm cấm.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("78627311-083d-42f9-a921-168e88e5f187"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("b6f4b2ea-cc44-4e64-a167-34d529ee1683"),
                    NoiDung = "Người điều khiển phương tiện giao thông đường bộ mà trong cơ thể có chất ma túy có bị nghiêm cấm hay không?",
                    LuaChonA = "Bị nghiêm cấm.",
                    LuaChonB = "Không bị nghiêm cấm.",
                    LuaChonC = "Không bị nghiêm cấm, nếu có chất ma túy ở mức nhẹ, có thể điều khiển phương tiện tham gia giao thông.",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Có ma túy bị nghiêm cấm",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("78627311-083d-42f9-a921-168e88e5f187"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("cf39ba06-f565-4f78-8f21-354b90b54dbc"),
                    NoiDung = "Khi điều khiển xe mô tô tay ga xuống đường dốc dài, độ dốc cao, người lái xe cần thực hiện các thao tác nào dưới đây để đảm bảo an toàn?",
                    LuaChonA = "Giữ tay ga ở mức độ phù hợp, sử dụng phanh trước và phanh sau để giảm tốc độ.",
                    LuaChonB = "Nhả hết tay ga, tắt động cơ, sử dụng phanh trước và phanh sau để giảm tốc độ.",
                    LuaChonC = "Sử dụng phanh trước để giảm tốc độ kết hợp với tắt chìa khóa điện của xe.",
                    LuaChonD = null,
                    DapAnDung = 'A',
                    GiaiThich = "Xe mô tô xuống dốc dài cần sử dụng cả phanh trước và phanh sau để giảm tốc độ.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("cdcfb0da-a22d-4891-bac4-1fea55614508"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("fb056cb8-1fe7-4e3c-9355-375a41dbdeb1"),
                    NoiDung = "Chủ phương tiện cơ giới đường bộ có được tự ý thay đổi màu sơn...",
                    LuaChonA = "Được phép thay đổi bằng cách dán đề can với màu sắc phù hợp.",
                    LuaChonB = "Không được phép thay đổi.",
                    LuaChonC = "Tùy từng loại phương tiện cơ giới đường bộ.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Không được phép thay đổi so với giấy chứng nhận đăng ký xe.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("1365e7dd-6325-4662-ba5a-fe0312d5519a"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("668c65d9-aa62-49bc-994a-38e95163dfff"),
                    NoiDung = "Trong hoạt động vận tải khách, những hành vi nào dưới đây bị nghiêm cấm?",
                    LuaChonA = "Cạnh tranh nhau nhằm tăng lợi nhuận.",
                    LuaChonB = "Giảm giá để thu hút khách.",
                    LuaChonC = "Đe dọa, xúc phạm, tranh giành, lôi kéo hành khách; bắt ép hành khách sử dụng dịch vụ ngoài ý muốn; xuống khách nhằm trốn tránh phát hiện xe chở quá số người quy định.",
                    LuaChonD = "Tất cả các ý trên.",
                    DapAnDung = 'C',
                    GiaiThich = "Nghiêm cấm đe dọa, xúc phạm, tranh giành, lôi kéo hành khách.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("1c44f24a-713c-4ebf-bbff-396c10f45693"),
                    NoiDung = "Bạn đang lái xe trong khu dân cư, có đông xe qua lại, nếu muốn quay đầu bạn cần làm gì để tránh ùn tắc và đảm bảo an toàn giao thông?",
                    LuaChonA = "Đi tiếp đến điểm giao cắt gần nhất hoặc nơi có biển báo cho phép quay đầu xe.",
                    LuaChonB = "Bấm đèn khẩn cấp và quay đầu xe từ từ bảo đảm an toàn.",
                    LuaChonC = "Bấm còi liên tục khi quay đầu để cảnh báo các xe khác.",
                    LuaChonD = "Nhờ một người ra hiệu giao thông trên đường chậm lại trước khi quay đầu.",
                    DapAnDung = 'A',
                    GiaiThich = "Chỉ quay đầu xe ở điểm giao cắt hoặc nơi có biển báo cho phép quay đầu.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("0e29fb07-bc96-41de-be34-3a5ac5adbcb6"),
                    NoiDung = "Hành vi giao xe cơ giới, xe máy chuyên dùng cho người không đủ điều kiện để điều khiển xe tham gia giao thông có được phép hay không?",
                    LuaChonA = "Chỉ được thực hiện nếu đã hướng dẫn đầy đủ.",
                    LuaChonB = "Không được phép.",
                    LuaChonC = "Được phép tùy từng trường hợp.",
                    LuaChonD = "Chỉ được phép thực hiện với thành viên trong gia đình.",
                    DapAnDung = 'B',
                    GiaiThich = "Không được phép giao xe cho người không đủ điều kiện tham gia giao thông.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("78627311-083d-42f9-a921-168e88e5f187"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("b17ab884-4488-4658-8ee8-3c22895dc21a"),
                    NoiDung = "Khi lái xe trên đường, người lái xe cần quan sát và đảm bảo tốc độ phương tiện như thế nào?",
                    LuaChonA = "Chỉ lớn hơn tốc độ tối đa cho phép khi đường vắng.",
                    LuaChonB = "Chỉ lớn hơn tốc độ tối đa cho phép vào ban đêm.",
                    LuaChonC = "Không vượt quá tốc độ cho phép.",
                    LuaChonD = null,
                    DapAnDung = 'C',
                    GiaiThich = null,
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("057a01a9-d102-4013-8d4e-40e580010abe"),
                    NoiDung = "Khi sơ cứu người bị tai nạn giao thông đường bộ, có vết thương chảy máu ngoài, màu đỏ tươi phun thành tia và phun mạnh khi mạch đập, bạn phải làm gì dưới đây?",
                    LuaChonA = "Thực hiện cầm máu trực tiếp.",
                    LuaChonB = "Thực hiện cầm máu không trực tiếp (chặn động mạch).",
                    LuaChonC = null,
                    LuaChonD = null,
                    DapAnDung = 'B',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("62f88d57-380c-49c6-8dc6-418133f8c194"),
                    NoiDung = "Ở phần đường dành cho người đi bộ qua đường, trên cầu, đầu cầu, đường cao tốc, đường hẹp, đường dốc, tại nơi đường bộ giao nhau cùng mức với đường sắt có được quay đầu xe hay không?",
                    LuaChonA = "Được phép.",
                    LuaChonB = "Không được phép.",
                    LuaChonC = "Tùy từng trường hợp.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Không được phép quay đầu xe ở phần đường dành cho người đi bộ qua đường.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("1760daa6-f038-4f65-8531-418edc1056f8"),
                    NoiDung = "Theo Luật phòng chống tác hại của rượu, bia đối tượng nào dưới đây bị cấm sử dụng rượu, bia khi tham gia giao thông?",
                    LuaChonA = "Người điều khiển: xe ô tô, me mô tô, xe đạp, xe gắn máy.",
                    LuaChonB = "Người ngồi phía sau người điều khiển xe cơ giới.",
                    LuaChonC = "Người đi bộ.",
                    LuaChonD = "Cả ý 1 và ý 2.",
                    DapAnDung = 'A',
                    GiaiThich = "Người điều khiển bị cấm sử dụng rượu, bia khi tham gia giao thông.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("222fc5e2-7ec5-446f-983d-43f589a82a5e"),
                    NoiDung = "Theo Luật phòng chống tác hại của rượu, bia đối tượng nào dưới đây bị cấm sử dụng rượu, bia khi tham gia giao thông?",
                    LuaChonA = "Người điều khiển: xe ô tô, me mô tô, xe đạp, xe gắn máy.",
                    LuaChonB = "Người ngồi phía sau người điều khiển xe cơ giới.",
                    LuaChonC = "Người đi bộ.",
                    LuaChonD = "Cả ý 1 và ý 2.",
                    DapAnDung = 'A',
                    GiaiThich = "Người điều khiển bị cấm sử dụng rượu, bia khi tham gia giao thông.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("78627311-083d-42f9-a921-168e88e5f187"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("1395eeb5-dcaf-47fa-b4b9-46197875e73f"),
                    NoiDung = "Phần của đường bộ được sử dụng cho các phương tiện giao thông qua lại là gì?",
                    LuaChonA = "Phần mặt đường và lề đường.",
                    LuaChonB = "Phần đường xe chạy.",
                    LuaChonC = "Phần đường xe cơ giới.",
                    LuaChonD = null,
                    DapAnDung = 'B',
                    GiaiThich = "Phần đường xe chạy là phần của đường bộ được sử dụng cho phương tiện giao thông qua lại.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = "image",
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("78627311-083d-42f9-a921-168e88e5f187"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("2e88303a-6350-4a59-bb0f-4829665fea92"),
                    NoiDung = "Khi xe đã kéo 1 xe hoặc xe đã kéo 1 rơ moóc, bạn có được phép kéo thêm xe (kể cả xe thô sơ) hoặc rơ moóc thứ hai hay không?",
                    LuaChonA = "Chỉ được thực hiện trên đường quốc lộ có hai làn xe một chiều.",
                    LuaChonB = "Chỉ được thực hiện trên đường cao tốc.",
                    LuaChonC = "Không được thực hiện vào ban ngày.",
                    LuaChonD = "Không được phép.",
                    DapAnDung = 'D',
                    GiaiThich = "Xe kéo đã kéo rơ moóc không được kéo thêm xe.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("dd25ba7a-3692-4aae-8e60-486edfbf96f0"),
                    NoiDung = "Người điều khiển xe mô tô hai bánh, ba bánh, xe gắn máy có được phép sử dụng xe để kéo hoặc đẩy các phương tiện khác khi tham gia giao thông không?",
                    LuaChonA = "Được phép.",
                    LuaChonB = "Nếu phương tiện được kéo, đẩy có khối lượng nhỏ hơn phương tiện của mình",
                    LuaChonC = "Tùy trường hợp.Tùy trường hợp.",
                    LuaChonD = "Không được phép.",
                    DapAnDung = 'D',
                    GiaiThich = "Xe mô tô không được kéo xe khác.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("8be205cc-3339-4f8c-a331-491f6bb6a4de"),
                    NoiDung = "Khi vào số để khởi hành xe ô tô có số tự động...",
                    LuaChonA = "Đạp bàn đạp phanh chân hết hành trình...",
                    LuaChonB = "Đạp bàn đạp để tăng ga với mức độ phù hợp...",
                    LuaChonC = "",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Khởi hành xe ô tô số tự động cần đạp phanh chân hết hành trình.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("cdcfb0da-a22d-4891-bac4-1fea55614508"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("160cfe60-432a-48d8-b501-4d288bfe2d5a"),
                    NoiDung = "nội dung 2 sua",
                    LuaChonA = "1",
                    LuaChonB = "1",
                    LuaChonC = "1",
                    LuaChonD = null,
                    DapAnDung = 'A',
                    GiaiThich = "ádfasdf súadf",
                    DiemLiet = true,
                    MediaUrl = "/images/838377c1-3ae9-4b08-89bb-7d6b1d104d67.png",
                    LoaiMedia = "image",
                    MeoGhiNho = "gfdsgfsd",
                    isDeleted = false,
                    ChuDeId = Guid.Parse("a1720db9-1416-4e62-a922-e34364f67418"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("71482e7a-3b46-415c-b38b-50ac587d8b18"),
                    NoiDung = "Người lái xe có văn hóa khi tham gia giao thông phải đáp ứng các điều kiện nào dưới đây?",
                    LuaChonA = "Có trách nhiệm với bản thân và với cộng đồng; tôn trọng, nhường nhịn người khác.",
                    LuaChonB = "Tận tình giúp đỡ người tham gia giao thông gặp hoạn nạn; giúp đỡ người khuyết tật, trẻ em và người cao tuổi.",
                    LuaChonC = "Cả ý 1 và ý 2.",
                    LuaChonD = null,
                    DapAnDung = 'C',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("7ea708f2-b8b1-4787-a953-58d5e0cf27e0"),
                    NoiDung = "Sử dụng rượu, bia khi lái xe, nếu bị phát hiện thì bị xử lý như thế nào?",
                    LuaChonA = "Chỉ bị nhắc nhở.",
                    LuaChonB = "Bị xử phạt hành chính hoặc có thể bị xử lý hình sự tùy theo mức độ vi phạm.",
                    LuaChonC = "Không bị xử lý hình sự.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Sử dụng rượu, bia khi lái xe bị phạt hành chính hoặc xử lý hình sự.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("78627311-083d-42f9-a921-168e88e5f187"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("fd2bcfe7-baf1-41c2-ae87-5a353bb6de65"),
                    NoiDung = "Khi quay đầu xe, người lái xe cần phải quan sát và thực hiện các thao tác nào để đảm bảo an toàn giao thông?",
                    LuaChonA = "Quan sát biển báo hiệu để biết nơi được phép quay đầu...",
                    LuaChonB = "Quan sát biển báo hiệu để biết nơi được phép quay đầu...",
                    LuaChonC = "",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Thực hiện quay đầu xe với tốc độ thấp.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("cdcfb0da-a22d-4891-bac4-1fea55614508"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("ce50b6f1-6abc-481b-8a26-5fc8946efba0"),
                    NoiDung = "Lái xe kinh doanh vận tải khách phải có trách nhiệm gì sau đây?",
                    LuaChonA = "Kiểm tra các điều kiện bảo đảm an toàn của xe trước khi khởi hành; kiểm tra việc sắp xếp, chằng buộc hành lý, hàng hóa bảo đảm an toàn.",
                    LuaChonB = "Đóng cửa lên xuống của xe trước và trong khi xe chạy.",
                    LuaChonC = "Đón trả khách tại vị trí do khách hàng yêu cầu.",
                    LuaChonD = "Cả ý 1 và ý 2.",
                    DapAnDung = 'D',
                    GiaiThich = "Cả ý 1 và ý 2 đều đúng. Bởi vì không thể tự do trả khách theo yêu cầu được.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("48e73c98-017f-4788-aa43-620816a54274"),
                    NoiDung = "Người lái xe ô tô chở người trên 30 chỗ ngồi (hạng E), lái xe hạng D kéo rơ moóc (FD) phải đủ bao nhiêu tuổi trở lên?",
                    LuaChonA = "23 tuổi.",
                    LuaChonB = "24 tuổi.",
                    LuaChonC = "27 tuổi.",
                    LuaChonD = "30 tuổi.",
                    DapAnDung = 'C',
                    GiaiThich = "Độ tuổi lấy bằng theo hạng (cách nhau 3 tuổi): 16: Xe dưới 50cm3; 18: Hạng A, B1, B2; 21: Hạng C, FB; 24: Hạng D, FC; 27: Hạng E, FD.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("d8e4bfbe-0914-49ac-aa8b-6321d20f1aca"),
                    NoiDung = "Hành vi giao xe cơ giới, xe máy chuyên dùng cho người không đủ điều kiện để điều khiển xe tham gia giao thông có được phép hay không?",
                    LuaChonA = "Chỉ được thực hiện nếu đã hướng dẫn đầy đủ.",
                    LuaChonB = "Không được phép.",
                    LuaChonC = "Được phép tùy từng trường hợp.",
                    LuaChonD = "Chỉ được phép thực hiện với thành viên trong gia đình.",
                    DapAnDung = 'B',
                    GiaiThich = "Không được phép giao xe cho người không đủ điều kiện tham gia giao thông.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("fecde70e-505e-4dd4-bd1f-6374bbcd0760"),
                    NoiDung = "Người lái xe có văn hóa khi tham gia giao thông phải đáp ứng các điều kiện nào dưới đây?",
                    LuaChonA = "Có trách nhiệm với bản thân và với cộng đồng...",
                    LuaChonB = "Tận tình giúp đỡ người tham gia giao thông gặp hoạn nạn...",
                    LuaChonC = "Cả ý 1 và ý 2.",
                    LuaChonD = "",
                    DapAnDung = 'C',
                    GiaiThich = "",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("6cc0e7d0-9cd0-4a2e-bac3-6a9e49996449"),
                    NoiDung = "Trong hoạt động vận tải khách, những hành vi nào dưới đây bị nghiêm cấm?",
                    LuaChonA = "Cạnh tranh nhau nhằm tăng lợi nhuận.",
                    LuaChonB = "Giảm giá để thu hút khách.",
                    LuaChonC = "Đe dọa, xúc phạm, tranh giành, lôi kéo hành khách; bắt ép hành khách sử dụng dịch vụ ngoài ý muốn; xuống khách nhằm trốn tránh phát hiện xe chở quá số người quy định.",
                    LuaChonD = "Tất cả các ý trên.",
                    DapAnDung = 'C',
                    GiaiThich = "Nghiêm cấm đe dọa, xúc phạm, tranh giành, lôi kéo hành khách.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("9c7a8aa0-8976-47ce-854d-6ade9c767913"),
                    NoiDung = "Khi vào số để khởi hành xe ô tô có số tự động, người lái xe phải thực hiện các thao tác nào để đảm bảo an toàn?",
                    LuaChonA = "Đạp bàn đạp phanh chân hết hành trình, vào số và nhả phanh tay, kiểm tra lại xem có bị nhầm số không rồi mới cho xe lăn bánh.",
                    LuaChonB = "Đạp bàn đạp để tăng ga với mức độ phù hợp, vào số và kiểm tra lại xem có bị nhầm số không rồi mới cho xe lăn bánh.",
                    LuaChonC = null,
                    LuaChonD = null,
                    DapAnDung = 'A',
                    GiaiThich = "Khởi hành xe ô tô số tự động cần đạp phanh chân hết hành trình.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("cdcfb0da-a22d-4891-bac4-1fea55614508"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("c39d5ae0-6dc4-4457-89f4-6c76340d7291"),
                    NoiDung = "Trong các hành vi dưới đây, người lái xe mô tô có văn hóa giao thông phải ứng xử như thế nào?",
                    LuaChonA = "Điều khiển xe đi trên phần đường, làn đường có ít phương tiện...",
                    LuaChonB = "Chấp hành quy định về tốc độ, đèn tín hiệu...",
                    LuaChonC = "Cả ý 1 và ý 2.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("3693b3fb-0656-4358-aa61-6f1852544cb2"),
                    NoiDung = "Trên làn đường dành cho ô tô có vũng nước lớn...",
                    LuaChonA = "Cho xe chạy thật nhanh qua vũng nước.",
                    LuaChonB = "Giảm tốc độ cho xe chạy chậm qua vũng nước.Giảm tốc độ cho xe chạy chậm qua vũng nước.",
                    LuaChonC = "Giảm tốc độ cho xe chạy qua làn đường dành cho mô tô để tránh vũng nước.",
                    LuaChonD = "",
                    DapAnDung = 'C',
                    GiaiThich = "",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("ee39efa3-5791-40d3-a0e4-71c56ad67046"),
                    NoiDung = "Hành vi giao xe cơ giới, xe máy chuyên dùng cho người không đủ điều kiện để điều khiển xe tham gia giao thông có được phép hay không?",
                    LuaChonA = "Chỉ được thực hiện nếu đã hướng dẫn đầy đủ.",
                    LuaChonB = "Không được phép.",
                    LuaChonC = "Được phép tùy từng trường hợp.",
                    LuaChonD = "Chỉ được phép thực hiện với thành viên trong gia đình.",
                    DapAnDung = 'B',
                    GiaiThich = "Không được phép giao xe cho người không đủ điều kiện tham gia giao thông.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("79b7fb83-88ca-4e71-a0ff-741cb374ff85"),
                    NoiDung = "Người điều khiển phương tiện giao thông trên đường phố có được dừng xe, đỗ xe trên miệng cống thoát nước, miệng hầm của đường điện thoại, điện cao thế, chỗ dành riêng cho xe chữa cháy lấy nước hay không?",
                    LuaChonA = "Được dừng xe, đỗ xe trong trường hợp cần thiết.",
                    LuaChonB = "Không được dừng xe, đỗ xe.",
                    LuaChonC = "Được dừng xe, không được đỗ xe.",
                    LuaChonD = null,
                    DapAnDung = 'B',
                    GiaiThich = "Không được dừng, đỗ xe trên miệng cống thoát nước.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("fc2e0758-e3e8-4bcc-8388-74a81ff4c06f"),
                    NoiDung = "nội dung 1",
                    LuaChonA = "1",
                    LuaChonB = "1",
                    LuaChonC = "1",
                    LuaChonD = null,
                    DapAnDung = 'A',
                    GiaiThich = "nội dung 1",
                    DiemLiet = true,
                    MediaUrl = "/images/d3fe7cfb-08ad-4626-9736-c53189250a7b.png",
                    LoaiMedia = "image",
                    MeoGhiNho = "nội dung 1",
                    isDeleted = true,
                    ChuDeId = Guid.Parse("78627311-083d-42f9-a921-168e88e5f187"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("e1d2178f-44fe-4b9a-8766-757e6f7f38ce"),
                    NoiDung = "Hành vi điều khiển xe cơ giới chạy quá tốc độ quy định, giành đường, vượt ẩu có bị nghiêm cấm hay không?",
                    LuaChonA = "Bị nghiêm cấm tùy từng trường hợp.",
                    LuaChonB = "Không bị nghiêm cấm.",
                    LuaChonC = "Bị nghiêm cấm.",
                    LuaChonD = null,
                    DapAnDung = 'C',
                    GiaiThich = null,
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("7cb30d72-1d84-485b-b255-7bcf3b8f6cde"),
                    NoiDung = "Yêu cầu của kính chắn gió, chọn \"Loại kính an toàn\".",
                    LuaChonA = "Biển 1.",
                    LuaChonB = "Biển 2.",
                    LuaChonC = "Biển 1 và 3.",
                    LuaChonD = "Cả ba biển.",
                    DapAnDung = 'A',
                    GiaiThich = "",
                    DiemLiet = false,
                    MediaUrl = "/images/20250515231400_4fe4816e_h2.jpg",
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("a1720db9-1416-4e62-a922-e34364f67418"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("0d63c479-795a-417f-8c79-7fcbd15f9ae9"),
                    NoiDung = "Việc sản xuất, mua bán, sử dụng biển số xe cơ giới, xe máy chuyên dùng được quy định như thế nào trong Luật Giao thông đường bộ?",
                    LuaChonA = "Được phép sản xuất, sử dụng khi bị mất biển số.",
                    LuaChonB = "Được phép mua bán, sử dụng khi bị mất biển số.",
                    LuaChonC = "Nghiêm cấm sản xuất, mua bán, sử dụng trái phép.",
                    LuaChonD = "",
                    DapAnDung = 'C',
                    GiaiThich = "",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("de1a4aa2-7f7a-4ce4-9abf-84979173ef35"),
                    NoiDung = "Biển nào cấm máy kéo?",
                    LuaChonA = "Biển 1.",
                    LuaChonB = "Biển 2 và 3.",
                    LuaChonC = "Biển 1 và 3.",
                    LuaChonD = "Cả ba biển.",
                    DapAnDung = 'B',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = "/images/h3.jpg",
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("a5674e1d-af51-4a56-bdd8-758210677c1a"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("e1abc8bf-9e95-4ee8-8116-84e41132aa6e"),
                    NoiDung = "Trên đường có nhiều làn đường, khi điều khiển phương tiện ở tốc độ chậm bạn phải đi ở làn đường nào?",
                    LuaChonA = "Đi ở làn bên phải trong cùng.",
                    LuaChonB = "Đi ở làn phía bên trái.",
                    LuaChonC = "Đi ở làn giữa.",
                    LuaChonD = "Đi ở bất cứ làn nào nhưng phải bấm đèn cảnh báo nguy hiểm để báo hiệu cho các phương tiện khác.",
                    DapAnDung = 'A',
                    GiaiThich = "Tốc độ chậm đi ở làn bên phải trong cùng",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("df6b5953-a946-45f2-91ee-85bcfdc7b93f"),
                    NoiDung = "Trên đường có nhiều làn đường, khi điều khiển phương tiện ở tốc độ chậm bạn phải đi ở làn đường nào?",
                    LuaChonA = "Đi ở làn bên phải trong cùng.",
                    LuaChonB = "Đi ở làn phía bên trái.",
                    LuaChonC = "Đi ở làn giữa.",
                    LuaChonD = "Đi ở bất cứ làn nào nhưng phải bấm đèn cảnh báo nguy hiểm để báo hiệu cho các phương tiện khác.",
                    DapAnDung = 'A',
                    GiaiThich = "Tốc độ chậm đi ở làn bên phải trong cùng",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("4b2328e6-2256-4d66-bcb4-878a4ba6c433"),
                    NoiDung = "Xe ô tô tham gia giao thông trên đường bộ phải có đủ các loại đèn gì dưới đây?",
                    LuaChonA = "Đèn chiếu sáng gần và xa.",
                    LuaChonB = "Đèn soi biển số, đèn báo hãm và đèn tín hiệu.",
                    LuaChonC = "Dàn đèn pha trên nóc xe.",
                    LuaChonD = "Cả ý 1 và ý 2.",
                    DapAnDung = 'D',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("1365e7dd-6325-4662-ba5a-fe0312d5519a"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("aba11629-bd97-4eb8-bc28-8846ccd51a4f"),
                    NoiDung = "Hành vi điều khiển xe cơ giới chạy quá tốc độ quy định, giành đường, vượt ẩu có bị nghiêm cấm hay không?",
                    LuaChonA = "Bị nghiêm cấm tùy từng trường hợp.",
                    LuaChonB = "Không bị nghiêm cấm.",
                    LuaChonC = "Bị nghiêm cấm.",
                    LuaChonD = "",
                    DapAnDung = 'C',
                    GiaiThich = "",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("f8468803-69d9-4e31-b979-8b203bc267e1"),
                    NoiDung = "Người đủ bao nhiêu tuổi trở lên thì được điều khiển xe mô tô hai bánh, xe mô tô ba bánh có dung tích xi lanh từ 50 cm3 trở lên và các loại xe có kết cấu tương tự; xe ô tô tải, máy kéo có trọng tải dưới 3.500 kg; xe ô tô chở người đến 9 chỗ ngồi?",
                    LuaChonA = "16 tuổi.",
                    LuaChonB = "18 tuổi.",
                    LuaChonC = "17 tuổi.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Độ tuổi lấy bằng theo hạng (cách nhau 3 tuổi): 16: Xe dưới 50cm3; 18: Hạng A, B1, B2; 21: Hạng C, FB; 24: Hạng D, FC; 27: Hạng E, FD.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("2720fc79-6586-496b-b8c4-8f59e913a926"),
                    NoiDung = "Khi điều khiển xe mô tô hai bánh, xe mô tô ba bánh, xe gắn máy, những hành vi buông cả hai tay; sử dụng xe để kéo, đẩy xe khác, vật khác; sử dụng chân chống của xe quệt xuống đường khi xe đang chạy có được phép hay không?",
                    LuaChonA = "Được phép.",
                    LuaChonB = "Tùy trường hợp.",
                    LuaChonC = "Không được phép.",
                    LuaChonD = "",
                    DapAnDung = 'C',
                    GiaiThich = "",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("5291883a-5d70-4d44-be76-8fc7e5196da1"),
                    NoiDung = "Thứ tự các xe đi như thế nào là đúng quy tắc giao thông?",
                    LuaChonA = "Xe tải, xe khách, xe con, mô tô.",
                    LuaChonB = "Xe khách, xe tải, xe con, mô tô.",
                    LuaChonC = "Mô tô, xe khách, xe tải, xe con.",
                    LuaChonD = "Mô tô, xe khách, xe tải, xe con.",
                    DapAnDung = 'B',
                    GiaiThich = "Thứ tự ưu tiên: Xe ưu tiên - Đường ưu tiên - Đường cùng cấp theo thứ tự bên phải trống - rẽ phải - đi thẳng - rẽ trái. 1. Xe tải: Đường ưu tiên và đi thẳng; 2. Mô tô: Đường ưu tiên và rẽ trái; 3. Xe khách: Đường không ưu tiên, đi thẳng. 4. Xe con: Đường không ưu tiên, rẽ trái.",
                    DiemLiet = false,
                    MediaUrl = "/images/h6.webp",
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("a1720db9-1416-4e62-a922-e34364f67418"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("8196069a-93ea-4aa5-bf8b-90799f10b999"),
                    NoiDung = "Theo hướng mũi tên, thứ tự các xe đi như thế nào là đúng quy tắc giao thông?",
                    LuaChonA = "Xe tải, xe công an, xe khách, xe con.",
                    LuaChonB = "Xe công an, xe khách, xe con, xe tải.",
                    LuaChonC = "Xe công an, xe con, xe tải, xe khách.",
                    LuaChonD = "Xe công an, xe tải, xe khách, xe con",
                    DapAnDung = 'D',
                    GiaiThich = "Thứ tự ưu tiên: Xe ưu tiên - Đường ưu tiên - Đường cùng cấp theo thứ tự bên phải trống - rẽ phải - đi thẳng - rẽ trái. 1. Xe công an: Xe ưu tiên; 2. Xe tải: Đường ưu tiên; 3. Xe khách: Đường không ưu tiên, bên phải trống; 4. Xe con: Đường không ưu tiên, bên phải vướng xe khách nên phải nhường.",
                    DiemLiet = false,
                    MediaUrl = "/images/h7.webp",
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("a1720db9-1416-4e62-a922-e34364f67418"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("76763bcd-5e4c-4bcf-9e89-916b48d7e991"),
                    NoiDung = "Người lái xe cố tình không phân biệt làn đường...",
                    LuaChonA = "Là bình thường.",
                    LuaChonB = "Là thiếu văn hóa giao thông.",
                    LuaChonC = "Là có văn hóa giao thông.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("2240d397-9322-4194-ac69-9174f4a6dfa6"),
                    NoiDung = "Khái niệm \"Khổ giới hạn của đường bộ\" được hiểu như thế nào là đúng?",
                    LuaChonA = "Là khoảng trống có kích thước giới hạn về chiều cao, chiều rộng của đường, cầu, bến phà, hầm đường bộ để các xe kể cả hàng hóa xếp trên xe đi qua được an toàn.",
                    LuaChonB = "Là khoảng trống có kích thước giới hạn về chiều rộng của đường, cầu, bến phà, hầm trên đường bộ để các xe kể cả hàng hóa xếp trên xe đi qua được an toàn.",
                    LuaChonC = null,
                    LuaChonD = null,
                    DapAnDung = 'A',
                    GiaiThich = "Khổ giới hạn đường bộ có giới hạn về chiều cao, chiều rộng.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("78627311-083d-42f9-a921-168e88e5f187"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("cd008ad4-3948-4a23-8341-94ac6e3b549f"),
                    NoiDung = "Khi lái xe trong khu đô thị và đông dân cư trừ các khu vực có biển cấm sử dụng còi, người lái xe được sử dụng còi như thế nào trong các trường hợp dưới đây?",
                    LuaChonA = "Từ 22 giờ đêm đến 5 giờ sáng.",
                    LuaChonB = "Từ 5 giờ sáng đến 22 giờ tối.",
                    LuaChonC = "Từ 23 giờ đêm đến 5 giờ sáng hôm sau.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Chỉ sử dụng còi từ 5 giờ sáng đến 22 giờ tối.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("a7082f8b-b688-4513-89da-95166775c8c8"),
                    NoiDung = "Biển nào cấm ô tô tải?",
                    LuaChonA = "Cả ba biển.",
                    LuaChonB = "Biển 2 và 3.",
                    LuaChonC = "Biển 1 và 3.",
                    LuaChonD = "Biển 1 và 2.",
                    DapAnDung = 'D',
                    GiaiThich = "",
                    DiemLiet = false,
                    MediaUrl = "/images/h2.jpg",
                    LoaiMedia = "Image",
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("a1720db9-1416-4e62-a922-e34364f67418"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("b795ed49-e1fb-4e2f-b1ce-994b257f0733"),
                    NoiDung = "Người điều khiển xe mô tô, ô tô, máy kéo trên đường mà trong máu hoặc hơi thở có nồng độ cồn có bị nghiêm cấm không?",
                    LuaChonA = "Bị nghiêm cấm.",
                    LuaChonB = "Không bị nghiêm cấm",
                    LuaChonC = "Không bị nghiêm cấm, nếu nồng độ cồn trong máu ở mức nhẹ, có thể điều khiển phương tiện tham gia giao thông.",
                    LuaChonD = null,
                    DapAnDung = 'A',
                    GiaiThich = "Trong máu hoặc hơi thở có nồng độ cồn bị nghiêm cấm.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("e1a5b85f-3b8d-4213-aad5-9f3ddca19f29"),
                    NoiDung = "Việc lái xe mô tô, ô tô, máy kéo ngay sau khi uống rượu, bia có được phép hay không?",
                    LuaChonA = "Không được phép",
                    LuaChonB = "Chỉ được lái ở tốc độ chậm và quãng đường ngắn.",
                    LuaChonC = "Chỉ được lái nếu trong cơ thể có nồng độ cồn thấp.",
                    LuaChonD = null,
                    DapAnDung = 'A',
                    GiaiThich = "Uống rượu bia không được lái xe,",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("18b0cc7f-aa5e-497c-9a7d-a158ca561315"),
                    NoiDung = "Khái niệm về văn hóa giao thông được hiểu như thế nào là đúng?",
                    LuaChonA = "Là sự hiểu biết và chấp hành nghiêm chỉnh pháp luật về giao thông; là ý thức trách nhiệm với cộng đồng khi tham gia giao thông.",
                    LuaChonB = "Là ứng xử có văn hóa, có tình yêu thương con người trong các tình huống không may xảy ra khi tham gia giao thông",
                    LuaChonC = "Cả ý 1 và ý 2.",
                    LuaChonD = null,
                    DapAnDung = 'C',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("7fe4fdbf-084a-4fbc-82af-a1a18b2606d5"),
                    NoiDung = "Thời gian làm việc của người lái xe ô tô không được lái xe liên tục quá bao nhiêu giờ trong trường hợp nào dưới đây?",
                    LuaChonA = "Không quá 4 giờ.",
                    LuaChonB = "Không quá 6 giờ.",
                    LuaChonC = "Không quá 8 giờ.",
                    LuaChonD = "Liên tục tùy thuộc vào sức khỏe và khả năng của người lái xe.",
                    DapAnDung = 'A',
                    GiaiThich = "Không lái xe liên tục quá 4 giờ.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("874c0ec3-a8b1-45e7-9001-a3f15bf55500"),
                    NoiDung = "\"Làn đường\" là gì?",
                    LuaChonA = "Là một phần của phần đường xe chạy được chia theo chiều dọc của đường, sử dụng cho xe chạy.",
                    LuaChonB = "Là một phần của phần đường xe chạy được chia theo chiều dọc của đường, có bề rộng đủ cho xe chạy an toàn.",
                    LuaChonC = "Là đường cho xe ô tô chạy, dừng, đỗ an toàn.",
                    LuaChonD = null,
                    DapAnDung = 'B',
                    GiaiThich = "Làn đường có bề rộng đủ cho xe chạy an toàn.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = "image",
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("78627311-083d-42f9-a921-168e88e5f187"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("355f1057-5b9c-4ef7-9483-a696f784b122"),
                    NoiDung = "Người lái xe ô tô chở người trên 30 chỗ ngồi (hạng E), lái xe hạng D kéo rơ moóc (FD) phải đủ bao nhiêu tuổi trở lên?",
                    LuaChonA = "23 tuổi.",
                    LuaChonB = "24 tuổi.",
                    LuaChonC = "27 tuổi.",
                    LuaChonD = "30 tuổi.",
                    DapAnDung = 'C',
                    GiaiThich = "Độ tuổi lấy bằng theo hạng (cách nhau 3 tuổi): 16: Xe dưới 50cm3; 18: Hạng A, B1, B2; 21: Hạng C, FB; 24: Hạng D, FC; 27: Hạng E, FD.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("ac022356-dfff-478a-8920-a8246b340851"),
                    NoiDung = "Kính chắn gió của xe ô tô phải đảm bảo yêu cầu nào dưới đây?",
                    LuaChonA = "Là loại kính an toàn, kính nhiều lớp, đúng quy cách...",
                    LuaChonB = "Là loại kính trong suốt, không rạn nứt...",
                    LuaChonC = "",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Yêu cầu của kính chắn gió, chọn \"Loại kính an toàn\".",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("1365e7dd-6325-4662-ba5a-fe0312d5519a"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("e33caaa1-67bb-48fc-8247-ac19d87a7057"),
                    NoiDung = "Khi tránh nhau trên đường hẹp, người lái xe cần phải chú ý những điểm nào để đảm bảo an toàn giao thông?",
                    LuaChonA = "Không nên đi cố vào đường hẹp...",
                    LuaChonB = "Trong khi tránh nhau không nên đổi số...",
                    LuaChonC = "Khi tránh nhau ban đêm, phải thường xuyên bật đèn pha tắt đèn cốt.",
                    LuaChonD = "Cả ý 1 và ý 2.",
                    DapAnDung = 'D',
                    GiaiThich = "Cả ý 1 và ý 2 đều đúng. Ý 3 tránh nhau ban đêm bật đèn pha là sai.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("cdcfb0da-a22d-4891-bac4-1fea55614508"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("a4712242-f7a6-4c4c-a680-afa6af560e21"),
                    NoiDung = "Hành vi vượt xe tại các vị trí có tầm nhìn hạn chế, đường vòng, đầu dốc có bị nghiêm cấm hay không?",
                    LuaChonA = "Không bị nghiêm cấm.",
                    LuaChonB = "Không bị nghiêm cấm khi rất vội.",
                    LuaChonC = "Bị nghiêm cấm.",
                    LuaChonD = "Không bị nghiêm cấm khi khẩn cấp.",
                    DapAnDung = 'C',
                    GiaiThich = null,
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("35a9212a-d786-46c8-b6e4-b1b1b7b96c57"),
                    NoiDung = "Chủ phương tiện cơ giới đường bộ có được tự ý thay đổi màu sơn, nhãn hiệu hoặc các đặc tính kỹ thuật của phương tiện so với chứng nhận đăng ký xe hay không?",
                    LuaChonA = "Được phép thay đổi bằng cách dán đề can với màu sắc phù hợp.",
                    LuaChonB = "Không được phép thay đổi.",
                    LuaChonC = "Tùy từng loại phương tiện cơ giới đường bộ.",
                    LuaChonD = null,
                    DapAnDung = 'B',
                    GiaiThich = "Không được phép thay đổi so với giấy chứng nhận đăng ký xe.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("1365e7dd-6325-4662-ba5a-fe0312d5519a"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("cebbff7c-ee35-4241-9f2b-b5f7bc948c83"),
                    NoiDung = "Biển nào cấm máy kéo?",
                    LuaChonA = "Biển 1.",
                    LuaChonB = "Biển 2 và 3.",
                    LuaChonC = "Biển 1 và 3.",
                    LuaChonD = "Cả ba biển.",
                    DapAnDung = 'B',
                    GiaiThich = "",
                    DiemLiet = false,
                    MediaUrl = "/images/h3.jpg",
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("a1720db9-1416-4e62-a922-e34364f67418"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("f7007013-7bb6-4c61-b1e0-b7bf8daa9b8e"),
                    NoiDung = "Hành vi điều khiển xe cơ giới chạy quá tốc độ quy định, giành đường, vượt ẩu có bị nghiêm cấm hay không?",
                    LuaChonA = "Bị nghiêm cấm tùy từng trường hợp.",
                    LuaChonB = "Không bị nghiêm cấm.",
                    LuaChonC = "Bị nghiêm cấm.",
                    LuaChonD = null,
                    DapAnDung = 'C',
                    GiaiThich = null,
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("5fab968e-1c99-47f8-954e-b7d8750594ea"),
                    NoiDung = "Biển nào cấm ô tô tải?",
                    LuaChonA = "Cả ba biển.",
                    LuaChonB = "Biển 2 và 3.",
                    LuaChonC = "Biển 1 và 3.",
                    LuaChonD = "Biển 1 và 2.",
                    DapAnDung = 'D',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = "/images/h2.jpg",
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("a5674e1d-af51-4a56-bdd8-758210677c1a"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("df574d98-027c-4559-952b-b9916e7b47f5"),
                    NoiDung = "Thứ tự các xe đi như thế nào là đúng quy tắc giao thông?",
                    LuaChonA = "Xe tải, xe khách, xe con, mô tô.",
                    LuaChonB = "Xe khách, xe tải, xe con, mô tô.",
                    LuaChonC = "Mô tô, xe khách, xe tải, xe con.",
                    LuaChonD = "Mô tô, xe khách, xe tải, xe con.",
                    DapAnDung = 'B',
                    GiaiThich = "Thứ tự ưu tiên: Xe ưu tiên - Đường ưu tiên - Đường cùng cấp theo thứ tự bên phải trống - rẽ phải - đi thẳng - rẽ trái. 1. Xe tải: Đường ưu tiên và đi thẳng; 2. Mô tô: Đường ưu tiên và rẽ trái; 3. Xe khách: Đường không ưu tiên, đi thẳng. 4. Xe con: Đường không ưu tiên, rẽ trái.",
                    DiemLiet = false,
                    MediaUrl = "/images/h6.webp",
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("a1720db9-1416-4e62-a922-e34364f67418"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("3a8c9643-02b5-4536-9abe-bcaecba531e1"),
                    NoiDung = "Thời gian làm việc trong một ngày của người lái xe ô tô không được vượt quá bao nhiêu giờ trong trường hợp dưới đây?",
                    LuaChonA = "Không quá 8 giờ.",
                    LuaChonB = "Không quá 10 giờ.",
                    LuaChonC = "Không quá 12 giờ.",
                    LuaChonD = "Không hạn chế tùy thuộc vào sức khỏe và khả năng của người lái xe.",
                    DapAnDung = 'B',
                    GiaiThich = "Không làm việc 1 ngày của lái xe quá 10 giờ.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("9dcc5c60-d114-4389-aab5-be90f76c40fd"),
                    NoiDung = "Hành vi vượt xe tại các vị trí có tầm nhìn hạn chế, đường vòng, đầu dốc có bị nghiêm cấm hay không?",
                    LuaChonA = "Không bị nghiêm cấm.",
                    LuaChonB = "Không bị nghiêm cấm khi rất vội.",
                    LuaChonC = "Bị nghiêm cấm.",
                    LuaChonD = "Không bị nghiêm cấm khi khẩn cấp.",
                    DapAnDung = 'C',
                    GiaiThich = null,
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("eab73797-6fe7-4401-a2e2-c25e627534d7"),
                    NoiDung = "Trên đường có nhiều làn đường, khi điều khiển phương tiện ở tốc độ chậm bạn phải đi ở làn đường nào?",
                    LuaChonA = "Đi ở làn bên phải trong cùng.",
                    LuaChonB = "Đi ở làn phía bên trái.",
                    LuaChonC = "Đi ở làn giữa.",
                    LuaChonD = "Đi ở bất cứ làn nào nhưng phải bấm đèn cảnh báo nguy hiểm để báo hiệu cho các phương tiện khác.",
                    DapAnDung = 'A',
                    GiaiThich = "Tốc độ chậm đi ở làn bên phải trong cùng",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("215ea988-8d2d-48f5-bd03-c63e965a7be3"),
                    NoiDung = "Người lái xe không được quay đầu xe trong các trường hợp nào dưới đây?",
                    LuaChonA = "Ở phần đường dành cho người đi bộ qua đường, trên cầu, đầu cầu, đường cao tốc, đường hẹp, đường dốc, tại nơi đường bộ giao nhau cùng mức với đường sắt.",
                    LuaChonB = "Ở phía trước hoặc phía sau của phần đường dành cho người đi bộ qua đường, trên đường quốc lộ, tại nơi đường bộ giao nhau không cùng mức với đường sắt.",
                    LuaChonC = "Cả ý 1 và ý 2.",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Không được phép quay đầu xe ở phần đường dành cho người đi bộ qua đường.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("537e9dd8-6ad4-4c73-93f5-c651354b7d14"),
                    NoiDung = "Yêu cầu của kính chắn gió, chọn \"Loại kính an toàn\".",
                    LuaChonA = "Biển 1.",
                    LuaChonB = "Biển 2.",
                    LuaChonC = "Biển 1 và 3.",
                    LuaChonD = "Cả ba biển.",
                    DapAnDung = 'A',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = "/images/h1.webp",
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("a5674e1d-af51-4a56-bdd8-758210677c1a"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("ba1ea3e8-6ea1-40cf-9ca0-c79a5cc32dcf"),
                    NoiDung = "Người điều khiển phương tiện giao thông đường bộ mà trong cơ thể có chất ma túy có bị nghiêm cấm hay không?",
                    LuaChonA = "Bị nghiêm cấm.",
                    LuaChonB = "Không bị nghiêm cấm.",
                    LuaChonC = "Không bị nghiêm cấm, nếu có chất ma túy ở mức nhẹ, có thể điều khiển phương tiện tham gia giao thông.",
                    LuaChonD = null,
                    DapAnDung = 'A',
                    GiaiThich = "Có ma túy bị nghiêm cấm",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("7ead6682-19ab-4252-8d15-ca735035b4f1"),
                    NoiDung = "Người ngồi trên xe mô tô hai bánh, xe mô tô ba bánh, xe gắn máy khi tham gia giao thông có được bám, kéo hoặc đẩy các phương tiện khác không?",
                    LuaChonA = "Được phép",
                    LuaChonB = "Được bám trong trường hợp phương tiện của mình bị hỏng.",
                    LuaChonC = "Được kéo, đẩy trong trường hợp phương tiện khác bị hỏng.",
                    LuaChonD = "Không được phép.",
                    DapAnDung = 'D',
                    GiaiThich = "Xe mô tô không được kéo xe khác.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("ff6d4a7d-b623-48c8-abe0-d0aad856cbc0"),
                    NoiDung = "Người lái xe khách phải chấp hành những quy định nào dưới đây?",
                    LuaChonA = "Đón, trả khách đúng nơi quy định, không trở hành khách trên mui, trong khoang hành lý hoặc để hành khách đu bám bên ngoài xe.",
                    LuaChonB = "Không chở hàng nguy hiểm, hàng có mùi hôi thối hoặc động vật, hàng hóa khác có ảnh hưởng đến sức khỏe của hành khách.",
                    LuaChonC = "Chở hành khách trên mui; để hàng hóa trong khoang chở khách, chở quá số người theo quy định.",
                    LuaChonD = "Cả ý 1 và ý 2.",
                    DapAnDung = 'D',
                    GiaiThich = "Cả ý 1 và ý 2 đều đúng. Bởi vì không thể chở hành khách trên mui được, nên ý 3 sai.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("14be8af4-5d8f-4c40-a51e-d323d23fbcd9"),
                    NoiDung = "Xe ô tô tham gia giao thông đường bộ phải bảo đảm các quy định về chất lượng, an toàn kỹ thuật và bảo vệ môi trường nào ghi dưới đây?",
                    LuaChonA = "Kính chắn gió, kính cửa phải là loại kính an toàn, bảo đảm tần nhìn cho người điều khiển; có đủ hệ thống hãm và hệ thống chuyển hướng có hiệu lực, tay lái xe ô tô ở bên trái của xe, có còi với âm lượng đúng quy chuẩn kỹ thuật.",
                    LuaChonB = "Có đủ đèn chiếu sáng gần và xa, đèn soi biển số, đèn báo hãm, đèn tín hiệu; có đủ bộ phận giảm thanh, giảm khói, các kết cấu phải đủ độ bền và bảo đảm tính năng vận hành ổn định.",
                    LuaChonC = "Cả ý 1 và ý 2.",
                    LuaChonD = null,
                    DapAnDung = 'C',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("1365e7dd-6325-4662-ba5a-fe0312d5519a"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("5dfc4fdf-f830-4e96-9b4c-d3601a8a4214"),
                    NoiDung = "Hành vi vận chuyển đồ vật cồng kềnh bằng xe mô tô, xe gắn máy khi tham gia giao thông có được phép hay không?",
                    LuaChonA = "Không được vận chuyển.",
                    LuaChonB = "Chỉ được vận chuyển khi đã chằng buộc cẩn thận.",
                    LuaChonC = "Chỉ được vận chuyển vật cồng kềnh trên xe máy nếu khoảng cách về nhà ngắn hơn 2 km.",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Xe mô tô không được kéo xe khác.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("b0e3fc57-e1f5-4d66-a275-d80e1521cd0d"),
                    NoiDung = "Xe mô tô và xe ô tô tham gia giao thông trên đường bộ phải bắt buộc có đủ bộ phận giảm thanh không?",
                    LuaChonA = "Không bắt buộc.",
                    LuaChonB = "Bắt buộc.",
                    LuaChonC = "Tùy từng trường hợp.",
                    LuaChonD = null,
                    DapAnDung = 'B',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("1365e7dd-6325-4662-ba5a-fe0312d5519a"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("302e8466-579c-4a1f-8b76-d84b452a7b2e"),
                    NoiDung = "Theo Luật phòng chống tác hại của rượu, bia đối tượng nào dưới đây bị cấm sử dụng rượu, bia khi tham gia giao thông?",
                    LuaChonA = "Người điều khiển: xe ô tô, me mô tô, xe đạp, xe gắn máy.",
                    LuaChonB = "Người ngồi phía sau người điều khiển xe cơ giới.",
                    LuaChonC = "Người đi bộ.",
                    LuaChonD = "Cả ý 1 và ý 2.",
                    DapAnDung = 'A',
                    GiaiThich = "Người điều khiển bị cấm sử dụng rượu, bia khi tham gia giao thông.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("fb377bf2-a274-405b-b9e0-d981823b8a3e"),
                    NoiDung = "Người điều khiển phương tiện giao thông trên đường phố có được dừng xe, đỗ xe trên miệng cống thoát nước, miệng hầm của đường điện thoại, điện cao thế, chỗ dành riêng cho xe chữa cháy lấy nước hay không?",
                    LuaChonA = "Được dừng xe, đỗ xe trong trường hợp cần thiết.",
                    LuaChonB = "Không được dừng xe, đỗ xe.",
                    LuaChonC = "Được dừng xe, không được đỗ xe.",
                    LuaChonD = null,
                    DapAnDung = 'B',
                    GiaiThich = "Không được dừng, đỗ xe trên miệng cống thoát nước.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("2892a1a3-c46d-4dd0-8c64-da93d5432c69"),
                    NoiDung = "Người lái xe cố tình không phân biệt làn đường, vạch phân làn, phóng nhanh, vượt ẩu, vượt đèn đỏ, đi vào đường cấm, đường một chiều được coi là hành vi nào trong các hành vi dưới đây?",
                    LuaChonA = "Là bình thường.",
                    LuaChonB = "Là thiếu văn hóa giao thông.",
                    LuaChonC = "Là có văn hóa giao thông.",
                    LuaChonD = null,
                    DapAnDung = 'B',
                    GiaiThich = null,
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("51923743-b8a3-42e8-9810-219fcffaa9ee"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("1c6dbaba-a773-4df9-a5e2-dcfa93ce43db"),
                    NoiDung = "Khi điều khiển xe mô tô hai bánh, xe mô tô ba bánh, xe gắn máy, những hành vi nào không được phép?",
                    LuaChonA = "Buông cả hai tay; sử dụng xe để kéo, đẩy xe khác, vật khác; sử dụng chân chống của xe để quệt xuống đường khi xe đang chạy.",
                    LuaChonB = "Buông một tay; sử dụng xe để chở người hoặc hàng hóa; để chân chạm xuống đất khi khởi hành.",
                    LuaChonC = "Đội mũ bảo hiểm; chạy xe đúng tốc độ quy định và chấp hành đúng quy tắc giao thông đường bộ",
                    LuaChonD = "Chở người ngồi sau dưới 16 tuổi.",
                    DapAnDung = 'A',
                    GiaiThich = "",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("6e08d610-d90e-4c49-81ed-dd2544e0e2d3"),
                    NoiDung = "Người lái xe khách, xe buýt cần thực hiện những nhiệm vụ gì dưới đây?",
                    LuaChonA = "1. Luôn có ý thức về tính tổ chức, kỷ luật, thực hiện nghiêm biểu đồ xe chạy được phân công; thực hiện đúng hành trình, lịch trình, đón trả khách đúng nơi quy định; giúp đỡ hành khách đi xe, đặc biệt là những người khuyết tật, người già, trẻ em và phụ nữ có thai, có con nhỏ.",
                    LuaChonB = "2. Luôn có ý thức về tính tổ chức, kỷ luật, thực hiện linh hoạt biểu đồ xe chạy được phân công để tiết kiệm chi phí; thực hiện đúng hành trình, lịch trình khi có khách đi xe, đón trả khách ở những nơi thuận tiện cho hành khách đi xe.",
                    LuaChonC = "",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Lái xe khách, xe buýt thực hiện nghiêm biểu đồ chạy xe được phân công.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("d1e0a0fe-edce-445d-9e10-7428b28d1fe4"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("7078b656-ed8b-4e2a-bc52-df9b6bcab446"),
                    NoiDung = "Khi lái xe trên đường, người lái xe cần quan sát và đảm bảo tốc độ phương tiện như thế nào?",
                    LuaChonA = "Chỉ lớn hơn tốc độ tối đa cho phép khi đường vắng.",
                    LuaChonB = "Chỉ lớn hơn tốc độ tối đa cho phép vào ban đêm.",
                    LuaChonC = "Không vượt quá tốc độ cho phép.",
                    LuaChonD = null,
                    DapAnDung = 'C',
                    GiaiThich = null,
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("7377f93b-481c-4ca4-9c55-dff8d700783f"),
                    NoiDung = "Bạn đang lái xe, phía trước có một xe cảnh sát giao thông không phát tín hiệu ưu tiên bạn có được phép vượt hay không?",
                    LuaChonA = "Không được vượt.",
                    LuaChonB = "Được vượt khi đang đi trên cầu.",
                    LuaChonC = "Được phép vượt khi đi qua nơi giao nhau có ít phương tiện cùng tham gia giao thông.",
                    LuaChonD = "Được vượt khi đảm bảo an toàn.",
                    DapAnDung = 'D',
                    GiaiThich = "Được vượt khi xe không phát tín hiệu ưu tiên",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("3b850e12-f5a8-408c-88d2-e22e3ddf1598"),
                    NoiDung = "Người đủ bao nhiêu tuổi trở lên thì được điều khiển xe mô tô hai bánh, xe mô tô ba bánh có dung tích xi lanh từ 50 cm3 trở lên và các loại xe có kết cấu tương tự; xe ô tô tải, máy kéo có trọng tải dưới 3.500 kg; xe ô tô chở người đến 9 chỗ ngồi?",
                    LuaChonA = "16 tuổi.",
                    LuaChonB = "18 tuổi.",
                    LuaChonC = "17 tuổi.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Độ tuổi lấy bằng theo hạng (cách nhau 3 tuổi): 16: Xe dưới 50cm3; 18: Hạng A, B1, B2; 21: Hạng C, FB; 24: Hạng D, FC; 27: Hạng E, FD.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("f74e7ee0-4b3c-4635-a279-e2642106afc5"),
                    NoiDung = "Người lái xe không được lùi xe ở những khu vực nào dưới đây?",
                    LuaChonA = "Ở khu vực cho phép đỗ xe.",
                    LuaChonB = "Ở khu vực cấm dừng và trên phần đường dành cho người đi bộ qua đường.",
                    LuaChonC = "Nơi đường bộ giao nhau, đường bộ giao nhau cùng mức với đường sắt, nơi tầm nhìn bị che khuất, trong hầm đường bộ, đường cao tốc.",
                    LuaChonD = "Cả ý 2 và ý 3.",
                    DapAnDung = 'D',
                    GiaiThich = "Cấm lùi xe ở khu vực cấm dừng và nơi đường bộ giao nhau.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("84c5978f-a44b-4f49-8b30-ed8734debdd2"),
                    NoiDung = "Khi lái xe trên đường, người lái xe cần quan sát và đảm bảo tốc độ phương tiện như thế nào?",
                    LuaChonA = "Chỉ lớn hơn tốc độ tối đa cho phép khi đường vắng.",
                    LuaChonB = "Chỉ lớn hơn tốc độ tối đa cho phép vào ban đêm.",
                    LuaChonC = "Không vượt quá tốc độ cho phép.",
                    LuaChonD = "",
                    DapAnDung = 'C',
                    GiaiThich = "",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("73dd9082-af95-49a5-9151-ef2afc730462"),
                    NoiDung = "Người lái xe không được vượt xe khác khi gặp trường hợp nào ghi ở dưới đây?",
                    LuaChonA = "Trên cầu hẹp có một làn xe. Nơi đường giao nhau, đường bộ giao nhau cùng mức với đường sắt; xe được quyền ưu tiên đang phát tín hiệu ưu tiên đi làm nhiệm vụ.",
                    LuaChonB = "Trên cầu có từ 02 làn xe trở lên; nơi đường bộ giao nhau không cùng mức với đường sắt; xe được quyền ưu tiên đang đi phía trước nhưng không phát tín hiệu ưu tiên.",
                    LuaChonC = "Trên đường có 2 làn đường được phân chia làn bằng vạch kẻ nét đứt.",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Không được vượt trên cầu hẹp có một làn xe.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("9903bb11-6828-462c-8fea-f1b0dc069cc4"),
                    NoiDung = "Hành vi lắp đặt, sử dụng còi, đèn không đúng thiết kế của nhà sản xuất đối với từng loại xe cơ giới có được phép hay không?",
                    LuaChonA = "Được phép.",
                    LuaChonB = "Không được phép.",
                    LuaChonC = "Được phép tùy từng trường hợp.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Không được phép lắp đặt còi đèn không đúng thiết kế.",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("93e30bee-85c5-43ba-b539-f3088817a5a7"),
                    NoiDung = "Kính chắn gió của xe ô tô phải đảm bảo yêu cầu nào dưới đây?",
                    LuaChonA = "Là loại kính an toàn, kính nhiều lớp, đúng quy cách, không rạn nứt, đảm bảo hình ảnh quan sát rõ ràng, không bị méo mó.",
                    LuaChonB = "Là loại kính trong suốt, không rạn nứt, đảm bảo tầm nhìn cho người điều khiển về phía trước mặt và hai bên.",
                    LuaChonC = null,
                    LuaChonD = null,
                    DapAnDung = 'A',
                    GiaiThich = "Yêu cầu của kính chắn gió, chọn \"Loại kính an toàn\".",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("1365e7dd-6325-4662-ba5a-fe0312d5519a"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("1616459f-d0ba-4f82-a028-f52f596e6cdb"),
                    NoiDung = "Bạn đang lái xe, phía trước có một xe cứu thương đang phát tín hiệu ưu tiên bạn có được phép vượt hay không?",
                    LuaChonA = "Không được vượt.",
                    LuaChonB = "Được vượt khi đang đi trên cầu.",
                    LuaChonC = "Được phép vượt khi đi qua nơi giao nhau có ít phương tiện cùng tham gia giao thông.",
                    LuaChonD = "Được vượt khi đảm bảo an toàn.",
                    DapAnDung = 'A',
                    GiaiThich = "Không được vượt khi đang phát tín hiệu ưu tiên.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("aa89c371-0ecf-4a9a-bb8a-d1b28c329a0a")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("5b5d2259-c2c1-4960-bfcd-f8ced656b95a"),
                    NoiDung = "Việc lái xe mô tô, ô tô, máy kéo ngay sau khi uống rượu, bia có được phép hay không?",
                    LuaChonA = "Không được phép",
                    LuaChonB = "Chỉ được lái ở tốc độ chậm và quãng đường ngắn.",
                    LuaChonC = "Chỉ được lái nếu trong cơ thể có nồng độ cồn thấp.",
                    LuaChonD = "",
                    DapAnDung = 'A',
                    GiaiThich = "Uống rượu bia không được lái xe,",
                    DiemLiet = true,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("78627311-083d-42f9-a921-168e88e5f187"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                },
                new CauHoi()
                {
                    Id = Guid.Parse("6dd81400-e240-4796-bcec-fa453132e043"),
                    NoiDung = "Người đủ bao nhiêu tuổi trở lên thì được điều khiển xe ô tô tải, máy kéo có trọng tải từ 3.500 kg trở lên; xe hạng B2 kéo rơ moóc (FB2)?",
                    LuaChonA = "19 tuổi.",
                    LuaChonB = "21 tuổi.",
                    LuaChonC = "20 tuổi.",
                    LuaChonD = "",
                    DapAnDung = 'B',
                    GiaiThich = "Độ tuổi lấy bằng theo hạng (cách nhau 3 tuổi): 16: Xe dưới 50cm3; 18: Hạng A, B1, B2; 21: Hạng C, FB; 24: Hạng D, FC; 27: Hạng E, FD.",
                    DiemLiet = false,
                    MediaUrl = null,
                    LoaiMedia = null,
                    MeoGhiNho = null,
                    isDeleted = false,
                    ChuDeId = Guid.Parse("52e4f349-ed48-4ab3-92aa-1aeeb73f1395"),
                    LoaiBangLaiId = Guid.Parse("f5f61a67-1fc5-4917-8d76-e184b6d83848")
                }
            };

            modelBuilder.Entity<CauHoi>().HasData(cauHois);

            var chiTietBaiThis = new List<ChiTietBaiThi>()
            {
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("dc0a953c-a089-419d-ae45-02e7cbfe39b7"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("5fab968e-1c99-47f8-954e-b7d8750594ea")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("bfd90d24-eb69-4796-a14c-09eec597a3de"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("057a01a9-d102-4013-8d4e-40e580010abe")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("14a46bcd-158a-449e-92e2-0a40cd026bec"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("2e311460-e16f-44e1-981c-1c40c1268c1a")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("04135ce2-4223-423f-bf4e-17d8622cd4a5"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("62f88d57-380c-49c6-8dc6-418133f8c194")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("54fce508-5f8c-41b8-abdf-199d61af8094"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("cf39ba06-f565-4f78-8f21-354b90b54dbc")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("68108da6-bceb-4747-ad52-283fe3a5a3a0"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("de1a4aa2-7f7a-4ce4-9abf-84979173ef35")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("6647a7d5-bee3-4511-8d66-2a0dfd8b25c6"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("c386c5d2-a62c-46d0-90fc-28c7b066a298")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("98714fe1-fdb6-4486-8fb2-2ec114e50838"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("fc2e0758-e3e8-4bcc-8388-74a81ff4c06f")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("8904a321-5dd6-44ee-aed7-403f23092a33"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("3393dbff-9dee-4f39-9d0d-08219b69d1ed")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("154f3758-9a3b-486c-b11c-494ddd248aa4"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("71482e7a-3b46-415c-b38b-50ac587d8b18")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("18107078-8e3a-4db6-9c01-4b16eade4895"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("9ecc10c6-4184-49b1-8417-2ccac03588de")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("b8a61fac-5b0e-42d2-bdca-4beee01dcb72"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("2e88303a-6350-4a59-bb0f-4829665fea92")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("6d5c8536-fbc1-4296-b71b-5c84e9294ab4"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("1760daa6-f038-4f65-8531-418edc1056f8")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("de9c9382-5245-4ecb-b515-662188b8d0a7"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("33ca0bba-4d98-429a-9aee-2ddcf4cd4778")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("4c59ce7b-7582-41fb-8481-707314803dae"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("b0e3fc57-e1f5-4d66-a275-d80e1521cd0d")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("07d4dab3-fdf8-407b-9dac-85010738eed5"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("14be8af4-5d8f-4c40-a51e-d323d23fbcd9")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("99f65f26-d3c8-47c9-9dac-85010738eed5"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("9c7a8aa0-8976-47ce-854d-6ade9c767913")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("74b69f03-ac0e-48c8-bee9-946b6a4d7f00"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("537e9dd8-6ad4-4c73-93f5-c651354b7d14")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("db79b5c3-3570-4604-97ba-95d504b2d572"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("755996ec-0569-4a13-aeb4-28b38b33d0d3")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("fdb59178-9329-48a3-badd-a3c2d8b84eab"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("160cfe60-432a-48d8-b501-4d288bfe2d5a")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("4ff2476f-687d-441d-a12b-b523523b4a5d"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("e1d2178f-44fe-4b9a-8766-757e6f7f38ce")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("e10a6435-7b18-48d5-b7a4-c2e4cf84c8a3"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("459f6775-e179-4288-9c2d-1d725cf3b6d8")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("96d6bbc6-da76-4a88-9c54-c6b798fb4e58"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("ce50b6f1-6abc-481b-8a26-5fc8946efba0")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("89116701-a827-4acd-8b2d-d02780ca362e"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("e1a5b85f-3b8d-4213-aad5-9f3ddca19f29")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("2df9031a-02d6-414f-88e0-f75518c89e52"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("2892a1a3-c46d-4dd0-8c64-da93d5432c69")
                },
                new ChiTietBaiThi()
                {
                    Id = Guid.Parse("120b9ee0-57f8-4ed3-a534-fc87618f3c12"),
                    BaiThiId = Guid.Parse("beb046b2-7109-481f-9e9e-5c2804c178a0"),
                    CauHoiId = Guid.Parse("0e418bb1-c0a8-429c-964d-08d8b97149bf")
                }
            };
            modelBuilder.Entity<ChiTietBaiThi>().HasData(chiTietBaiThis);
        }
    }
}
