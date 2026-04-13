using System.Security.Cryptography;
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

    public Board()
    {
        Build();
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
    
        for(int i = 0; i < 8; i++)
        {
            sb.AppendLine("  +---+---+---+---+---+---+---+---+");
            for(int j = 0; j < 8; j++)
            {
                char symbol = getSymbol(board[i,j], (i+j) %2 != 0);
                sb.Append($" | {symbol}");   
            }
            sb.AppendLine(" |");
        }
        sb.AppendLine("  +---+---+---+---+---+---+---+---+");
        return sb.ToString();
    }

    public void Build()
    {

        SetupBackrow(7, ChessFigure.PieceColor.White);
        SetupBackrow(0, ChessFigure.PieceColor.Black);

        for (int col = 0; col < 8; col++)
        {
            board[6, col] = new Pawn(ChessFigure.PieceColor.White);
            board[1, col] = new Pawn(ChessFigure.PieceColor.Black);
        }
    }

    private void SetupBackrow(int row, ChessFigure.PieceColor color)
    {
        board[row, 0] = new Rook(color);
        board[row, 7] = new Rook(color);
        
        board[row, 1] = new Knight(color);
        board[row, 6] = new Knight(color);
        
        board[row, 2] = new Bishop(color);
        board[row, 5] = new Bishop(color);
        
        board[row, 3] = new Queen(color);
        board[row, 4] = new King(color);
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
        if(!IsInside(row,col))
        {
            throw new ArgumentException($"Invalid coordinates: {row} {col}");
        }
        return true;
    }
/*
    public (int row, int col)? GetExactPos(ChessFigure.PieceType wanted, ChessFigure.PieceColor color)
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                if(board[i,j] != null && board[i,j].Type == wanted && board[i,j].Color == color)
                {
                    return (i,j);
                }
            }
        }
        return null;
    }*/
    public bool IsInside(int row, int col) => row >= 0 && row < 8 && col >= 0 && col < 8;

    public void Move(int currentPosRow, int currentPosCol, int goalPosRow, int goalPosCol)
    {
        var PieceToMove = GetFigure(currentPosRow, currentPosCol);

        if(PieceToMove == null)
        {
            throw new ArgumentException($"There is no figure to be moved {currentPosRow} {currentPosCol}");
        }

        var availableMoves = PieceToMove.GetAvailableMoves(this,currentPosRow, currentPosCol);
        if(availableMoves.Contains((goalPosRow, goalPosCol)))
        {
            DeleteFigure(currentPosRow, currentPosCol);
            SetFigure(goalPosRow, goalPosCol, PieceToMove);
        }
    }

    public void DeleteFigure(int row, int col)
    {
        if(IsInside(row,col))
        {
            board[row,col] = null;
            return;
        }

        throw new ArgumentException("There is no figure to be deleted!");
    }
}

public abstract class ChessFigure
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
    public abstract List<(int row, int col)> GetAvailableMoves(Board board,int currentRow, int currentCol);
}

public class King : ChessFigure
{
    public King(PieceColor color) : base(color, PieceType.King)
    {
        var Color = color;
    }

    private static readonly (int row, int col)[] _moveOffsets = new[]
    {
        (0, 1), (0, -1), (1, 0), (-1, 0), 
        (1, 1), (1, -1), (-1, 1), (-1, -1)
    };

    public override List<(int row, int col)> GetAvailableMoves(Board board,int currentRow, int currentCol)
    {
        var moves = new List<(int row, int col)>();

        if(board.GetFigure(currentRow,currentCol) == null) return moves;

        foreach(var offset in _moveOffsets)
        {
            int targetRow = currentRow + offset.row;
            int targetCol = currentCol + offset.col;

            if(!board.IsInside(targetRow,targetCol))
            {
                continue;
            }

            var targetPiece = board.GetFigure(targetRow, targetCol);
                
            if (targetPiece == null || targetPiece.Color != this.Color)
            {
                moves.Add((targetRow,targetCol));
            }
        }
        return moves;
    }

}
public class Queen : ChessFigure
{
    public Queen(PieceColor color) : base(color, PieceType.Queen)
    {
        
    }
    private static readonly (int row, int col)[] _moveOffsets = new[]
    {
        (0, 1), (0, -1), (1, 0), (-1, 0), 
        (1, 1), (1, -1), (-1, 1), (-1, -1)
    };

    public override List<(int row, int col)> GetAvailableMoves(Board board,int currentRow, int currentCol)
    {
        var moves = new List<(int row, int col)>();

        if(board.GetFigure(currentRow,currentCol) == null) return moves;

        foreach(var offset in _moveOffsets)
        {
            int targetRow = currentRow + offset.row;
            int targetCol = currentCol + offset.col;

            if(!board.IsInside(targetRow,targetCol))
            {
                continue;
            }

            var targetPiece = board.GetFigure(targetRow, targetCol);
                
            if (targetPiece == null || targetPiece.Color != this.Color)
            {
                moves.Add((targetRow,targetCol));
            }
        }
        return moves;
    }
}

