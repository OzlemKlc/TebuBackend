namespace Tebu.API.Repository.Extentions
{
    public static class Injection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<UserRepository>();
            services.AddScoped<VehicleRepository>();
            services.AddScoped<AddressRepository>();
            services.AddScoped<OrderRepository>();
        }
    }
}
