using System;
using System.Collections.Generic;

public class Example
{
    public static void Main()
    {
        var treeList = new List<ChristmasTree> {
            // new StaticCharChristmasTree('@'),
            // new StaticCharChristmasTree('$'),
            new ChristmasTree()
    };

        foreach(var tree in treeList) {
            tree.AskForParameters();
            tree.Print();
        }

        Console.ReadKey();
    }  
}

public class StaticCharChristmasTree: ChristmasTree {
    private char predefinedChar { get; set; }
    public StaticCharChristmasTree(char _predefinedChar) {
        predefinedChar = _predefinedChar;
    }

    protected override char AskForTreeChar() {
        return predefinedChar;
    }
}

public class ChristmasTree
{
    private int Height { get; set; }
    private char TreeChar { get; set; }

    public void Print()
    {
        for (int i = 0; i < Height; i++)
        {
            string ile = new string(TreeChar, i + 1);
            int spacja = (Height - i);
            string move = new string(' ', spacja);

            int right = i;
            string newright = new string(TreeChar, right);
            Console.WriteLine(move + ile + newright);
        }
    }

    protected virtual char AskForTreeChar() {
        Console.WriteLine("Jakigo znaku chcesz użyć do rysowania choinki? (*, #)");
        while (true)
        {
            var selectedChar = Console.ReadKey().KeyChar;
            if (selectedChar == '*' || selectedChar == '#') {
                return selectedChar;
            }
        }
    }

    public int AskForHeight()
    {
        Console.WriteLine();
        Console.WriteLine("Jak wysoka ma być choinka?");
        var userInput = Console.ReadLine();
        bool isNumber = int.TryParse(userInput, out var height);

        return height;
    }

    public void AskForParameters()
    {
        TreeChar = AskForTreeChar();

        height = AskForHeight();
    }
    
}

