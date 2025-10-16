using Autofac;
using Autofac.Extensions.DependencyInjection;
using SaharBeautyWeb.Configurations.Interfaces;

namespace SaharBeautyWeb.Configurations.Outofacs;

public static class AutofacConfig
{
    public static ConfigureHostBuilder AddAutofac(this ConfigureHostBuilder builder, string baseAddress)
    {
        builder.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.ConfigureContainer<ContainerBuilder>(option =>
        {
            option.RegisterModule(new AutofacModule(baseAddress));
        });

        return builder;
    }
}

public class AutofacModule : Module
{
    private readonly string _baseAddress;
    public AutofacModule(string baseAddress)
    {
        _baseAddress = baseAddress;
    }
    
    protected override void Load(ContainerBuilder _)
    {

        var assembly = System.Reflection.Assembly.GetAssembly(typeof(AutofacConfig));
        var baseAddress = _baseAddress;

        if (assembly != null)
        {
            var serviceTypes = assembly.GetTypes()
                .Where(t => typeof(IService)
                .IsAssignableFrom(t) && t.IsInterface && t != typeof(IService))
                .ToList();
            foreach (var serviceType in serviceTypes)
            {
                var implementationType = assembly.GetTypes()
                    .FirstOrDefault(t => !t.IsInterface && serviceType.IsAssignableFrom(t));
                if (implementationType != null)
                {
                    _.RegisterType(implementationType)
                        .As(serviceType)
                        .WithParameter("baseAddress", baseAddress)
                        .InstancePerLifetimeScope();
                }
            }

            _.RegisterType<ErrorMessages>()
                .AsSelf()
                .SingleInstance();

            _.RegisterType<HttpContextAccessor>()
                .As<IHttpContextAccessor>()
                .SingleInstance();
        }


        _.Register(ctx =>
        {
            var clientFactory = ctx.Resolve<IHttpClientFactory>();
            var client = clientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseAddress); // تنظیم BaseAddress همانجا
            return client;
        }).As<HttpClient>().InstancePerLifetimeScope();

        base.Load(_);
    }

}