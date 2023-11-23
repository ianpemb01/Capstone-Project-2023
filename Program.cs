using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


class Cube

{
    public double Height { get; set; }//property and field
   
    public double Width { get; set; }

    public double Length { get; set; }
}

class Cylinder
{
    public double Height { get; set; }
    public double Width { get; set; }

    public const double P = double.Pi;
}

class Sphere
{ 
    public double Width { get; set; }

    public const double S = 4 / 3;

    public const double P = double.Pi;
}

class Pyramid
{
    public double Width { get; set; }
    public double Height { get; set; }
    public double Length { get; set; }
}
namespace MMC
    {
    class Program
    {
        static void Main(string[] args) 
        
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

                    Cube cube = new Cube();

                    int water = 80;
                    int plaster = 3;

                    Console.WriteLine("Enter the Height, Width, Length");
                    
                    double height1 = Convert.ToDouble(Console.ReadLine());
                    double width1 = Convert.ToDouble(Console.ReadLine());
                    double length1 = Convert.ToDouble(Console.ReadLine());

                    double result1 = height1 * width1 * length1;
                    double resultw1 = result1 / water;
                    double resultp1 = Math.Round(resultw1) * plaster;

                    Console.WriteLine($" {height1} * {width1} * {length1} = {result1} cubic inches ");

                    Console.WriteLine($"You will need {resultw1} quarts of water");

                    Console.WriteLine($"and {resultp1} pounds of plaster.");

                    break;

                case "2":

                    int water2 = 80;
                    int plaster2 = 3;
                    double pi2 = double.Pi;
                    

                    Cylinder cylinder = new Cylinder();

                    Console.WriteLine("Enter the Height, Radius");

                    double height2 = Convert.ToDouble(Console.ReadLine());
                    double radius2 = Convert.ToDouble(Console.ReadLine());
                    
                    double area = radius2 * radius2;
                    double result2 = height2 * area * pi2;
                    double resultw2 = result2 / water2;
                    double resultp2 = Math.Round(resultw2) * plaster2;

                    Console.WriteLine($" {height2} * {area} * {pi2} = {result2} cubic inches ");

                    Console.WriteLine($"You will need {resultw2} quarts of water");

                    Console.WriteLine($"and {resultp2} pounds of plaster.");

                    break;


                case "3":

                    int water3 = 80;
                    int plaster3 = 3;
                    double pi3 = double.Pi;
                    double constant3 = 4 / 3;

                    Sphere sphere = new Sphere();

                    Console.WriteLine("Enter the Radius");
                   
                    double radius3 = Convert.ToDouble(Console.ReadLine());
                    
                    double area3 = radius3 * radius3;
                    double result3 = area3 * pi3 * constant3;
                    double resultw3 = result3 / water3;
                    double resultp3 = Math.Round(resultw3) * plaster3;

                    Console.WriteLine($" {area3} * {pi3} * {constant3} = {result3} cubic inches ");

                    Console.WriteLine($"You will need {resultw3} quarts of water");

                    Console.WriteLine($"and {resultp3} pounds of plaster.");

                    break;

                case "4":

                    int water4 = 80;
                    int plaster4 = 3;

                    Pyramid pyramid = new Pyramid();

                    Console.WriteLine("Enter the Height, Width, Length");

                    double height4 = Convert.ToDouble(Console.ReadLine());
                    double width4 = Convert.ToDouble(Console.ReadLine());
                    double length4 = Convert.ToDouble(Console.ReadLine());

                    double result4 = height4 * width4 * length4;
                    double resultw4 = result4 / water4;
                    double resultp4 = Math.Round(resultw4) * plaster4;

                    Console.WriteLine($" {height4} * {width4} * {length4} = {result4} cubic inches ");

                    Console.WriteLine($"You will need {resultw4} quarts of water");

                    Console.WriteLine($"and {resultp4} pounds of plaster.");

                    break;
            }
            
        }
       
            
        }
        
    }
    


