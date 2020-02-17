using System;

public class Example
{
    public static void Main()
    {
        int tree;
        Console.WriteLine("Jak wysoka ma być choinka:");
        do
        {
            string input = Console.ReadLine();
             tree = int.Parse(input);
           
            //test
        } while (tree != 0);
    }
}