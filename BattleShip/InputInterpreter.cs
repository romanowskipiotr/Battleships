using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShips.App
{
    public static class InputInterpreter
    {
        public static void InterpretCell(string cell, out int rowNumber, out int columnNumber)
        {
            if (cell == string.Empty)
                throw new ArgumentException("Cell can't be an empty string", cell);
            if (cell[0] < 65 && cell[0] > 90)
                throw new ArgumentException("First symbol needs to be letter A - Z", cell);

            rowNumber = cell[0] - 65;

            if (!int.TryParse(cell.Substring(1, cell.Length - 1), out var result))
                throw new ArgumentException("Wrong format. Cell should containt row letter and column number, for example: A5");

            columnNumber = result - 1;
        }
    }
}
