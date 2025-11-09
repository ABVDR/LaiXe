using Libs.Data;
using Libs.Entity;
using Libs.Models;
using Libs.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Service
{
    public class AdminService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMoPhongRepository moPhongRepository;
        private readonly IUserRepository userRepository;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        public AdminService(ApplicationDbContext dbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager) {
            this.dbContext = dbContext;
            this.moPhongRepository = new MoPhongRepository(dbContext);
            this.userRepository = new UserRepository(dbContext);
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }
        public async Task<PageList<MoPhong>> GetPagedMoPhong(int pageNumber, int pageSize, string? search, string? sortCol, string sortDir)
        {
            return await moPhongRepository.GetPagedMoPhong(pageNumber, pageSize, search, sortCol, sortDir);
        }
        public async Task<PageList<User>> GetPagedUser(int pageNumber, int pageSize, string? search, string? sortCol, string sortDir)
        {
            return await userRepository.GetPagedUser(pageNumber, pageSize, search, sortCol, sortDir);
        }
        public async Task<int> GetTotalUserCountAsync()
        {
            return await userRepository.GetAllUserCountByMonthAsync();
        }
        public async Task<Dictionary<int, int>> GetUserCountByMonthInCurrentYearAsync()
        {
            return await userRepository.GetUserCountByMonthInCurrentYearAsync();
        }
        public async Task<int> GetAllUsersCountAsync()
        {
            return await userRepository.GetAllUsersCountAsync();
        }
        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }
        public async Task<int> SetRoleUser(Guid id, string Role)
        {
            var userFinder = await userManager.FindByIdAsync(id.ToString());
            if (userFinder == null)
            {
                return 0;
            }
            if(!await roleManager.RoleExistsAsync(Role))
            {
                return 0;
            }
            var existingRoles = await userManager.GetRolesAsync(userFinder);

            if (existingRoles.Any())
            {
                await userManager.RemoveFromRolesAsync(userFinder, existingRoles);
            }
            var result = await userManager.AddToRoleAsync(userFinder, Role);
            return result.Succeeded ? 1 : 0;
        }
        public async Task<IEnumerable<IdentityRole>> GetRoleListAsync()
        {
            return await roleManager.Roles.ToListAsync();
        }
        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            return user;
        }
        public async Task<UserModel> GetUserByIdIncludeRolesAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            var roles = await userManager.GetRolesAsync(user);
            return new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                CreateAt = user.CreatedAt,
                Role = roles
            };
        }
    }
}
