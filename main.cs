using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using F1Simulator.Pilots;
using F1Simulator.Teams;
using F1Simulator.Cars;
using F1Simulator.Equipment;
using F1Simulator.Circuits;
using F1Simulator.Strategy;
using F1Simulator.Races;
using F1Simulator.RaceResults;
using F1Simulator.Weather;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using F1Simulator.Abilities;
// using F1Simulator.Ability;
using Microsoft.Extensions.DependencyInjection;

// using F1Simulator.Abilities;


// dotnet ef migrations add InitialCreate
//     dotnet ef database update

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<SimulatorContext>(options =>
            options.UseSqlite("Data Source=simulator.db")); // Sau altă configurație specifică
    }
}


public class SimulatorContext : DbContext
{
    public DbSet<Pilot> Pilots { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Cars> Cars { get; set; }
    public DbSet<Strategies> Strategies { get; set; }
    public DbSet<Circuits> Circuits { get; set; }
    public DbSet<Equipments> Equipments { get; set; }
    public DbSet<Races> Races { get; set; }
    public DbSet<RaceResults> RaceResults { get; set; }
    public DbSet<Weather> Weather { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=localdatabase.db")
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Pilot>()
            .HasKey(p => p.IDPilot);


        modelBuilder.Entity<Ability>()
            .HasKey(p => p.IDAbility);

        modelBuilder.Entity<Team>()
            .HasKey(t => t.IDTeam);
        
        modelBuilder.Entity<Team>()
            .HasOne(t => t.Pilot1)
            .WithMany()
            .HasForeignKey(t => t.IDPilot1);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.Pilot2)
            .WithMany()
            .HasForeignKey(t => t.IDPilot2);
        

        
 
        modelBuilder.Entity<Pilot>()
            .HasOne(p => p.Car)
            .WithMany() 
            .HasForeignKey(p => p.IDCars); 
        
        modelBuilder.Entity<Pilot>()
            .HasMany(p => p.Abilities)
            .WithOne(a => a.PilotA)
            .HasForeignKey(a => a.IDPilot);
        

        modelBuilder.Entity<Ability>()
            .ToTable("Abilities");
        

        modelBuilder.Entity<Weather>()
            .HasKey(w => w.IDWeather);
        modelBuilder.Entity<Weather>()
            .HasIndex(w => w.IDWeather)
            .IsUnique();
        
        modelBuilder.Entity<Cars>()
            .HasKey(c => c.IDCars);
        modelBuilder.Entity<Cars>()
            .HasIndex(c => c.IDCars)
            .IsUnique();
        
        // modelBuilder.Entity<Team>()
        //     .HasKey(t => t.IDTeam);
        // modelBuilder.Entity<Team>()
        //     .HasIndex(c => c.IDTeam)
        //     .IsUnique();

        
        
        modelBuilder.Entity<Strategies>()
            .HasKey(s => s.IDStrategy);
        modelBuilder.Entity<Strategies>()
            .HasIndex(c => c.IDStrategy)
            .IsUnique();
        
        modelBuilder.Entity<Circuits>()
            .HasKey(c => c.IDCircuits);
        modelBuilder.Entity<Circuits>()
            .HasIndex(c => c.IDCircuits)
            .IsUnique();
        
        modelBuilder.Entity<Equipments>()
            .HasKey(e => e.IDEquipments);
        modelBuilder.Entity<Equipments>()
            .HasIndex(c => c.IDEquipments)
            .IsUnique();
        
        modelBuilder.Entity<Races>()
            .HasKey(r => r.IDRace);
        modelBuilder.Entity<Races>()
            .HasIndex(c => c.IDRace)
            .IsUnique();
        
        modelBuilder.Entity<RaceResults>()
            .HasKey(rr => rr.IDRaceResult);
        modelBuilder.Entity<RaceResults>()
            .HasIndex(c => c.IDRaceResult)
            .IsUnique();

        
        
        
        
        
        // modelBuilder.Entity<Team>()
        //     .HasMany(t => t.Pilots)
        //     .WithOne(p => p.ATeam)
        //     .HasForeignKey(p => p.IDTeam);
        
        
        // modelBuilder.Entity<Pilot>()
        //     .HasOne(p => p.ATeam)
        //     .WithMany(t => t.Pilots)
        //     .HasForeignKey(p => p.IDTeam);
        
        // modelBuilder.Entity<Pilot>()
        //     .HasOne(p => p.ATeam)  // Relația de tipul "Un Pilot aparține unei echipe"
        //     .WithMany(t => t.Pilots)  // Relația de tipul "O echipă are mulți piloti"
        //     .HasForeignKey(p => p.IDTeam);
        
        
        
        
        
        
        
        
        
        modelBuilder.Entity<Team>()
            .HasOne(t => t.Strategy)
            .WithMany() 
            .HasForeignKey(t => t.IDStrategy) 
            .IsRequired(false);
        
        

        modelBuilder.Entity<Races>()
            .HasOne(r => r.Weather)
            .WithMany()
            .HasForeignKey(r => r.IDWeather)
            .OnDelete(DeleteBehavior.Restrict);
        



        base.OnModelCreating(modelBuilder);


        
        
        
        
        modelBuilder.Entity<Team>().HasData(
            new Team()
            {
                IDTeam = 4500, Budget = 145, Name = "Oracle Red Bull Racing", Manager_First_Name = "Christian", Manager_Last_Name = "Horner",
                IDPilot1 = 100, IDPilot2 = 101, IDStrategy = null
            },
            new Team()
            {
                IDTeam = 4501, Budget = 120, Name = "McLaren Formula 1 Team", Manager_First_Name = "Brown", Manager_Last_Name = "Zak",
                IDPilot1 = 102, IDPilot2 = 103, IDStrategy = null
            },
            new Team()
            {
                IDTeam = 4502, Budget = 157, Name = "Scuderia Ferrari", Manager_First_Name = "Frédéric", Manager_Last_Name = "Vasseur",
                IDPilot1 = 104, IDPilot2 = 105, IDStrategy = null
            },
            new Team()
            {
                IDTeam = 4503, Budget = 148, Name = "Mercedes-AMG PETRONAS F1 Team", Manager_First_Name = "Toto", Manager_Last_Name = "Wolff",
                IDPilot1 = 106, IDPilot2 = 107, IDStrategy = null
            },
            new Team()
            {
                IDTeam = 4504, Budget = 139, Name = "Aston Martin Aramco F1 Team", Manager_First_Name = "Mike", Manager_Last_Name = "Krack",
                IDPilot1 = 108, IDPilot2 = 109, IDStrategy = null
            },
            new Team()
            {
                IDTeam = 4505, Budget = 136, Name = "Visa Cash App RB Formula One Team", Manager_First_Name = "Jody", Manager_Last_Name = "Egginton",
                IDPilot1 = 110, IDPilot2 = 111, IDStrategy = null
            },
            new Team()
            {
                IDTeam = 4506, Budget = 126, Name = "MoneyGram Haas F1 Team", Manager_First_Name = "Ayao", Manager_Last_Name = "Komatsu",
                IDPilot1 = 112, IDPilot2 = 113, IDStrategy = null
            },
            new Team()
            {
                IDTeam = 4507, Budget = 134, Name = "BWT Alpine F1 Team", Manager_First_Name = "Oliver", Manager_Last_Name = "Oakes",
                IDPilot1 = 114, IDPilot2 = 115, IDStrategy = null
            },
            new Team()
            {
                IDTeam = 4508, Budget = 124, Name = "Williams Racing", Manager_First_Name = "James", Manager_Last_Name = "Vowles",
                IDPilot1 = 116, IDPilot2 = 117, IDStrategy = null
            },
            new Team()
            {
                IDTeam = 4509, Budget = 112, Name = "Stake F1 Team Kick Sauber", Manager_First_Name = "Alessandro", Manager_Last_Name = "Alunni Bravi",
                IDPilot1 = 118, IDPilot2 = 119, IDStrategy = null
            }
        );
        
        
        
        

        

        modelBuilder.Entity<Ability>().HasData(
            new Ability() { IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 95, IDPilot = 100},
            new Ability() { IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 91, IDPilot = 100},
            new Ability() { IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 84, IDPilot = 100},
            new Ability() { IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 97, IDPilot = 100},
            new Ability() { IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 93, IDPilot = 100},
            new Ability() { IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 91, IDPilot = 100},
            new Ability() { IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 87, IDPilot = 100},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 911, IDPilot = 101}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 86, IDPilot = 101}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 93, IDPilot = 101}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 89, IDPilot = 101},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 94, IDPilot = 101},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 81, IDPilot = 101}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 92, IDPilot = 101},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 98, IDPilot = 102}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 84, IDPilot = 102}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 94, IDPilot = 102}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 99, IDPilot = 102},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 97, IDPilot = 102}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 98, IDPilot = 102}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 89, IDPilot = 102},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 89, IDPilot = 103}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 92, IDPilot = 103}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 95, IDPilot = 103}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 90, IDPilot = 103},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 97, IDPilot = 103}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 96, IDPilot = 103}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 95, IDPilot = 103},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 98, IDPilot = 104}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 88, IDPilot = 104}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 97, IDPilot = 104}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 85, IDPilot = 104},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 97, IDPilot = 104}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 99, IDPilot = 104}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 95, IDPilot = 104},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 94, IDPilot = 105}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 92, IDPilot = 105}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 99, IDPilot = 105}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 85, IDPilot = 105},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 92, IDPilot = 105}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 97, IDPilot = 105}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 84, IDPilot = 105},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 87, IDPilot = 106}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 84, IDPilot = 106}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 98, IDPilot = 106}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 93, IDPilot = 106},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 81, IDPilot = 106}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 97, IDPilot = 106}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 92, IDPilot = 106},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 97, IDPilot = 107}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 82, IDPilot = 107}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 94, IDPilot = 107}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 88, IDPilot = 107},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 81, IDPilot = 107}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 99, IDPilot = 107}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 91, IDPilot = 107},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 91, IDPilot = 108}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 79, IDPilot = 108}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 96, IDPilot = 108}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 78, IDPilot = 108},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 82, IDPilot = 108}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 81, IDPilot = 108}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 87, IDPilot = 108},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 86, IDPilot = 109}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 73, IDPilot = 109}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 99, IDPilot = 109}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 80, IDPilot = 109},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 75, IDPilot = 109}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 80, IDPilot = 109}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 83, IDPilot = 109},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 82, IDPilot = 110}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 64, IDPilot = 110}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 90, IDPilot = 110}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 54, IDPilot = 110},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 79, IDPilot = 110}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 75, IDPilot = 110}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 83, IDPilot = 110},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 85, IDPilot = 111}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 69, IDPilot = 111}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 94, IDPilot = 111}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 58, IDPilot = 111},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 71, IDPilot = 111}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 62, IDPilot = 111}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 73, IDPilot = 111},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 81, IDPilot = 112}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 62, IDPilot = 112}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 91, IDPilot = 112}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 43, IDPilot = 112},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 49, IDPilot = 112}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 52, IDPilot = 112}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 82, IDPilot = 112},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 87, IDPilot = 113}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 65, IDPilot = 113}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 86, IDPilot = 113}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 54, IDPilot = 113},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 42, IDPilot = 113}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 46, IDPilot = 113}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 55, IDPilot = 113},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 82, IDPilot = 114}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 69, IDPilot = 114}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 94, IDPilot = 114}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 59, IDPilot = 114},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 65, IDPilot = 114}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 41, IDPilot = 114}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 52, IDPilot = 114},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 85, IDPilot = 115}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 61, IDPilot = 115}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 93, IDPilot = 115}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 73, IDPilot = 115},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 59, IDPilot = 115}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 63, IDPilot = 115}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 92, IDPilot = 115},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 91, IDPilot = 116}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 62, IDPilot = 116}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 83, IDPilot = 116}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 46, IDPilot = 116},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 55, IDPilot = 116}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 62, IDPilot = 116}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 77, IDPilot = 116},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 74, IDPilot = 117}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 61, IDPilot = 117}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 97, IDPilot = 117}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 58, IDPilot = 117},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 52, IDPilot = 117}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 48, IDPilot = 117}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 59, IDPilot = 117},
            
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Cornering skills", abilityValue = 79, IDPilot = 118}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Race strategy", abilityValue = 78, IDPilot = 118}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Physical resistance", abilityValue = 86, IDPilot = 118}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Car overtaking", abilityValue = 58, IDPilot = 118},
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Tire management", abilityValue = 43, IDPilot = 118}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Starting reaction", abilityValue = 41, IDPilot = 118}, 
            new Ability(){ IDAbility = Guid.NewGuid(), abilityName = "Adaptability to weather conditions", abilityValue = 78, IDPilot = 118}
        );
        
        
        
        modelBuilder.Entity<Cars>().HasData(
            new Cars
            {
                IDCars = 1800, model = "Ferrari SF-24 #1", speedMax = 340, accTime = 2.5f, hp = 1050, mass = 798,
                fuel = "Benzine", tiresPressure = 22
            },
            new Cars { 
                IDCars = 1801, model = "Ferrari SF-24 #2", speedMax = 340, accTime = 2.5f, hp = 1050, mass = 798, 
                fuel = "Benzine", tiresPressure = 22
            },
            new Cars
            {
                IDCars = 1802, model = "Mercedes W15 #1", speedMax = 338, accTime = 2.6f, hp = 1030, mass = 801, 
                fuel = "Benzine", tiresPressure = 21
            },
            new Cars
            {
                IDCars = 1803, model = "Mercedes W15 #2", speedMax = 338, accTime = 2.6f, hp = 1030, mass = 801, 
                fuel = "Benzine", tiresPressure = 21
            },
            new Cars
            {
                IDCars = 1804, model = "Red Bull RB20 #1", speedMax = 342, accTime = 2.4f, hp = 1060, mass = 800, 
                fuel = "Benzine", tiresPressure = 23
            },
            new Cars
            {
                IDCars = 1805, model = "Red Bull RB20 #2", speedMax = 342, accTime = 2.4f, hp = 1060, mass = 800, 
                fuel = "Benzine", tiresPressure = 23
            },
            new Cars
            {
                IDCars = 1806, model = "McLaren MCL60 #1", speedMax = 336, accTime = 2.7f, hp = 1010, mass = 797, 
                fuel = "Benzine", tiresPressure = 21
            },
            new Cars
            {
                IDCars = 1807, model = "McLaren MCL60 #2", speedMax = 336, accTime = 2.7f, hp = 1010, mass = 797, 
                fuel = "Benzine", tiresPressure = 21
            },
            new Cars
            {
                IDCars = 1808, model = "Alpine A524 #1", speedMax = 335, accTime = 2.8f, hp = 1025, mass = 799, 
                fuel = "Benzine", tiresPressure = 21
            },
            new Cars
            {
                IDCars = 1809, model = "Alpine A524 #2", speedMax = 335, accTime = 2.8f, hp = 1025, mass = 799, 
                fuel = "Benzine", tiresPressure = 21
            },
            new Cars
            {
                IDCars = 1810, model = "Aston Martin AMR24 #1", speedMax = 337, accTime = 2.6f, hp = 1040, mass = 801, 
                fuel = "Benzine", tiresPressure = 23
            },
            new Cars
            {
                IDCars = 1811, model = "Aston Martin AMR24 #2", speedMax = 337, accTime = 2.6f, hp = 1040, mass = 801, 
                fuel = "Benzine", tiresPressure = 23
            },
            new Cars
            {
                IDCars = 1812, model = "Williams FW46 #1", speedMax = 334, accTime = 2.9f, hp = 1000, mass = 801, 
                fuel = "Benzine", tiresPressure = 20
            },
            new Cars
            {
                IDCars = 1813, model = "Williams FW46 #2", speedMax = 334, accTime = 2.9f, hp = 1000, mass = 801, 
                fuel = "Benzine", tiresPressure = 20
            },
            new Cars
            {
                IDCars = 1814, model = "RB-Honda RBPT RB24 #1", speedMax = 343, accTime = 2.2f, hp = 1070, mass = 797, 
                fuel = "Benzine", tiresPressure = 21
            },
            new Cars
            {
                IDCars = 1815, model = "RB-Honda RBPT RB24 #2", speedMax = 343, accTime = 2.2f, hp = 1070, mass = 797, 
                fuel = "Benzine", tiresPressure = 21
            },
            new Cars
            {
                IDCars = 1816, model = "Haas VF-24 #1", speedMax = 330, accTime = 3.2f, hp = 970, mass = 801, 
                fuel = "Benzine", tiresPressure = 18
            },
            new Cars
            {
                IDCars = 1817, model = "Haas VF-24 #2", speedMax = 330, accTime = 3.2f, hp = 970, mass = 801, 
                fuel = "Benzine", tiresPressure = 18
            },
            new Cars
            {
                IDCars = 1818, model = "Kick Sauber C44 #1", speedMax = 334, accTime = 3.0f, hp = 1005, mass = 800, 
                fuel = "Benzine", tiresPressure = 20
            },
            new Cars
            {
                IDCars = 1819, model = "Kick Sauber C44 #2", speedMax = 334, accTime = 3.0f, hp = 1005, mass = 800, 
                fuel = "Benzine", tiresPressure = 20
            }
            
        );
        
        
        
        
        
        
        modelBuilder.Entity<Pilot>().HasData(
            new Pilot()
            {
                IDPilot = 100, Age = 27, Experience = 9, IDCars = 1804, First_Name = "Max", Last_Name= "Verstappen", IDTeam = 4500
            },   
            new Pilot()
            {
                IDPilot = 101, Age = 34, Experience = 13, IDCars = 1805, First_Name = "Sergio", Last_Name = "Perez", IDTeam = 4500
            },
            //98 84 94 99 97 98 89
            new Pilot()
            {  
                IDPilot = 102, Age = 25, Experience = 5, IDCars = 1806, First_Name = "Lando", Last_Name= "Norris", IDTeam = 4501
            },
            new Pilot()
            {
                //89 92 95 90 97 96 95
                IDPilot = 103, Age = 23, Experience = 4, IDTeam = 4501, IDCars = 1807, First_Name = "Oscar", Last_Name = "Piastri"
            },
            new Pilot()
            {
                IDPilot = 104, age = 26, experience = 6, IDTeam = 4502, IDCars = 1800, firstName = "Charles", lastName = "Leclerc"
            },
            new Pilot()
            {
                //94 92 99 85 92 97 84
                IDPilot = 105, Age = 30, Experience = 9, IDTeam = 4502, IDCars = 1801, First_Name = "Carlos", Last_Name = "Sainz"
            },
            new Pilot()
            {
                //87 84 98 93 81 95 92
                IDPilot = 106, age = 39, experience = 17, IDTeam = 4503, IDCars = 1802, firstName = "Lewis", lastName = "Hamilton"
            },
            new Pilot()
            {
                //97 82 94 88 81 99 91
                IDPilot = 107, age = 26, experience = 9, IDTeam = 4503, IDCars = 1803, firstName = "George", lastName = "Russell"
            },
            new Pilot()
            {
                //91 79 96 78 82 84 87
                IDPilot = 108, age = 43, experience = 19, IDTeam = 4504, IDCars = 1810, firstName = "Fernando", lastName = "Alonso"
            },
            new Pilot()
            {
                //86 73 99 80 75 79 81
                IDPilot = 109, age = 27, experience = 6, IDTeam = 4504, IDCars = 1811, firstName = "Lance", lastName = "Stroll"
            },
            new Pilot()
            {
                //82 64 90 54 79 75 83
                IDPilot = 110, age = 24, experience = 5, IDTeam = 4505, IDCars = 1814, firstName = "Yuki", lastName = "Tsunoda"
            },
            new Pilot()
            {
                // 85 69 94 58 71 62 73
                IDPilot = 111, age = 35, experience = 12, IDTeam = 4505, IDCars = 1815, firstName = "Daniel", lastName = "Ricciardo"
            },
            new Pilot()
            {
                // 81 62 91 43 49 52 67
                IDPilot = 112, age = 37, experience = 16, IDTeam = 4506, IDCars = 1816, firstName = "Nico", lastName = "Hulkenberg"
            },
            new Pilot()
            {
                // 87 65 86 54 42 46 55
                IDPilot = 113, age = 32, experience = 10, IDTeam = 4506, IDCars = 1817, firstName = "Kevin", lastName = "Magnussen"
            },
            new Pilot()
            {
                //82 69 94 59 65 41 52
                IDPilot = 114, age = 28, experience = 9, IDTeam = 4507, IDCars = 1808, firstName = "Pierre", lastName = "Gasly"
            },
            new Pilot()
            {
                //85 61 92 52 73 59 63
                IDPilot = 115, age = 28, experience = 7, IDTeam = 4507, IDCars = 1809, firstName = "Esteban", lastName = "Ocon"
            },
            new Pilot()
            {
                //92 62 83 46 55 62 77
                IDPilot = 116, age = 28, experience = 10, IDTeam = 4508, IDCars = 1812, firstName = "Alexander", lastName = "Albon"
            },
            new Pilot()
            {
                //74 61 97 58 52 48 50
                IDPilot = 117, age = 21, experience = 3, IDTeam = 4508, IDCars = 1813, firstName = "Franco", lastName = " Colapinto"
            },
            new Pilot()
            {
                //79 78 86 58 59 41 78
                IDPilot = 118, age = 35, experience = 12, IDTeam = 4509, IDCars = 1818, firstName = "Valtteri", lastName = " Bottas"
            } 
        );
        
        
        
        
        

        
        modelBuilder.Entity<Strategies>().HasData(
            new Strategies()
            {
                IDStrategy = 10000, strategyType = "Conservative", points = 55, description = "Focuses on maintaining the car in optimal condition and avoiding overuse of resources"
            },
            new Strategies()
            {
                IDStrategy = 10001, strategyType = "Aggressive", points = 86, description = "Focuses on maximizing short-term performance, even at the risk of higher wear and potential failure"
            },
            new Strategies()
            {
                IDStrategy = 10002, strategyType = "Aerodynamic Enhancement", points = 68, description = "Prioritizes improving the car’s aerodynamics to reduce air resistance and increase top speed"
            },
            new Strategies()
            { 
                IDStrategy = 10003, strategyType = "Fuel Optimization", points = 32, description = "Focuses on optimizing fuel consumption to maximize endurance and reduce the need for frequent pit stops"
            },
            new Strategies()
            {
                IDStrategy = 10004, strategyType = "Adaptive", points = 61, description = "Adapts to varying weather conditions and track states to maximize performance under changing circumstances"
            }
        );

