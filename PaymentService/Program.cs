using MassTransit;
using Shared.Consumers;

var builder = WebApplication.CreateBuilder(args);

//add mass transit and rabbitmq


builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter(); // Endpoint adlarýný otomatik ayarlar

    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("order-created-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
