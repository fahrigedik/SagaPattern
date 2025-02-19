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
    //Db ye g�nder sipari�i olu�tur.


    //payment i�in kuyru�a g�nder.
    await publishEndpoint.Publish(new CreateOrder());

    //i�lemi tamamlay�p son kullan�c�ya haber ver.

});

app.Run();
