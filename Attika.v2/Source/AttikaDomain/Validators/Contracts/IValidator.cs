using Infotecs.Attika.AttikaDomain.Entities.Contracts;

namespace Infotecs.Attika.AttikaDomain.Validators.Contracts
{
    public interface IValidator<T> where T:IEntity
    {
        bool Validate(T entity, out string[] errors);
    }
}