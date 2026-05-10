namespace Infraestructure.Configurations;

public class DatabaseOptions
{
    public const string Section = "ConnectionStrings";
    public string Db_User { get; set; } = string.Empty;
}
