using TestMQ.API.Services;
using TestMQ.API.Services.Contect;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IMessageProducer, MessageProducer>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();