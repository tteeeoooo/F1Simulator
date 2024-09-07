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
using F1Simulator.Abilities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
// using F1Simulator.Ability;
using Microsoft.Extensions.DependencyInjection;



// public class Startup
// {
//     public IConfiguration Configuration { get; }
//
//     public Startup(IConfiguration configuration)
//     {
//         Configuration = configuration;
//     }
//     
//     public void ConfigureServices(IServiceCollection services)
//     {
//         services.AddDbContext<SimulatorContext>(options =>
//             options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
//     }
// }



public class SimulatorContext : DbContext
{
    public SimulatorContext() { }

    public SimulatorContext(DbContextOptions<SimulatorContext> options)
        : base(options) { }
    public DbSet<Pilot> Pilots { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Cars> Cars { get; set; }
    public DbSet<Strategies> Strategies { get; set; }
    public DbSet<Circuits> Circuits { get; set; }
    public DbSet<Equipments> Equipments { get; set; }
    public DbSet<Races> Races { get; set; }
    public DbSet<RaceResults> RaceResults { get; set; }
    public DbSet<Weather> Weather { get; set; }
    public DbSet<Ability> Abilities { get; set; }

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
        
        modelBuilder.Entity<Weather>()
            .HasKey(w => w.IDWeather);
        
        modelBuilder.Entity<Cars>()
            .HasKey(c => c.IDCars);
        
        modelBuilder.Entity<Strategies>()
            .HasKey(s => s.strategiesIDStrategy);
        
        modelBuilder.Entity<Circuits>()
            .HasKey(c => c.IDCircuits);
        
        modelBuilder.Entity<Equipments>()
            .HasKey(e => e.IDEquipments);
        
        modelBuilder.Entity<Races>()
            .HasKey(r => r.IDRace);
        
        modelBuilder.Entity<RaceResults>()
            .HasKey(rr => rr.IDRaceResult);
        

        
        
        modelBuilder.Entity<Team>()
            .HasOne(t => t.Pilot1)
            .WithMany()
            .HasForeignKey(t => t.IDPilot1)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.Pilot2)
            .WithMany()
            .HasForeignKey(t => t.IDPilot2)
            .OnDelete(DeleteBehavior.Restrict);
 
        modelBuilder.Entity<Pilot>()
            .HasOne(p => p.Car)
            .WithMany() 
            .HasForeignKey(p => p.IDCars); 
        
        modelBuilder.Entity<Ability>()
            .HasOne(a => a.PilotA)  
            .WithMany(p => p.Abilities)  
            .HasForeignKey(a => a.IDPilot);
        
        modelBuilder.Entity<Team>()
            .HasOne(t => t.Strategy) 
            .WithMany()            
            .HasForeignKey(t => t.strategiesIDStrategy); 
        
        modelBuilder.Entity<Races>()
            .HasOne(r => r.Weather)
            .WithMany()
            .HasForeignKey(r => r.IDWeather)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Races>()
            .HasOne(r => r.Circuit)
            .WithMany()
            .HasForeignKey(r => r.IDCircuit)
            .OnDelete(DeleteBehavior.Restrict);
        

        base.OnModelCreating(modelBuilder);
    }
}




