using System;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Infotecs.Attika.AttikaInfrastructure.Data.Mappings
{
    public sealed class CommentMap : ClassMapping<CommentState>
    {
        public CommentMap()
        {
            Table("Comment");
            Id(c => c.Id, map => map.Generator(Generators.Assigned));
            Property(c => c.Text, map => map.Length(50));
            Property(c => c.Created);
            ManyToOne(c => c.ArticleState, map =>
            {
                map.Column("ArticleId");
                map.Cascade(Cascade.Merge);
            });
        }
    }
}
