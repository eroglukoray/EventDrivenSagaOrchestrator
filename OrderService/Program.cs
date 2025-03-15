using MassTransit;
using OrderService.Sagas;

var builder = WebApplication.CreateBuilder(args);

//add mass transit
//Bu kod, MassTransit'i RabbitMQ ile ba�lar ve mesajlar� publish edebilir hale getirir.

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter(); // Endpoint adlar�n� otomatik ayarlar

    x.AddSagaStateMachine<OrderStateMachine, OrderState>()
     .InMemoryRepository(); // Saga durumlar�n� bellek i�inde sakla

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
