using Libs.Data;
using Libs.Entity;
using Libs.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<int> GetAllUserCountByMonthAsync();
        public Task<Dictionary<int, int>> GetUserCountByMonthInCurrentYearAsync();
        public Task<PageList<User>> GetPagedUser(int pageNumber, int pageSize, string? search, string? sortCol, string? sortDir);
        public Task<int> GetAllUsersCountAsync();

    }
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<PageList<User>> GetPagedUser(int pageNumber, int pageSize, string? search, string? sortCol, string? sortDir)
        {
            IQueryable<User> UserQuery = _dbContext.Users.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                UserQuery = UserQuery.Where(p => p.UserName.Contains(search));
            }
            var entityProps = typeof(User)
            .GetProperties()
            .ToDictionary(p => p.Name.ToLower(), p => p.Name, StringComparer.OrdinalIgnoreCase);
            if (!string.IsNullOrWhiteSpace(sortCol) && entityProps.ContainsKey(sortCol))
            {
                string actualCol = entityProps[sortCol];
                bool isDescending = sortDir?.Equals("desc", StringComparison.OrdinalIgnoreCase) ?? false;

                UserQuery = isDescending
                    ? UserQuery.OrderByDescending(q => EF.Property<object>(q, actualCol))
                    : UserQuery.OrderBy(q => EF.Property<object>(q, actualCol));
            }
            else
            {
                UserQuery = UserQuery.OrderBy(q => q.Id);
            }
            var Users = await PageList<User>.CreatePageAsync(UserQuery, pageNumber, pageSize);
            return Users;
        }
        public async Task<int> GetAllUserCountByMonthAsync()
        {
            var thisMonth = DateTime.Now.Month;
            int userCountByMonth = await _dbContext.Users
                .Where(u => u.CreatedAt.Month == thisMonth)
                .CountAsync();
            return userCountByMonth;
        }
        // Implementation
        public async Task<Dictionary<int, int>> GetUserCountByMonthInCurrentYearAsync()
        {
            var currentYear = DateTime.Now.Year;

            var counts = await _dbContext.Users
                .Where(u => u.CreatedAt.Year == currentYear)
                .GroupBy(u => u.CreatedAt.Month)
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToListAsync();
            var result = Enumerable.Range(1, 12)
                .ToDictionary(
                    m => m,
                    m => counts.FirstOrDefault(x => x.Month == m)?.Count ?? 0
                );

            return result;
        }
        public async Task<int> GetAllUsersCountAsync()
        {
            return await _dbContext.Users.CountAsync();
        }
    }
}
