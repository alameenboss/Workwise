using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Workwise.Data.Interface;
using Workwise.Model;

namespace Workwise.Data
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public async Task<Post> SavePost(Post post)
        {
            var dtImage = new DataTable();
            dtImage.Columns.Add("Id", typeof(int));
            dtImage.Columns.Add("Path", typeof(string));
            post.PostImages?.ToList().ForEach(path => dtImage.Rows.Add(0, path.ImageUrl));

            var parameter = new DynamicParameters();
            parameter.AddDynamicParams(new
            {
                Worktype = post.Worktype,
                Rate=post.Rate,
                Title=post.Title,
                Location=post.Location,
                Description=post.Description,
                PostedById=post.PostedById,
                ListImage = dtImage.AsTableValuedParameter("dbo.[TVP_Image]")
            });
            var result = await QueryData<Post>("AddPost", parameter);
            post.Id = result.Id;
            return post;
        }

        public async Task<IEnumerable<Post>> GetLatestPostByUser(string UserId)
        {
            var spName = "GetPostByUserId";
            var parameter = new DynamicParameters();
            parameter.Add("@UserId", UserId, DbType.String, ParameterDirection.Input);
            parameter.Add("@Page", 1, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@Size", 100, DbType.Int32, ParameterDirection.Input);
            return await QueryData<IEnumerable<Post>>(spName, parameter);
        }
        
    }
}