namespace F1Simulator
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SimulatorContext>();
            optionsBuilder.UseSqlite("Data Source=localdatabase.db");
            
            
            using (var context = new SimulatorContext(optionsBuilder.Options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                
                
                Circuits.Circuits circuit1 = new Circuits.Circuits()
                {
                    IDCircuits = 10, Length = 5.7f, CurbsNumber = 8, Difficulty = 7, Laps = 53, Name = "Monza"
                };
                Circuits.Circuits circuit2 = new Circuits.Circuits()
                {
                    IDCircuits = 11, Length = 3.33f, CurbsNumber = 19, Difficulty = 9, Laps = 78, Name = "Monaco"
                };

                Circuits.Circuits circuit3 = new Circuits.Circuits()
                {
                    IDCircuits = 12, Length = 5.8f, CurbsNumber = 18, Difficulty = 8, Laps = 53, Name = "Suzuka Circuit"
                };
                Circuits.Circuits circuit4 = new Circuits.Circuits()
                {
                    IDCircuits = 13, Length = 6.14f, CurbsNumber = 27, Difficulty = 9, Laps = 50,
                    Name = "Jeddah Corniche Circuit"
                };
                Circuits.Circuits circuit5 = new Circuits.Circuits()
                {
                    IDCircuits = 14, Length = 5.41f, CurbsNumber = 19, Difficulty = 5, Laps = 57,
                    Name = "Miami International Autodrome"
                };
                Circuits.Circuits circuit6 = new Circuits.Circuits()
                {
                    IDCircuits = 15, Length = 7.0f, CurbsNumber = 19, Difficulty = 4, Laps = 44,
                    Name = "Circuit de Spa-Francorchamps"
                };
                Circuits.Circuits circuit7 = new Circuits.Circuits()
                {
                    IDCircuits = 16, Length = 4.3f, CurbsNumber = 17, Difficulty = 4, Laps = 71,
                    Name = "Autódromo Hermanos Rodríguez"
                };
                Circuits.Circuits circuit8 = new Circuits.Circuits()
                {
                    IDCircuits = 17, Length = 6.2f, CurbsNumber = 17, Difficulty = 5, Laps = 50,
                    Name = "Las Vegas Strip Circuit"
                };
                Circuits.Circuits circuit9 = new Circuits.Circuits()
                {
                    IDCircuits = 18, Length = 5.28f, CurbsNumber = 16, Difficulty = 6, Laps = 58,
                    Name = "Yas Marina Circuit"
                };
                Circuits.Circuits circuit10 = new Circuits.Circuits()
                {
                    IDCircuits = 19, Length = 4.3f, CurbsNumber = 15, Difficulty = 7, Laps = 71,
                    Name = "Autódromo José Carlos Pace"
                };
                Circuits.Circuits circuit11 = new Circuits.Circuits()
                {
                    IDCircuits = 20, Length = 4.9f, CurbsNumber = 19, Difficulty = 8, Laps = 63,
                    Name = "Autodromo Internazionale Enzo e Dino Ferrari"
                };
                
                if (!context.Circuits.Any())
                {
                    context.AddRange(circuit1, circuit2, circuit3, circuit4, circuit5, circuit6, circuit7, circuit8, circuit9, circuit10, circuit11);
                    try
                    {
                        context.SaveChanges();
                        Console.WriteLine("Hello world");
                    }
                    catch (DbUpdateException ex)
                    {
                        
                        Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}");
                    }
                }
                
                
                
                
                
                Strategies strategy1 = new Strategies()
                {
                    strategiesIDStrategy = 10000, StrategyType = "Conservative", Points = 55,
                    Description = "Focuses on maintaining the car in optimal condition and avoiding overuse of resources"
                };
                Strategies strategy2 = new Strategies()
                {
                    strategiesIDStrategy = 10001, StrategyType = "Aggressive", Points = 86,
                    Description = "Focuses on maximizing short-term performance, even at the risk of higher wear and potential failure"
                };
                Strategies strategy3 = new Strategies()
                {
                    strategiesIDStrategy = 10002, StrategyType = "Aerodynamic Enhancement", Points = 68,
                    Description = "Prioritizes improving the car’s aerodynamics to reduce air resistance and increase top speed"
                };
                Strategies strategy4 = new Strategies()
                {
                    strategiesIDStrategy = 10003, StrategyType = "Fuel Optimization", Points = 32,
                    Description = "Focuses on optimizing fuel consumption to maximize endurance and reduce the need for frequent pit stops"
                };
                Strategies strategy5 = new Strategies()
                {
                    strategiesIDStrategy = 10004, StrategyType = "Adaptive", Points = 61,
                    Description = "Adapts to varying weather conditions and track states to maximize performance under changing circumstances"
                };
                
                if (!context.Strategies.Any())
                {
                    context.Strategies.AddRange(strategy1, strategy2, strategy3, strategy4, strategy5);
                    context.SaveChanges();
                }
                
                
                
                
                Cars.Cars car1 = new Cars.Cars()
                {
                    IDCars = 1800, model = "Ferrari SF-24 #1", speedMax = 340, accTime = 2.5f, hp = 1050,
                    mass = 798,
                    fuel = "Benzine", tiresPressure = 22
                };
                Cars.Cars car2 = new Cars.Cars()
                {
                    IDCars = 1801, model = "Ferrari SF-24 #2", speedMax = 340, accTime = 2.5f, hp = 1050,
                    mass = 798,
                    fuel = "Benzine", tiresPressure = 22
                };
                Cars.Cars car3 = new Cars.Cars()
                {
                    IDCars = 1802, model = "Mercedes W15 #1", speedMax = 338, accTime = 2.6f, hp = 1030, mass = 801,
                    fuel = "Benzine", tiresPressure = 21
                };
                Cars.Cars car4 = new Cars.Cars()
                {
                    IDCars = 1803, model = "Mercedes W15 #2", speedMax = 338, accTime = 2.6f, hp = 1030, mass = 801,
                    fuel = "Benzine", tiresPressure = 21
                };
                Cars.Cars car5 = new Cars.Cars()
                {
                    IDCars = 1804, model = "Red Bull RB20 #1", speedMax = 342, accTime = 2.4f, hp = 1060,
                    mass = 800,
                    fuel = "Benzine", tiresPressure = 23
                };
                Cars.Cars car6 = new Cars.Cars()
                {
                    IDCars = 1805, model = "Red Bull RB20 #2", speedMax = 342, accTime = 2.4f, hp = 1060,
                    mass = 800,
                    fuel = "Benzine", tiresPressure = 23
                };
                Cars.Cars car7 = new Cars.Cars()
                {
                    IDCars = 1806, model = "McLaren MCL60 #1", speedMax = 336, accTime = 2.7f, hp = 1010,
                    mass = 797,
                    fuel = "Benzine", tiresPressure = 21
                };
                Cars.Cars car8 = new Cars.Cars()
                {
                    IDCars = 1807, model = "McLaren MCL60 #2", speedMax = 336, accTime = 2.7f, hp = 1010,
                    mass = 797,
                    fuel = "Benzine", tiresPressure = 21
                };
                Cars.Cars car9 = new Cars.Cars()
                {
                    IDCars = 1808, model = "Alpine A524 #1", speedMax = 335, accTime = 2.8f, hp = 1025, mass = 799,
                    fuel = "Benzine", tiresPressure = 21
                };
                Cars.Cars car10 = new Cars.Cars()
                {
                    IDCars = 1809, model = "Alpine A524 #2", speedMax = 335, accTime = 2.8f, hp = 1025, mass = 799,
                    fuel = "Benzine", tiresPressure = 21
                };
                Cars.Cars car11 = new Cars.Cars()
                {
                    IDCars = 1810, model = "Aston Martin AMR24 #1", speedMax = 337, accTime = 2.6f, hp = 1040,
                    mass = 801,
                    fuel = "Benzine", tiresPressure = 23
                };
                Cars.Cars car12 = new Cars.Cars()
                {
                    IDCars = 1811, model = "Aston Martin AMR24 #2", speedMax = 337, accTime = 2.6f, hp = 1040,
                    mass = 801,
                    fuel = "Benzine", tiresPressure = 23
                };
                Cars.Cars car13 = new Cars.Cars()
                {
                    IDCars = 1812, model = "Williams FW46 #1", speedMax = 334, accTime = 2.9f, hp = 1000,
                    mass = 801,
                    fuel = "Benzine", tiresPressure = 20
                };
                Cars.Cars car14 = new Cars.Cars()
                {
                    IDCars = 1813, model = "Williams FW46 #2", speedMax = 334, accTime = 2.9f, hp = 1000,
                    mass = 801,
                    fuel = "Benzine", tiresPressure = 20
                };
                Cars.Cars car15 = new Cars.Cars()
                {
                    IDCars = 1814, model = "RB-Honda RBPT RB24 #1", speedMax = 343, accTime = 2.2f, hp = 1070,
                    mass = 797,
                    fuel = "Benzine", tiresPressure = 21
                };
                Cars.Cars car16 = new Cars.Cars()
                {
                            IDCars = 1815, model = "RB-Honda RBPT RB24 #2", speedMax = 343, accTime = 2.2f, hp = 1070,
                            mass = 797,
                            fuel = "Benzine", tiresPressure = 21
                };    
                Cars.Cars car17 = new Cars.Cars()
                {
                    IDCars = 1816, model = "Haas VF-24 #1", speedMax = 330, accTime = 3.2f, hp = 970, mass = 801,
                    fuel = "Benzine", tiresPressure = 18
                };
                Cars.Cars car18 = new Cars.Cars()
                {
                    IDCars = 1817, model = "Haas VF-24 #2", speedMax = 330, accTime = 3.2f, hp = 970, mass = 801,
                    fuel = "Benzine", tiresPressure = 18
                };
                Cars.Cars car19 = new Cars.Cars()
                {
                    IDCars = 1818, model = "Kick Sauber C44 #1", speedMax = 334, accTime = 3.0f, hp = 1005,
                    mass = 800,
                    fuel = "Benzine", tiresPressure = 20
                };
                Cars.Cars car20 = new Cars.Cars() 
                { 
                    IDCars = 1819, model = "Kick Sauber C44 #2", speedMax = 334, accTime = 3.0f, hp = 1005, mass = 800, 
                    fuel = "Benzine", tiresPressure = 20 
                };
                
                if (!context.Cars.Any())
                {
                    context.Cars.AddRange(car1, car2, car3, car4, car5, car6, car7, car8, car9, car10, car11, car12, car13, car14, car15, car16, car17,
                        car18, car19, car20);
                    context.SaveChanges();
                }
                
                
                Pilot pilot1 = new Pilot()
                { 
                    IDPilot = 100, Age = 27, Experience = 9, IDCars = car1.IDCars, First_Name = "Max", 
                    Last_Name = "Verstappen", IDTeam = null, Abilities = null
                };
                Pilot pilot2 = new Pilot()
                {
                    IDPilot = 101, Age = 34, Experience = 13, IDCars = car2.IDCars, First_Name = "Sergio", 
                    Last_Name = "Perez", IDTeam = null, Abilities = null
                };
                Pilot pilot3 = new Pilot()
                {
                    IDPilot = 102, Age = 25, Experience = 5, IDCars = car3.IDCars, First_Name = "Lando",
                    Last_Name = "Norris", IDTeam = null, Abilities = null
                };
                Pilot pilot4 = new Pilot()
                {
                    IDPilot = 103, Age = 23, Experience = 4, IDCars = car4.IDCars, First_Name = "Oscar",
                    Last_Name = "Piastri", Abilities = null, IDTeam = null
                };
                Pilot pilot5 = new Pilot()
                {
                    IDPilot = 104, Age = 26, Experience = 6, IDCars = car5.IDCars, 
                    First_Name = "Charles", Last_Name = "Leclerc", Abilities = null , IDTeam = null
                };
                Pilot pilot6 = new Pilot()
                {
                    IDPilot = 105, Age = 30, Experience = 9, IDCars = car6.IDCars,
                    First_Name = "Carlos", Last_Name = "Sainz", Abilities = null, IDTeam = null
                };
                Pilot pilot7 = new Pilot()
                {
                    IDPilot = 106, Age = 39, Experience = 17, IDCars = car7.IDCars,
                    First_Name = "Lewis", Last_Name = "Hamilton", Abilities = null, IDTeam = null
                };
                Pilot pilot8 = new Pilot()
                {
                    IDPilot = 107, Age = 26, Experience = 9, IDCars = car8.IDCars,
                    First_Name = "George", Last_Name = "Russell", Abilities = null, IDTeam = null
                };
                Pilot pilot9 = new Pilot()
                {
                    IDPilot = 108, Age = 43, Experience = 19, IDCars = car9.IDCars,
                    First_Name = "Fernando", Last_Name = "Alonso", Abilities = null, IDTeam = null
                };
                Pilot pilot10 = new Pilot()
                {
                    IDPilot = 109, Age = 27, Experience = 6, IDCars = car10.IDCars, First_Name = "Lance",
                    Last_Name = "Stroll", Abilities = null, IDTeam = null
                };
                Pilot pilot11 = new Pilot()
                {
                    IDPilot = 110, Age = 24, Experience = 55, IDCars = car11.IDCars, First_Name = "Yuki",
                    Last_Name = "Tsunoda", Abilities = null, IDTeam = null
                };
                Pilot pilot12 = new Pilot()
                {
                    IDPilot = 111, Age = 35, Experience = 12, IDCars = car12.IDCars,
                    First_Name = "Daniel", Last_Name = "Ricciardo", Abilities = null, IDTeam = null
                };
                Pilot pilot13 = new Pilot()
                {
                    IDPilot = 112, Age = 37, Experience = 16, IDCars = car13.IDCars, First_Name = "Nico",
                    Last_Name = "Hulkenberg", Abilities = null, IDTeam = null
                };
                Pilot pilot14 = new Pilot()
                {
                    IDPilot = 113, Age = 32, Experience = 10, IDCars = car14.IDCars,
                    First_Name = "Kevin", Last_Name = "Magnussen", Abilities = null, IDTeam = null
                };
                Pilot pilot15 = new Pilot()
                {
                    IDPilot = 114, Age = 28, Experience = 9, IDCars = car15.IDCars,
                    First_Name = "Pierre", Last_Name = "Gasly", Abilities = null, IDTeam = null
                };
                Pilot pilot16 = new Pilot()
                {
                    IDPilot = 115, Age = 28, Experience = 7, IDCars = car16.IDCars,
                    First_Name = "Esteban", Last_Name = "Ocon", Abilities = null, IDTeam = null
                };
                Pilot pilot17 = new Pilot()
                {
                    IDPilot = 116, Age = 28, Experience = 10, IDCars = car17.IDCars,
                    First_Name = "Alexander", Last_Name = "Albon", Abilities = null, IDTeam = null
                };
                Pilot pilot18 = new Pilot()
                {
                    IDPilot = 117, Age = 21, Experience = 3, IDCars = car18.IDCars,
                    First_Name = "Franco", Last_Name = " Colapinto", Abilities = null, IDTeam = null
                };
                Pilot pilot19 = new Pilot()
                {
                    IDPilot = 118, Age = 35, Experience = 12, IDCars = car19.IDCars,
                    First_Name = "Valtteri", Last_Name = " Bottas", Abilities = null, IDTeam = null
                };
                Pilot pilot20 = new Pilot()
                { 
                    IDPilot = 119, Age = 25, Experience = 4, IDCars = car20.IDCars, First_Name = "Zhou", 
                    Last_Name = "Guanyu", Abilities = null, IDTeam = null
                };
                
                
                
                
                
                var a1 = new Ability() { IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 95, IDPilot = pilot1.IDPilot};
                var a2 = new Ability() { IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 91, IDPilot = pilot1.IDPilot};
                var a3 = new Ability() { IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 84, IDPilot = pilot1.IDPilot};
                var a4 = new Ability() { IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 97, IDPilot = pilot1.IDPilot};
                var a5 = new Ability() { IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 93, IDPilot = pilot1.IDPilot}; 
                var a6 = new Ability() { IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 91, IDPilot = pilot1.IDPilot};
                var a7 = new Ability() { IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 87, IDPilot = pilot1.IDPilot};
                        
                var a8 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 911, IDPilot = pilot2.IDPilot};
                var a9 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 86, IDPilot = pilot2.IDPilot};
                var a10 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 93, IDPilot = pilot2.IDPilot};
                var a11 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 89, IDPilot = pilot2.IDPilot};
                var a12 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 94, IDPilot = pilot2.IDPilot};
                var a13 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 81, IDPilot = pilot2.IDPilot};
                var a14 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 92, IDPilot = pilot2.IDPilot};
                        
                var a15 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 98, IDPilot = pilot3.IDPilot};
                var a16 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 84, IDPilot = pilot3.IDPilot};
                var a17 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 94, IDPilot = pilot3.IDPilot};
                var a18 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 99, IDPilot = pilot3.IDPilot};
                var a19 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 97, IDPilot = pilot3.IDPilot};
                var a20 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 98, IDPilot = pilot3.IDPilot};
                var a21 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 89, IDPilot = pilot3.IDPilot};
                        
                var a22 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 89, IDPilot = pilot4.IDPilot};
                var a23 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 92, IDPilot = pilot4.IDPilot};
                var a24 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 95, IDPilot = pilot4.IDPilot};
                var a25 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 90, IDPilot = pilot4.IDPilot};
                var a26 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 97, IDPilot = pilot4.IDPilot};
                var a27 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 96, IDPilot = pilot4.IDPilot};
                var a28 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 95, IDPilot = pilot4.IDPilot};
                        
                var a29 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 98, IDPilot = pilot5.IDPilot};
                var a30 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 88, IDPilot = pilot5.IDPilot};
                var a31 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 97, IDPilot = pilot5.IDPilot};
                var a32 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 85, IDPilot = pilot5.IDPilot};
                var a33 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 97, IDPilot = pilot5.IDPilot};
                var a34 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 99, IDPilot = pilot5.IDPilot};
                var a35 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 95, IDPilot = pilot5.IDPilot};
                        
                var a36 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 94, IDPilot = pilot6.IDPilot};
                var a37 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 92, IDPilot = pilot6.IDPilot};
                var a38 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 99, IDPilot = pilot6.IDPilot};
                var a39 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 85, IDPilot = pilot6.IDPilot};
                var a40 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 92, IDPilot = pilot6.IDPilot};
                var a41 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 97, IDPilot = pilot6.IDPilot};
                var a42 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 84, IDPilot = pilot6.IDPilot};

                var a43 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 87, IDPilot = pilot7.IDPilot};
                var a44 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 84, IDPilot = pilot7.IDPilot};
                var a45 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 98, IDPilot = pilot7.IDPilot};
                var a46 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 93, IDPilot = pilot7.IDPilot};
                var a47 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 81, IDPilot = pilot7.IDPilot};
                var a48 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 97, IDPilot = pilot7.IDPilot};
                var a49 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 92, IDPilot = pilot7.IDPilot};
                        
                var a50 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 97, IDPilot = pilot8.IDPilot};
                var a51 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 82, IDPilot = pilot8.IDPilot};
                var a52 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 94, IDPilot = pilot8.IDPilot};
                var a53 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 88, IDPilot = pilot8.IDPilot};
                var a54 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 81, IDPilot = pilot8.IDPilot};
                var a55 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 99, IDPilot = pilot8.IDPilot};
                var a56 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 91, IDPilot = pilot8.IDPilot};
                        
                var a57 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 91, IDPilot = pilot9.IDPilot};
                var a58 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 79, IDPilot = pilot9.IDPilot};
                var a59 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 96, IDPilot = pilot9.IDPilot};
                var a60 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 78, IDPilot = pilot9.IDPilot};
                var a61 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 82, IDPilot = pilot9.IDPilot};
                var a62 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 81, IDPilot = pilot9.IDPilot};
                var a63 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 87, IDPilot = pilot9.IDPilot};
                        
                var a64 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 86, IDPilot = pilot10.IDPilot};
                var a65 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 73, IDPilot = pilot10.IDPilot};
                var a66 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 99, IDPilot = pilot10.IDPilot};
                var a67 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 80, IDPilot = pilot10.IDPilot};
                var a68 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 75, IDPilot = pilot10.IDPilot};
                var a69 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 80, IDPilot = pilot10.IDPilot};
                var a70 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 83, IDPilot = pilot10.IDPilot};
                        
                var a71 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 82, IDPilot = pilot11.IDPilot};
                var a72 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 64, IDPilot = pilot11.IDPilot};
                var a73 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 90, IDPilot = pilot11.IDPilot};
                var a74 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 54, IDPilot = pilot11.IDPilot};
                var a75 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 79, IDPilot = pilot11.IDPilot};
                var a76 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 75, IDPilot = pilot11.IDPilot};
                var a77 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 83, IDPilot = pilot11.IDPilot};
                        
                var a78 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 85, IDPilot = pilot12.IDPilot};
                var a79 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 69, IDPilot = pilot12.IDPilot};
                var a80 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 94, IDPilot = pilot12.IDPilot};
                var a81 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 58, IDPilot = pilot12.IDPilot};
                var a82 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 71, IDPilot = pilot12.IDPilot};
                var a83 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 62, IDPilot = pilot12.IDPilot};
                var a84 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 73, IDPilot = pilot12.IDPilot};
                        
                var a85 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 81, IDPilot = pilot13.IDPilot};
                var a86 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 62, IDPilot = pilot13.IDPilot};
                var a87 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 91, IDPilot = pilot13.IDPilot};
                var a88 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 43, IDPilot = pilot13.IDPilot};
                var a89 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 49, IDPilot = pilot13.IDPilot};
                var a90 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 52, IDPilot = pilot13.IDPilot};
                var a91 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 82, IDPilot = pilot13.IDPilot};
                        
                var a92 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 87, IDPilot = pilot14.IDPilot};
                var a93 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 65, IDPilot = pilot14.IDPilot};
                var a94 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 86, IDPilot = pilot14.IDPilot};
                var a95 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 54, IDPilot = pilot14.IDPilot};
                var a96 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 42, IDPilot = pilot14.IDPilot};
                var a97 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 46, IDPilot = pilot14.IDPilot};
                var a98 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 55, IDPilot = pilot14.IDPilot};
                        
                var a99 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 82, IDPilot = pilot15.IDPilot};
                var a100 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 69, IDPilot = pilot15.IDPilot};
                var a101 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 94, IDPilot = pilot15.IDPilot};
                var a102 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 59, IDPilot = pilot15.IDPilot};
                var a103 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 65, IDPilot = pilot15.IDPilot};
                var a104 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 41, IDPilot = pilot15.IDPilot};
                var a105 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 52, IDPilot = pilot15.IDPilot};
                        
                var a106 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 85, IDPilot = pilot16.IDPilot};
                var a107 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 61, IDPilot = pilot16.IDPilot};
                var a108 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 93, IDPilot = pilot16.IDPilot};
                var a109 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 73, IDPilot = pilot16.IDPilot};
                var a110 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 59, IDPilot = pilot16.IDPilot};
                var a111 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 63, IDPilot = pilot16.IDPilot};
                var a112 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 92, IDPilot = pilot16.IDPilot};
                        
                var a113 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 91, IDPilot = pilot17.IDPilot};
                var a114 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 62, IDPilot = pilot17.IDPilot};
                var a115 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 83, IDPilot = pilot17.IDPilot};
                var a116 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 46, IDPilot = pilot17.IDPilot};
                var a117 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 55, IDPilot = pilot17.IDPilot};
                var a118 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 62, IDPilot = pilot17.IDPilot};
                var a119 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 77, IDPilot = pilot17.IDPilot};
                        
                var a120 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 74, IDPilot = pilot18.IDPilot};
                var a121 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 61, IDPilot = pilot18.IDPilot};
                var a122 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 97, IDPilot = pilot18.IDPilot};
                var a123 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 58, IDPilot = pilot18.IDPilot};
                var a124 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 52, IDPilot = pilot18.IDPilot};
                var a125 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 48, IDPilot = pilot18.IDPilot};
                var a126 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 59, IDPilot = pilot18.IDPilot};
                        
                var a127 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 79, IDPilot = pilot19.IDPilot};
                var a128 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 78, IDPilot = pilot19.IDPilot};
                var a129 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 86, IDPilot = pilot19.IDPilot};
                var a130 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 58, IDPilot = pilot19.IDPilot};
                var a131 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 43, IDPilot = pilot19.IDPilot};
                var a132 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 41, IDPilot = pilot19.IDPilot};
                var a133 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 78, IDPilot = pilot19.IDPilot};
                        
                var a134 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Cornering skills", AbilityValue = 75, IDPilot = pilot20.IDPilot};
                var a135 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Race strategy", AbilityValue = 71, IDPilot = pilot20.IDPilot};
                var a136 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Physical resistance", AbilityValue = 96, IDPilot = pilot20.IDPilot};
                var a137 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Car overtaking", AbilityValue = 40, IDPilot = pilot20.IDPilot};
                var a138 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Tire management", AbilityValue = 56, IDPilot = pilot20.IDPilot};
                var a139 = new Ability(){ IDAbility = Guid.NewGuid(), AbilityName = "Starting reaction", AbilityValue = 45, IDPilot = pilot20.IDPilot};
                var a140 = new Ability() { IDAbility = Guid.NewGuid(), AbilityName = "Adaptability to weather conditions", AbilityValue = 67, IDPilot = pilot20.IDPilot };

                if (!context.Pilots.Any())
                {
                    context.Pilots.AddRange(pilot1, pilot2, pilot3, pilot4, pilot5, pilot6, pilot7, pilot8, pilot9, pilot10, pilot11, pilot12, pilot13, pilot14, pilot15,
                        pilot16, pilot17, pilot18, pilot19, pilot20);
                    context.SaveChanges();
                }
                
                
                if (!context.Abilities.Any())
                {
                    context.Abilities.AddRange(a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, a11, a12, a13, a14, a15, a16,
                        a17, a18, a19, a20, a21, a22, a23, a24, a25, a26, a27, a28, a29,
                        a30, a31, a32, a33, a34, a35, a36, a37, a38, a39, a40, a41, a42, a43, a44, a45, a46, a47, a48,
                        a49, a50, a51, a52, a53, a54, a55, a56, a57, a58, a59, a60, a61, a62, a63, a64, a65, a66, a67, a68, a69, a70, a71, a72, a73, a74, a75, a76, a77, a78, a79,
                        a80, a81, a82, a83, a84, a85, a86, a87, a88, a89, a90, a91, a92, a93, a94, a95, a96, a97, a98, a99, a100, a101, a102, a103, a104, a105, a106, a107, a108, a109,
                        a110, a111, a112, a113, a114, a115, a116, a117, a118, a119, a120, a121, a122, a123, a124, a125, a126, a127, a128, a129, a130, a131, a132, a133, a134, a135, a136, a137, a138, a139, a140
                    );
                }
                
                

                
                
                
                
                
                
                
                Team team1 = new Team()
                {
                    IDTeam = 4500, Budget = 145, Name = "Oracle Red Bull Racing",
                    Manager_First_Name = "Christian", Manager_Last_Name = "Horner",
                    IDPilot1 = pilot1.IDPilot, IDPilot2 = pilot2.IDPilot,
                    strategiesIDStrategy = null
                };
                Team team2 = new Team()
                {
                    IDTeam = 4501, Budget = 120, Name = "McLaren Formula 1 Team", Manager_First_Name = "Brown",
                    Manager_Last_Name = "Zak",
                    IDPilot1 = pilot3.IDPilot, IDPilot2 = pilot4.IDPilot, strategiesIDStrategy = 10000
                };
                Team team3 = new Team()
                {
                    IDTeam = 4502, Budget = 157, Name = "Scuderia Ferrari", Manager_First_Name = "Frédéric",
                    Manager_Last_Name = "Vasseur",IDPilot1 = pilot5.IDPilot, IDPilot2 = pilot6.IDPilot, strategiesIDStrategy = 10000
                };
                Team team4 = new Team()
                {
                    IDTeam = 4503, Budget = 148, Name = "Mercedes-AMG PETRONAS F1 Team",
                    Manager_First_Name = "Toto", Manager_Last_Name = "Wolff",IDPilot1 = pilot7.IDPilot, IDPilot2 = pilot8.IDPilot,
                    strategiesIDStrategy = 10000
                    // IDPilot1 = 106, IDPilot2 = 107, strategiesIDStrategy = 10000
                };
                Team team5 = new Team()
                {
                    IDTeam = 4504, Budget = 139, Name = "Aston Martin Aramco F1 Team",
                    Manager_First_Name = "Mike", Manager_Last_Name = "Krack",IDPilot1 = pilot9.IDPilot, IDPilot2 = pilot10.IDPilot,
                    strategiesIDStrategy = 10000
                };
                Team team6 = new Team()
                {
                    IDTeam = 4505, Budget = 136, Name = "Visa Cash App RB Formula One Team",
                    Manager_First_Name = "Jody", Manager_Last_Name = "Egginton", IDPilot1 = pilot11.IDPilot, IDPilot2 = pilot12.IDPilot, strategiesIDStrategy = 10000
                    // IDPilot1 = 110, IDPilot2 = 111, strategiesIDStrategy = 10000
                };
                Team team7 = new Team()
                {
                    IDTeam = 4506, Budget = 126, Name = "MoneyGram Haas F1 Team", Manager_First_Name = "Ayao",
                    Manager_Last_Name = "Komatsu", IDPilot1 = pilot13.IDPilot, IDPilot2 = pilot14.IDPilot, strategiesIDStrategy = 10000
                    // IDPilot1 = 112, IDPilot2 = 113, strategiesIDStrategy = 10000
                };
                Team team8 = new Team()
                {
                    IDTeam = 4507, Budget = 134, Name = "BWT Alpine F1 Team", Manager_First_Name = "Oliver",
                    Manager_Last_Name = "Oakes", IDPilot1 = pilot15.IDPilot, IDPilot2 = pilot16.IDPilot, strategiesIDStrategy = 10000
                    // IDPilot1 = 114, IDPilot2 = 115, strategiesIDStrategy = 10000
                };
                Team team9 = new Team()
                {
                    IDTeam = 4508, Budget = 124, Name = "Williams Racing", Manager_First_Name = "James",
                    Manager_Last_Name = "Vowles", IDPilot1 = pilot17.IDPilot, IDPilot2 = pilot18.IDPilot, strategiesIDStrategy = 10000
                    // IDPilot1 = 116, IDPilot2 = 117, strategiesIDStrategy = 10000
                };
                Team team10 = new Team()
                {
                    IDTeam = 4509, Budget = 112, Name = "Stake F1 Team Kick Sauber",
                    Manager_First_Name = "Alessandro", Manager_Last_Name = "Alunni Bravi", IDPilot1 = pilot19.IDPilot, IDPilot2 = pilot20.IDPilot, strategiesIDStrategy = 10000
                    // IDPilot1 = 118, IDPilot2 = 119, strategiesIDStrategy = 10000
                };
                // team1.IDPilot1 = pilot1.IDPilot;
                // team1.IDPilot2 = pilot2.IDPilot;
                // team2.IDPilot1 = pilot3.IDPilot;
                // team2.IDPilot2 = pilot4.IDPilot;
                // team3.IDPilot1 = pilot5.IDPilot;
                // team3.IDPilot2 = pilot6.IDPilot;
                // team4.IDPilot1 = pilot7.IDPilot;
                // team4.IDPilot2 = pilot8.IDPilot;
                // team5.IDPilot1 = pilot9.IDPilot;
                // team5.IDPilot2 = pilot10.IDPilot;
                // team6.IDPilot1 = pilot11.IDPilot;
                // team6.IDPilot2 = pilot12.IDPilot;
                // team7.IDPilot1 = pilot13.IDPilot;
                // team7.IDPilot2 = pilot14.IDPilot;
                // team8.IDPilot1 = pilot15.IDPilot;
                // team8.IDPilot2 = pilot16.IDPilot;
                // team9.IDPilot1 = pilot17.IDPilot;
                // team9.IDPilot2 = pilot18.IDPilot;
                // team10.IDPilot1 = pilot19.IDPilot;
                // team10.IDPilot2 = pilot20.IDPilot;

                if (!context.Teams.Any())
                {
                    context.Teams.AddRange(team1, team2, team3, team4, team5, team6, team7, team8, team9, team10);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}");
                    }
                }
                


                



                var teams = context.Teams.ToList();
                Console.WriteLine(teams.Count);
                var pilots = context.Pilots.Include(p => p.Abilities);
                var cars = context.Cars.ToList();
                var strategies = context.Strategies.ToList();
                var circuits = context.Circuits.ToList();

                var race = new Races.Races();
                race.IDCircuit = null;

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
                race.Weather = weatherConditions;
                race.IDWeather = weatherConditions.IDWeather;
                
                Console.WriteLine("Choose a circuit: ");
                int j = 1;
                foreach (var circuit in circuits)
                {
                    Console.WriteLine($"Circuit number {j++}");
                    Console.WriteLine($"Circuit name: {circuit.Name}");
                    Console.WriteLine($"Circuit difficulty: {circuit.Difficulty}");
                    Console.WriteLine($"Number of laps: {circuit.Laps}");
                    Console.WriteLine($"Circuit length: {circuit.Length}");
                    Console.WriteLine($"Number of curbs : {circuit.CurbsNumber}");
                    Console.WriteLine("");
                }

                
                Console.Write("Choose the index of the circuit: ");
                string input = Console.ReadLine();
                int intInput = int.Parse(input);
                race.Circuit = circuits[intInput - 1];
                

                int i = 1;
                foreach (var strategy in strategies)
                {
                    Console.WriteLine($"Strategy number {i++}");
                    Console.WriteLine($"Name of the strategy: {strategy.StrategyType}");
                    Console.WriteLine($"Description of the strategy: {strategy.Description}");
                    Console.WriteLine($"Points of this strategy: {strategy.Points}");
                    Console.WriteLine("");
                }
                
                Console.WriteLine("For each team, choose the index of a strategy to be adopted: ");
                foreach (var team in teams)
                {
                    Console.WriteLine($"Team name: {team.Name}");
                    input = Console.ReadLine();
                    intInput = int.Parse(input);
                    team.strategiesIDStrategy = strategies[intInput].strategiesIDStrategy;
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
        public string Model;
        public int SpeedMax;
        public float AccTime; //timp de accelerare
        public int HP; //cai putere
        public float Mass; //greutate masina
        public string Fuel; //carburant
        public double TiresPressure; //presiune pneuri
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

        public int IDPilot { get; set; }
        public Pilot PilotA;
        public Ability()
        {
            IDAbility = new Guid();
            AbilityName = "";
            AbilityValue = 0;
        }

        public Ability(string AbilityName, int AbilityValue)
        {
            AbilityName = AbilityName;
            AbilityValue = AbilityValue;
        }

        // public string AbilityName
        // {
        //     get { return AbilityName; }
        //     set { AbilityName = value; }
        // }
        //
        // public int AbilityValue
        // {
        //     get { return AbilityValue; }
        //     set { AbilityValue = value; }
        // }
    }
}


