namespace Consumer.Client
{
    public static class ApiClientRegister
    {
        public static IServiceCollection AddApiClient(this IServiceCollection services
            ,Action<HttpClient> clientConfiguration)
        {
            services.AddTransient<HttpContextMiddleware>();

            services.AddHttpClient<ApiClient>(clientConfiguration)
                .AddHttpMessageHandler<HttpContextMiddleware>();

            return services;
        }
    }
}