//id, length, curbsnumber, difficulty, laps, name
        modelBuilder.Entity<Circuits>().HasData(
            new Circuits()
            {
                IDCircuits = 10, length = 5.7f, curbsNumber = 8, difficulty = 7, laps = 53, name = "Monza"
            },
            new Circuits()
            {
                IDCircuits = 11, length = 3.33f, curbsNumber = 19, difficulty = 9, laps = 78, name = "Monaco"
            },
            new Circuits()
            {
                IDCircuits = 12, length = 5.8f, curbsNumber = 18, difficulty = 8, laps = 53, name = "Suzuka Circuit"
            },
            new Circuits()
            {
                IDCircuits = 13, length = 6.14f, curbsNumber = 27, difficulty = 9, laps = 50, name = "Jeddah Corniche Circuit"
            },
            new Circuits()
            {
                IDCircuits = 14, length = 5.41f, curbsNumber = 19, difficulty = 5, laps = 57, name = "Miami International Autodrome"
            },
            new Circuits()
            {
                IDCircuits = 15, length = 7.0f, curbsNumber = 19, difficulty = 4, laps = 44, name = "Circuit de Spa-Francorchamps"
            },
            new Circuits()
            {
                IDCircuits = 16, length = 4.3f, curbsNumber = 17, difficulty = 4, laps = 71, name = "Autódromo Hermanos Rodríguez"
            },
            new Circuits()
            {
                IDCircuits = 17, length = 6.2f, curbsNumber = 17, difficulty = 5, laps = 50, name = "Las Vegas Strip Circuit"
            },
            new Circuits()
            {
                IDCircuits = 18, length = 5.28f, curbsNumber = 16, difficulty = 6, laps = 58, name = "Yas Marina Circuit"
            },
            new Circuits()
            {
                IDCircuits = 19, length = 4.3f, curbsNumber = 15, difficulty = 7, laps = 71, name = "Autódromo José Carlos Pace"
            },
            new Circuits()
            {
                IDCircuits = 20, length = 4.9f, curbsNumber = 19, difficulty = 8, laps = 63, name = "Autodromo Internazionale Enzo e Dino Ferrari"
            }
        );
        base.OnModelCreating(modelBuilder);
        
    }

}

