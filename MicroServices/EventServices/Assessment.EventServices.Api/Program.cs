using Assessment.Core.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(p =>
{
    p.UseCamelCasing(true);
});

var producerConfiguration = new ProducerConfig();
builder.Configuration.Bind("ProducerConfiguration", producerConfiguration);
producerConfiguration.ClientId = Dns.GetHostName();
builder.Services.AddSingleton(producerConfiguration);
builder.Services.AddLogging();
var producers = Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .Where(x => !x.IsAbstract && x.IsClass && typeof(IProducer).IsAssignableFrom(x)).ToList();
producers.ForEach(x => builder.Services.AddScoped(x));

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
