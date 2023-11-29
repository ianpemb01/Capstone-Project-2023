using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Options;

namespace MMC;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    public MyDbContext()
    {
    }

    public static void SetSqliteFileOptions(DbContextOptionsBuilder<MyDbContext> optionsBuilder, string fileName)
        {
            var connectionString = $"Data Source={fileName}.db";
            optionsBuilder.UseSqlite(connectionString);
        }
        public DbSet<Cube> Cubes { get; set; }

        public DbSet<Cylinder> Cylinders { get; set; }

        public DbSet<Sphere> Spheres { get; set; }

        public DbSet<Pyramid> Pyramids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) //Defines the project names as Primary Keys
        {
            modelBuilder.Entity<Cube>().HasKey(c => c.Projectname1);
            modelBuilder.Entity<Cylinder>().HasKey(c => c.Projectname2);
            modelBuilder.Entity<Sphere>().HasKey(c => c.Projectname3);
            modelBuilder.Entity<Pyramid>().HasKey(c => c.Projectname4);


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                var fileName = "default_file_name"; 
                SetSqliteFileOptions(optionsBuilder, fileName);
            }

            base.OnConfiguring(optionsBuilder);
        }

        private static void SetSqliteFileOptions(DbContextOptionsBuilder optionsBuilder, string fileName)
        {
            throw new NotImplementedException();
        }
   }

public class Cube //class for dynamic data of 3D object to be molded

{

    public string? Projectname1 { get; set; }

    public double Height1 { get; set; } //property and field

    public double Width1 { get; set; }

    public double Length1 { get; set; }

    public double Water1 { get; set; }

    public double Plaster1 { get; set; }


}

public class Cylinder //class for dynamic data of 3D object to be molded
{
    public string? Projectname2 { get; set; }
    public double Height2 { get; set; }

    public double Radius2 { get; set; }

    public const double P = double.Pi;

    public double Water2 { get; set; }

    public double Plaster2 { get; set; }
}

public class Sphere //class for dynamic data of 3D object to be molded
{
    public string? Projectname3 { get; set; }
    public double Radius3 { get; set; }

    public const double S = 4 / 3;

    public const double P = double.Pi;

    public double Water3 { get; set; }

    public double Plaster3 { get; set; }
}

public class Pyramid //class for dynamic data of 3D object to be molded
{
    public string? Projectname4 { get; set; }
    public double Height4 { get; set; }

    public double Width4 { get; set; }

    public double Length4 { get; set; }

    public double Water4 { get; set; }

    public double Plaster4 { get; set; }
}

