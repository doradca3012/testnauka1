using System;

public class Example
{
    public static void Main()
    {
        string userInput;
        Console.WriteLine("Jak wysoka ma być choinka?");
        userInput = Console.ReadLine();
        bool isNumber = int.TryParse(userInput, out var high);
        if (isNumber == false)
        {
            Console.WriteLine("Nie sciemaniaj, podaj liczbę");
        }
        else
        {
            for (int i = 0; i < high; i++)
            {
                string ile = new string('*', i+1);
                string spacja = new string(' ', high);
                Console.WriteLine(spacja+ile);
            }
        }
        Console.ReadKey();
    }
}