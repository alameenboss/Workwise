using Workwise.API.Data.EFCore;
using Workwise.API.Data.Interface;
using Workwise.API.Models;

namespace Workwise.API.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly WorkwiseDbContext workwiseDbContext;

        public PostRepository(WorkwiseDbContext workwiseDbContext)
        {
            this.workwiseDbContext = workwiseDbContext;
        }
        public async Task<Post> SavePost(Post post)
        {
            //var dtImage = new DataTable();
            //dtImage.Columns.Add("Id", typeof(int));
            //dtImage.Columns.Add("Path", typeof(string));
            //post.PostImages?.ToList().ForEach(path => dtImage.Rows.Add(0, path.ImageUrl));

            //var parameter = new DynamicParameters();
            //parameter.AddDynamicParams(new
            //{
            //    Worktype = post.Worktype,
            //    Rate=post.Rate,
            //    Title=post.Title,
            //    Location=post.Location,
            //    Description=post.Description,
            //    PostedById=post.PostedById,
            //    ListImage = dtImage.AsTableValuedParameter("dbo.[TVP_Image]")
            //});
            //var result = await QueryData<Post>("AddPost", parameter);

            _ = await workwiseDbContext.Posts.AddAsync(post);
            return post;
        }

        public async Task<IEnumerable<Post>> GetLatestPostByUser(string UserId)
        {
            //var spName = "GetPostByUserId";
            //var parameter = new DynamicParameters();
            //parameter.Add("@UserId", UserId, DbType.String, ParameterDirection.Input);
            //parameter.Add("@Page", 1, DbType.Int32, ParameterDirection.Input);
            //parameter.Add("@Size", 100, DbType.Int32, ParameterDirection.Input);

            //return await QueryData<IEnumerable<Post>>(spName, parameter);

            //await workwiseDbContext.Posts
            await Task.CompletedTask;
            return new List<Post>();
        }

    }
}