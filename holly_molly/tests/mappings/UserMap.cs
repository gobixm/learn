using NHibernate.Mapping.ByCode.Conformist;
using tests.entities;

namespace tests.mappings
{
    public class UserMap : SubclassMapping<User>
    {
        public UserMap() {
            DiscriminatorValue("user");

            Property(x => x.Name);
        }
    }
}