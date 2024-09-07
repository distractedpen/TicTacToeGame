using System.Text;
using TicTacToeGame;

namespace ConsoleRunner;

public class Program
{
    private const string HorizontalSpacer = "-----";
    private const char Player1Char = 'X';
    private const char Player2Char = 'O';
    private const char VerticalSpacer = '|';
    private const char NewLine = '\n';

    /**
     * Console Runner for TicTacToeGame Class Library
     */
    
    public static void Main(string[] args)
    {
        var endGame = false;
        while (!endGame)
        {
            var userSelection = RunMainMenu(Console.Out, Console.In);
            if (userSelection != "q")
            {
                var result = RunGame(Console.Out, Console.In);
                DisplayResult(Console.Out, result);
                endGame = ConfirmEndGame(Console.Out, Console.In);
            }
        }
    }

    public static bool ConfirmEndGame(TextWriter outWriter, TextReader inReader)
    {
        outWriter.WriteLine("Play Again? y/N");
        var input = GetUserInput(inReader, ["y", "Y"]);
        return input == string.Empty;
    }

    public static void DisplayResult(TextWriter outWriter, int results)
    {
        if (results == 1)
            outWriter.WriteLine("Player 1 Wins!");
        else if (results == -1)
            outWriter.WriteLine("Player 2 Wins!");
        else
            outWriter.WriteLine("Tie!");
    }

    public static string RunMainMenu(TextWriter outWriter, TextReader inReader)
    {
        var validInputs = new[] { "q", "2" };
        outWriter.WriteLine("""
                              Welcome to TicTacToe.
                                2 ) Two Players.
                                q ) Quit.               
                              Please choose a game mode:
                              """
        );
        var input = "";
        while (input == string.Empty)
        {
            input = GetUserInput(inReader, validInputs);
            if (input == string.Empty)
                outWriter.WriteLine("Invalid Option.");
        }
        return input;
    }

    /**
     * Get and Validated User Input based on given valid options
     * :params validInputs string array of valid options
     * :returns input string.Empty if no valid option selected, else valid option
     */
    public static string GetUserInput(TextReader inReader, string[] validInputs)
    {
        var input = inReader.ReadLine();
        foreach (var t in validInputs)
        {
            if (input == t)
            {
                return input;
            }
        }
        return string.Empty;
    }

    /**
     * Run the Game using the Game Class Library
     * :param gameMode 1=One Player, 2=Two Player
     * :returns -1=Player 2 wins, 0=Tie, 1=Player 1 wins, -2=Game Quit
     */
    public static int RunGame(TextWriter outWriter, TextReader inReader)
    {
        var game = new Game();
        string? errorMessage = null;
        var board = game.GetBoard();
        while (game.GetTurnsRemaining() > 0)
        {
            // show board state
            outWriter.Flush();
            outWriter.Write(PrintBoard(board, errorMessage));
            errorMessage = null; // clear errorMessage once board has been printed
            var input = "";
            while (input == string.Empty)
            {
                outWriter.WriteLine();
                outWriter.WriteLine(game.GetPlayer() == 1 ? "Player 1 Turn" : "Player 2 Turn");
                outWriter.WriteLine("Chose a position ('q' to quit): ");
                input = GetUserInput(inReader, ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "q"]);
                
                if (input == "q") return -2;

                var position = int.Parse(input);
                if (!game.MakeMove(position))
                    errorMessage = "Position already taken";
                
                if (input == string.Empty)
                    errorMessage = "Invalid Option";
            }

            var winner = game.DetermineWinner();
            if (winner != 0) return winner;
        }
        
        outWriter.Flush();
        outWriter.Write(PrintBoard(board));
        // technically this will always be zero if we reach this?
        return game.DetermineWinner();
    }


    /**
     * Print the Tic Tac Toe Board
     * :params board integer array representing the game board
     */
    public static string PrintBoard(int[] board, string? errorMessage = null)
    {
        var sb = new StringBuilder();
        if (errorMessage != null) sb.Append(errorMessage + NewLine);
        sb.Append(HorizontalSpacer + NewLine);
        for (var row = 0; row < 3; row++)
        {
            for (var col = 0; col < 3; col++)
            {
                var index = 3 * row + col;
                var value = board[index];
                switch (value)
                {
                    case 1:
                        sb.Append(Player1Char);
                        break;
                    case -1:
                        sb.Append(Player2Char);
                        break;
                    default:
                        sb.Append(index);
                        break;
                }
                if (col < 2)
                    sb.Append(VerticalSpacer);
            }
            sb.Append(NewLine);
        }
        sb.Append(HorizontalSpacer + NewLine);

        return sb.ToString();
    }
}