﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CourseLibrary.Shared.Infrastructure.Contracts;

public static class Extensions
{
    public static IServiceCollection AddContracts(this IServiceCollection services)
    {
        services.AddSingleton<IContractRegistry, ContractRegistry>();

        return services;
    }

    public static IApplicationBuilder ValidateContracts(this IApplicationBuilder app, IEnumerable<Assembly> assemblies)
    {
        var contractRegistry = app.ApplicationServices.GetRequiredService<IContractRegistry>();
        contractRegistry.Validate(assemblies);

        return app;
    }

    public static IContractRegistry UseContracts(this IApplicationBuilder app)
        => app.ApplicationServices.GetRequiredService<IContractRegistry>();
}
