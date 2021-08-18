using System;
using System.Reflection;
using System.Web.Configuration;

using PowerBI.ReportingServices.Security.Abstractions;

namespace PowerBI.ReportingServices.Security.Services
{
    public class Options<T> : IOptions<T> where T : class
    {
        private readonly T options;

        public Options()
        {
            this.options = (T)Activator.CreateInstance(typeof(T));
            var props = typeof(T).GetProperties(
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.GetProperty |
                BindingFlags.SetProperty);
            foreach (var prop in props)
            {
                if (prop.PropertyType != typeof(string)) { continue; }

                var key = prop.Name;
                var keyAttr = prop.GetCustomAttribute<KeyNameAttribute>();
                if (keyAttr != null)
                {
                    key = keyAttr.Name;
                }

                var value = WebConfigurationManager.AppSettings.Get(key);
                prop.SetValue(options, value);
            }
        }

        public T Value => options;
    }
}
