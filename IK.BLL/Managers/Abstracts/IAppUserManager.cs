using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;

namespace IK.BLL.Managers.Abstracts
{
    public interface IAppUserManager:IManager<AppUser>
    {
        //Admin için yeni kullanıcı oluşturma ve rol atama
        Task<AppUser> CreateUserWithRoleAsync(string username, string password, string rolename);
        //Kullanıcı adı ile kullanıcıyı getirme
        Task<List<AppUser>> GetUsersByRoleAsync(string roleName);
        //Kullanıcı rolünü değiştirme
        Task ChangeUserRoleAsync(int userId, string newRole);
        //Kullanıcıyı silme(identity tablosundan)
        Task DeleteUserAsync(int userId);

    }
}
