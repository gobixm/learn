using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using tests.entities;

namespace tests.mappings
{
    public class NodeLinkMap : SubclassMapping<NodeLink>
    {
        public NodeLinkMap()
        {
            DiscriminatorValue("node_node");

            ManyToOne(x => x.Node1, mapper =>
            {
                mapper.Lazy(LazyRelation.NoLazy);
            });

            ManyToOne(x => x.Node2, mapper =>
            {
                mapper.Lazy(LazyRelation.NoLazy);
            });
        }
    }
}