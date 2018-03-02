using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using tube.Model;
using tube.Ultilites;

namespace tube.Business
{
    public class APIFileBusiness :  IAPIKeyBusiness
    {
        public string URLROOT =  CommonKey.KeylocalFile;
        private static readonly log4net.ILog log =
          log4net.LogManager.GetLogger(
                   System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool CheckVaildVideo(string id)
        {
            throw new NotImplementedException();
        }

        public void GetDefaultKey()
        {
            if (string.IsNullOrEmpty(CommonKey.KeyActive))
            {

                string key = string.Empty;
                if (CommonKey.Keys == null)
                    CommonKey.Keys = ReadKeyLocal();

                key = CommonKey.Keys.Where(a => !a.IsOverLimit).Select(a => a.KeyAPI).FirstOrDefault();
                if (string.IsNullOrEmpty(key))
                {
                    ResetOverLimit();
                    var datalocal = CommonKey.Keys;
                    key = datalocal.Where(a => !a.IsOverLimit).Select(a => a.KeyAPI).FirstOrDefault();
                }
                CommonKey.KeyActive = key;
            }

        }

        public List<APIFile> ReadKeyLocal()
        {
            bool isexist = File.Exists(URLROOT);

            if (isexist)
            {
                string jsonLocal = File.ReadAllText(URLROOT);
                List<APIFile> datalocal = Newtonsoft.Json.JsonConvert.DeserializeObject<List<APIFile>>(jsonLocal);
                return datalocal;
            }
            return null;
        }

        public void SaveKeys()
        {
            if (CommonKey.Keys == null) return;
            bool isexist = File.Exists(URLROOT);

            if (isexist)
            {
                log.Info("Saved !" + URLROOT);
                string jsonLocal = Newtonsoft.Json.JsonConvert.SerializeObject(CommonKey.Keys);
                File.WriteAllText(URLROOT, jsonLocal);
                return;
            }
            return;
        }

        public void SetOverLimit()
        {
            if (string.IsNullOrEmpty(CommonKey.KeyActive))
            {
                return;
            }

            if (CommonKey.Keys == null) return;

            var key = CommonKey.Keys.Where(a => a.KeyAPI == CommonKey.KeyActive).FirstOrDefault();
            CommonKey.Keys.Remove(key);
            log.Info("total key availabel " + CommonKey.Keys.Count);
            CommonKey.KeyActive = string.Empty;
        }

        public void ResetOverLimit()
        {
            foreach (var item in CommonKey.Keys)
            {
                item.IsOverLimit = false;
                item.UpdatedDate = DateTime.Now;
            }
        }

        public void InitKey()
        {
            // kiem tra key local       

            var datalocal = ReadKeyLocal();
            CommonKey.Keys = datalocal;
            CommonKey.KeyActive = CommonKey.Keys.Select(a => a.KeyAPI).FirstOrDefault();

        }

    }
}
