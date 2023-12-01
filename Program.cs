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

    public List<string?> GetEntities<T>(string propertyName) where T : class
    {
        return [.. Set<T>().Select(entity => entity.GetType().GetProperty(propertyName)!.GetValue(entity)!.ToString()).ToList()];
    }

    public MyDbContext()
    {
        
    }

    public List<Cube> GetAllCubes()
    {
        return [.. Cubes];
    }

    public List<Cylinder> GetAllCylinders()
    {
        return [.. Cylinders];
    }

    public List<Sphere> GetAllSpheres()
    {
        return [.. Spheres];
    }

    public List<Pyramid> GetAllPyramids()
    {
        return [.. Pyramids];
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

    public static string DbPath
    {
        get
        {
            const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            return Path.Join(path, "C:\\Users\\15025\\OneDrive\\Desktop\\Documents\\Code\\Capstone-Project\\MoldMakingCalculator\\MMC.db");
        }
    }
    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source = C:\\Users\\15025\\OneDrive\\Desktop\\Documents\\Code\\Capstone-Project\\MoldMakingCalculator\\MMC.db");
   }



public class Cube //class for dynamic data of 3D object to be molded

{

    public string? Projectname1 { get; set; }

    public double Height1 { get; set; } //property and field

    public double Width1 { get; set; }

    public double Length1 { get; set; }

    public double Water1 { get; set; }

    public double Plaster1 { get; set; }

