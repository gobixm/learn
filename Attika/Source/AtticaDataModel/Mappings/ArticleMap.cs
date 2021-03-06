﻿using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Infotecs.Attika.AtticaDataModel.Mappings
{
    public sealed class ArticleMap : ClassMapping<Article>
    {
        public ArticleMap()
        {
            Id(p => p.Id, map => map.Generator(Generators.Assigned));
            Property(p => p.Title, map => map.Length(100));
            Property(p => p.Description);
            Property(p => p.Text, map => map.Length(200));
            Property(p => p.Created);
            Bag(p => p.Comments, map => map.Key(k => k.Column("ArticleId")), cer => cer.OneToMany());
        }
    }
}