namespace F1Simulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (var context = new SimulatorContext())
            {
                var teams = context.Teams.ToList();
                // teams = context.Teams;

                var pilots = context.Pilots;
                    // .Include(p => p.Abilities);

                var cars = context.Cars;
                var strategies = context.Strategies;
                    // .ToList();
                var circuits = context.Circuits;
                    // .ToList();

                    //sunny cloudy  rainy snowy

                Weather.Weather weatherConditions = new Weather.Weather();
                    
                Console.WriteLine("Select the weather: ");
                
                Console.Write("Type of weather (Sunny / Cloudy / Rainy / Snowy): ");
                string condition = Console.ReadLine();
                Console.WriteLine("");
                
                Console.Write("Humidity level: ");
                double hum = Double.Parse(Console.ReadLine());
                Console.Write("Temperature (\u00b0C): ");
                double temp = Double.Parse(Console.ReadLine());
                Console.WriteLine("");

                int i = 1;
                foreach (var strategy in strategies)
                {
                    Console.WriteLine($"Strategy number {i}");
                    Console.WriteLine($"Name of the strategy: {strategy.strategyType}");
                    Console.WriteLine($"Description of the strategy: {strategy.description}");
                    Console.WriteLine($"Points of this strategy: {strategy.points}");
                    Console.WriteLine("");
                }

            }
        }
    }
}




