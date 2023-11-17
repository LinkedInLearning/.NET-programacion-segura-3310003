using Microsoft.EntityFrameworkCore;

namespace datos_sensibles.Db;

public class WpmDbContext : DbContext
{
    public DbSet<Pet> Pets { get; set; }
}

public class Pet
{
    public int Id { get; set; }

    public string Name { get; set; }
}