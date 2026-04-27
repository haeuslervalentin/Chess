using Classlib;
using Game;

class Program
{
    public static int Main()
    {
        GameClass chessGame = new();
        chessGame.Start();
        return 0;
    }
}