using System;
using System.Linq;
using tube.Data;
using tube.Entities;

namespace tube.Business
{
    public interface IAPIKeyBusiness
    {
        /// <summary>
        /// lấy key mac dinh chua vuot han muc
        /// </summary>
        void GetDefaultKey();
        /// <summary>
        /// cap nhat vuot han muc
        /// </summary>
        void SetOverLimit();
        
    }
    public class APIKeyBusiness :  IAPIKeyBusiness
    {
        #region Contructor
        public APIKeyBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
          

        }
        #endregion
        #region Properties
        private readonly IUnitOfWork unitOfWork;
       
        #endregion

        #region Methods

        public void GetDefaultKey()
        {
            var repository =unitOfWork.Repository<Apikey>();
            var key = repository.GetQueryable().Where(a => !a.IsOverLimit).FirstOrDefault();
            if (key == null)
            {
                ResetOverLimit();
                key = repository.GetQueryable().Where(a => !a.IsOverLimit).FirstOrDefault();

            }
            CommonKey.KeyActive = key.KeyAPI;
        }
        public void SetOverLimit()
        {
            var repository = unitOfWork.Repository<Apikey>();
            var key = repository.GetQueryable().Where(a => a.KeyAPI == CommonKey.KeyActive).FirstOrDefault();
            key.IsOverLimit = true;
            key.UpdatedDate = DateTime.Now;
            repository.Update(key);
            unitOfWork.Commit();
        }
        public void ResetOverLimit()
        {
            var repository = unitOfWork.Repository<Apikey>();
            var keys = unitOfWork.Repository<Apikey>().GetQueryable().Where(a => a.IsOverLimit).ToList();
            foreach (var item in keys)
            {
                item.IsOverLimit = false;
                item.UpdatedDate = DateTime.Now;
                repository.Update(item);
            }
            unitOfWork.Commit();

        }


      
        #endregion
    }
}
    