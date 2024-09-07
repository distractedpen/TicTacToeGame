namespace TicTacToeGame;

public class Game
{
    private int[] _board;
    private int _turnsLeft;
    private int _player;

    public Game()
    {
        _board = new []{0,0,0,0,0,0,0,0,0};
        _turnsLeft = _board.Length;
        _player = 1;
    }
    
    // get board
    public int[] GetBoard()
    {
        return _board;
    }
    
    // get turn
    public int GetTurnsRemaining()
    {
        return _turnsLeft;
    }

    public int GetPlayer()
    {
        return _player;
    }
    
    // make move
    public bool MakeMove(int position)
    {
        if (!ValidateMove(position))
        {
            return false;
        }

        _board[position] = _player;
        _player = -_player;
        _turnsLeft--;
        return true;
    }
    
    // validate move
    private bool ValidateMove(int position)
    {
        return (_board[position] == 0);
    }
    
    /**
     * Determine the winner of the game
     * :returns 1=Player 1 Wins, -1=Player 2 Wins, 0=No winner
     */
    public int DetermineWinner()
    {
        
        
        // check rows
        if ((_board[6] + _board[7] + _board[8] == 3) || // check rows
            (_board[3] + _board[4] + _board[5] == 3) ||
            (_board[0] + _board[1] + _board[2] == 3) ||
            (_board[6] + _board[3] + _board[0] == 3) || // check cols
            (_board[7] + _board[4] + _board[1] == 3) ||
            (_board[8] + _board[5] + _board[2] == 3) ||
            (_board[6] + _board[4] + _board[2] == 3) || // check diags
            (_board[8] + _board[4] + _board[0] == 3))
            return 1;
            
        if ((_board[6] + _board[7] + _board[8] == -3) || // check rows
            (_board[3] + _board[4] + _board[5] == -3) ||
            (_board[0] + _board[1] + _board[2] == -3) ||
            (_board[6] + _board[3] + _board[0] == -3) || // check cols
            (_board[7] + _board[4] + _board[1] == -3) ||
            (_board[8] + _board[5] + _board[2] == -3) ||
            (_board[6] + _board[4] + _board[2] == -3) || // check diags
            (_board[8] + _board[4] + _board[0] == -3))
            return -1;

        return 0;
    }
}