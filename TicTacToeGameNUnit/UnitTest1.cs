using TicTacToeGame;

namespace TicTacToeGameNUnit;

[TestFixture]
public class TicTacToeGameIsShould
{
    private Game _game;
    
    [SetUp]
    public void Setup()
    {
        _game = new Game();
    }

    [Test]
    public void Game_BoardIsInitializedEmpty()
    {
        var board = _game.GetBoard();
        foreach (var t in board)
        {
            Assert.That(t, Is.EqualTo(0));
        }
    }

    [Test]
    public void Game_MakeMoveSetsPlayer2Marker()
    {
        _game.MakeMove(0);
        _game.MakeMove(1);
        var board = _game.GetBoard();
        Assert.That(board[1], Is.EqualTo(-1));
    }

    [Test]
    public void Game_MakeMoveReturnsFalseWhenInvalidPosition()
    {
        _game.MakeMove(0);
        var valid = _game.MakeMove(0);
        Assert.That(valid, Is.False);
    }
    

    [Test]
    public void Game_IsPlayer1TurnAfter2Turns()
    {
        _game.MakeMove(0);
        _game.MakeMove(1);
        Assert.That(_game.GetPlayer(), Is.EqualTo(1));
    }

    [Test]
    public void Game_IsPlayer1Winner()
    {
        _game.MakeMove(0);
        _game.MakeMove(2);
        _game.MakeMove(3);
        _game.MakeMove(8);
        _game.MakeMove(6);
        Assert.That(_game.DetermineWinner(), Is.EqualTo(1));
    }

    [Test]
    public void Game_IsPlayer2Winner()
    {
        _game.MakeMove(2);
        _game.MakeMove(0);
        _game.MakeMove(8);
        _game.MakeMove(3);
        _game.MakeMove(4);
        _game.MakeMove(6);
        Assert.That(_game.DetermineWinner(), Is.EqualTo(-1));
    }

    [Test]
    public void Game_IsTie()
    {
        _game.MakeMove(4);
        _game.MakeMove(0);
        _game.MakeMove(1);
        _game.MakeMove(2);
        _game.MakeMove(5);
        _game.MakeMove(3);
        _game.MakeMove(6);
        _game.MakeMove(7);
        _game.MakeMove(8);
        Assert.That(_game.DetermineWinner(), Is.EqualTo(0));
    }

    [Test]
    public void Game_5TurnsRemaining()
    {
        _game.MakeMove(0);
        _game.MakeMove(1);
        _game.MakeMove(2);
        _game.MakeMove(3);
        Assert.That(_game.GetTurnsRemaining(), Is.EqualTo(5));
    }
    
}