using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Infotecs.Attika.AtticaDataModel.Repos
{
    public class StandardQueryRepo : IQueryRepo
    {
        private readonly string _connectionString;

        public StandardQueryRepo()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dapper"].ConnectionString;
        }

        public Article GetArticle(Guid articleId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var query = @"select * from Articles where Id=@ArticleId; 
                                 select * from Comments where ArticleId=@ArticleId order by Created desc;";

                using (var multi = conn.QueryMultiple(query, new {ArticleId = articleId}))
                {
                    var article = multi.Read<Article>().First();
                    article.Comments = multi.Read<Comment>().ToList();
                    return article;
                }
            }
        }

        public IEnumerable<Comment> GetComments(Guid articleId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var comments = conn.Query<Comment>("select ArticleId=@Id", new {Id = articleId});
                return comments;
            }
        }

        public IEnumerable<ArticleHeader> GetHeaders()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                return conn.Query<ArticleHeader>("select Id as ArticleId, Title from Articles order by Title");
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}