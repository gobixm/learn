using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace HyberBench.Orm
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id, map =>
            {
                map.Generator(Generators.Identity);
                map.Column("Id");
            });
            Property(x => x.Name, map => map.Length(500));
            Bag(x => x.Groups, map =>
            {
                map.Table("UserGroup");
                map.Key(x=>x.Column("UserId"));
            },
                map => map.ManyToMany(p => { p.Column("GroupId"); }));
        }
    }
}