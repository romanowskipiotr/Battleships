using System;
using System.Text;

namespace BattleShips.App
{
    public class ViewAdapter
    {
        private string[,] _view;
        private int _size;

        public ViewAdapter(int size)
        {
            _size = size;
            _view = PrintStartingGrid(size);
        }


        private string[,] PrintStartingGrid(int size)
        {
            var result = new string[size, size];

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    result[i, j] = "O";
                }
            }

            return result;
        }

        public void MarkHit(int x, int y)
        {
            _view[x, y] = "X";
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            for (var i = 0; i < _size; i++)
            {
                var row = string.Empty;
                for (var j = 0; j < _size; j++)
                {
                    row += _view[i, j];
                }

                result.AppendLine(row);
            }

            return result.ToString();
        }
    }
}
