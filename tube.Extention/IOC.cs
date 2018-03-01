using CommonServiceLocator;
using System;

namespace tube.Extention
{
    public static class IoC
    {
        public static T Get<T>()
        {
            return ServiceLocator.Current.GetInstance<T>();
        }

        public static T Get<T>(string key)
        {
            return ServiceLocator.Current.GetInstance<T>(key);
        }
    }
}
