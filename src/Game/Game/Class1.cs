namespace Game;
using Classlib;

public class Game
{
    public Board GameField {get;set;} = new ();
    public ChessFigure.PieceColor CurrentTurn {get; private set;} = ChessFigure.PieceColor.White;

    private void SwitchCurrentPlayer()
    {
        if(CurrentTurn == ChessFigure.PieceColor.White) 
        {
            CurrentTurn = ChessFigure.PieceColor.Black;
            return;
        }
        CurrentTurn = ChessFigure.PieceColor.White;
        return;
    }

    public void TryMove(int start_row, int start_col, int goal_row, int goal_col)
    {
        GameField.IsValide(start_row, start_col);
        GameField.IsValide(goal_row, goal_col);
        ChessFigure FigureToMove = GameField.GetFigure(start_row, start_col);

        if(FigureToMove.PieceColor != CurrentTurn) throw new ArgumentException("Wrong PieceColor! Pick your own pieces to move!");

        if(!FigureToMove.GetAvailableMoves(GameField, start_row, start_col).Contains((goal_row, goal_col))) throw new ArgumentException($"Row: {goal_row}, Col: {goal_col} is not a valide Move!");

        GameField.Move(start_row, start_col, goal_row, goal_col);
        SwitchCurrentPlayer();
    }

    public void Start()
    {
        Console.WriteLine("--- Chess Game ---");
        Console.WriteLine(GameField);
        while(true)
        {
            Console.WriteLine($"PlayerColor {(CurrentTurn == ChessFigure.PieceColor.White ? "White" : "Black")}. IT IS YOUR MOVE.");
            var input = Console.ReadLine();
            int row = input[0] - '0';
            int col = input[2] - '0';

            var figure = GameField.GetFigure(row, col);
            if (figure == null || figure.Color != CurrentTurn) {
                Console.WriteLine("Keine eigene Figur an dieser Stelle!");
                continue;
            }

            Console.WriteLine($"{GameField.ShowMoves(row, col, GameField.GetFigure(row, col).GetAvailableMoves(GameField, row, col))}");
            Console.WriteLine("Where do you want to move: (row, col)");

            input = Console.ReadLine();

            int goal_row = input[0] - '0';
            int goal_col = input[2] - '0';

            TryMove(row, col, goal_row, goal_col);
            Console.WriteLine(GameField);
        }
    }
}
