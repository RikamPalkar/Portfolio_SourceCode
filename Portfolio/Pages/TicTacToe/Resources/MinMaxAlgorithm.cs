using System.ComponentModel;

namespace Portfolio.Pages.TicTacToe.Resources
{
    public class MinMaxAlgorithm
    {

        #region [Brain]
        /// <summary>
        /// This is the minimax function. It considers all
        //  the possible ways the game can go and returns
        //  the value of the board
        /// </summary>
        static int Minnimax(char[,] board,
                        int depth, Boolean isMax)
        {
            int score = Evaluate(board);

            // If AI has won the game return his/her evaluated score
            if (score == 10)
                return score;

            // If AI has won the game return his/her evaluated score
            if (score == -10)
                return score;

            // If there are no more moves and no winner then it is a tie
            if (IsMovesLeft(board) == false)
                return 0;

            // If this AI's move
            if (isMax)
            {
                int best = -1000;

                // Traverse all cells
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check if cell is empty
                        if (board[i, j] == ' ')
                        {
                            // Make the move
                            board[i, j] = PlayerSymbol.AI;

                            // Call minimax recursively and choose the maximum value
                            best = Math.Max(best, Minnimax(board,
                                            depth + 1, !isMax));

                            // Undo the move
                            board[i, j] = ' ';
                        }
                    }
                }
                return best;
            }

            // If this minimizer's move
            else
            {
                int best = 1000;

                // Traverse all cells
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        // Check if cell is empty
                        if (board[i, j] == ' ')
                        {
                            // Make the move
                            board[i, j] = PlayerSymbol.OPPONENT;

                            // Call minimax recursively and choose the minimum value
                            best = Math.Min(best, Minnimax(board,
                                            depth + 1, !isMax));

                            // Undo the move
                            board[i, j] = ' ';
                        }
                    }
                }
                return best;
            }
        }
        #endregion

        #region [Helper methods]


        /// <summary>
        /// This function returns true if there are moves
        //  remaining on the board. It returns false if
        //  there are no moves left to play.
        /// </summary>
        static bool IsMovesLeft(char[,] board)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (board[i, j] == ' ')
                        return true;
            return false;
        }

        static int Evaluate(char[,] b)
        {
            // Checking for Rows for X or O victory.
            for (int row = 0; row < 3; row++)
            {
                if (b[row, 0] == b[row, 1] &&
                    b[row, 1] == b[row, 2])
                {
                    if (b[row, 0] == PlayerSymbol.AI)
                        return +10;
                    else if (b[row, 0] == PlayerSymbol.OPPONENT)
                        return -10;
                }
            }

            // Checking for Columns for X or O victory.
            for (int col = 0; col < 3; col++)
            {
                if (b[0, col] == b[1, col] &&
                    b[1, col] == b[2, col])
                {
                    if (b[0, col] == PlayerSymbol.AI)
                        return +10;

                    else if (b[0, col] == PlayerSymbol.OPPONENT)
                        return -10;
                }
            }

            // Checking for Diagonals for X or O victory.
            if (b[0, 0] == b[1, 1] && b[1, 1] == b[2, 2])
            {
                if (b[0, 0] == PlayerSymbol.AI)
                    return +10;
                else if (b[0, 0] == PlayerSymbol.OPPONENT)
                    return -10;
            }

            if (b[0, 2] == b[1, 1] && b[1, 1] == b[2, 0])
            {
                if (b[0, 2] == PlayerSymbol.AI)
                    return +10;
                else if (b[0, 2] == PlayerSymbol.OPPONENT)
                    return -10;
            }

            // Else if none of them have won then return 0
            return 0;
        }

        public static Move FindBestMove(char[,] board)
        {
            int bestVal = -1000;
            Move bestMove = new()
            {
                row = -1,
                col = -1
            };

            // Traverse all cells, evaluate minimax function  for all empty cells. And return the cell
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Check if cell is empty
                    if (board[i, j] == ' ')
                    {
                        // Make the move
                        board[i, j] = PlayerSymbol.AI;

                        // compute evaluation function for this move.
                        int moveVal = Minnimax(board, 0, false);

                        // Undo the move
                        board[i, j] = ' ';

                        // If the value of the current move is more than the best value, then update best
                        if (moveVal > bestVal)
                        {
                            bestMove.row = i;
                            bestMove.col = j;
                            bestVal = moveVal;
                        }
                    }
                }
            }
            return bestMove;
        }
        #endregion

    }
}

