namespace SlotMachineHandler
{
    public interface ISlotMachine
    {
        int[,] Machine { get; }
        int[,] ExplodeAndUpdate(int row, int column, int colorToExplode);
        void PrintBoard();
    }
}