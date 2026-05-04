using Xunit;
using Classlib;
using Game;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Classlib.Test
{

    public abstract class ChessTestBase
        {
            protected Board CreateEmptyBoard()
            {
                var board = new Board();
                // Clear all pieces from starting position
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {                    if (board.GetFigure(i, j) != null)
                        {
                            board.DeleteFigure(i, j);
                        }
                    }
                }
                return board;
            }
        } 
    
    public class BoardTests : ChessTestBase
    {     
        [Fact]
        public void Board_Initialization_CreatesCorrectStartingPosition()
        {
            // Arrange & Act
            var board = new Board();
            
            // Assert - Check back row pieces
            Assert.IsType<Rook>(board.GetFigure(7, 0));
            Assert.IsType<Knight>(board.GetFigure(7, 1));
            Assert.IsType<Bishop>(board.GetFigure(7, 2));
            Assert.IsType<Queen>(board.GetFigure(7, 3));
            Assert.IsType<King>(board.GetFigure(7, 4));
            Assert.IsType<Bishop>(board.GetFigure(7, 5));
            Assert.IsType<Knight>(board.GetFigure(7, 6));
            Assert.IsType<Rook>(board.GetFigure(7, 7));
            
            Assert.IsType<Rook>(board.GetFigure(0, 0));
            Assert.IsType<Knight>(board.GetFigure(0, 1));
            Assert.IsType<Bishop>(board.GetFigure(0, 2));
            Assert.IsType<Queen>(board.GetFigure(0, 3));
            Assert.IsType<King>(board.GetFigure(0, 4));
            Assert.IsType<Bishop>(board.GetFigure(0, 5));
            Assert.IsType<Knight>(board.GetFigure(0, 6));
            Assert.IsType<Rook>(board.GetFigure(0, 7));
            
            // Assert - Check pawns
            for (int col = 0; col < 8; col++)
            {
                Assert.IsType<Pawn>(board.GetFigure(6, col));
                Assert.Equal(ChessFigure.PieceColor.White, board.GetFigure(6, col).Color);
                Assert.IsType<Pawn>(board.GetFigure(1, col));
                Assert.Equal(ChessFigure.PieceColor.Black, board.GetFigure(1, col).Color);
            }
            
            // Assert - Check empty rows
            for (int row = 2; row <= 5; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Assert.Null(board.GetFigure(row, col));
                }
            }
        }
        
        [Fact]
        public void Board_Initialization_CorrectColors()
        {
            // Arrange & Act
            var board = new Board();
            
            // Assert
            Assert.Equal(ChessFigure.PieceColor.Black, board.GetFigure(0, 0).Color);
            Assert.Equal(ChessFigure.PieceColor.White, board.GetFigure(7, 0).Color);
            Assert.Equal(ChessFigure.PieceColor.Black, board.GetFigure(1, 0).Color);
            Assert.Equal(ChessFigure.PieceColor.White, board.GetFigure(6, 0).Color);
        }
        
        [Fact]
        public void GetFigure_ValidPosition_ReturnsFigure()
        {
            // Arrange
            var board = new Board();
            
            // Act
            var figure = board.GetFigure(7, 4);
            
            // Assert
            Assert.NotNull(figure);
            Assert.IsType<King>(figure);
        }
        
        [Fact]
        public void GetFigure_InvalidPosition_ThrowsArgumentException()
        {
            // Arrange
            var board = new Board();
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() => board.GetFigure(-1, 0));
            Assert.Throws<ArgumentException>(() => board.GetFigure(8, 0));
            Assert.Throws<ArgumentException>(() => board.GetFigure(0, -1));
            Assert.Throws<ArgumentException>(() => board.GetFigure(0, 8));
        }
        
        [Fact]
        public void SetFigure_ValidPosition_SetsFigureCorrectly()
        {
            // Arrange
            var board = new Board();
            var pawn = new Pawn(ChessFigure.PieceColor.White);
            
            // Act
            board.SetFigure(3, 3, pawn);
            
            // Assert
            var retrievedFigure = board.GetFigure(3, 3);
            Assert.NotNull(retrievedFigure);
            Assert.IsType<Pawn>(retrievedFigure);
            Assert.Equal(ChessFigure.PieceColor.White, retrievedFigure.Color);
        }
        
        [Fact]
        public void SetFigure_InvalidPosition_ThrowsArgumentException()
        {
            // Arrange
            var board = new Board();
            var pawn = new Pawn(ChessFigure.PieceColor.White);
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() => board.SetFigure(-1, 0, pawn));
        }
        
        [Fact]
        public void DeleteFigure_ValidPosition_RemovesFigure()
        {
            // Arrange
            var board = new Board();
            Assert.NotNull(board.GetFigure(7, 4));
            
            // Act
            board.DeleteFigure(7, 4);
            
            // Assert
            Assert.Null(board.GetFigure(7, 4));
        }
        
        [Fact]
        public void DeleteFigure_InvalidPosition_ThrowsArgumentException()
        {
            // Arrange
            var board = new Board();
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() => board.DeleteFigure(-1, 0));
        }
        
        [Fact]
        public void IsInside_ValidCoordinates_ReturnsTrue()
        {
            // Arrange
            var board = new Board();
            
            // Act & Assert
            Assert.True(board.IsInside(0, 0));
            Assert.True(board.IsInside(7, 7));
            Assert.True(board.IsInside(3, 4));
        }
        
        [Fact]
        public void IsInside_InvalidCoordinates_ReturnsFalse()
        {
            // Arrange
            var board = new Board();
            
            // Act & Assert
            Assert.False(board.IsInside(-1, 0));
            Assert.False(board.IsInside(8, 0));
            Assert.False(board.IsInside(0, -1));
            Assert.False(board.IsInside(0, 8));
        }
        
        [Fact]
        public void ToString_ReturnsFormattedBoard()
        {
            // Arrange
            var board = new Board();
            
            // Act
            string boardString = board.ToString();
            
            // Assert
            Assert.Contains("0---1---2---3---4---5---6---7---", boardString);
            Assert.Contains("+---+---+---+---+---+---+---+---+", boardString);
            Assert.Contains("R", boardString);
            Assert.Contains("K", boardString);
            Assert.Contains("P", boardString);
        }
        
        [Fact]
        public void Move_ValidMove_UpdatesBoard()
        {
            // Arrange
            var board = new Board();
            var pawn = board.GetFigure(6, 0);
            
            // Act
            board.Move(6, 0, 4, 0);
            
            // Assert
            Assert.Null(board.GetFigure(6, 0));
            Assert.NotNull(board.GetFigure(4, 0));
            Assert.IsType<Pawn>(board.GetFigure(4, 0));
            Assert.True(board.GetFigure(4, 0).isMoved);
        }
        
        [Fact]
        public void Move_InvalidMove_ThrowsArgumentException()
        {
            // Arrange
            var board = new Board();
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() => board.Move(3, 0, 4, 0));
            Assert.Throws<ArgumentException>(() => board.Move(-1, 0, 4, 0));
        }
        
    [Fact]
    public void Move_CaptureOpponentPiece_UpdatesBoard()
    {
        var board = new Board();
        board.SetFigure(4, 1, new Pawn(ChessFigure.PieceColor.White));
        board.SetFigure(3, 0, new Pawn(ChessFigure.PieceColor.Black));
        
        board.Move(3, 0, 4, 1);
        
        Assert.IsType<Pawn>(board.GetFigure(4, 1));
        Assert.Equal(ChessFigure.PieceColor.Black, board.GetFigure(4, 1).Color); // Schwarzer Bauer hat geschlagen
        Assert.Null(board.GetFigure(3, 0)); // Alte Position ist leer
    } 
    }
    
    public class KingTests : ChessTestBase
    {
        [Fact]
        public void GetAvailableMoves_CenterPosition_ReturnsAllDirections()
        {
            // Arrange
            var board = new Board();
            var king = new King(ChessFigure.PieceColor.White);
            board.SetFigure(3, 3, king);
            
            // Act
            var moves = king.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.Equal(8, moves.Count);
            Assert.Contains((2, 2), moves);
            Assert.Contains((2, 3), moves);
            Assert.Contains((2, 4), moves);
            Assert.Contains((3, 2), moves);
            Assert.Contains((3, 4), moves);
            Assert.Contains((4, 2), moves);
            Assert.Contains((4, 3), moves);
            Assert.Contains((4, 4), moves);
        }
        
        [Fact]
        public void GetAvailableMoves_CornerPosition_ReturnsLimitedMoves()
        {
            // Arrange
            var board = new Board();
            var king = new King(ChessFigure.PieceColor.White);
            board.SetFigure(0, 0, king);
            
            // Act
            var moves = king.GetAvailableMoves(board, 0, 0);
            
            // Assert
            Assert.Equal(3, moves.Count);
            Assert.Contains((0, 1), moves);
            Assert.Contains((1, 0), moves);
            Assert.Contains((1, 1), moves);
        }
        
        [Fact]
        public void GetAvailableMoves_CanCaptureOpponent()
        {
            // Arrange
            var board = new Board();
            var king = new King(ChessFigure.PieceColor.White);
            var opponentPawn = new Pawn(ChessFigure.PieceColor.Black);
            board.SetFigure(3, 3, king);
            board.SetFigure(3, 4, opponentPawn);
            
            // Act
            var moves = king.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.Contains((3, 4), moves);
        }
        
        [Fact]
        public void GetAvailableMoves_CannotCaptureOwnPiece()
        {
            // Arrange
            var board = new Board();
            var king = new King(ChessFigure.PieceColor.White);
            var ownPawn = new Pawn(ChessFigure.PieceColor.White);
            board.SetFigure(3, 3, king);
            board.SetFigure(3, 4, ownPawn);
            
            // Act
            var moves = king.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.DoesNotContain((3, 4), moves);
        }
        
        [Fact]
        public void GetAvailableMoves_NullFigure_ReturnsEmptyList()
        {
            // Arrange
            var board = new Board();
            
            // Act
            var king = new King(ChessFigure.PieceColor.White);
            var moves = king.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.Empty(moves);
        }
    }
    
    public class QueenTests : ChessTestBase
    {
        [Fact]
        public void GetAvailableMoves_CenterPosition_ReturnsAllDirections()
        {
                    // Arrange - Use empty board
            var board = CreateEmptyBoard();
            var queen = new Queen(ChessFigure.PieceColor.White);
            board.SetFigure(3, 3, queen);
            
            // Act
            var moves = queen.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.Contains((0, 3), moves); // Up
            Assert.Contains((7, 3), moves); // Down
            Assert.Contains((3, 0), moves); // Left
            Assert.Contains((3, 7), moves); // Right
            Assert.Contains((0, 0), moves); // Up-Left diagonal
            Assert.Contains((7, 7), moves); // Down-Right diagonal
            Assert.Contains((0, 6), moves); // Up-Right diagonal
            Assert.Contains((6, 0), moves); // Down-Left diagonal (6,0 not 7,0)
        }
        
        [Fact]
        public void GetAvailableMoves_BlockedByOwnPiece_StopsBefore()
        {
            // Arrange
            var board = new Board();
            var queen = new Queen(ChessFigure.PieceColor.White);
            var ownPawn = new Pawn(ChessFigure.PieceColor.White);
            board.SetFigure(3, 3, queen);
            board.SetFigure(5, 3, ownPawn);
            
            // Act
            var moves = queen.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.Contains((4, 3), moves);
            Assert.DoesNotContain((5, 3), moves);
            Assert.DoesNotContain((6, 3), moves);
        }
        
        [Fact]
        public void GetAvailableMoves_CanCaptureOpponent()
        {
            // Arrange
            var board = new Board();
            var queen = new Queen(ChessFigure.PieceColor.White);
            var opponentPawn = new Pawn(ChessFigure.PieceColor.Black);
            board.SetFigure(3, 3, queen);
            board.SetFigure(5, 3, opponentPawn);
            
            // Act
            var moves = queen.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.Contains((5, 3), moves);
            Assert.DoesNotContain((6, 3), moves);
        }
        
        [Fact]
        public void GetAvailableMoves_NullFigure_ReturnsEmptyList()
        {
            // Arrange
            var board = new Board();
            var queen = new Queen(ChessFigure.PieceColor.White);
            
            // Act
            var moves = queen.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.Empty(moves);
        }
    }
    
    public class RookTests : ChessTestBase
    {
        [Fact]
        public void GetAvailableMoves_CenterPosition_ReturnsStraightLines()
        {
            var board = CreateEmptyBoard();
            var rook = new Rook(ChessFigure.PieceColor.White);
            board.SetFigure(3, 3, rook);
            
            // Act
            var moves = rook.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.Contains((0, 3), moves); // Up
            Assert.Contains((7, 3), moves); // Down
            Assert.Contains((3, 0), moves); // Left
            Assert.Contains((3, 7), moves); // Right
            
            // Should not contain diagonal moves
            Assert.DoesNotContain((0, 0), moves);
            Assert.DoesNotContain((7, 7), moves);
        }
        
        [Fact]
        public void GetAvailableMoves_BlockedByOpponent_CanCapture()
        {
            // Arrange
            var board = new Board();
            var rook = new Rook(ChessFigure.PieceColor.White);
            var opponentPawn = new Pawn(ChessFigure.PieceColor.Black);
            board.SetFigure(3, 3, rook);
            board.SetFigure(3, 6, opponentPawn);
            
            // Act
            var moves = rook.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.Contains((3, 6), moves);
            Assert.DoesNotContain((3, 7), moves);
        }
    }
    
    public class BishopTests : ChessTestBase
    {
        [Fact]
        public void GetAvailableMoves_CenterPosition_ReturnsDiagonals()
        {
            var board = CreateEmptyBoard();
            var bishop = new Bishop(ChessFigure.PieceColor.White);
            board.SetFigure(3, 3, bishop);
            
            // Act
            var moves = bishop.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.Contains((0, 0), moves);
            Assert.Contains((7, 7), moves);
            Assert.Contains((0, 6), moves);
            Assert.Contains((6, 0), moves);
            
            // Should not contain straight moves
            Assert.DoesNotContain((0, 3), moves);
            Assert.DoesNotContain((3, 0), moves);
        }
        
        [Fact]
        public void GetAvailableMoves_BlockedByOwnPiece_StopsBefore()
        {
            // Arrange
            var board = new Board();
            var bishop = new Bishop(ChessFigure.PieceColor.White);
            var ownPawn = new Pawn(ChessFigure.PieceColor.White);
            board.SetFigure(3, 3, bishop);
            board.SetFigure(5, 5, ownPawn);
            
            // Act
            var moves = bishop.GetAvailableMoves(board, 3, 3);
            
            // Assert
            Assert.Contains((4, 4), moves);
            Assert.DoesNotContain((5, 5), moves);
            Assert.DoesNotContain((6, 6), moves);
        }
    }
    
    public class KnightTests : ChessTestBase
    {
        [Fact]
        public void GetAvailableMoves_CenterPosition_ReturnsAllKnightMoves()
        {
            var board = CreateEmptyBoard();
            var knight = new Knight(ChessFigure.PieceColor.White);
            board.SetFigure(4, 4, knight);
            
            // Act
            var moves = knight.GetAvailableMoves(board, 4, 4);
            
            // Assert
            Assert.Equal(8, moves.Count);
            Assert.Contains((2, 3), moves);
            Assert.Contains((2, 5), moves);
            Assert.Contains((3, 2), moves);
            Assert.Contains((3, 6), moves);
            Assert.Contains((5, 2), moves);
            Assert.Contains((5, 6), moves);
            Assert.Contains((6, 3), moves);
            Assert.Contains((6, 5), moves);
        }
        
        [Fact]
        public void GetAvailableMoves_CanJumpOverPieces()
        {
            var board = CreateEmptyBoard();
            var knight = new Knight(ChessFigure.PieceColor.White);
            board.SetFigure(4, 4, knight);
            
            // Surround knight with pieces
            board.SetFigure(4, 3, new Pawn(ChessFigure.PieceColor.White));
            board.SetFigure(4, 5, new Pawn(ChessFigure.PieceColor.Black));
            board.SetFigure(3, 4, new Pawn(ChessFigure.PieceColor.White));
            board.SetFigure(5, 4, new Pawn(ChessFigure.PieceColor.Black));
            
            // Act
            var moves = knight.GetAvailableMoves(board, 4, 4);
            
            // Assert - Knight should be able to jump over pieces
            // From (4,4): (2,3), (2,5), (6,3), (6,5), (3,2), (3,6), (5,2), (5,6)
            Assert.Equal(8, moves.Count);
            Assert.Contains((2, 3), moves);
            Assert.Contains((2, 5), moves);
            Assert.Contains((6, 3), moves);
            Assert.Contains((6, 5), moves);
        }
        
        [Fact]
        public void GetAvailableMoves_CornerPosition_ReturnsLimitedMoves()
        {
            // Arrange
            var board = new Board();
            var knight = new Knight(ChessFigure.PieceColor.White);
            board.SetFigure(0, 0, knight);
            
            // Act
            var moves = knight.GetAvailableMoves(board, 0, 0);
            
            // Assert
            Assert.Equal(2, moves.Count);
            Assert.Contains((1, 2), moves);
            Assert.Contains((2, 1), moves);
        }
    }
    
    public class PawnTests : ChessTestBase
    {
        [Fact]
        public void GetAvailableMoves_WhitePawn_CanMoveOneStepForward()
        {
            // Arrange
            var board = new Board();
            var pawn = new Pawn(ChessFigure.PieceColor.White);
            board.SetFigure(6, 0, pawn);
            
            // Act
            var moves = pawn.GetAvailableMoves(board, 6, 0);
            
            // Assert
            Assert.Contains((5, 0), moves);
            Assert.Contains((4, 0), moves); // Double move
        }
        
        [Fact]
        public void GetAvailableMoves_BlackPawn_CanMoveOneStepForward()
        {
            // Arrange
            var board = new Board();
            var pawn = new Pawn(ChessFigure.PieceColor.Black);
            board.SetFigure(1, 0, pawn);
            
            // Act
            var moves = pawn.GetAvailableMoves(board, 1, 0);
            
            // Assert
            Assert.Contains((2, 0), moves);
            Assert.Contains((3, 0), moves); // Double move
        }
        
        [Fact]
        public void GetAvailableMoves_WhitePawn_CanCaptureDiagonally()
        {
            // Arrange
            var board = new Board();
            var pawn = new Pawn(ChessFigure.PieceColor.White);
            var opponentPawn = new Pawn(ChessFigure.PieceColor.Black);
            board.SetFigure(6, 3, pawn);
            board.SetFigure(5, 2, opponentPawn);
            board.SetFigure(5, 4, opponentPawn);
            
            // Act
            var moves = pawn.GetAvailableMoves(board, 6, 3);
            
            // Assert
            Assert.Contains((5, 2), moves);
            Assert.Contains((5, 4), moves);
            Assert.Contains((5, 3), moves); // Forward move
        }
        
        [Fact]
        public void GetAvailableMoves_Pawn_CannotMoveForwardIfBlocked()
        {
            // Arrange
            var board = new Board();
            var pawn = new Pawn(ChessFigure.PieceColor.White);
            var blockingPiece = new Pawn(ChessFigure.PieceColor.Black);
            board.SetFigure(6, 0, pawn);
            board.SetFigure(5, 0, blockingPiece);
            
            // Act
            var moves = pawn.GetAvailableMoves(board, 6, 0);
            
            // Assert
            Assert.DoesNotContain((5, 0), moves);
            Assert.DoesNotContain((4, 0), moves); // Double move also blocked
        }
        
        [Fact]
        public void GetAvailableMoves_Pawn_AfterMoveCannotDoubleMove()
        {
            // Arrange
            var board = new Board();
            var pawn = new Pawn(ChessFigure.PieceColor.White);
            pawn.isMoved = true;
            board.SetFigure(6, 0, pawn);
            
            // Act
            var moves = pawn.GetAvailableMoves(board, 6, 0);
            
            // Assert
            Assert.Contains((5, 0), moves);
            Assert.DoesNotContain((4, 0), moves); // No double move
        }
        
        [Fact]
        public void GetAvailableMoves_Pawn_AtEndOfBoard_ReturnsEmpty()
        {
            // Arrange
            var board = new Board();
            var pawn = new Pawn(ChessFigure.PieceColor.White);
            board.SetFigure(0, 0, pawn); // White pawn at the end
            
            // Act
            var moves = pawn.GetAvailableMoves(board, 0, 0);
            
            // Assert
            Assert.Empty(moves);
        }
    }
    
    public class GameClassTests : ChessTestBase
    {
        [Fact]
        public void GameClass_Initialization_WhiteStarts()
        {
            // Arrange & Act
            var game = new GameClass();
            
            // Assert
            Assert.Equal(ChessFigure.PieceColor.White, game.CurrentTurn);
            Assert.NotNull(game.GameField);
        }
        
        [Fact]
        public void TryMove_ValidMove_SwitchesTurn()
        {
            // Arrange
            var game = new GameClass();
            var initialTurn = game.CurrentTurn;
            
            // Act
            game.TryMove(6, 0, 5, 0); // Move white pawn
            
            // Assert
            Assert.NotEqual(initialTurn, game.CurrentTurn);
            Assert.Equal(ChessFigure.PieceColor.Black, game.CurrentTurn);
        }
        
        [Fact]
        public void TryMove_WrongColor_ThrowsArgumentException()
        {
            // Arrange
            var game = new GameClass();
            
            // Act & Assert - Try to move black pawn when it's white's turn
            Assert.Throws<ArgumentException>(() => game.TryMove(1, 0, 2, 0));
        }
        
        [Fact]
        public void TryMove_EmptyPosition_ThrowsArgumentException()
        {
            // Arrange
            var game = new GameClass();
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() => game.TryMove(3, 0, 4, 0));
        }
        
        [Fact]
        public void TryMove_InvalidCoordinates_ThrowsArgumentException()
        {
            // Arrange
            var game = new GameClass();
            
            // Act & Assert
            Assert.Throws<ArgumentException>(() => game.TryMove(-1, 0, 4, 0));
            Assert.Throws<ArgumentException>(() => game.TryMove(6, 0, -1, 0));
        }
        
        [Fact]
        public void TryMove_InvalidMove_ThrowsArgumentException()
        {
            // Arrange
            var game = new GameClass();
            
            // Act & Assert - Try to move pawn backwards
            Assert.Throws<ArgumentException>(() => game.TryMove(6, 0, 7, 0));
        }
        
        [Fact]
        public void TryMove_CompleteGame_AlternatesCorrectly()
        {
            // Arrange
            var game = new GameClass();
            
            // Act & Assert
            Assert.Equal(ChessFigure.PieceColor.White, game.CurrentTurn);
            game.TryMove(6, 0, 4, 0); // White pawn
            Assert.Equal(ChessFigure.PieceColor.Black, game.CurrentTurn);
            game.TryMove(1, 0, 3, 0); // Black pawn
            Assert.Equal(ChessFigure.PieceColor.White, game.CurrentTurn);
            game.TryMove(7, 1, 5, 2); // White knight
            Assert.Equal(ChessFigure.PieceColor.Black, game.CurrentTurn);
        }
    }
    
    public class ShowMovesTests : ChessTestBase
    {
        [Fact]
        public void ShowMoves_EmptyBoard_ShowsCorrectFormatting()
        {
            // Arrange
            var board = new Board();
            var moves = new List<(int row, int col)> { (3, 3), (4, 4) };
            
            // Act
            string result = board.ShowMoves(0, 0, moves);
            
            // Assert
            Assert.Contains("(+)" , result);
            Assert.Contains("0   1   2   3   4   5   6   7", result);
            Assert.Contains("+---+---+---+---+---+---+---+---+", result);
        }
        
        [Fact]
        public void ShowMoves_WithCapture_ShowsCapturedPiece()
        {
            var board = new Board();
            // Move a pawn forward so we can see capture possibility
            var moves = new List<(int row, int col)> { (1, 0) }; // Target black pawn position
        
            // Act
            string result = board.ShowMoves(2, 1, moves); // Show move to capture at (1,0)
        
            // Assert - Should show captured pawn at (1,0) 
            Assert.Contains("(p)", result);
        }
    }
    
    public class IntegrationTests
    {
        [Fact]
        public void FullGameFlow_SeveralMoves_WorksCorrectly()
        {
            // Arrange
            var game = new GameClass();
            
            // Act & Assert - E4
            game.TryMove(6, 4, 4, 4);
            Assert.IsType<Pawn>(game.GameField.GetFigure(4, 4));
            Assert.Null(game.GameField.GetFigure(6, 4));
            
            // E5
            game.TryMove(1, 4, 3, 4);
            Assert.IsType<Pawn>(game.GameField.GetFigure(3, 4));
            
            // Nf3
            game.TryMove(7, 6, 5, 5);
            Assert.IsType<Knight>(game.GameField.GetFigure(5, 5));
            
            // Nc6
            game.TryMove(0, 1, 2, 2);
            Assert.IsType<Knight>(game.GameField.GetFigure(2, 2));
            
            Assert.Equal(ChessFigure.PieceColor.White, game.CurrentTurn);
        }
        
        [Fact]
        public void CaptureSequence_WorksCorrectly()
        {
            // Arrange
            var game = new GameClass();
            game.TryMove(6, 3, 4, 3); // d4
            game.TryMove(1, 4, 3, 4); // e5
            
            // Act - Capture with pawn
            game.TryMove(4, 3, 3, 4); // dxe5
            
            // Assert
            Assert.IsType<Pawn>(game.GameField.GetFigure(3, 4));
            Assert.Equal(ChessFigure.PieceColor.White, game.GameField.GetFigure(3, 4).Color);
        }
    }
    
    public class ChessFigureTests : ChessTestBase
    {
        [Fact]
        public void ChessFigure_Constructor_SetsCorrectProperties()
        {
            // Arrange & Act
            var king = new King(ChessFigure.PieceColor.White);
            var queen = new Queen(ChessFigure.PieceColor.Black);
            
            // Assert
            Assert.Equal(ChessFigure.PieceType.King, king.Type);
            Assert.Equal(ChessFigure.PieceColor.White, king.Color);
            Assert.Equal(ChessFigure.PieceType.Queen, queen.Type);
            Assert.Equal(ChessFigure.PieceColor.Black, queen.Color);
        }
        
        [Fact]
        public void isMoved_DefaultValue_IsFalse()
        {
            // Arrange & Act
            var piece = new Pawn(ChessFigure.PieceColor.White);
            
            // Assert
            Assert.False(piece.isMoved);
        }
    }
}