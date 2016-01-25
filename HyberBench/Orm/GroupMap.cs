using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace HyberBench.Orm
{
    public class GroupMap : ClassMapping<Group>
    {
        public GroupMap()
        {
            Table("Groups");
            Id(x => x.Id, map =>
            {
                map.Generator(Generators.Identity);
                map.Column("Id");
            });
            Property(x => x.Name, map => map.Length(500));
            Bag(x => x.Users, map =>
            {
                map.Table("UserGroup");
                map.Key(x => x.Column("GroupId"));
            },
            map => map.ManyToMany(p => p.Column("UserId")));
        }
    }
}