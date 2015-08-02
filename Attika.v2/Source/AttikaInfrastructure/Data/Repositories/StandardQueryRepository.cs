using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Infotecs.Attika.AttikaInfrastructure.Data.Models;

namespace Infotecs.Attika.AttikaInfrastructure.Data.Repositories
{
    public sealed class StandardQueryRepository : IQueryRepository
    {
        private readonly string _connectionString;

        public StandardQueryRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dapper"].ConnectionString;
        }

        public ArticleState GetArticle(Guid articleId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var query = @"select * from Article where Id=@ArticleId; 
                                 select * from Comment where ArticleId=@ArticleId order by Created desc;";

                using (var multi = conn.QueryMultiple(query, new {ArticleId = articleId}))
                {
                    var article = multi.Read<ArticleState>().FirstOrDefault();
                    if (article != null)
                        article.Comments = multi.Read<CommentState>().ToList();
                    return article;
                }
            }
        }

        public IEnumerable<ArticleHeaderState> GetHeaders()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                return conn.Query<ArticleHeaderState>("select Id as ArticleId, Title from Article order by Title");
            }
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}