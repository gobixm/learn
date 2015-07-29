using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace Infotecs.Attika.AtticaDataModel.Repositories
{
    public sealed class StandardQueryRepository : IQueryRepository
    {
        private readonly string _connectionString;

        public StandardQueryRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dapper"].ConnectionString;
        }

        public Article GetArticle(Guid articleId)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = @"select * from Article where Id=@ArticleId; 
                                 select * from Comment where ArticleId=@ArticleId order by Created desc;";

                using (SqlMapper.GridReader multi = conn.QueryMultiple(query, new {ArticleId = articleId}))
                {
                    Article article = multi.Read<Article>().FirstOrDefault();
                    if (article != null)
                        article.Comments = multi.Read<Comment>().ToList();
                    return article;
                }
            }
        }

        public IEnumerable<Comment> GetComments(Guid articleId)
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                IEnumerable<Comment> comments = conn.Query<Comment>("select ArticleId=@Id", new {Id = articleId});
                return comments;
            }
        }

        public IEnumerable<ArticleHeader> GetHeaders()
        {
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                return conn.Query<ArticleHeader>("select Id as ArticleId, Title from Article order by Title");
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}