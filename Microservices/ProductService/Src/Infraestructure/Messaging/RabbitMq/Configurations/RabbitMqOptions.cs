namespace Infraestructure.Messaging.RabbitMq.Configurations;

public class RabbitMqOptions
{
    public const string Section = "RabbitMq";
    public string HostName {get; set;} = string.Empty;
    public string UserName {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty;
}
