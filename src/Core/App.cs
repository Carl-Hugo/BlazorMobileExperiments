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
using Core.Services;

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

                    // Register libs/scan assembly
                    var coreAssembly = typeof(AppState).Assembly;
                    //services.AddAutoMapper(currentAssembly);
                    services.AddValidatorsFromAssembly(coreAssembly);
                    services.AddMediatR(coreAssembly);
                    services.Scan(s => s
                        .FromAssemblies(coreAssembly)
                        .AddClasses(classes => classes.AssignableTo(typeof(ExceptionBehavior<,>)))
                        .As(typeof(IPipelineBehavior<,>))
                        .WithTransientLifetime()
                    );

                    // Register OS-specific services implementations
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
            var mediator = _host.Services.GetRequiredService<IMediator>();
            await mediator.Publish(new Features.Errors.UnhandledExceptionOccurred.Event(source, ex));
        }
    }
}
