using Classlib;

class Program
{
    public static int Main()
    {
        Board game = new();
        Console.WriteLine(game);
        game.Move(1,0, 2,0);
        Console.WriteLine(game);
        return 0;
    }
}