namespace F1Simulator.Cars
{
    public class Cars
    {
        public int IDCars;
        private string Model;
        private int SpeedMax;
        private float AccTime; //timp de accelerare
        private int HP; //cai putere
        private float Mass; //greutate masina
        private string Fuel; //carburant
        private double TiresPressure; //presiune pneuri
        // public Pilot APilot;

        public Cars()
        {
            IDCars = new int();
            Model = "Unknown";
            SpeedMax = 0;
            AccTime = 0f;
            HP = 0;
            Mass = 0f;
            Fuel = null;
            TiresPressure = 0;
            //APilot = new Pilot();
        }

        public Cars(int id, string model, int speedMax, float accTime, int hp, float mass, string fuel, double tiresPressure)
        {
            if (!CheckIfLegal())
            {
                throw new ArgumentException("This car does not have the minimum mass that is legal");
            }
            IDCars = id;
            Model = model;
            SpeedMax = speedMax;
            AccTime = accTime;
            HP = hp;
            Mass = mass;
            Fuel = fuel;
            TiresPressure = tiresPressure;
            // APilot = pilot;
        }

        public int hp
        {
            get { return HP; }
            set { HP = value; }
        }

        public float mass
        {
            get { return Mass; }
            set { Mass = value; }
        }

        public string fuel
        {
            get { return Fuel; }
            set { Fuel = value; }
        }

