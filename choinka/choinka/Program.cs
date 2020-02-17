using System;

public class Example
{
    public static void Main()
    {
        string userInput;
        Console.WriteLine("Jakigo znaku chcesz użyć do rysowania choinki?");
        char sign;
        sign = char.Parse(Console.ReadLine());
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
                string ile = new string(sign, i+1);
                int spacja = (high - i);
                string move = new string(' ', spacja);

                int right = i;
                string newright = new string(sign, right);
                Console.WriteLine(move + ile + newright);
            }
        }
        Console.ReadKey();
    }
}