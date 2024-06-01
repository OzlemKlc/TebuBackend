namespace Tebu.API.Service.Extentions
{
    public static class Injection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<CurrentUserService>();
            services.AddScoped<UserService>();
        }


    }
}