        public double tiresPressure
        {
            get { return TiresPressure; }
            set { TiresPressure = value; }
        }

        // public Pilot pilot
        // {
        //     get { return APilot; }
        //     set { APilot = value; }
        // }

        public string model
        {
            get { return Model; }
            set { Model = value; }
        }
        
        public int speedMax
        {
            get { return SpeedMax; }
            set { SpeedMax = value; }
        }

        public float accTime
        {
            get { return AccTime; }
            set { AccTime = value; }
        }



        public bool CheckIfLegal()
        {
            if (Mass < 798)
            {
                return false;
            }

            return true;
        }

        public float GetPerformance()
        {
            return HP / Mass;
        }
    }
}







namespace F1Simulator.Abilities
{
    public class Ability
    {
        public Guid IDAbility { get; set; }
        public string AbilityName { get; set; }
        public int AbilityValue { get; set; }

        public int IDPilot;
        public Pilot PilotA;
        public Ability()
        {
            IDAbility = new Guid();
            AbilityName = "";
            AbilityValue = 0;
        }

        public Ability(string abilityName, int abilityValue)
        {
            AbilityName = abilityName;
            AbilityValue = abilityValue;
        }

        public string abilityName
        {
            get { return AbilityName; }
            set { AbilityName = value; }
        }

