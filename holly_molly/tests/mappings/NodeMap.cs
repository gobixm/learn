using NHibernate.Mapping.ByCode.Conformist;
using tests.entities;

namespace tests.mappings
{
    public class NodeMap : SubclassMapping<Node>
    {
        public NodeMap() {
            DiscriminatorValue("node");

            Property(x => x.Name);
        }
    }
}