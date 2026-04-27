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
        try
        {
            ChessFigure FigureToMove = GameField.GetFigure(start_row, start_col);
        }

        if(FigureToMove != CurrentTurn) throw new ArgumentException("Wrong PieceColor! Pick your own pieces to move!");

        if(!FigureToMove.GetAvailableMoves.Contains((goal_row, goal_col))) throw new ArgumentException($"Row: {goal_row}, Col: {goal_col} is not a valide Move!");

        GameField.Move(start_row, start_col, goal_row, goal_col);
        SwitchCurrentPlayer();
    }

}
