using Microsoft.JSInterop;
using Portfolio.Pages.TicTacToe.Resources;

namespace Portfolio.Pages.TicTacToe
{
    public partial class TicTacToe
	{
        #region [Fields]

        readonly char[,] board = { { ' ', ' ', ' ' }, { ' ', ' ', ' ' }, { ' ', ' ', ' ' } };

        readonly List<List<int[]>> combos = new()
        {
            new List<int[]>() {new int[] { 0,0 }, new int[] { 0, 1 }, new int[] { 0, 2} },
            new List<int[]>() {new int[] { 1,0 }, new int[] { 1, 1 }, new int[] { 1, 2} },
            new List<int[]>() {new int[] { 2,0 }, new int[] { 2, 1 }, new int[] { 2, 2} },

            new List<int[]>() {new int[] { 0,0 }, new int[] { 1, 0 }, new int[] { 2, 0} },
            new List<int[]>() {new int[] { 0,1 }, new int[] { 1, 1 }, new int[] { 2, 1} },
            new List<int[]>() {new int[] { 0,2 }, new int[] { 1, 2 }, new int[] { 2, 2} },

            new List<int[]>() {new int[] { 0,0 }, new int[] { 1, 1 }, new int[] { 2, 2} },
            new List<int[]>() {new int[] { 0,2 }, new int[] { 1, 1 }, new int[] { 2, 0} },
        };
        #endregion

        #region [Methdos]

        protected override void OnInitialized()
        {
            Move move = MinMaxAlgorithm.FindBestMove(board);
            board[move.row, move.col] = 'X';
        }

        private async Task MakeMove(int row, int col)
        {
            board[row, col] = PlayerSymbol.OPPONENT;

            Move move = MinMaxAlgorithm.FindBestMove(board);
            if (!(move.row == -1 && move.col == -1))
                board[move.row, move.col] = 'X';

            foreach (var combo in combos)
            {
                int[] first = combo[0];
                int[] second = combo[1];
                int[] third = combo[2];
                if (board[first[0], first[1]] == ' ' || board[second[0], second[1]] == ' ' || board[third[0], third[1]] == ' ') continue;
                if (board[first[0], first[1]] == board[second[0], second[1]] && board[second[0], second[1]] == board[third[0], third[1]] && board[first[0], first[1]] == board[third[0], third[1]])
                { 
                    await JS.InvokeVoidAsync("ShowSwal", "AI");
                    await Task.Delay(1000);
                    ResetGame();
                }
            }

            if (IsGameReset())
            {
                await JS.InvokeVoidAsync("ShowTie");
                ResetGame();
            }
        }

        private bool IsGameReset()
        {
            bool isReset = true;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        isReset = false;
                    }
                }
            }
            return isReset;
        }

        private void ResetGame()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
        }
        #endregion
    }
}

