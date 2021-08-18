using System;

namespace PowerBI.ReportingServices.Security
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class KeyNameAttribute : Attribute
    {
        public KeyNameAttribute()
        {
        }

        public KeyNameAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
