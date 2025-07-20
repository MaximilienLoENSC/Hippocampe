using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DbSet<Film> Films { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Pays> Pays { get; set; } = null!;
    public DbSet<Realisateur> Realisateurs { get; set; } = null!;
    public DbSet<Acteur> Acteurs { get; set; } = null!;
    public DbSet<Compositeur> Compositeurs { get; set; } = null!;
    public string DbPath { get; private set; }

    public DataContext()
    {
        // Path to SQLite database file
        DbPath = "ApiHippocampe.db";
    }

    // The following configures EF to create a SQLite database file locally
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Use SQLite as database
        options.UseSqlite($"Data Source={DbPath}");
        // Optional: log SQL queries to console
        //options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
    }
}
