using Contracts;
using Oakton.Resources;
using WebApi.Extensions;
using Wolverine;
using Wolverine.Postgresql;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSwaggerGen()
    .AddEndpointsApiExplorer()
    .AddEndpoints()
    .AddDatabase(builder.Configuration);

builder.Host.UseWolverine(options =>
{
    options.UseRabbitMq(new Uri("amqp://guest:guest@localhost:5672")).AutoProvision();
    options.PersistMessagesWithPostgresql("Host=localhost;Port=5432;Database=Wolverine;Username=postgres;Password=Pass2020!;");
    options.PublishMessage<ProductCreated>().ToRabbitExchange("product-exchange-pay");
    
    
});

builder.Host.UseResourceSetupOnStartup();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();

