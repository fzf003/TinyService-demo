//namespace TinyService.Infrastructure.CommonComposition
//{
//    using System;

 
//    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
//    public class NamedAttribute : Attribute
//    {
         
//        public NamedAttribute(string name)
//        {
//            if (name == null)
//                throw new ArgumentNullException("name");
//            if (name.Length == 0)
//                throw new ArgumentOutOfRangeException("name");

//            this.Name = name;
//        }
 
//        public string Name { get; private set; }
//    }
//}
