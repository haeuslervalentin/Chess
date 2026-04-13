using Classlib;

class Program
{
    public static int Main()
    {
        Board game = new();
        Console.WriteLine(game);
        game.Move(7,0, 6,0);
        return 0;
    }
}