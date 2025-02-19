using MassTransit;
using SagaPattern.OrderService;
using SagaPattern.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConsumer>();
    x.UsingRabbitMq((context, configure) =>
    {
        configure.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        configure.ReceiveEndpoint("payment-result", e =>
        {
            e.ConfigureConsumer<OrderConsumer>(context);
        });
    });
});

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapGet("/create-order", async (IPublishEndpoint publishEndpoint) =>
{
    //Db ye gönder sipariþi oluþtur.


    //payment için kuyruða gönder.
    await publishEndpoint.Publish(new CreateOrder());

    //iþlemi tamamlayýp son kullanýcýya haber ver.

});

app.Run();