public class Rook : ChessFigure
{
    public Rook(PieceColor color) : base(color, PieceType.Rook)
    {
        
    }
    private static readonly (int row, int col)[] _moveOffsets = new[]
    {
        (0, 1), (0, -1), (1, 0), (-1, 0), 
        (1, 1), (1, -1), (-1, 1), (-1, -1)
    };

    public override List<(int row, int col)> GetAvailableMoves(Board board,int currentRow, int currentCol)
    {
        var moves = new List<(int row, int col)>();

        if(board.GetFigure(currentRow,currentCol) == null) return moves;

        foreach(var offset in _moveOffsets)
        {
            int targetRow = currentRow + offset.row;
            int targetCol = currentCol + offset.col;

            if(!board.IsInside(targetRow,targetCol))
            {
                continue;
            }

            var targetPiece = board.GetFigure(targetRow, targetCol);
                
            if (targetPiece == null || targetPiece.Color != this.Color)
            {
                moves.Add((targetRow,targetCol));
            }
        }
        return moves;
    }
}

public class Bishop : ChessFigure
{
    public Bishop(PieceColor color) : base(color, PieceType.Bishop)
    {
        
    }
    private static readonly (int row, int col)[] _moveOffsets = new[]
    {
        (0, 1), (0, -1), (1, 0), (-1, 0), 
        (1, 1), (1, -1), (-1, 1), (-1, -1)
    };

    public override List<(int row, int col)> GetAvailableMoves(Board board,int currentRow, int currentCol)
    {
        var moves = new List<(int row, int col)>();

        if(board.GetFigure(currentRow,currentCol) == null) return moves;

        foreach(var offset in _moveOffsets)
        {
            int targetRow = currentRow + offset.row;
            int targetCol = currentCol + offset.col;

            if(!board.IsInside(targetRow,targetCol))
            {
                continue;
            }

            var targetPiece = board.GetFigure(targetRow, targetCol);
                
            if (targetPiece == null || targetPiece.Color != this.Color)
            {
                moves.Add((targetRow,targetCol));
            }
        }
        return moves;
    }
}

public class Knight : ChessFigure
{
    private static readonly (int row, int col)[] _moveOffsets = new[]
    {
        (2, 1),   (2, -1),  
        (-2, 1),  (-2, -1), 
        (1, 2),   (1, -2),  
        (-1, 2),  (-1, -2)
    };

    public override List<(int row, int col)> GetAvailableMoves(Board board,int currentRow, int currentCol)
    {
        var moves = new List<(int row, int col)>();

        if(board.GetFigure(currentRow,currentCol) == null) return moves;

        foreach(var offset in _moveOffsets)
        {
            int targetRow = currentRow + offset.row;
            int targetCol = currentCol + offset.col;

            if(!board.IsInside(targetRow, targetCol))
            {
                continue;
            }

            var targetPiece = board.GetFigure(targetRow, targetCol);

            if(targetPiece == null || targetPiece.Color != this.Color)
            {
                moves.Add((targetRow, targetCol));
            }
        }
        return moves;
    }

    public Knight(PieceColor color) : base(color, PieceType.Knight)
    {
        
    }
}

public class Pawn : ChessFigure
{
    public Pawn(PieceColor color) : base(color, PieceType.Pawn)
    {
        
    }

    public override List<(int row, int col)> GetAvailableMoves(Board board,int currentRow, int currentCol)
    {
        var moves = new List<(int row, int col)>();

        if(board.GetFigure(currentRow,currentCol) == null) return moves;

        var direction = this.Color == ChessFigure.PieceColor.White ? -1 : 1;

        
        int targetRow = currentRow + direction;
            int targetCol = currentCol;

        if(!board.IsInside(targetRow, targetCol))
        {
            return moves;
        }

        var targetPiece = board.GetFigure(targetRow, targetCol);

        if(targetPiece == null)
        {
            moves.Add((targetRow, targetCol));
        }
        if(!board.IsInside(targetRow, targetCol-1)) return moves;
        if(!board.IsInside(targetRow, targetCol+1)) return moves;
        if(board.GetFigure(targetRow,targetCol-1) != null && board.GetFigure(targetRow,targetCol-1).Color != this.Color) moves.Add((targetRow, targetCol-1));
        if(board.GetFigure(targetRow,targetCol+1) != null && board.GetFigure(targetRow, targetCol+1).Color != this.Color) moves.Add((targetRow, targetCol+1));
    
        return moves;
    }

}
