using Xunit;
using Game;
using Classlib;

namespace Chess.Tests
{
    public class GameLogicTests
    {
        [Fact]
        public void TryMove_ShouldUpdateBoardAndSwitchTurn()
        {
            var game = new GameClass();

            game.TryMove(6, 0, 5, 0);

            Assert.Null(game.GameField.GetFigure(6, 0));
            Assert.NotNull(game.GameField.GetFigure(5, 0));
            Assert.Equal(ChessFigure.PieceColor.Black, game.CurrentTurn);
        }

        [Fact]
        public void TryMove_WrongColor_ShouldThrowException()
        {
            var game = new GameClass();

            var exception = Assert.Throws<ArgumentException>(() => game.TryMove(1, 0, 2, 0));
            Assert.Equal("Wrong PieceColor! Pick your own pieces to move!", exception.Message);
        }

        [Fact]
        public void TryMove_InvalidMove_ShouldThrowException()
        {
            var game = new GameClass();

            Assert.Throws<ArgumentException>(() => game.TryMove(6, 0, 3, 0));
        }

        [Fact]
        public void IsMoved_Flag_ShouldBeTrueAfterFirstMove()
        {
            var game = new GameClass();
            var figure = game.GameField.GetFigure(6, 0);

            Assert.False(figure.isMoved);

            game.TryMove(6, 0, 5, 0);

            Assert.True(figure.isMoved);
        }
    }
}