using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using tests.entities;

namespace tests.mappings
{
    public class LinkMap: ClassMapping<Link>
    {
        public LinkMap() {
            Id(x => x.Id);

            ManyToOne(x => x.Entity1, mapper => {});

            ManyToOne(x => x.Entity2, mapper => {});

            Discriminator(x => x.Column("discriminator"));
        }
    }
}