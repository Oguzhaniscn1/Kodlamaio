using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception //aspect metodun basında sonunda neresinde istersek çalıştıracağımız sınıfı eziyoruz.
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {   
            //defensive coding
            //validator mü?
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("bu bir doğrulama sınıfı değidir.");//asp içine mesaj dosyasıda oluşturulabilir.
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//productvalidatorü newledi
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];//productvalidatorun türediğiyerden türünü al. 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);//
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
