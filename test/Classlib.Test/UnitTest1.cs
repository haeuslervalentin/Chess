using Xunit;
using Classlib;
using System.Linq;

public class BoardTests
{
    [Fact]
    public void Board_ShouldInitializeWithPieces()
    {
        var board = new Board();

        Assert.NotNull(board.GetFigure(0, 0)); // Black Rook
        Assert.NotNull(board.GetFigure(7, 4)); // White King
        Assert.NotNull(board.GetFigure(1, 0)); // Black Pawn
        Assert.NotNull(board.GetFigure(6, 0)); // White Pawn
    }

    [Fact]
    public void GetFigure_InvalidPosition_ShouldThrow()
    {
        var board = new Board();

        Assert.Throws<ArgumentException>(() => board.GetFigure(8, 8));
    }

    [Fact]
    public void Move_ValidMove_ShouldUpdateBoard()
    {
        var board = new Board();

        board.Move(6, 0, 5, 0); // White Pawn forward

        Assert.Null(board.GetFigure(6, 0));
        Assert.NotNull(board.GetFigure(5, 0));
    }

    [Fact]
    public void Move_InvalidMove_ShouldNotMovePiece()
    {
        var board = new Board();

        board.Move(6, 0, 4, 0); // aktuell NICHT erlaubt (kein 2-step)

        Assert.NotNull(board.GetFigure(6, 0));
        Assert.Null(board.GetFigure(4, 0));
    }

    [Fact]
    public void Move_NoPiece_ShouldThrow()
    {
        var board = new Board();

        Assert.Throws<ArgumentException>(() => board.Move(4, 4, 5, 5));
    }
}

public class PawnTests
{
    [Fact]
    public void Pawn_ShouldMoveForward_IfEmpty()
    {
        var board = new Board();

        var pawn = board.GetFigure(6, 0);
        var moves = pawn.GetAvailableMoves(board, 6, 0);

        Assert.Contains((5, 0), moves);
    }

    [Fact]
    public void Pawn_ShouldNotMoveForward_IfBlocked()
    {
        var board = new Board();

        // Blockiere Feld vor Pawn
        board.SetFigure(5, 0, new Pawn(ChessFigure.PieceColor.White));

        var pawn = board.GetFigure(6, 0);
        var moves = pawn.GetAvailableMoves(board, 6, 0);

        Assert.DoesNotContain((5, 0), moves);
    }

    [Fact]
    public void Pawn_ShouldCaptureDiagonally()
    {
        var board = new Board();

        // Gegner setzen
        board.SetFigure(5, 1, new Pawn(ChessFigure.PieceColor.Black));

        var pawn = board.GetFigure(6, 0);
        var moves = pawn.GetAvailableMoves(board, 6, 0);

        Assert.Contains((5, 1), moves);
    }

    [Fact]
    public void Pawn_ShouldNotCrash_OnBoardEdge()
    {
        var board = new Board();

        var pawn = board.GetFigure(6, 0);
        var moves = pawn.GetAvailableMoves(board, 6, 0);

        Assert.NotNull(moves);
    }
}

public class RookTests
{
    [Fact]
    public void Rook_ShouldHaveNoMoves_AtStart()
    {
        var board = new Board();

        var rook = board.GetFigure(7, 0);
        var moves = rook.GetAvailableMoves(board, 7, 0);

        Assert.Empty(moves);
    }

    [Fact]
    public void Rook_ShouldMove_WhenPathIsClear()
    {
        var board = new Board();

        board.DeleteFigure(6, 0); // Pawn entfernen

        var rook = board.GetFigure(7, 0);
        var moves = rook.GetAvailableMoves(board, 7, 0);

        Assert.Contains((6, 0), moves);
    }
}

public class KnightTests
{
    [Fact]
    public void Knight_ShouldJumpOverPieces()
    {
        var board = new Board();

        var knight = board.GetFigure(7, 1);
        var moves = knight.GetAvailableMoves(board, 7, 1);

        Assert.Contains((5, 0), moves);
        Assert.Contains((5, 2), moves);
    }
}
public class KingTests
{
    [Fact]
    public void King_ShouldHaveNoMoves_AtStart()
    {
        var board = new Board();

        var king = board.GetFigure(7, 4);
        var moves = king.GetAvailableMoves(board, 7, 4);

        Assert.Empty(moves);
    }

    [Fact]
    public void King_ShouldMove_WhenFree()
    {
        var board = new Board();

        board.DeleteFigure(6, 4);

        var king = board.GetFigure(7, 4);
        var moves = king.GetAvailableMoves(board, 7, 4);

        Assert.Contains((6, 4), moves);
    }
}