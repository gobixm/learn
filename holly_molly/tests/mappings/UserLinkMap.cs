using System;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using tests.entities;

namespace tests.mappings
{
    public class UserLinkMap : SubclassMapping<UserLink>
    {
        public UserLinkMap()
        {

            DiscriminatorValue("user_user");
            
            
            //            ManyToOne(x => x.User2);

            //            Any(x => x.User1, typeof(Guid), mapper => {
            //                mapper.MetaType(typeof(string));
            //                mapper.Columns(x => { x.Name("Entity1"); },
            //                    x => { x.Name("discriminator"); });
            //                mapper.MetaValue("user_user", typeof(User));
            //            });
            //
            //            Any(x => x.User2, typeof(string), mapper => {
            //                mapper.MetaType(typeof(string));
            //                mapper.Columns(x => { x.Name("Entity2"); },
            //                    x => { x.Name("discriminator"); });
            //                mapper.MetaValue("user_user", typeof(User));
            //            });
        }
    }
}