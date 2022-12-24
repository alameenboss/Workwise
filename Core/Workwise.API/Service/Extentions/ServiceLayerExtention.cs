using Workwise.API.Data;
using Workwise.API.Data.Interface;
using Workwise.API.Service.Interface;

namespace Workwise.API.Service.Extentions
{
    public static class ServiceLayerExtention
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            _ = services.AddScoped<ICompanyService, CompanyService>();
            _ = services.AddScoped<IMessageService, MessageService>();
            _ = services.AddScoped<IPostService, PostService>();
            _ = services.AddScoped<IUserService, UserService>();
            _ = services.AddScoped<IMessageRepository, MessageRepository>();
            _ = services.AddScoped<ICompanyRepository, CompanyRepository>();
            _ = services.AddScoped<IPostRepository, PostRepository>();
            _ = services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
