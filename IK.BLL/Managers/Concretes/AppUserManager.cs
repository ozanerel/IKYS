using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Enums;
using IK.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;

namespace IK.BLL.Managers.Concretes
{
    public class AppUserManager:BaseManager<AppUser>,IAppUserManager
    {
        readonly IAppUserRepository _repository;
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole<int>> _roleManager;
        public AppUserManager(IAppUserRepository repository,UserManager<AppUser> userManager,RoleManager<IdentityRole<int>> roleManager):base(repository)
        {
            _repository = repository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task ChangeUserRoleAsync(int userId, string newRole)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!await _roleManager.RoleExistsAsync(newRole))
                await _roleManager.CreateAsync(new IdentityRole<int>(newRole));

            await _userManager.AddToRoleAsync(user, newRole);

        }

        public async Task<AppUser> CreateUserWithRoleAsync(string username, string email, string password, string roleName)
        {
            // Kullanıcı var mı kontrolü
            var existingUser = await _userManager.FindByNameAsync(username);
            if (existingUser != null)
                throw new Exception("Bu kullanıcı adı zaten mevcut.");

            // Email kontrolü (Identity email zorunlu tutuyor)
            var existingEmail = await _userManager.FindByEmailAsync(email);
            if (existingEmail != null)
                throw new Exception("Bu email zaten kayıtlı.");

            // Yeni user oluştur
            var newUser = new AppUser
            {
                UserName = username,
                Email = email,
                CreatedDate = DateTime.Now,
                SecurityStamp = Guid.NewGuid().ToString(),
                Status = DataStatus.Inserted,
                EmailConfirmed = true
            };

            var createResult = await _userManager.CreateAsync(newUser, password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(" | ", createResult.Errors.Select(e => e.Description));
                throw new Exception("Kullanıcı oluşturulamadı: " + errors);
            }

            // Rol yoksa oluştur
            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new IdentityRole<int>(roleName));

            // Role ekle
            await _userManager.AddToRoleAsync(newUser, roleName);

            return newUser;
        }



        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            user.Status = DataStatus.Passive;
            user.UpdatedDate = DateTime.Now;

            await _userManager.UpdateAsync(user);
        }

        public async Task<List<AppUser>> GetUsersByRoleAsync(string roleName)
        {
            return await _repository.GetUserByRoleAsync(roleName);
        }
    }
}
