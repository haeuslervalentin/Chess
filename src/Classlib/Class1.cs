using System.Text;
namespace Classlib;

public class Board
{
    public ChessFigure?[,] board = new ChessFigure?[8, 8];

    public void SetFigure(int row, int col, ChessFigure figure)
    {
        if(IsValide(row,col))
        board[row, col] = figure;
    }

    public ChessFigure? GetFigure(int row, int col)
    {
        if(IsValide(row,col))
        {
            return board[row,col];
        }
        return null;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        int counter = 0;
        for(int i = 0; i < 8; i++)
        {
            sb.AppendLine("  +---+---+---+---+---+---+---+---+");
            for(int j = 0; j < 8; j++)
            {
                sb.Append($"  | {board[i,j].PieceType}")
                counter++;
            }
        }
        sb.AppendLine("  +---+---+---+---+---+---+---+---+");
        return sb.ToString();
    }

    private char getSymbol(ChessFigure? figure, bool isDarkField)
    {
         if (figure == null)
        {
            return isDarkField ? '#' : ' '; 
        }

    char symbol = figure.Type switch
    {
        ChessFigure.PieceType.King   => 'K',
        ChessFigure.PieceType.Queen  => 'Q',
        ChessFigure.PieceType.Rook   => 'R',
        ChessFigure.PieceType.Bishop => 'B',
        ChessFigure.PieceType.Knight => 'N', 
        ChessFigure.PieceType.Pawn   => 'P',
        _ => '?'
    };
    
    return figure.Color == ChessFigure.PieceColor.White 
           ? char.ToUpper(symbol) 
           : char.ToLower(symbol);
    }

    private bool IsValide(int row, int col)
    {
        if(!(row >= 0 && row < 8))
        {
            throw new ArgumentException($"Row must be greater than ZERO and smaller than 8. Input: {row}");
        }
        if(!(col >= 0 && col < 8))
        {
            throw new ArgumentException($"Col must be greater than ZERO and smaller than 8. Input: {col}");
        }
        return true;
    }
}

public class ChessFigure
{
    public PieceColor Color {get;}
    public PieceType Type {get;}
    public enum PieceColor 
    {
        White,
        Black
    }

    public enum PieceType
    {
        King,
        Queen,
        Rook,
        Bishop,
        Knight,
        Pawn
    }

    public ChessFigure(PieceColor color, PieceType type)
    {
        Color = color;
        Type = type;
    }
}

public class King : ChessFigure
{
    public King(PieceColor color) : base(color, PieceType.King)
    {
        
    }
}

public class Queen : ChessFigure
{
    public Queen(PieceColor color) : base(color, PieceType.Queen)
    {
        
    }
}

public class Rook : ChessFigure
{
    public Rook(PieceColor color) : base(color, PieceType.Rook)
    {
        
    }
}

public class Bishop : ChessFigure
{
    public Bishop(PieceColor color) : base(color, PieceType.Bishop)
    {
        
    }
}

public class Knight : ChessFigure
{
    public Knight(PieceColor color) : base(color, PieceType.Knight)
    {
        
    }
}

public class Pawn : ChessFigure
{
    public Pawn(PieceColor color) : base(color, PieceType.Pawn)
    {
        
    }
}
