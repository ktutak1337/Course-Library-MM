using CourseLibrary.Shared.Abstractions;
using CourseLibrary.Shared.Abstractions.Dispatchers;
using CourseLibrary.Shared.Abstractions.Modules;
using CourseLibrary.Shared.Abstractions.Storage;
using CourseLibrary.Shared.Abstractions.Time;
using CourseLibrary.Shared.Infrastructure.Api;
using CourseLibrary.Shared.Infrastructure.Auth;
using CourseLibrary.Shared.Infrastructure.Commands;
using CourseLibrary.Shared.Infrastructure.Contexts;
using CourseLibrary.Shared.Infrastructure.Contracts;
using CourseLibrary.Shared.Infrastructure.Dispatchers;
using CourseLibrary.Shared.Infrastructure.Events;
using CourseLibrary.Shared.Infrastructure.Exceptions;
using CourseLibrary.Shared.Infrastructure.Kernel;
using CourseLibrary.Shared.Infrastructure.Logging;
using CourseLibrary.Shared.Infrastructure.Messaging;
using CourseLibrary.Shared.Infrastructure.Modules;
using CourseLibrary.Shared.Infrastructure.Mongo;
using CourseLibrary.Shared.Infrastructure.Queries;
using CourseLibrary.Shared.Infrastructure.Security;
using CourseLibrary.Shared.Infrastructure.Serialization;
using CourseLibrary.Shared.Infrastructure.Storage;
using CourseLibrary.Shared.Infrastructure.Time;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CourseLibrary.Shared.Infrastructure;

public static class Extensions
{
    private const string CorrelationIdKey = "correlation-id";

    public static bool IsEmpty(this string value)
        => string.IsNullOrWhiteSpace(value);

    public static IServiceCollection AddInitializer<T>(this IServiceCollection services) where T : class, IInitializer
        => services.AddTransient<IInitializer, T>();

    public static IServiceCollection AddModularInfrastructure(this IServiceCollection services,
        IConfiguration configuration, IList<Assembly> assemblies, IList<IModule> modules)
    {
        var section = configuration.GetSection("app");
        services.Configure<AppOptions>(section);
        var appOptions = section.BindOptions<AppOptions>();

        var appInfo = new AppInfo(appOptions.Name, appOptions.Version);
        services.AddSingleton(appInfo);

        var disabledModules = new List<string>();
        foreach (var (key, value) in configuration.AsEnumerable())
        {
            if (!key.Contains(":module:enabled"))
            {
                continue;
            }

            if (!bool.Parse(value))
            {
                disabledModules.Add(key.Split(":")[0]);
            }
        }

        services.AddCorsPolicy(configuration);
        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.CustomSchemaIds(x => x.FullName);
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Course Library API",
                Version = "v1"
            });
        });

        services.AddMemoryCache();
        services.AddHttpClient();
        services.AddSingleton<IRequestStorage, RequestStorage>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
        services.AddModuleInfo(modules);
        services.AddModuleRequests(assemblies);
        services.AddAuth(configuration, modules);
        services.AddErrorHandling();
        services.AddContext();
        services.AddContracts();
        services.AddCommands(assemblies);
        services.AddQueries(assemblies);
        services.AddEvents(assemblies);
        services.AddDomainEvents(assemblies);
        services.AddMessaging(configuration);
        services.AddSecurity(configuration);
        services.AddMongo(configuration);
        services.AddSingleton<IClock, UtcClock>();
        services.AddSingleton<IDispatcher, InMemoryDispatcher>();
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                var removedParts = new List<ApplicationPart>();
                foreach (var disabledModule in disabledModules)
                {
                    var parts = manager.ApplicationParts.Where(x => x.Name.Contains(disabledModule,
                        StringComparison.InvariantCultureIgnoreCase));
                    removedParts.AddRange(parts);
                }

                foreach (var part in removedParts)
                {
                    manager.ApplicationParts.Remove(part);
                }

                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        return services;
    }

    public static IApplicationBuilder UseModularInfrastructure(this IApplicationBuilder app)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.All
        });
        app.UseCors("cors");
        app.UseCorrelationId();
        app.UseErrorHandling();
        app.UseSwagger();
        app.UseReDoc(reDoc =>
        {
            reDoc.RoutePrefix = "docs";
            reDoc.SpecUrl("/swagger/v1/swagger.json");
            reDoc.DocumentTitle = "Course Library API";
        });
        app.UseAuthentication();
        app.UseContext();
        app.UseLogging();
        app.UseRouting();
        app.UseAuthorization();

        return app;
    }

    public static T BindOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
        => BindOptions<T>(configuration.GetSection(sectionName));

    public static T BindOptions<T>(this IConfigurationSection section) where T : new()
    {
        var options = new T();
        section.Bind(options);
        return options;
    }

    public static string GetModuleName(this object value)
        => value?.GetType().GetModuleName() ?? string.Empty;

    public static string GetModuleName(this Type type, string namespacePart = "Modules", int splitIndex = 2)
    {
        if (type?.Namespace is null)
        {
            return string.Empty;
        }

        return type.Namespace.Contains(namespacePart)
            ? type.Namespace.Split(".")[splitIndex].ToLowerInvariant()
            : string.Empty;
    }

    public static IApplicationBuilder UseCorrelationId(this IApplicationBuilder app)
        => app.Use((ctx, next) =>
        {
            ctx.Items.Add(CorrelationIdKey, Guid.NewGuid());
            return next();
        });

    public static Guid? TryGetCorrelationId(this HttpContext context)
        => context.Items.TryGetValue(CorrelationIdKey, out var id) ? (Guid)id : null;
}
