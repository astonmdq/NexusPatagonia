using Microsoft.EntityFrameworkCore;
using NexusPatagonia.Domain.Entities;
using NexusPatagonia.Domain.Interfaces;
using NexusPatagonia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NexusPatagonia.Application.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<User> _dbSet;
        public UserRepository(ApplicationDbContext context)
        { 
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public async Task<bool> ValidateUserPassword(string user, string password)
        {
            var currentUser = await _dbSet.Where(x => x.UserName == user && x.PasswordHash == password).FirstAsync();
            return currentUser != null;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        { 
            var currentUser = await _dbSet.AsNoTracking().Where(x => x.UserName == username).FirstOrDefaultAsync();
            return currentUser; 
        }
    }
}
