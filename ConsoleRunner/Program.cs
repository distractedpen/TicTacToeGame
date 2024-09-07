using TicTacToeGame;

namespace ConsoleRunner;

class Program
{
    /**
     * Console Runner for TicTacToeGame Class Library
     */
    public static void Main(string[] args)
    {
        var gamePlayed = false;
        while (true)
        {
            Console.Out.WriteLine("""
                                  Welcome to TicTacToe.
                                    2 ) Two Players.
                                    q ) Quit.               
                                  Please choose a game mode:
                                  """
            );
            var input = "";
            input = GetUserInput(["q", "2"]);
            
            if (input == "q") break;
            if (input == "2")
            {
                var gameStatus = RunGame(2);
                if (gameStatus != 0)
                {
                    Console.Out.WriteLine(gameStatus == 1 ? "Player 1 Wins!" : "Player 2 Wins!");
                }
                else
                {
                    Console.Out.WriteLine("Tie!");
                }
                gamePlayed = true;
            }
            else
            {
                Console.Out.WriteLine("Invalid Option.");
            }
            
            if (!gamePlayed) continue;
            
            Console.Out.WriteLine("Play Again? y/N");
            input = GetUserInput(["y", "Y"]);
            if (input == string.Empty) break;
        }
        
        Console.Out.WriteLine("Good Bye!");
    }

    /**
     * Get and Validated User Input based on given valid options
     * :params validInputs string array of valid options
     * :returns input string.Empty if no valid option selected, else valid option
     */
    private static string GetUserInput(string[] validInputs)
    {
        var input = Console.In.ReadLine();
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
    private static int RunGame(int gameMode)
    {
        var game = new Game();
        string? errorMessage = null;
        var board = game.GetBoard();
        while (game.GetTurnsRemaining() > 0)
        {
            // show board state
            PrintBoard(board, errorMessage);
            errorMessage = null; // clear errorMessage once board has been printed
            var input = "";
            while (input == string.Empty)
            {
                Console.Out.WriteLine();
                Console.Out.WriteLine(game.GetPlayer() == 1 ? "Player 1 Turn" : "Player 2 Turn");
                Console.Out.WriteLine("Chose a position ('q' to quit): ");
                input = GetUserInput(["0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "q"]);
                
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
        
        // technically this will always be zero if we reach this?
        PrintBoard(board);
        return game.DetermineWinner();
    }


    /**
     * Print the Tic Tac Toe Board
     * :params board integer array representing the game board
     */
    private static void PrintBoard(int[] board, string? errorMessage = null)
    {
        Console.Clear();
        if (errorMessage != null) Console.Out.WriteLine(errorMessage);
        Console.Out.WriteLine("-----");
        for (var row = 0; row < 3; row++)
        {
            for (var col = 0; col < 3; col++)
            {
                var index = 3 * row + col;
                var value = board[index];
                switch (value)
                {
                    case 1:
                        Console.Out.Write(col == 2 ? "X" : "X|");
                        break;
                    case -1:
                        Console.Out.Write(col == 2 ? "O" : "O|");
                        break;
                    default:
                        Console.Out.Write(col == 2 ? $"{index}" : $"{index}|");
                        break;
                }
            }
            Console.Out.Write("\n");
            Console.Out.WriteLine("-----");
        }
    }
}