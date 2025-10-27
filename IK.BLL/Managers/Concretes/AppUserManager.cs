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

        public async Task<AppUser> CreateUserWithRoleAsync(string username, string password, string rolename)
        {
            var existingUser = await _repository.GetByUserNameAsync(username);
            if (existingUser != null)
                throw new Exception("Bu kullanıcı adı zaten mevcut.");

            var newUser = new AppUser
            {
                UserName = username,
                CreatedDate = DateTime.Now,
                Status = DataStatus.Inserted,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true // E-posta onayı yok, varsayılan true
            };

            var createResult = await _userManager.CreateAsync(newUser, password);
            if (!createResult.Succeeded)
                throw new Exception(string.Join(", ", createResult.Errors.Select(e => e.Description)));

            if (!await _roleManager.RoleExistsAsync(rolename))
                await _roleManager.CreateAsync(new IdentityRole<int>(rolename));

            await _userManager.AddToRoleAsync(newUser, rolename);
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
