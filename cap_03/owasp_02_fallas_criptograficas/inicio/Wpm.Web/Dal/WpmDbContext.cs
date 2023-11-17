﻿using Microsoft.EntityFrameworkCore;
using Wpm.Web.Domain;

namespace Wpm.Web.Dal;

public class WpmDbContext : DbContext
{
    public DbSet<Species> Species { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<Owner> Owners { get; set; }

    public WpmDbContext(DbContextOptions<WpmDbContext> options) : base(options)
    {    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Owner>().HasData(
                    new Owner() { Id = 1, Name = "Rodrigo" },
                    new Owner() { Id = 2, Name = "Leonardo" },
                    new Owner() { Id = 3, Name = "Alicia" },
                    new Owner() { Id = 4, Name = "Jon" },
                    new Owner() { Id = 5, Name = "Elmer" },
                    new Owner() { Id = 6, Name = "Sam" },
                    new Owner() { Id = 7, Name = "Jessica" }
                    );
        modelBuilder.Entity<Species>().HasData(
                    new Species() { Id = 1, Name = "Dog" },
                    new Species() { Id = 2, Name = "Cat" },
                    new Species() { Id = 3, Name = "Rabbit" }
                    );
        modelBuilder.Entity<Breed>().HasData(
                    new Breed() { Id = 1, Name = "Beagle", IdealMaxWeight = 20, SpeciesId = 1 },
                    new Breed() { Id = 2, Name = "Staffordshire Terrier", IdealMaxWeight = 25, SpeciesId = 1 },
                    new Breed() { Id = 3, Name = "British Shorthair", IdealMaxWeight = 20, SpeciesId = 2 },
                    new Breed() { Id = 4, Name = "Mixed", IdealMaxWeight = 30, SpeciesId = 2 },
                    new Breed() { Id = 5, Name = "Gray", IdealMaxWeight = 20, SpeciesId = 3 },
                    new Breed() { Id = 6, Name = "American White", IdealMaxWeight = 30, SpeciesId = 3 }
                    );
        modelBuilder.Entity<Pet>().HasData(
                    new Pet() { Id = 1, Name = "Gianni", Age = 10, Weight = 19, PhotoUrl= "/images/gianni.jpg", BreedId = 1 },
                    new Pet() { Id = 2, Name = "Nina", Age = 8, Weight = 24, PhotoUrl = "/images/nina.jpg", BreedId = 1},
                    new Pet() { Id = 3, Name = "Cati", Age = 8, Weight = 33.5m, PhotoUrl = "/images/cati.jpg", BreedId = 2 },
                    new Pet() { Id = 4, Name = "Cheshire", Age = 20, Weight = 12, BreedId = 3 },
                    new Pet() { Id = 5, Name = "Garfield", Age = 20, Weight = 12, BreedId = 4 },
                    new Pet() { Id = 6, Name = "Bugs", Age = 40, Weight = 25, BreedId = 5 },
                    new Pet() { Id = 7, Name = "Roger", Age = 35, Weight = 31, BreedId = 6 }
                    );
        modelBuilder.Entity("OwnerPet").HasData(
                    new[]
                    {
                                new { PetsId = 1, OwnersId = 1 },
                                new { PetsId = 1, OwnersId = 2 },
                                new { PetsId = 2, OwnersId = 1 },
                                new { PetsId = 2, OwnersId = 2 },
                                new { PetsId = 3, OwnersId = 1 },
                                new { PetsId = 3, OwnersId = 2 },
                                new { PetsId = 4, OwnersId = 3 },
                                new { PetsId = 5, OwnersId = 4 },
                                new { PetsId = 6, OwnersId = 5 },
                                new { PetsId = 6, OwnersId = 6 },
                                new { PetsId = 7, OwnersId = 7 },
                    }
                );
    }
}