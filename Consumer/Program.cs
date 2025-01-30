using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseWolverine(options =>
{
    options.UseRabbitMq(new Uri("amqp://guest:guest@localhost:5672"));

    options.ListenToRabbitQueue("product-queue");
});

var app = builder.Build();

app.Run();
