using System.Diagnostics.CodeAnalysis;

namespace SlotMachineTests
{
    internal class MachineComparer : IEqualityComparer<int[,]>
    {
        public bool Equals(int[,]? x, int[,]? y)
        {
            if (x?.GetLength(0) != y.GetLength(0) || x?.GetLength(1) != y.GetLength(1)) return false;

            for (int row = 0; row < x?.GetLength(0); row++)
            {
                for (int column = 0; column < x?.GetLength(1); column++)
                {
                    if (x?[row, column] != y?[row, column]) return false;
                }
            }
            return true;
        }

        public int GetHashCode([DisallowNull] int[,] obj)
        {
            return obj.GetHashCode();
        }
    }
}
