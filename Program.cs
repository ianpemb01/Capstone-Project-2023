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
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { } //SRP of solid principle, MyDbcontext is only for handling database executions.

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
            return Path.Join(path, "MMC.db");
        }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}


public class Cube //Only contains classes and methods pertaining to the creation of cuboid molds, OCP

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

public class Cylinder //Only contains classes and methods pertaining to the creation of cuboid molds
{
    public string? Projectname2 { get; set; }
    public double Height2 { get; set; }

    public double Radius2 { get; set; }

    public const double P = double.Pi;

    public double Water2 { get; set; }

    public double Plaster2 { get; set; }
} 

public class Sphere //Only contains classes and methods pertaining to the creation of cuboid molds
{
    public string? Projectname3 { get; set; }
    public double Radius3 { get; set; }

    public const double S = 4.0 / 3;

    public const double P = double.Pi;

    public double Water3 { get; set; }

    public double Plaster3 { get; set; }
}  

public class Pyramid //Only contains classes and methods pertaining to the creation of cuboid molds
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
    
    public static async Task Main()
    {

        using var dbContext = new MyDbContext();
        dbContext.Database.EnsureCreated();

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
                    Console.Write("Height: ");

                    double Height1 = Convert.ToDouble(Console.ReadLine());
                    cube.Height1 = Height1;

                    Console.Write("Width: ");

                    double Width1 = Convert.ToDouble(Console.ReadLine());
                    cube.Width1 = Width1;

                    Console.Write("Length: ");

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

                    dbContext.Cubes.Add( cube );

                    await dbContext.SaveChangesAsync();

                    Console.WriteLine("Would you like to see the full list of project names in the cube category?(Y = Yes)");

                    userInput = Console.ReadLine()!.ToUpper();

                    if (userInput == "Y")
                    {
                        var projectNameList = dbContext.Cubes.Select(cube=>cube.Projectname1);

                        foreach (var projectName in projectNameList)
                        {
                            Console.WriteLine($"{projectName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Okay, not showing the list.");
                    }

                    var allCubes = dbContext.GetAllCubes();
                    foreach (var retreivedCube in allCubes)

                    {
                        Console.WriteLine($"Cube: Project Name - {retreivedCube.Projectname1}, Water amount - {retreivedCube.Water1} quarts of water, Plaster amount - {retreivedCube.Plaster1} pounds of plaster");
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
                    Console.Write("Height: ");

                    double height2 = Convert.ToDouble(Console.ReadLine());
                    cylinder.Height2 = height2;

                    Console.Write("Radius: ");

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

                    dbContext.Cylinders.Add( cylinder );

                    await dbContext.SaveChangesAsync();

                    var allCylinders = dbContext.GetAllCylinders();
                    foreach (var retreivedCylinder in allCylinders)

                    {
                        Console.WriteLine($"Cube: Project Name - {retreivedCylinder.Projectname2}, Water amount - {retreivedCylinder.Water2} quarts of water, Plaster amount - {retreivedCylinder.Plaster2} pounds of plaster");
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

                    Console.Write("Radius: ");

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

                    dbContext.Spheres.Add(sphere);

                    await dbContext.SaveChangesAsync();

                    var allSpheres = dbContext.GetAllSpheres();
                    foreach (var retreivedSpheres in allSpheres)

                    {
                        Console.WriteLine($"Cube: Project Name - {retreivedSpheres.Projectname3}, Water amount - {retreivedSpheres.Water3} quarts of water, Plaster amount - {retreivedSpheres.Plaster3} pounds of plaster");
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
                    Console.Write("Height: ");

                    double height4 = Convert.ToDouble(Console.ReadLine());
                    pyramid.Height4 = height4;

                    Console.Write("Width: ");

                    double width4 = Convert.ToDouble(Console.ReadLine());
                    pyramid.Width4 = width4;

                    Console.Write("Length: ");

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

                    dbContext.Pyramids.Add(pyramid);

                    await dbContext.SaveChangesAsync();

                    var allPyramids = dbContext.GetAllPyramids();
                    foreach (var retreivedPyramids in allPyramids)

                    {
                        Console.WriteLine($"Cube: Project Name - {retreivedPyramids.Projectname4}, Water amount - {retreivedPyramids.Water4} quarts of water, Plaster amount - {retreivedPyramids.Plaster4} pounds of plaster");
                    }

                    break;

                default:
                    Console.WriteLine("Unkwon input");
                    break;
            }
            Console.WriteLine("Type Y to continue. (Y = yes):");
            userInput = Console.ReadLine()!;

        } while (userInput?.ToUpper() == "Y");


    }


}





