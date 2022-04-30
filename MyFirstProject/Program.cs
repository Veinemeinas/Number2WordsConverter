using System;
using System.Text;

namespace Number2WordsConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(120, 20);
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Skaičių konvertavimas į žodinę formą";
            Console.WriteLine("Skaičių konvertavimas į žodinę formą.\nPrograma lengvai ir greitai pateiks jums atsakymą į įvestą skaičių.\nGalima konvertuoti sveikus skaičius nuo 0 iki 999 nanilijonų", Console.ForegroundColor = ConsoleColor.Blue);
            Console.WriteLine("Įveskite bet kokį sveiką skaičių nuo 0 iki 999999999999999999999999999999999.\n", Console.ForegroundColor = ConsoleColor.White);

            string number = Console.ReadLine();

            Number2Words number2Words = new Number2Words();
            Console.WriteLine($"{number} => {number2Words.GetNumber(number)}");
            Console.ReadLine();
        }
    }
}
