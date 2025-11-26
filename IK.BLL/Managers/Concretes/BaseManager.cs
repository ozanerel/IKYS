using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IK.BLL.Managers.Abstracts;
using IK.DAL.Repositories.Abstracts;
using IK.ENTITIES.Interfaces;


namespace IK.BLL.Managers.Concretes
{
    public abstract class BaseManager<T> : IManager<T> where T : class, IEntity
    {
        readonly IRepository<T> _repository;

        protected BaseManager(IRepository<T> repository)
        {
            _repository = repository;
        }
        public async Task CreateAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = ENTITIES.Enums.DataStatus.Inserted;

            await _repository.CreateAsync(entity);
        }

        public List<T> GetActives()
        {
            //Normal şartlarda buraya repository cagrılmadan önce Business Logic yazılır...
            return _repository.Where(x => x.Status != ENTITIES.Enums.DataStatus.Deleted).ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public List<T> GetModifieds()
        {
            return _repository.Where(x => x.Status == ENTITIES.Enums.DataStatus.Updated).ToList();
        }

        public List<T> GetPassives()
        {
            return _repository.Where(x => x.Status == ENTITIES.Enums.DataStatus.Deleted).ToList();
        }

        public async Task MakePassiveAsync(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            entity.Status = ENTITIES.Enums.DataStatus.Passive;
            T originalValue = await _repository.GetByIdAsync(entity.Id);
            await _repository.UpdateAsync(entity);
        }

        public async Task<string> DeleteAsync(T entity)
        {
            if (entity.Status != ENTITIES.Enums.DataStatus.Deleted)
            {
                return "Silme işlemi sadece pasif veriler üzerinden yapılabilir";
            }
            T originalValue = await _repository.GetByIdAsync(entity.Id);
            await _repository.DeleteAsync(originalValue);
            return "Silme işlemi basarıyla gercekleştirildi";
        }

        public async Task UpdateAsync(T entity)
        {
            entity.UpdatedDate = DateTime.Now;
            entity.Status = ENTITIES.Enums.DataStatus.Updated;
            T originalValue = await _repository.GetByIdAsync(entity.Id);
            if (originalValue == null)
            {
                return;
            }

            

            await _repository.UpdateAsync(entity);
        }

        public List<T> Where(Expression<Func<T, bool>> exp)
        {
            return _repository.Where(exp).ToList();
        }
    }
}
