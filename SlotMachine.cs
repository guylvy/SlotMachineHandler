using System;
using System.Linq;

namespace SlotMachineHandler
{
    public class SlotMachine : ISlotMachine
    {
        #region MEMBERS

        #region CONSTS
        private const int EXPLOSION_MARK = -1;
        private const int NEW_CELL_MARK = 0;
        private const string BOARD_CELL_FORMAT = "{0}, ";
        #endregion

        public int[,] Machine { get; private set; }

        #endregion

        #region INIT
        public SlotMachine(int[,] initialMachine)
        {
            Machine = initialMachine;
        }
        #endregion

        #region METHODS
        private void UpdateColumn(int columnToUpdate)
        {
            for (int row = 0; row < Machine.GetLength(0); row++)
            {
                if (Machine[row, columnToUpdate] == EXPLOSION_MARK)
                {
                    int explosionLocation = row;
                    while (explosionLocation > 0 && Machine[explosionLocation - 1, columnToUpdate] != EXPLOSION_MARK)
                    {//swap items
                        int toSwap = Machine[explosionLocation - 1, columnToUpdate];
                        Machine[explosionLocation - 1, columnToUpdate] = EXPLOSION_MARK;
                        Machine[explosionLocation, columnToUpdate] = toSwap;
                        explosionLocation--;
                    }
                }
            }
        }
        private void MarkExplosions(int row, int column, int colorToExplode)
        {
            if (row < Machine.GetLength(0) &&
                column < Machine.GetLength(1) &&
                row >= 0 && column >= 0 &&
                Machine[row, column] == colorToExplode)
                Machine[row, column] = EXPLOSION_MARK;
            else return;
            MarkExplosions(row + 1, column, colorToExplode);
            MarkExplosions(row, column + 1, colorToExplode);
            MarkExplosions(row - 1, column, colorToExplode);
            MarkExplosions(row, column - 1, colorToExplode);
        }
        private void ZeroExplosionCells()
        {
            for (int row = 0; row < Machine.GetLength(0); row++)
            {
                for (int column = 0; column < Machine.GetLength(1); column++)
                {
                    if (Machine[row, column] == EXPLOSION_MARK)
                        Machine[row, column] = NEW_CELL_MARK;
                }
            }
        }
        public int[,] ExplodeAndUpdate(int row, int column, int colorToExplode)
        {
            MarkExplosions(row, column, colorToExplode);
            foreach (int columnToUpdate in Enumerable.Range(0, Machine.GetLength(1)))
                UpdateColumn(columnToUpdate);
            ZeroExplosionCells();
            return Machine;
        }
        public void PrintBoard()
        {
            for (int i = 0; i < Machine.GetLength(0); i++)
            {
                for (int j = 0; j < Machine.GetLength(1); j++)
                {
                    Console.Write(BOARD_CELL_FORMAT, Machine[i, j]);
                }
                Console.WriteLine();
            }
        }
        #endregion
    }
}
