namespace TinyService.Infrastructure
{
    using System;
    using System.ComponentModel;


    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ComponentAttribute : Attribute
    {

        public ComponentAttribute()
        {
            IsSingleton = false;
        }


        [DefaultValue(false)]
        public bool IsSingleton { get; set; }
    }
}