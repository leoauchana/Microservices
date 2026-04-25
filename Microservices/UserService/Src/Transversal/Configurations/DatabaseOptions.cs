namespace Transversal.Configurations;

public class DatabaseOptions
{
    public const string Section = "Db_User";
    public string ConnectionString { get; set; } = string.Empty;
}
