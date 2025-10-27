using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Models;

namespace IK.BLL.Managers.Concretes
{
    public class AppUserProfileManager:BaseManager<AppUserProfile>, IAppUserProfileManager
    {
        readonly IAppUserProfileRepository _repository;
        public AppUserProfileManager(IAppUserProfileRepository repository):base(repository)
        {
            _repository = repository;
        }

        public async Task UpdateProfilePhotoAsync(int profileId, string photoPath)
        {
            var profile = await _repository.GetByIdAsync(profileId);
            if (profile == null) return;

            profile.PhotoPath =  photoPath;
            profile.UpdatedDate = DateTime.Now;
            await _repository.UpdateAsync(profile, profile);
        }
    }
}
