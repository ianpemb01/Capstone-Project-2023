using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

class Cube

{
   
    public double Height { get; set; }//property and field

    public double Width { get; set; }

    public double Length { get; set; }

    public double Multiply(double Height, double Width, double Length)
    {
        return (Height*Width*Length);
    }

}

class Cylinder
{
    public double Height { get; set; }
    public double Width { get; set; }
    public const double P = 3.14;
}

class Sphere
{ 
    public double Width { get; set; }
    public const double S = 4 / 3;
    public const double P = 3.14;
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
            Cube cube = new Cube();

            switch (input) 
            {
                case "1":
                    Console.WriteLine("Enter the Height, Width, Length");
                    
                    var Height = Console.ReadLine();
                    var Width = Console.ReadLine();
                    var Length = Console.ReadLine();

                    Console.WriteLine("{Height}*{Width}*{Length} = ");
                    Console.Write((Height, Width, Length));
                    break;
            }
            
        }
       
            
        }
        
    }
    