        public int abilityValue
        {
            get { return AbilityValue; }
            set { AbilityValue = value; }
        }
    }
}


namespace F1Simulator.Pilots
{
    public class Pilot 
    {
        public int IDPilot;
        public string First_Name;
        public string Last_Name;
        public int Age;
        public int Experience;
        
        public int IDTeam;
        public Team ATeam;
        
        public Cars.Cars Car;
        public int IDCars;
        public ICollection<Ability> Abilities { get; set; } = new List<Ability>();
        

        // public List<Ability.Ability> abilities
        // {
        //     get { return Abilities; }
        //     set { Abilities = value; }
        // }

        public int age
        {
            get { return Age; }
            set { Age = value; }
        }

        public int experience
        {
            get { return Experience; }
            set { Experience = value; }
        }

        

        public int idcar
        {
            get { return IDCars; }
        }

        public int idpilot
        {
            get { return IDPilot; }
            set { IDPilot = value; }
        }

        public string firstName
        {
            get { return First_Name; }
            set { First_Name = value; }
        }
        
        public string lastName
        {
            get { return Last_Name; }
            set { Last_Name = value; }
        }

        public void SetFirstName(string name)
        {
            First_Name = name;
        }

        public void SetLastName(string name)
        {
            Last_Name = name;
        }

        public void SetAge(int age)
        {
            Age = age;
        }

        public void SetExperience(int experience)
        {
            Experience = experience;
        }


        public Pilot()
        {
            IDPilot = new int();
            First_Name = "Unknown";
            Last_Name = "Unknown";
            Age = 0;
            Experience = 0;
            Abilities = new List<Ability>();
            Car = null;
        }

        public Pilot(int id, string firstName, string lastName, int age, int experience, Team aTeam,
            Dictionary<string, int> abilities, Cars.Cars car, Dictionary<string, int> abilitiesList)
        {
            IDPilot = id;
            First_Name = firstName;
            Last_Name = lastName;
            Age = age;
            Experience = experience;
            ATeam = aTeam;
            Abilities = Abilities;
            Car = car;
        }

        public int GetExperience()
        {
            return Experience;
        }

        // public Ability abilities
        // {
        //     get { return Abilities; }
        //     set { Abilities = value; }
        // }
        

        public int SumAbilities()
        {
            int sum = 0;
            foreach (var ability in Abilities)
            {
                sum += ability.abilityValue;
            }

            return sum;
        }

        public int CompareTo(Pilot Pilot2)
        {
            if (Pilot2 == null) return 1;
            if (this.SumAbilities() > Pilot2.SumAbilities()) return 1;
            return -1;
        }

        // public bool Equals(object obj) => Equals(obj as Pilot);
        // public override int GetHashCode() => (First_Name, Experience).GetHashCode();
    }
}

namespace F1Simulator.Teams
{
    public class Team 
    {
        public int IDTeam;
        public string Name;
        
        public int IDPilot1;
        public Pilot Pilot1;
        public int IDPilot2;
        public Pilot Pilot2;
        
