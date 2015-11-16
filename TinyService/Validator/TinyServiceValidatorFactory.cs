using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Infrastructure;

namespace TinyService.Validator
{
    [Component(IsSingleton=true)]
    public class TinyServiceValidatorFactory : AttributedValidatorFactory
    {
         
        public override IValidator GetValidator(Type type)
        {
            if (type != null)
            {
                var attribute = (ValidatorAttribute)Attribute.GetCustomAttribute(type, typeof(ValidatorAttribute));
                if ((attribute != null) && (attribute.ValidatorType != null))
                {
               
                    object instance;
                   instance= ObjectFactory.Servicelocator.GetService(attribute.ValidatorType);
                   if (instance != null)
                   {
                       return instance as IValidator;
                   }
                    
                    return null;

                }
            }
            return null;

        }
    }
}