namespace F1Simulator.Pilots
{
    public class Pilot 
    {
        public int IDPilot { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }
        
        public int? IDTeam { get; set; }
        public Team? ATeam { get; set; } 
        
        public Cars.Cars Car { get; set; }
        public int IDCars { get; set; }
        public ICollection<Ability>? Abilities { get; set; }
        

        // public List<Ability.Ability> abilities
        // {
        //     get { return Abilities; }
        //     set { Abilities = value; }
        // }

        // public int age
        // {
        //     get { return Age; }
        //     set { Age = value; }
        // }
        //
        // public int experience
        // {
        //     get { return Experience; }
        //     set { Experience = value; }
        // }
        //
        //
        //
        // public int idcar
        // {
        //     get { return IDCars; }
        // }

        // public int idpilot
        // {
        //     get { return IDPilot; }
        //     set { IDPilot = value; }
        // }

        


        public Pilot()
        {
            IDPilot = new int();
            First_Name = "Unknown";
            Last_Name = "Unknown";
            Age = 0;
            Experience = 0;
            Abilities = null;
            Car = null;
            IDTeam = 0;
        }

        public Pilot(int id, string firstName, string lastName, int age, int experience, Team aTeam,
            Dictionary<string, int> abilities, Cars.Cars car, Dictionary<string, int> abilitiesList, int idTeam)
        {
            IDPilot = id;
            First_Name = firstName;
            Last_Name = lastName;
            Age = age;
            Experience = experience;
            ATeam = aTeam;
            Abilities = Abilities;
            Car = car;
            IDTeam = idTeam;
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
                sum += ability.AbilityValue;
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
        public int IDTeam { get; set; }
        public string Name { get; set; }
        
        public int? IDPilot1 { get; set; }
        public Pilot? Pilot1 { get; set; }
        public int? IDPilot2 { get; set; }
        public Pilot? Pilot2 { get; set; }
        
        public int Budget { get; set; }
        public string Manager_First_Name { get; set; }
        public string Manager_Last_Name { get; set; }
        
        public int? strategiesIDStrategy { get; set; }
        public Strategies? Strategy { get; set; }

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
            strategiesIDStrategy = 0;
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
            strategiesIDStrategy = idStrategy;
        }