        public int Budget;
        public string Manager_First_Name;
        public string Manager_Last_Name;
        
        public Strategies Strategy;
        public int? IDStrategy;

        // public ICollection<Pilot> pilots
        // {
        //     get { return Pilots; }
        //     set { Pilots = value; }
        // }
        

        public Team()
        {
            IDTeam = new int();
            IDPilot1 = 0;
            IDPilot2 = 0;
            Pilot1 = new Pilot();
            Pilot2 = new Pilot();
            Budget = 0;
            Manager_First_Name = "Unknown";
            Manager_Last_Name = "Unknown";
            Strategy = new Strategies();
            Name = "Unknown";
            IDStrategy = 0;
        }

        public Team(int id, Pilot pilot1, Pilot pilot2, int budget, string managerFirstName, string managerLastName, Strategies strategies, string name, int idStrategy)
        {
            IDTeam = id;
            Pilot1 = pilot1;
            Pilot2 = pilot2;
            Budget = budget;
            Manager_First_Name = managerFirstName;
            Manager_Last_Name = managerLastName;
            Strategy = strategies;
            Name = name;
            IDStrategy = idStrategy;
        }

        public Strategies strategy
        {
            get { return Strategy; }
            set { Strategy = value; }
        }

        public int idteam
        {
            get { return IDTeam; }
            set { IDTeam = value; }
        }

        public int budget
        {
            get { return Budget; }
            set { Budget = value; }
        }

        public string managerFirstName
        {
            get { return Manager_First_Name; }
            set { Manager_First_Name = value; }
        }
        
        public string managerLastName
        {
            get { return Manager_Last_Name; }
            set { Manager_Last_Name = value; }
        }

        public Strategies strategies
        {
            get { return Strategy; }
            set { Strategy = value; }
        }

        public double TeamPerformance()
        {
            return Pilot1.experience * Pilot1.experience + Pilot2.experience * Pilot2.SumAbilities();
        }

        
        
        
        // System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        // {
        //     return GetEnumerator();
        // }
        // public IEnumerator<Pilot> GetEnumerator()
        // {
        //     return Pilots.GetEnumerator();
        // }
    }
}





namespace F1Simulator.Circuits
{
    public class Circuits
    {
        public int IDCircuits;
        private float Length;
        private int CurbsNumber;
        private int Difficulty; //nivel de dificultate
        private int Laps;
        private string Name;

        public float length
        {
            get { return Length; }
            set { Length = value; }
        }

        public int curbsNumber
        {
            get { return CurbsNumber; }
            set { CurbsNumber = value; }
        }

        public int difficulty
        {
            get { return Difficulty; }
            set { Difficulty = value; }
        }

        public int laps
        {
            get { return Laps; }
            set { Laps = value; }
        }

        public string name
        {
            get { return Name; }
            set { Name = value; }
        }
        

        public Circuits()
        {
            IDCircuits = new int();
            Length = 0;
            CurbsNumber = 0;
            Difficulty = 0;
            Laps = 0;
            Name = "Unknown";
        }

        public Circuits(int id, int length, int curbsNumber, int difficulty, int laps, string name)
        {
            IDCircuits = id;
            Length = length;
            CurbsNumber = curbsNumber;
            Difficulty = difficulty;
            Laps = laps;
            Name = name;
        }
    }
}





namespace F1Simulator.Strategy
{
    public class Strategies
    {
        public int IDStrategy;
        private string StrategyType;
        private int Points;
        private string Description;

        public Strategies()
        {
            IDStrategy = new int();
            StrategyType = "Unknown";
            Points = 0;
            Description = "Unknown";
        }

        public Strategies(int id, string strategyType, int points, string description)
        {
            IDStrategy = id;
            StrategyType = strategyType;
            Points = points;
            Description = description;
        }

        public int points
        {
            get { return Points; }
            set { Points = value; }
        }

        public string strategyType
        {
            get { return StrategyType; }
            set { StrategyType = value; }
        }

        public string description
        {
            get { return Description; }
            set { Description = value; }
        }
    }
}















namespace F1Simulator.Races
{
    public class Races
    {
        public Guid IDRace;
        private List<Pilot> Pilots;
        private Circuits.Circuits Circuit;
        public int IDWeather;
        public Weather.Weather Weather;
        private List<Team> Teams;

        public Races()
        {
            IDRace = new Guid();
            Pilots = new List<Pilot>();
            Circuit = new Circuits.Circuits();
            Weather = new Weather.Weather();
        }

        public Races(List<Pilot> pilots, Circuits.Circuits circuit, Weather.Weather weather)
        {
            IDRace = new Guid();
            Pilots = pilots;
            Circuit = circuit;
            Weather = weather;
        }
        
        public List<Team> teams
        {
            get { return Teams; }
            set { Teams = value; }
        }
        
