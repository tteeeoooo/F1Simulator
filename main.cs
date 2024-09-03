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
using F1Simulator.Weather;


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
    public DbSet<Weather> Weather { get; set; }

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
        
        modelBuilder.Entity<Team>()
            .HasKey(t => t.IDTeam);
        modelBuilder.Entity<Team>()
            .HasIndex(c => c.IDTeam)
            .IsUnique();
        
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
        
        modelBuilder.Entity<Pilot>()
            .HasOne(p => p.ATeam)      
            .WithMany(t => t.Pilots)  
            .HasForeignKey(p => p.IDTeam); 
        
        modelBuilder.Entity<Pilot>()
            .HasOne(p => p.Car)      
            .WithOne(c => c.APilot)  
            .HasForeignKey<Pilot>(p => p.IDCars); 
        
        modelBuilder.Entity<Team>()
            .HasOne(e => e.strategies)
            .WithMany() 
            .HasForeignKey(e => e.IDStrategy);

        modelBuilder.Entity<Races>()
            .HasOne(r => r.Weather)
            .WithMany()
            .HasForeignKey(r => r.IDWeather)
            .OnDelete(DeleteBehavior.Restrict);


        base.OnModelCreating(modelBuilder);


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
            //age experience idteam abilities (dictionar string int), idcar, idpilot, firstname, lastname

            
        );

        modelBuilder.Entity<Pilot>().HasData(
            new Pilot()
            {
                idpilot = 100, abilities =
                {
                    {"Cornering skills", 95}, {"Race strategy", 91}, {"Physical resistance", 84}, {"Car overtaking", 97},
                    {"Tire management", 93}, {"Starting reaction", 91}, {"Adaptability to weather conditions", 87}
                }, age = 27, experience = 9, IDTeam = 4500, IDCars = 1804, firstName = "Max", lastName = "Verstappen"
            },
            new Pilot()
            {
                idpilot = 101, abilities =
                {
                    {"Cornering skills", 91}, {"Race strategy", 86}, {"Physical resistance", 93}, {"Car overtaking", 89},
                    {"Tire management", 94}, {"Starting reaction", 81}, {"Adaptability to weather conditions", 92}
                }, age = 34, experience = 13, IDTeam = 4500, IDCars = 1805, firstName = "Sergio", lastName = "Perez"
            },
            new Pilot()
            {
                idpilot = 102, abilities =
                {
                    {"Cornering skills", 98}, {"Race strategy", 84}, {"Physical resistance", 94}, {"Car overtaking", 99},
                    {"Tire management", 97}, {"Starting reaction", 98}, {"Adaptability to weather conditions", 89}
                }, age = 25, experience = 5, IDTeam = 4501, IDCars = 1806, firstName = "Lando", lastName = "Norris"
            },
            new Pilot()
            {
                idpilot = 103, abilities =
                {
                    {"Cornering skills", 89}, {"Race strategy", 92}, {"Physical resistance", 95}, {"Car overtaking", 90},
                    {"Tire management", 97}, {"Starting reaction", 96}, {"Adaptability to weather conditions", 95}
                }, age = 23, experience = 4, IDTeam = 4501, IDCars = 1807, firstName = "Oscar", lastName = "Piastri"
            },
            new Pilot()
            {
                idpilot = 104, abilities =
                {
                    {"Cornering skills", 96}, {"Race strategy", 88}, {"Physical resistance", 97}, {"Car overtaking", 85},
                    {"Tire management", 97}, {"Starting reaction", 99}, {"Adaptability to weather conditions", 95}
                }, age = 26, experience = 6, IDTeam = 4502, IDCars = 1800, firstName = "Charles", lastName = "Leclerc"
            },
            new Pilot()
            {
                idpilot = 105, abilities =
                {
                    {"Cornering skills", 94}, {"Race strategy", 92}, {"Physical resistance", 99}, {"Car overtaking", 85},
                    {"Tire management", 92}, {"Starting reaction", 97}, {"Adaptability to weather conditions", 84}
                }, age = 30, experience = 9, IDTeam = 4502, IDCars = 1801, firstName = "Carlos", lastName = "Sainz"
            },
            new Pilot()
            {
                idpilot = 106, abilities =
                {
                    {"Cornering skills", 87}, {"Race strategy", 84}, {"Physical resistance", 98}, {"Car overtaking", 93},
                    {"Tire management", 81}, {"Starting reaction", 95}, {"Adaptability to weather conditions", 92}
                }, age = 39, experience = 17, IDTeam = 4503, IDCars = 1802, firstName = "Lewis", lastName = "Hamilton"
            },
            new Pilot()
            {
                idpilot = 107, abilities =
                {
                    {"Cornering skills", 97}, {"Race strategy", 82}, {"Physical resistance", 94}, {"Car overtaking", 88},
                    {"Tire management", 81}, {"Starting reaction", 99}, {"Adaptability to weather conditions", 91}
                }, age = 26, experience = 9, IDTeam = 4503, IDCars = 1803, firstName = "George", lastName = "Russell"
            },
            new Pilot()
            {
                idpilot = 108, abilities =
                {
                    {"Cornering skills", 91}, {"Race strategy", 79}, {"Physical resistance", 96}, {"Car overtaking", 78},
                    {"Tire management", 82}, {"Starting reaction", 84}, {"Adaptability to weather conditions", 87}
                }, age = 43, experience = 19, IDTeam = 4504, IDCars = 1810, firstName = "Fernando", lastName = "Alonso"
            },
            new Pilot()
            {
                idpilot = 109, abilities =
                {
                    {"Cornering skills", 86}, {"Race strategy", 73}, {"Physical resistance", 99}, {"Car overtaking", 80},
                    {"Tire management", 75}, {"Starting reaction", 79}, {"Adaptability to weather conditions", 81}
                }, age = 27, experience = 6, IDTeam = 4504, IDCars = 1811, firstName = "Lance", lastName = "Stroll"
            },
            new Pilot()
            {
                idpilot = 110, abilities =
                {
                    {"Cornering skills", 82}, {"Race strategy", 64}, {"Physical resistance", 90}, {"Car overtaking", 54},
                    {"Tire management", 79}, {"Starting reaction", 67}, {"Adaptability to weather conditions", 71}
                }, age = 24, experience = 5, IDTeam = 4505, IDCars = 1814, firstName = "Yuki", lastName = "Tsunoda"
            },
            new Pilot()
            {
                idpilot = 111, abilities =
                {
                    {"Cornering skills", 85}, {"Race strategy", 69}, {"Physical resistance", 94}, {"Car overtaking", 58},
                    {"Tire management", 71}, {"Starting reaction", 62}, {"Adaptability to weather conditions", 73}
                }, age = 35, experience = 12, IDTeam = 4505, IDCars = 1815, firstName = "Daniel", lastName = "Ricciardo"
            },
            new Pilot()
            {
                idpilot = 112, abilities =
                {
                    {"Cornering skills", 81}, {"Race strategy", 62}, {"Physical resistance", 91}, {"Car overtaking", 43},
                    {"Tire management", 49}, {"Starting reaction", 52}, {"Adaptability to weather conditions", 67}
                }, age = 37, experience = 16, IDTeam = 4506, IDCars = 1816, firstName = "Nico", lastName = "Hulkenberg"
            },
            new Pilot()
            {
                idpilot = 113, abilities =
                {
                    {"Cornering skills", 87}, {"Race strategy", 65}, {"Physical resistance", 86}, {"Car overtaking", 54},
                    {"Tire management", 42}, {"Starting reaction", 46}, {"Adaptability to weather conditions", 55}
                }, age = 32, experience = 10, IDTeam = 4506, IDCars = 1817, firstName = "Kevin", lastName = "Magnussen"
            },
            new Pilot()
            {
                idpilot = 114, abilities =
                {
                    {"Cornering skills", 82}, {"Race strategy", 69}, {"Physical resistance", 94}, {"Car overtaking", 59},
                    {"Tire management", 65}, {"Starting reaction", 41}, {"Adaptability to weather conditions", 52}
                }, age = 28, experience = 9, IDTeam = 4507, IDCars = 1808, firstName = "Pierre", lastName = "Gasly"
            },
            new Pilot()
            {
                idpilot = 115, abilities =
                {
                    {"Cornering skills", 85}, {"Race strategy", 61}, {"Physical resistance", 92}, {"Car overtaking", 52},
                    {"Tire management", 73}, {"Starting reaction", 59}, {"Adaptability to weather conditions", 63}
                }, age = 28, experience = 7, IDTeam = 4507, IDCars = 1809, firstName = "Esteban", lastName = "Ocon"
            },
            new Pilot()
            {
                idpilot = 116, abilities =
                {
                    {"Cornering skills", 92}, {"Race strategy", 62}, {"Physical resistance", 83}, {"Car overtaking", 46},
                    {"Tire management", 55}, {"Starting reaction", 62}, {"Adaptability to weather conditions", 77}
                }, age = 28, experience = 10, IDTeam = 4508, IDCars = 1812, firstName = "Alexander", lastName = "Albon"
            },
            new Pilot()
            {
                idpilot = 117, abilities =
                {
                    {"Cornering skills", 74}, {"Race strategy", 61}, {"Physical resistance", 97}, {"Car overtaking", 58},
                    {"Tire management", 52}, {"Starting reaction", 48}, {"Adaptability to weather conditions", 50}
                }, age = 21, experience = 3, IDTeam = 4508, IDCars = 1813, firstName = "Franco", lastName = " Colapinto"
            },
            new Pilot()
            {
                idpilot = 118, abilities =
                {
                    {"Cornering skills", 79}, {"Race strategy", 78}, {"Physical resistance", 86}, {"Car overtaking", 58},
                    {"Tire management", 59}, {"Starting reaction", 81}, {"Adaptability to weather conditions", 80}
                }, age = 35, experience = 12, IDTeam = 4509, IDCars = 1818, firstName = "Valtteri", lastName = " Bottas"
            }
        );
        
        modelBuilder.Entity<Team>().HasData(
            new Team()
            {
                IDTeam = 4500, pilots = [], budget = 145, Name = "Oracle Red Bull Racing", managerFirstName = "Christian", managerLastName = "Horner"
            },
            new Team()
            {
                IDTeam = 4501, pilots = [], budget = 120, Name = "McLaren Formula 1 Team", managerLastName = "Brown", managerFirstName = "Zak"
            },
            new Team()
            {
                IDTeam = 4502, pilots = [], budget = 157, Name = "Scuderia Ferrari", managerFirstName = "Frédéric", managerLastName = "Vasseur"
            },
            new Team()
            {
                IDTeam = 4503, pilots = [], budget = 148, Name = "Mercedes-AMG PETRONAS F1 Team", managerFirstName = "Toto", managerLastName = "Wolff"
            },
            new Team()
            {
                IDTeam = 4504, pilots = [], budget = 139, Name = "Aston Martin Aramco F1 Team", managerFirstName = "Mike", managerLastName = "Krack"
            },
            new Team()
            {
                IDTeam = 4505, pilots = [], budget = 136, Name = "Visa Cash App RB Formula One Team", managerFirstName = "Jody", managerLastName = "Egginton"
            },
            new Team()
            {
                IDTeam = 4506, pilots = [], budget = 126, Name = "MoneyGram Haas F1 Team", managerFirstName = "Ayao", managerLastName = "Komatsu"
            },
            new Team()
            {
                IDTeam = 4507, pilots = [], budget = 134, Name = "BWT Alpine F1 Team", managerFirstName = "Oliver", managerLastName = "Oakes"
            },
            new Team()
            {
                IDTeam = 4508, pilots = [], budget = 124, Name = "Williams Racing", managerFirstName = "James", managerLastName = "Vowles"
            },
            new Team()
            {
                IDTeam = 4509, pilots = [], budget = 112, Name = "Stake F1 Team Kick Sauber", managerFirstName = "Alessandro", managerLastName = "Alunni Bravi"
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
        modelBuilder.Entity<Team>().HasData(
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
            }
        );


        //age experience idteam abilities (dictionar string int), idcar, idpilot, firstname, lastname

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
                var teams = context.Teams
                    .Include(t => t.Pilots)
                    .ThenInclude(p => p.Car) 
                    .ToList();
                var pilots = context.Pilots
                    .ToList();
                var cars = context.Cars
                    .ToList();
                var strategies = context.Strategies
                    .ToList();
                var circuits = context.Circuits
                    .ToList();
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

        public Pilot pilot
        {
            get { return APilot; }
            set { APilot = value; }
        }

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




namespace F1Simulator.Pilots
{
    public class Pilot : IComparable<Pilot>
    {
        public int IDPilot;
        private string First_Name;
        private string Last_Name;
        private int Age;
        private int Experience;
        public int IDTeam;
        public Team ATeam;
        private Dictionary<string, int> Abilities;
        public Cars.Cars Car;
        public int IDCars;

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

        public int idteam
        {
            get { return IDTeam; }
        }

        public Dictionary<string, int> abilities
        {
            get { return Abilities; }
            set { Abilities = value; }
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
        public string Name;
        public List<Pilot> Pilots;
        private int Budget;
        private string Manager_First_Name;
        private string Manager_Last_Name;
        private Strategies Strategy;
        public int IDStrategy;

        public Team()
        {
            IDTeam = new int();
            Pilots = new List<Pilot>();
            Budget = 0;
            Manager_First_Name = "Unknown";
            Manager_Last_Name = "Unknown";
            Strategy = new Strategies();
            Name = "Unknown";
        }

        public Team(int id, List<Pilot> pilots, int budget, string managerFirstName, string managerLastName, Strategies strategies, string name)
        {
            IDTeam = id;
            Pilots = pilots;
            Budget = budget;
            Manager_First_Name = managerFirstName;
            Manager_Last_Name = managerLastName;
            Strategy = strategies;
            Name = name;
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

        public List<Pilot> pilots
        {
            get { return Pilots; }
            set { Pilots = value; }
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
//sunny cloudy  rainy snowy

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
