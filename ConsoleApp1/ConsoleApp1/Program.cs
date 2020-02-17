using System;

public class Example
{
    public static void Main()
    {
        
        Console.WriteLine("Jak wysoka ma być choinka:");
        do
        {
            string input = Console.ReadLine();
            int tree; 
            bool high = int.TryParse(Console.ReadLine, out tree);
            
            
            for (tree = 1; tree < 10; tree++)
            {
                Console.WriteLine(tree);
            }
            //test
        } while (tree != 0);
    }
}