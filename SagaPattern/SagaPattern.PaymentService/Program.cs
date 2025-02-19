using MassTransit;
using SagaPattern.PaymentService;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddMassTransit(x =>
{


    x.AddConsumer<PaymentConsumer>();

    x.UsingRabbitMq((context, configure) =>
    {
        configure.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        configure.ReceiveEndpoint("payment-result", e =>
        {
            e.ConfigureConsumer<PaymentConsumer>(context);
        });

    });
});


var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();
