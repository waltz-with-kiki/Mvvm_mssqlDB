using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try2.Interfaces;

namespace Try2.Context
{

    public abstract class Entity : IEntity
    {
        public virtual int Id { get; set; }

    }

    public abstract class NamedEntity : Entity
    {
        [Required]
        public virtual string? Name { get; set; }
    }

    public abstract class Person : NamedEntity
    {
        public string? Surname { get; set; }
    }

    public class Right : NamedEntity
    {

        public ICollection<User>? Users { get; set; }

        public bool R { get; set; } = false;

        public bool W { get; set; } = false;

        public bool E { get; set; } = false;

        public bool D { get; set; } = false;


    }

    public class User : Person
    {
        public string? Password { get; set; }

        public virtual Right? Right { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }

    }



    public class Bank : NamedEntity, ICloneable
    {
        public virtual ICollection<Checking_account>? _accounts { get; set; }

        public object Clone()
        {
            return new Bank
            {
                
                Id = this.Id,
                Name = this.Name,
                
            };
        }

        public override string ToString()
        {
            return Name.ToString();
        }

    }

    public class Checking_account : Entity
    {

        public virtual Bank? Bank { get; set; }

        
        public string? Check { get; set; }

        public virtual ICollection<Client>? Clients { get; set; }

        

        public override string ToString()
        {
            return Check.ToString();
        }
    }

    public class Client : Person
    {

        public override string? Name { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsPhysical { get; set; } = true;




        public string? LegalPersonName { get; set; }

        public string? LegalAdress { get; set; }

        public virtual Checking_account? Checking_Account { get; set; }

        public string? Inn { get; set; }




        public string? SeriesAndNumberPass { get; set; }

        
        public DateTime DataOfIssue { get; set; }

        public string? IssuedBy { get; set; }


        public virtual ICollection<Order>? Orders { get; set; }



        public override string ToString()
        {
            return Id.ToString();
        }

    }

    public class Order : Entity
    {
        public DateTime OrderData { get; set; }

        public virtual Client? Client { get; set; }

        public string? LoadingAddress { get; set; }

        public string? UnloadingAddress { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal RouteLength { get; set; } //decimal

        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderCost { get; set; }

        public virtual ICollection<Cargo>? Cargos { get; set; }

        public virtual Flight? Flight { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

    public class Cargo : NamedEntity
    {
        public virtual Unit? Unit { get; set; }

        public int Amount { get; set; } //decimal

        public int Weight { get; set; } //decimal

        [Column(TypeName = "decimal(18,2)")]
        public decimal InsuranceValue { get; set; }

        public virtual Order? Order { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

    public class Unit : NamedEntity
    {
        public virtual ICollection<Cargo>? Cargos { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }

    public class Flight : Entity
    {
        public DateTime ArrivalDate { get; set; }

        public virtual Crew? Crew { get; set; }

        public virtual Automobile? Automobile { get; set; }


        public override string ToString()
        {
            return Id.ToString();
        }

    }

    public class Crew :NamedEntity
    {

        public virtual ICollection<Driver>? Drivers { get; set; }
        
        public virtual ICollection<Flight>? Flights { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }

    }

    public class Driver : Person
    {
        public virtual Crew? Crew { get; set; }

        public DateTime YearOfBirth { get; set; }

        public string? WorkExperience { get; set; }

        public virtual DriverCategory? Category { get; set; }

        public virtual DriverClass? Class { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

    public class DriverCategory : NamedEntity
    {
        public virtual ICollection<Driver>? Drivers { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }

    public class DriverClass : NamedEntity
    {
        public virtual ICollection<Driver>? Drivers { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }

    public class Automobile : NamedEntity
    {
        public string? GosNumber { get; set; }

        public virtual Brand? Brand { get; set; }

        public int LoadCapacity { get; set; } // int или decimal

        public string? Purpose { get; set; }

        public DateTime YearOfIssue { get; set; } //DateTime

        public DateTime YearOfRepair { get; set; } //DateTime

        public int Mileage { get; set; }

        public virtual ICollection<Flight>? Flights { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }

    }

    public class Brand : NamedEntity
    {
        public virtual ICollection<Automobile>? Automobiles { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }

    public class UsersContext : DbContext
    {

        //private readonly string connectionString;


        public UsersContext() :base()
        {

        }

        public UsersContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            
        }

     
        public DbSet<Right> Right { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Bank> Banks { get; set; }

        public DbSet<Checking_account> Checking_Accounts { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Crew> Crews { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Automobile> Automobiles { get; set; }

        public DbSet<DriverCategory> DriverCategories { get; set; }

        public DbSet<DriverClass> DriverClasses { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        public DbSet<Flight> Flights { get; set; } 

        public DbSet<Order> Orders { get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<Cargo> Cargos { get; set; }



        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseSqlServer(connectionString);
         }*/

    }


}
