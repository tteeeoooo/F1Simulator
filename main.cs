using System;
using System.Collections.Generic;
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






// dotnet ef migrations add InitialCreate
//     dotnet ef database update
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=localdatabase.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pilot>()
            .HasKey(p => p.IDPilot);
        modelBuilder.Entity<Pilot>()
            .HasIndex(p => p.IDPilot)
            .IsUnique();
        
        modelBuilder.Entity<Cars>()
            .HasKey(c => c.IDCars);
        modelBuilder.Entity<Cars>()
            .HasIndex(c => c.IDCars)
            .IsUnique();
        
        modelBuilder.Entity<Team>()
            .HasKey(t => t.IDTeam);
        modelBuilder.Entity<Team>()
            .HasIndex(c => c.IDTeam)
            .IsUnique();
        
        modelBuilder.Entity<Strategies>()
            .HasKey(s => s.IDStrategies);
        modelBuilder.Entity<Strategies>()
            .HasIndex(c => c.IDStrategies)
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
        
        modelBuilder.Entity<Pilot>()
            .HasOne(p => p.ATeam)      
            .WithMany(t => t.Pilots)  
            .HasForeignKey(p => p.IDTeam); 
        
        modelBuilder.Entity<Pilot>()
            .HasOne(p => p.Car)      
            .WithOne(c => c.APilot)  
            .HasForeignKey<Pilot>(p => p.IDCars); 

        base.OnModelCreating(modelBuilder);
    }

}

namespace F1Simulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            
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
        public Pilot APilot;

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
            APilot = new Pilot();
        }

        public Cars(int id, string model, int speedMax, float accTime, int hp, float mass, string fuel, double tiresPressure, Pilot pilot)
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
            APilot = pilot;
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




namespace F1Simulator.Pilots
{
    public class Pilot : IComparable<Pilot>
    {
        public int IDPilot;
        private string First_Name { get; set; }
        private string Last_Name { get; set; }
        private int Age;
        private int Experience;
        public int IDTeam;
        public Team ATeam;
        private Dictionary<string, int> Abilities;
        public Cars.Cars Car;
        public int IDCars;

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
            ATeam = new Team();
            Abilities = new Dictionary<string, int>();
            Car = new Cars.Cars();
        }

        public Pilot(int id, string firstName, string lastName, int age, int experience, Team aTeam,
            Dictionary<string, int> abilities, Cars.Cars car)
        {
            IDPilot = id;
            First_Name = firstName;
            Last_Name = lastName;
            Age = age;
            Experience = experience;
            ATeam = aTeam;
            Abilities = abilities;
            Car = car;
        }

        public int GetExperience()
        {
            return Experience;
        }
        

        public int SumAbilities()
        {
            int sum = 0;
            foreach (var ability in Abilities)
            {
                sum += ability.Value;
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
    public class Team: IEnumerable<Pilot>
    {
        public int IDTeam;
        public List<Pilot> Pilots;
        private int Budget;
        private string Manager_First_Name;
        private string Manager_Last_Name;
        private Strategies Strategy;

        public Team()
        {
            IDTeam = new int();
            Pilots = new List<Pilot>();
            Budget = 0;
            Manager_First_Name = "Unknown";
            Manager_Last_Name = "Unknown";
            Strategy = new Strategies();
        }

        public Team(int id, List<Pilot> pilots, int budget, string managerFirstName, string managerLastName, Strategies strategies)
        {
            IDTeam = id;
            Pilots = pilots;
            Budget = budget;
            Manager_First_Name = managerFirstName;
            Manager_Last_Name = managerLastName;
            Strategy = strategies;
        }

        public double TeamPerformance()
        {
            if (Pilots.Count == 0) return 0;
            return Pilots.Sum(p => p.GetExperience() * p.SumAbilities());
        }
        
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<Pilot> GetEnumerator()
        {
            return Pilots.GetEnumerator();
        }
    }
}

namespace F1Simulator.Circuits
{
    public class Circuits
    {
        public Guid IDCircuits;
        private int Length;
        private string Type;
        private int CurbsNumber;
        private int Difficulty; //nivel de dificultate
        private int Laps;

        public Circuits()
        {
            IDCircuits = new Guid();
            Length = 0;
            Type = "Unknown";
            CurbsNumber = 0;
            Difficulty = 0;
            Laps = 0;
        }

        public Circuits(int length, string type, int curbsNumber, int difficulty, int laps)
        {
            IDCircuits = new Guid();
            Length = length;
            Type = type;
            CurbsNumber = curbsNumber;
            Difficulty = difficulty;
            Laps = laps;
        }
    }
}

namespace F1Simulator.Strategy
{
    public class Strategies
    {
        public Guid IDStrategies;
        private string StrategyType;
        private float PitStopTime;  // secunde
        private List<string> CarChanges;

        public Strategies()
        {
            IDStrategies = new Guid();
            StrategyType = "Unknown";
            PitStopTime = 0;
            CarChanges = ["Unknown"];
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

namespace F1Simulator.Races
{
    public class Races
    {
        public Guid IDRace;
        private List<Pilot> Pilots;
        private Circuits.Circuits Circuit;

        public Races()
        {
            IDRace = new Guid();
            Pilots = new List<Pilot>();
            Circuit = new Circuits.Circuits();
        }

        public Races(List<Pilot> pilots, Circuits.Circuits circuit)
        {
            IDRace = new Guid();
            Pilots = pilots;
            Circuit = circuit;
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
