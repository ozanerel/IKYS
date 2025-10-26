using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IK.ENTITIES.Models;

namespace IK.BLL.Managers.Abstracts
{
    public interface IJobApplicationManager:IManager<JobApplication>
    {
        Task ApproveApplicationAsync(int id);
        Task RejectApplicationAsync(int id);
        Task SetPendingAsync(int id);
        Task UploadCvAsync(int id, string filePath);

        //Belirli bir pozisyona yapılmış tüm başvuruları getirir (Admin görüntüleme işlemi)
        Task<List<JobApplication>> GetApplicationsByPositionAsync(int positionId);
    }
}
