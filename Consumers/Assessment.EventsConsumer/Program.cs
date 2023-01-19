using Assessment.Core.Helpers;
using Assessment.Core.Settings;
using Assessment.EventsConsumer.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var config = ConfigurationHelper.GenerateConsumerConfiguration();
var settings = config.GetConfiguration<ConsumerSettings>("ConsumerSetting");

var services = new ServiceCollection();
services.AddSingleton(config);
services.AddLogging(opt =>
{
    opt.AddConsole();
    opt.AddConfiguration(config);
});

services.AddSingleton(settings);
services.AddTransient<EventConsumer>();


var serviceProvider = services.BuildServiceProvider();
var consumer = serviceProvider.GetService<EventConsumer>();
consumer.StartWithWaiting();