    public double CalculateVolume()
    {
        return Height1 * Width1 * Length1;
    }

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

public class WaterConstant
{
    public const int water = 80;
}

public class PlasterConstant
{
    public const int plaster = 3;
}

public class ProgramCalculator1
{
    private static readonly MyDbContext dbcontext = new ();
    static void Main()
    {

        using var dbContext = new MyDbContext(new DbContextOptions<MyDbContext>());
        

        string userInput;
        do
        {
            Console.WriteLine("________________");
            Console.WriteLine("Hello.Please pick the base shape of the object you would like to mold.");
            Console.WriteLine("One for cube");
            Console.WriteLine("Two for cylinder");
            Console.WriteLine("Three for sphere");
            Console.WriteLine("Four for pyramid");
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

                    double Height1 = Convert.ToDouble(Console.ReadLine());
                    cube.Height1 = Height1;

                    double Width1 = Convert.ToDouble(Console.ReadLine());
                    cube.Width1 = Width1;

                    double Length1 = Convert.ToDouble(Console.ReadLine());
                    cube.Length1 = Length1;

                    double volume1 = cube.CalculateVolume();
                    double water1 = volume1 / WaterConstant.water;
                    cube.Water1 = water1;
                    double plaster1 = Math.Round(water1) * PlasterConstant.plaster;
                    cube.Plaster1 = plaster1;

                    Console.WriteLine($" {Height1}*{Width1}*{Length1} = {volume1} cubic inches ");

                    Console.WriteLine($"You will need {water1} quarts of water");

                    Console.WriteLine($"and {plaster1} pounds of plaster.");

                    SaveChangesToDatabase(dbcontext);

                    Console.WriteLine("Would you like to see the full list of project names in the cube category?");

                    userInput = Console.ReadLine()!.ToUpper();

                    if (userInput == "Y")
                    {
                        var projectNameList = dbContext.GetEntities<Cube>("ProjectName1");

                        foreach (var projectName in projectNameList)
                        {
                            Console.WriteLine($"{projectName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Okay, not showing the list.");
                    }

                    var allCubes = dbcontext.GetAllCubes();
                    foreach (var retreivedCube in allCubes)

                    {
                        Console.WriteLine($"Cube: Project Name - {cube.Projectname1}, Water amount - {cube.Water1} quarts of water, Plaster amount - {cube.Plaster1} pounds of plaster");
                    }

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

                    const double pi2 = double.Pi;

                    Console.WriteLine("Enter the Height, Radius");

                    double height2 = Convert.ToDouble(Console.ReadLine());
                    cylinder.Height2 = height2;

                    double radius2 = Convert.ToDouble(Console.ReadLine());
                    cylinder.Radius2 = radius2;

                    double area = radius2 * radius2;
                    double result2 = height2 * area * pi2;
                    double resultw2 = result2 / WaterConstant.water;
                    cylinder.Water2 = resultw2;
                    double resultp2 = Math.Round(resultw2) * PlasterConstant.plaster;
                    cylinder.Plaster2 = resultp2;

                    Console.WriteLine($" {height2} * {area} * {pi2} = {result2} cubic inches ");

                    Console.WriteLine($"You will need {resultw2} quarts of water");

                    Console.WriteLine($"and {resultp2} pounds of plaster.");

                    SaveChangesToDatabase(dbcontext);

                    var allCylinders = dbcontext.GetAllCylinders();
                    foreach (var retreivedCylinder in allCylinders)

                    {
                        Console.WriteLine($"Cube: Project Name - {cylinder.Projectname2}, Water amount - {cylinder.Water2} quarts of water, Plaster amount - {cylinder.Plaster2} pounds of plaster");
                    }

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

                    const double pi3 = double.Pi;
                    const double constant3 = 4.0 / 3;

                    Console.WriteLine("Enter the Radius");

                    double radius3 = Convert.ToDouble(Console.ReadLine());
                    sphere.Radius3 = radius3;

                    double area3 = radius3 * radius3;
                    double result3 = area3 * pi3 * constant3;

                    double resultw3 = result3 / WaterConstant.water;
                    sphere.Water3 = resultw3;

                    double resultp3 = Math.Round(resultw3) * PlasterConstant.plaster;
                    sphere.Plaster3 = resultp3;

                    Console.WriteLine($" {area3} * {pi3} * {constant3} = {result3} cubic inches ");

                    Console.WriteLine($"You will need {resultw3} quarts of water");

                    Console.WriteLine($"and {resultp3} pounds of plaster.");

                    SaveChangesToDatabase(dbcontext);

                    var allSpheres = dbcontext.GetAllSpheres();
                    foreach (var retreivedSpheres in allSpheres)

                    {
                        Console.WriteLine($"Cube: Project Name - {sphere.Projectname3}, Water amount - {sphere.Water3} quarts of water, Plaster amount - {sphere.Plaster3} pounds of plaster");
                    }

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

                    Console.WriteLine("Enter the Height, Width, Length");

                    double height4 = Convert.ToDouble(Console.ReadLine());
                    pyramid.Height4 = height4;

                    double width4 = Convert.ToDouble(Console.ReadLine());
                    pyramid.Width4 = width4;

                    double length4 = Convert.ToDouble(Console.ReadLine());
                    pyramid.Length4 = length4;

                    double result4 = height4 * width4 * length4;

                    double resultw4 = result4 / WaterConstant.water;
                    pyramid.Water4 = resultw4;

                    double resultp4 = Math.Round(resultw4) * PlasterConstant.plaster;
                    pyramid.Plaster4 = resultp4;

                    Console.WriteLine($" {height4} * {width4} * {length4} = {result4} cubic inches ");

                    Console.WriteLine($"You will need {resultw4} quarts of water");

                    Console.WriteLine($"and {resultp4} pounds of plaster.");

                    SaveChangesToDatabase(dbcontext);

                    var allPyramids = dbcontext.GetAllPyramids();
                    foreach (var retreivedPyramids in allPyramids)

                    {
                        Console.WriteLine($"Cube: Project Name - {pyramid.Projectname4}, Water amount - {pyramid.Water4} quarts of water, Plaster amount - {pyramid.Plaster4} pounds of plaster");
                    }

                    break;

                default:
                    Console.WriteLine("Unkwon input");
                    break;
            }
            Console.WriteLine("Save or Continue? (Y = yes, N = No, Save = Save Project):");
            userInput = Console.ReadLine()!;

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
    }
}





