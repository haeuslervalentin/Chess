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
        if()
    }

}
