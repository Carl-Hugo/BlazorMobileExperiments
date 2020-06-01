using System;
using Microsoft.MobileBlazorBindings;
using Microsoft.Extensions.Hosting;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using Core.Models;
using MediatR;
using Core;
using FluentValidation;
using Core.Behaviors;
using System.Threading.Tasks;
using MediatR.Pipeline;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Input;
using Scrutor;
using Xamarin.Forms.Xaml;
using Core.Pages;

namespace Core
{
    public class App : Application
    {
        private readonly IHost _host;
        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register states/store
                    services
                        .AddSingleton<IStore, Store>()
                        .AddSingleton<AppState>()
                        .AddSingleton<CounterState>()
                        .AddSingleton<ErrorState>()
                    ;

                    // Register libs
                    var thisAssembly = typeof(App).Assembly;
                    var CoreAssembly = typeof(AppState).Assembly;
                    //services.AddAutoMapper(currentAssembly);
                    services.AddValidatorsFromAssembly(CoreAssembly);
                    services.AddMediatR(CoreAssembly);
                    services.Scan(s => s
                        .FromAssemblies(CoreAssembly)
                        .AddClasses(classes => classes.AssignableTo(typeof(ExceptionBehavior<,>)))
                        .As(typeof(IPipelineBehavior<,>))
                        .WithTransientLifetime()
                    );

                    // Register app-specific services implementations
                    services.AddTransient(sp => DependencyService.Get<IAlertManager>());
                })
                .Build();

            _host.AddComponent<ShellPage>(parent: this);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public async Task UnhandledExceptionAsync(string source, Exception ex)
        {
            //var mediator = _host.Services.GetService<IMediator>();
            //await mediator.Send(new Core.Features.Errors.UnhandledExceptionOccurred.Command(source, ex));
        }


    }

    public static class ScanHelper
    {
        public static bool CouldCloseTo(this Type openConcretion, Type closedInterface)
        {
            var openInterface = closedInterface.GetGenericTypeDefinition();
            var arguments = closedInterface.GenericTypeArguments;

            var concreteArguments = openConcretion.GenericTypeArguments;
            return arguments.Length == concreteArguments.Length && openConcretion.CanBeCastTo(openInterface);
        }

        public static bool CanBeCastTo(this Type pluggedType, Type pluginType)
        {
            if (pluggedType == null) return false;

            if (pluggedType == pluginType) return true;

            return pluginType.GetTypeInfo().IsAssignableFrom(pluggedType.GetTypeInfo());
        }

        public static bool IsOpenGeneric(this Type type)
        {
            return type.GetTypeInfo().IsGenericTypeDefinition || type.GetTypeInfo().ContainsGenericParameters;
        }

        public static IEnumerable<Type> FindInterfacesThatClose(this Type pluggedType, Type templateType)
        {
            return FindInterfacesThatClosesCore(pluggedType, templateType).Distinct();
        }

        public static IEnumerable<Type> FindInterfacesThatClosesCore(Type pluggedType, Type templateType)
        {
            if (pluggedType == null) yield break;

            if (!pluggedType.IsConcrete()) yield break;

            if (templateType.GetTypeInfo().IsInterface)
            {
                foreach (
                    var interfaceType in
                    pluggedType.GetInterfaces()
                        .Where(type => type.GetTypeInfo().IsGenericType && (type.GetGenericTypeDefinition() == templateType)))
                {
                    yield return interfaceType;
                }
            }
            else if (pluggedType.GetTypeInfo().BaseType.GetTypeInfo().IsGenericType &&
                     (pluggedType.GetTypeInfo().BaseType.GetGenericTypeDefinition() == templateType))
            {
                yield return pluggedType.GetTypeInfo().BaseType;
            }

            if (pluggedType.GetTypeInfo().BaseType == typeof(object)) yield break;

            foreach (var interfaceType in FindInterfacesThatClosesCore(pluggedType.GetTypeInfo().BaseType, templateType))
            {
                yield return interfaceType;
            }
        }

        public static bool IsConcrete(this Type type)
        {
            return !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface;
        }
    }
}
