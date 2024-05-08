using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BankApplication.model;
using System.ComponentModel.DataAnnotations;

namespace BankApplication.Controller
{
   public class UserRepository
   {
        private readonly BankDbContext _dbContext;

        public UserRepository(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(string phonenumber, string password)
        {
            var user = new User1
            {
                phonenumber = phonenumber,
                password = HashPassword(password),
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public bool AuthenticateUser1(string phonenumber, string password)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.phonenumber == phonenumber);

            if (user == null) return false;

            return user.password == HashPassword(password);

        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            { 
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