public class ProgramCalculator1
{
    private static readonly MyDbContext dbcontext = new ();
    static void Main()
    {

        {

            Console.WriteLine("Enter a file name to create or use an existing SQLite database:");
            var fileName = Console.ReadLine()!;


            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            MyDbContext.SetSqliteFileOptions(optionsBuilder, fileName);

            using var dbContext = new MyDbContext(optionsBuilder.Options);
        }

        string? userInput;
        do
        {
            Console.WriteLine("________________");
            Console.WriteLine("Hello.");
            Console.WriteLine(1);
            Console.WriteLine(2);
            Console.WriteLine(3);
            Console.WriteLine(4);
            Console.WriteLine("________________");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":

                    Cube cube = new(); //Initiating

                    Console.WriteLine("Please Enter a project name");
                    cube.Projectname1 = Console.ReadLine();

                    if (cube.Projectname1!.Length > 50)
                    {
                        Console.WriteLine("Description exceeds the character limit.");
                    }

                    if (cube.Projectname1.Length == 0)
                    {
                        Console.WriteLine("You must enter a project name too continue.");
                    }

                    Console.WriteLine("Enter the Height, Width, Length");

                    int water = 80;
                    int plaster = 3;

                    double Height1 = Convert.ToDouble(Console.ReadLine());
                    cube.Height1 = Height1;

                    double Width1 = Convert.ToDouble(Console.ReadLine());
                    cube.Width1 = Width1;

                    double Length1 = Convert.ToDouble(Console.ReadLine());
                    cube.Length1 = Length1;

                    double result1 = Height1 * Width1 * Length1;
                    double resultw1 = result1 / water;
                    cube.Water1 = resultw1;
                    double resultp1 = Math.Round(resultw1) * plaster;
                    cube.Plaster1 = resultp1;

                    Console.WriteLine($" {Height1}*{Width1}*{Length1} = {result1} cubic inches ");

                    Console.WriteLine($"You will need {resultw1} quarts of water");

                    Console.WriteLine($"and {resultp1} pounds of plaster.");

                    SaveChangesToDatabase(dbcontext);

                    break;

                case "2":

                    Cylinder cylinder = new();

                    Console.WriteLine("Please Enter a project name");
                    cylinder.Projectname2 = Console.ReadLine();

                    if (cylinder.Projectname2!.Length > 50)
                    {
                        Console.WriteLine("Description exceeds the character limit.");
                    }

                    if (cylinder.Projectname2.Length == 0)
                    {
                        Console.WriteLine("You must enter a project name too continue.");
                    }

                    const int water2 = 80;
                    const int plaster2 = 3;
                    const double pi2 = double.Pi;

                    Console.WriteLine("Enter the Height, Radius");

                    double height2 = Convert.ToDouble(Console.ReadLine());
                    cylinder.Height2 = height2;

                    double radius2 = Convert.ToDouble(Console.ReadLine());
                    cylinder.Radius2 = radius2;

                    double area = radius2 * radius2;
                    double result2 = height2 * area * pi2;
                    double resultw2 = result2 / water2;
                    cylinder.Water2 = resultw2;
                    double resultp2 = Math.Round(resultw2) * plaster2;
                    cylinder.Plaster2 = resultp2;

                    Console.WriteLine($" {height2} * {area} * {pi2} = {result2} cubic inches ");

                    Console.WriteLine($"You will need {resultw2} quarts of water");

                    Console.WriteLine($"and {resultp2} pounds of plaster.");

                    SaveChangesToDatabase(dbcontext);

                    break;


                case "3":

                    Sphere sphere = new();

                    Console.WriteLine("Please Enter a project name");
                    sphere.Projectname3 = Console.ReadLine();

                    if (sphere.Projectname3!.Length > 50)
                    {
                        Console.WriteLine("Description exceeds the character limit.");
                    }

                    if (sphere.Projectname3.Length == 0)
                    {
                        Console.WriteLine("You must enter a project name too continue.");
                    }

                    const double water3 = 80;
                    const double plaster3 = 3;
                    const double pi3 = double.Pi;
                    const double constant3 = 4.0 / 3;

                    Console.WriteLine("Enter the Radius");

                    double radius3 = Convert.ToDouble(Console.ReadLine());
                    sphere.Radius3 = radius3;

                    double area3 = radius3 * radius3;
                    double result3 = area3 * pi3 * constant3;

                    double resultw3 = result3 / water3;
                    sphere.Water3 = resultw3;

                    double resultp3 = Math.Round(resultw3) * plaster3;
                    sphere.Plaster3 = resultp3;

                    Console.WriteLine($" {area3} * {pi3} * {constant3} = {result3} cubic inches ");

                    Console.WriteLine($"You will need {resultw3} quarts of water");

                    Console.WriteLine($"and {resultp3} pounds of plaster.");

                    SaveChangesToDatabase(dbcontext);

                    break;

                case "4":

                    Pyramid pyramid = new();

                    Console.WriteLine("Please Enter a project name");
                    pyramid.Projectname4 = Console.ReadLine();

                    if (pyramid.Projectname4!.Length > 50)
                    {
                        Console.WriteLine("Description exceeds the character limit.");
                    }

                    if (pyramid.Projectname4.Length == 0)
                    {
                        Console.WriteLine("You must enter a project name too continue.");
                    }

                    int water4 = 80;
                    int plaster4 = 3;

                    Console.WriteLine("Enter the Height, Width, Length");

                    double height4 = Convert.ToDouble(Console.ReadLine());
                    pyramid.Height4 = height4;

                    double width4 = Convert.ToDouble(Console.ReadLine());
                    pyramid.Width4 = width4;

                    double length4 = Convert.ToDouble(Console.ReadLine());
                    pyramid.Length4 = length4;

                    double result4 = height4 * width4 * length4;

                    double resultw4 = result4 / water4;
                    pyramid.Water4 = resultw4;

                    double resultp4 = Math.Round(resultw4) * plaster4;
                    pyramid.Plaster4 = resultp4;

                    Console.WriteLine($" {height4} * {width4} * {length4} = {result4} cubic inches ");

                    Console.WriteLine($"You will need {resultw4} quarts of water");

                    Console.WriteLine($"and {resultp4} pounds of plaster.");

                    SaveChangesToDatabase(dbcontext);

                    break;

                default:
                    Console.WriteLine("Unkwon input");
                    break;
            }
            Console.WriteLine("Save or Continue? (Y = yes, N = No, Save = Save Project):");
            userInput = Console.ReadLine();

        } while (userInput?.ToUpper() == "Y");


    }


    static void SaveChangesToDatabase(MyDbContext dbContext)
    {
        Console.WriteLine("Enter 'confirm' to save changes to the database:");
        var confirmation = Console.ReadLine();

        if (confirmation?.ToLower() == "confirm")
        {
            try
            {
                dbContext.SaveChanges();
                Console.WriteLine("Changes saved to the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("Changes were not saved. No confirmation provided.");
        }
    }
}




