﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;
using System;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAutenticacaoService, AutenticacaoService>();

            services.AddHttpClient<ICatalogoService, CatalogoService>()
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
                //.AddTransientHttpErrorPolicy(
                //p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
                //.AddPolicyHandler(PollyExtensions.EsperarTentar())
                //.AddTransientHttpErrorPolicy(
                //p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            #region Refit

            services.AddHttpClient("Refit",
                    options =>
                    {
                        options.BaseAddress = new Uri(configuration.GetSection("CatalogoUrl").Value);
                    })
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);

            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();


        }
    }
}