        // public Strategies strategy
        // {
        //     get { return Strategy; }
        //     set { Strategy = value; }
        // }

        // public int idteam
        // {
        //     get { return IDTeam; }
        //     set { IDTeam = value; }
        // }

        // public int budget
        // {
        //     get { return Budget; }
        //     set { Budget = value; }
        // }

        // public string managerFirstName
        // {
        //     get { return Manager_First_Name; }
        //     set { Manager_First_Name = value; }
        // }
        //
        // public string managerLastName
        // {
        //     get { return Manager_Last_Name; }
        //     set { Manager_Last_Name = value; }
        // }
        //
        // public Strategies strategies
        // {
        //     get { return Strategy; }
        //     set { Strategy = value; }
        // }

        public double TeamPerformance()
        {
            return Pilot1.Experience * Pilot1.SumAbilities() + Pilot2.Experience * Pilot2.SumAbilities();
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
        public int IDCircuits { get; set; }
        public float Length { get; set; }
        public int CurbsNumber { get; set; }
        public int Difficulty { get; set; }
        public int Laps { get; set; }
        public string Name { get; set; }
        

        
        

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
        public int strategiesIDStrategy { get; set; }
        public string StrategyType { get; set; }
        public int Points { get; set; }
        public string Description { get; set; }

        public Strategies()
        {
            strategiesIDStrategy = new int();
            StrategyType = "Unknown";
            Points = 0;
            Description = "Unknown";
        }

        public Strategies(int id, string strategyType, int points, string description)
        {
            strategiesIDStrategy = id;
            StrategyType = strategyType;
            Points = points;
            Description = description;
        }

        // public int points
        // {
        //     get { return Points; }
        //     set { Points = value; }
        // }
        //
        // public string strategyType
        // {
        //     get { return StrategyType; }
        //     set { StrategyType = value; }
        // }
        //
        // public string description
        // {
        //     get { return Description; }
        //     set { Description = value; }
        // }
    }
}














namespace F1Simulator.Races
{
    public class Races
    {
        public Guid IDRace { get; set; }
        public List<Pilot> Pilots { get; set; }

