using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(opts =>
{
    // Configure RabbitMQ transport
    var rabbit = opts.UseRabbitMq(new Uri("amqp://guest:guest@localhost:5672")).AutoProvision();

    // Declare queue and bind to exchange
    rabbit.DeclareQueue("product-queue-pay", q => 
    { 
        q.BindExchange("product-exchange-pay");
    });

    // Configure message listening
    opts.ListenToRabbitQueue("product-queue-pay");
});

var app = builder.Build();

app.Run();
