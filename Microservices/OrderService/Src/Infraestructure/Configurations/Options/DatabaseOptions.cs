namespace Infraestructure.Configurations.Options;

public class DatabaseOptions
{
    public const string Section = "ConnectionStrings";
    public string Db_Order { get; set; } = string.Empty;
}
