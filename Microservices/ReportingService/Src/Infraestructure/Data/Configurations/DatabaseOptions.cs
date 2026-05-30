namespace Infraestructure.Data.Configurations;

public class DatabaseOptions
{
    public const string Section = "ConnectionStrings";
    public string Db_Report { get; set; } = null!;
}