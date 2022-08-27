using CourseLibrary.Shared.Abstractions.Messaging;
using CourseLibrary.Shared.Infrastructure.Messaging.Contexts;
using CourseLibrary.Shared.Infrastructure.Messaging.Dispatchers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseLibrary.Shared.Infrastructure.Messaging;

public static class Extensions
{
    private const string SectionName = "messaging";

    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<MessagingOptions>(section);
        var messagingOptions = section.BindOptions<MessagingOptions>();

        // services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
        services.AddTransient<IAsyncMessageDispatcher, AsyncMessageDispatcher>();
        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddSingleton<IMessageContextProvider, MessageContextProvider>();
        services.AddSingleton<IMessageContextRegistry, MessageContextRegistry>();

        if (messagingOptions.UseAsyncDispatcher)
        {
            services.AddHostedService<AsyncDispatcherJob>();
        }

        return services;
    }
}
