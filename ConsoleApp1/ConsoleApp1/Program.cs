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
            for (tree = 1; tree < 10; tree++)
            {
                Console.WriteLine(tree);
            }
            //test
        } while (tree != 0);
    }
}