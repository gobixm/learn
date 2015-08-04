using System.Collections.Generic;
using System.Linq;
using Infotecs.Attika.AttikaDomain.Entities.Contracts;
using Infotecs.Attika.AttikaDomain.Validators.Contracts;

namespace Infotecs.Attika.AttikaDomain.Validators
{
    public abstract class Validator<T> : IValidator<T> where T : IEntity
    {
        public bool Validate(T entity, out string[] errors)
        {
            errors = (from s in GetErrors(entity) select s).ToArray();
            return errors.Length == 0;
        }

        protected abstract IEnumerable<string> GetErrors(T entity);
    }
}