        public int? IDCircuit { get; set; }
        public Circuits.Circuits? Circuit { get; set; }
        public int IDWeather { get; set; }
        public Weather.Weather Weather { get; set; }
        public List<Team> Teams { get; set; }

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
                int strategyBoost = team.Strategy.Points;
                int weatherImpact = this.Weather.CalculatePerformanceLevel();
                
                string weatherCondition = this.Weather.condition;
                Strategies teamStrategy = team.Strategy;
                double strategyWeatherImpact = 0;
                
// 10000 = conservative ; 10001 = aggressive ; 10002 = aerodynamic enhancement ; 10003 = fuel optimization ; 10004 = adaptive

                if (weatherCondition == "Rainy" || weatherCondition == "Snowy" && teamStrategy.strategiesIDStrategy == 10001)
                {
                    strategyWeatherImpact = -12.5;
                }

                if (weatherCondition == "Rainy" || weatherCondition == "Snowy" && teamStrategy.strategiesIDStrategy == 10002)
                {
                    strategyWeatherImpact = -7.5;
                }

                if (weatherCondition == "Cloudy" && teamStrategy.strategiesIDStrategy == 10003)
                {
                    strategyWeatherImpact = -2.1;
                }

                if (weatherCondition == "Sunny" && teamStrategy.strategiesIDStrategy == 10004)
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
//         public Ability(string AbilityName, int AbilityValue)
//         {
//             AbilityName = AbilityName;
//             AbilityValue = AbilityValue;
//         }
//
//         public string AbilityName
//         {
//             get { return AbilityName; }
//             set { AbilityName = value; }
//         }
//
//         public int AbilityValue
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



// public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SimulatorContext>
// {
//     public SimulatorContext CreateDbContext(string[] args)
//     {
//         var optionsBuilder = new DbContextOptionsBuilder<SimulatorContext>();
//         optionsBuilder.UseSqlite("Data Source=DefaultConnection.db");
//
//         return new SimulatorContext(optionsBuilder.Options);
//     }
// }
