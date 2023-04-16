using SlotMachineHandler;
using SlotMachineHandlerTests;

namespace SlotMachineTests
{
    public struct MachineToTest
    {
        public int[,] InitialMachine;
        public int[,] FinalMachine;
        public int row,column,colorToExplode;
    }
    public class Tests
    {
        private const string MACHINES_INEQUALITY_MSG = "Machines are not equal!";

        public static MachineToTest Machine1 => new MachineToTest
        {
            InitialMachine = new int[,]
{
                { 0, 2, 2 ,4 },
                { 1, 2, 2 ,3 },
                { 1, 2, 1 ,2 },
                { 1, 2, 1 ,2 },
},
            FinalMachine = new int[,]
{
                { 0, 0, 0 ,4 },
                { 1, 0, 0 ,3 },
                { 1, 0, 1 ,2 },
                { 1, 0, 1 ,2 },
},
            row = 1,
            column = 1,
            colorToExplode = 2
        };
        public static MachineToTest Machine2 => new MachineToTest
        {
            InitialMachine = new int[,]
            {
                { 1, 1, 1 ,1 },
                { 1, 1, 1 ,1 },
                { 1, 1, 1 ,1 },
                { 1, 1, 1 ,1 },
            },
            FinalMachine = new int[,]
            {
                { 0, 0, 0 ,0 },
                { 0, 0, 0 ,0 },
                { 0, 0, 0 ,0 },
                { 0, 0, 0 ,0 },
            },
            row = 1,
            column = 1,
            colorToExplode = 1
        };
        public static MachineToTest Machine3 => new MachineToTest
        {
            InitialMachine = new int[,]
            {
                { 0, 0, 2 ,4 },
                { 1, 4, 4 ,4 },
                { 1, 2, 1 ,2 },
                { 1, 2, 4 ,2 },
            },
            FinalMachine = new int[,]
            {
                { 0, 0, 0 ,4 },
                { 1, 4, 2 ,4 },
                { 1, 2, 4 ,2 },
                { 1, 2, 1 ,2 },
            },
            row = 3,
            column = 2,
            colorToExplode = 4
        };
        public static MachineToTest Machine4 => new MachineToTest
        {
            InitialMachine = new int[,]
            {
                { 1, 2, 2 ,4 },
                { 3, 2, 4 ,3 },
                { 1, 1, 1 ,2 },
                { 1, 2, 1 ,2 },
            },
            FinalMachine = new int[,]
            {
                { 0, 0, 0 ,4 },
                { 0, 2, 0 ,3 },
                { 1, 2, 2 ,2 },
                { 3, 2, 4 ,2 },
            },
            row = 2,
            column = 1,
            colorToExplode = 1
        };
        private static IEnumerable<MachineToTest> _machines { 
            
            get
            {
                yield return Machine1;
                yield return Machine2;
                yield return Machine3;
                yield return Machine4;
            } 
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test, TestCaseSource(nameof(_machines))]
        public void WhenExplosionOccures_UpdateBoardCorrectly(MachineToTest machineTested)
        {
            ISlotMachine slotMachine = GetSlotMachine(machineTested.InitialMachine);

            slotMachine.ExplodeAndUpdate(machineTested.row, machineTested.column, machineTested.colorToExplode);

            Assert.That(slotMachine.Machine, Is.EqualTo(machineTested.FinalMachine).Using(new MachineComparer()), MACHINES_INEQUALITY_MSG);
        }

        public ISlotMachine GetSlotMachine(int[,] initialMachine) => new SlotMachine(initialMachine);


    }
}