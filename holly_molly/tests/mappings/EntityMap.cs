using NHibernate.Mapping.ByCode.Conformist;
using tests.entities;

namespace tests.mappings
{
    public class EntityMap : ClassMapping<Entity>
    {
        public EntityMap() {
            Id(x => x.Id);

            Discriminator(x => x.Column("discriminator"));
        }
    }
}