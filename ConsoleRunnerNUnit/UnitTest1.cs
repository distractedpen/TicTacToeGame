using System.Security.Cryptography;
using System.Text;

namespace ConsoleRunnerNUnit;

using ConsoleRunner;

public class ConsoleRunnerTests
{

    [Test]
    public void ConsoleRunner_PrintBoardDisplaysValidBoard()
    {
        const string expected = """
                                -----
                                O|1|2
                                3|X|5
                                6|7|8
                                -----
                                
                                """;
        var board = new[] { -1, 0, 0, 0, 1, 0, 0, 0, 0 };

        var actual = Program.PrintBoard(board);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void ConsoleRunner_PrintBoardDisplaysErrorMessage()
    {
        const string expected = """
                                Position already taken
                                -----
                                O|1|2
                                3|X|5
                                6|7|8
                                -----
                                
                                """;
        var board = new[] { -1, 0, 0, 0, 1, 0, 0, 0, 0 };
        const string errorMessage = "Position already taken";
        var actual = Program.PrintBoard(board, errorMessage);
        Assert.That(actual, Is.EqualTo(expected));

    }

    [Test]
    public void ConsoleRunner_ConfirmEndGameReturnsTrueWhenUserEntersY()
    {
        var sw = new StringWriter();
        var sr = new StringReader("Y\n");
        var actual = Program.ConfirmEndGame(sw, sr);
        sw.Close();
        sr.Close();
        Assert.That(actual, Is.False); // User Types Y to continue, so endGame is False
    }

    [Test]
    public void ConsoleRunner_DisplayResultsPlayer1Wins()
    {
        var sw = new StringWriter();
        Program.DisplayResult(sw, 1);
        var actual = sw.ToString();
        sw.Close();
        Assert.That(actual, Is.EqualTo("Player 1 Wins!\n"));
    }
    
    [Test]
    public void ConsoleRunner_DisplayResultsPlayer2Wins()
    {
        var sw = new StringWriter();
        Program.DisplayResult(sw, -1);
        var actual = sw.ToString();
        sw.Close();
        Assert.That(actual, Is.EqualTo("Player 2 Wins!\n"));
    }
    
    [Test]
    public void ConsoleRunner_DisplayResultsTie()
    {
        var sw = new StringWriter();
        Program.DisplayResult(sw, 0);
        var actual = sw.ToString();
        sw.Close();
        Assert.That(actual, Is.EqualTo("Tie!\n"));
    }

    [Test]
    public void ConsoleRunner_GetUserInputInvalidOption()
    {
        var validInputs = new[] { "valid" };
        var sr = new StringReader("69420\n");
        var actual = Program.GetUserInput(sr, validInputs) ;
        sr.Close();
        Assert.That(actual, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ConsoleRunner_RunMainMenuInvalidThenUserQuits()
    {
        var sr = new StringReader("8\nq\n");
        var sw = new StringWriter();
        var actual = Program.RunMainMenu(sw, sr);
        var swActualRaw = sw.ToString();
        var swActual = swActualRaw[^16..];
        Assert.Multiple(() =>
        {
            Assert.That(swActual, Is.EqualTo("Invalid Option.\n"));
            Assert.That(actual, Is.EqualTo("q"));
        });
    }
}