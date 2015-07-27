﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Infotecs.Attika.AtticaDataModel.Mappings
{
    public sealed class CommentMap : ClassMapping<Comment>
    {
        public CommentMap()
        {
            Id(c => c.Id, map => map.Generator(Generators.Guid));
            Property(c => c.Text, map => map.Length(50));
            Property(c => c.ArticleId);
            ManyToOne(c => c.Article, map => map.Column("ArticleId"));
        }
    }
}