﻿using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Clientes.Api.Application.Commands;
using NSE.Core.Mediator;
using NSE.Core.Messages.Integration;

namespace NSE.Clientes.Api.Services
{
    public class RegistroClienteIntegrationHandler : BackgroundService
    {
        private IBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroClienteIntegrationHandler(
                            IServiceProvider serviceProvider,
                            IBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }

        //private void SetResponder()
        //{
        //    _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
        //        await RegistrarCliente(request));

        //    _bus.AdvancedBus.Connected += OnConnect;
        //}

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
                await RegistrarCliente(request));

            //SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e)
        {
            //SetResponder();
        }

        private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            var clienteCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Email, message.Cpf);
            ValidationResult sucesso;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                sucesso = await mediator.EnviarComando(clienteCommand);
            }

            return new ResponseMessage(sucesso);
        }
    }
}
