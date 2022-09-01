using CourseLibrary.Shared.Abstractions.Commands;
using CourseLibrary.Shared.Abstractions.Events;
using CourseLibrary.Shared.Abstractions.Messaging;
using CourseLibrary.Shared.Infrastructure.Messaging.Brokers;
using CourseLibrary.Shared.Infrastructure.Messaging.Contexts;
using CourseLibrary.Shared.Infrastructure.Messaging.Dispatchers;
using CourseLibrary.Shared.Infrastructure.Messaging.Inbox;
using CourseLibrary.Shared.Infrastructure.Messaging.Outbox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseLibrary.Shared.Infrastructure.Messaging;

internal static class Extensions
{
    private const string SectionName = "messaging";

    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration,
        string sectionName = SectionName)
    {
        if (string.IsNullOrWhiteSpace(sectionName))
        {
            sectionName = SectionName;
        }

        var messagingSection = configuration.GetSection(sectionName);
        services.Configure<MessagingOptions>(messagingSection);

        var inboxSection = configuration.GetSection(sectionName);
        services.Configure<InboxOptions>(inboxSection);

        var outboxSection = configuration.GetSection(sectionName);
        services.Configure<OutboxOptions>(inboxSection);


        var messagingOptions = messagingSection.BindOptions<MessagingOptions>();
        var inboxOptions = inboxSection.BindOptions<InboxOptions>($"{sectionName}:inbox");
        var outboxOptions = outboxSection.BindOptions<OutboxOptions>($"{sectionName}:outbox");



        // var messagingOptions = services.BindOptions<MessagingOptions>(sectionName);
        // var inboxOptions = services.GetOptions<InboxOptions>($"{sectionName}:inbox");
        // var outboxOptions = services.GetOptions<OutboxOptions>($"{sectionName}:outbox");
        services
            .AddSingleton(messagingOptions)
            .AddSingleton(inboxOptions)
            .AddSingleton(outboxOptions)
            .AddSingleton<IAsyncMessageDispatcher, AsyncMessageDispatcher>()
            .AddSingleton<IMessageChannel, MessageChannel>()
            .AddSingleton<IMessageContextProvider, MessageContextProvider>()
            .AddSingleton<IMessageContextRegistry, MessageContextRegistry>()
            .AddTransient<IInbox, MongoInbox>()
            .AddTransient<IOutbox, MongoOutbox>()
            .AddScoped<IMessageBroker, InMemoryMessageBroker>();


        if (inboxOptions.Enabled)
        {
            services.TryDecorate(typeof(ICommandHandler<>), typeof(InboxCommandHandlerDecorator<>));
            services.TryDecorate(typeof(IEventHandler<>), typeof(InboxEventHandlerDecorator<>));
        }

        if (outboxOptions.Enabled)
        {
            services.AddHostedService<OutboxProcessor>();
        }

        if (messagingOptions.UseAsyncDispatcher)
        {
            services.AddHostedService<AsyncDispatcherJob>();
        }

        return services;
    }
}