        public List<Team> SimulateRace()
        {
            List<Tuple<Team, double>> results = new List<Tuple<Team, double>>();
            
            foreach (Team team in this.teams)
            {
                double basePerformance = team.TeamPerformance();
                int strategyBoost = team.strategy.points;
                int weatherImpact = this.Weather.CalculatePerformanceLevel();
                
                string weatherCondition = this.Weather.condition;
                Strategies teamStrategy = team.strategy;
                double strategyWeatherImpact = 0;
                
// 10000 = conservative ; 10001 = aggressive ; 10002 = aerodynamic enhancement ; 10003 = fuel optimization ; 10004 = adaptive

                if (weatherCondition == "Rainy" || weatherCondition == "Snowy" && teamStrategy.IDStrategy == 10001)
                {
                    strategyWeatherImpact = -12.5;
                }

                if (weatherCondition == "Rainy" || weatherCondition == "Snowy" && teamStrategy.IDStrategy == 10002)
                {
                    strategyWeatherImpact = -7.5;
                }

                if (weatherCondition == "Cloudy" && teamStrategy.IDStrategy == 10003)
                {
                    strategyWeatherImpact = -2.1;
                }

                if (weatherCondition == "Sunny" && teamStrategy.IDStrategy == 10004)
                {
                    strategyWeatherImpact = -0.7;
                }

                
                double finalPerformance = basePerformance + strategyBoost + weatherImpact + strategyWeatherImpact;
                
                results.Add(new Tuple<Team, double>(team, finalPerformance));
            }

            var RaceResult = results.OrderByDescending(result => result.Item2).ToList();
            
            List<Team> orderedTeams = RaceResult.Select(result => result.Item1).ToList();

            return orderedTeams;

        }
    }
}

namespace F1Simulator.Weather
{
    public class Weather
    {
        public int IDWeather;
        private string Condition;
        private double Temperature;
        private double Humidity;
// condition temperature humidity
        public Weather()
        {
            IDWeather = 0;
            Condition = "Unknown";
            Temperature = 0.0;
            Humidity = 0.0;
        }

        public Weather(int id, string condition, double temperature, double humidity, List<Team> team)
        {
            IDWeather = id;
            Condition = condition;
            Temperature = temperature;
            Humidity = humidity;
        }

        public string condition
        {
            get { return Condition; }
            set { Condition = value; }
        }
        
        public double temperature
        {
            get { return Temperature; }
            set { Temperature = value; }
        }

        public double humidity
        {
            get { return Humidity; }
            set { Humidity = value; }
        }
        
        public int CalculatePerformanceLevel()
        {
            int baseImpact = Condition switch
            {
                "Sunny" => 15,
                "Cloudy" => 8,
                "Rainy" => -2,
                "Snowy" => -12
            };

            int temperatureImpact = Temperature > 20 ? 40 : Temperature < 10 ? -5 : 25;

            int humidityImpact = Humidity > 70 ? 15 : Humidity < 30 ? 70 : 50;

            int performanceLevel = baseImpact + temperatureImpact + humidityImpact;

            return performanceLevel;
        }
        
    }
}


/*
public class DataImporter
{
    private readonly DbContext _context;

    public DataImporter(DbContext context)
    {
        _context = context;
    }
    
    // IDPilot   First_Name  Last_Name  Age  Experience  IDTeam  IDCars
    public async Task ImportPilots(string filePath)
    {
        var pilots = new List<Pilot>();
        foreach (var line in File.ReadLines(filePath))
        {
            var values = line.Split(',');

            if (values.Length >= 5)
            {
                var pilot = new Pilot(int.Parse(values[0]), values[1], values[2], int.Parse(values[3]), int.Parse(values[4]),
                    int.Parse(values[5]), int.Parse(values[6]))

                pilots.Add(pilot);
            }
        }
        _context.Pilots.AddRange(piloti);
        await _context.SaveChangesAsync();
    }
} */

// namespace F1Simulator.Ability
// {
//     public class Ability
//     {
//         public Guid IDAbilities;
//         private string AbilityName;
//         private int AbilityValue;
//
//         public Ability()
//         {
//             IDAbilities = new Guid();
//             AbilityName = "";
//             AbilityValue = 0;
//         }
//
//         public Ability(string abilityName, int abilityValue)
//         {
//             AbilityName = abilityName;
//             AbilityValue = abilityValue;
//         }
//
//         public string abilityName
//         {
//             get { return AbilityName; }
//             set { AbilityName = value; }
//         }
//
//         public int abilityValue
//         {
//             get { return AbilityValue; }
//             set { AbilityValue = value; }
//         }
//     }
// }

namespace F1Simulator.RaceResults
{
    public class RaceResults
    {
        public Guid IDRaceResult;
        private List<Pilot> Pilots;
        private Dictionary<Pilot, double> FinalTime; //timp final pentru fiecare pilot

        public RaceResults()
        {
            IDRaceResult = new Guid();
            Pilots = new List<Pilot>();
            FinalTime = new Dictionary<Pilot, double>();
        }

        public RaceResults(List<Pilot> pilots, Dictionary<Pilot, double> finalTime)
        {
            IDRaceResult = new Guid();
            Pilots = pilots;
            FinalTime = finalTime;
        }
        

        public void AddResult(Pilot pilot, double timer)
        {
            if (Pilots.Contains(pilot))
            {
                FinalTime[pilot] = timer;
            }
        }
    }
}

namespace F1Simulator.Equipment
{
    public class Equipments
    {
        public Guid IDEquipments;
        private string Type;
        private float MassValue;
        private double PerformanceValue;

        public Equipments()
        {
            IDEquipments = new Guid();
            Type = "Unknown";
            MassValue = 0;
            PerformanceValue = 0;
        }

        public Equipments(string type, float massValue, double performanceValue)
        {
            IDEquipments = new Guid();
            Type = type;
            MassValue = massValue;
            PerformanceValue = performanceValue;
        }
    }
}
