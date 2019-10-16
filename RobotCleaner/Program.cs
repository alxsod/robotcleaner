using System;

namespace RobotCleaner
{
    class Program
    {
        public static InputController inputController;
        static void Main(string[] args)
        {
            
            inputController = new InputController();
            inputController.Initialize();

            Robot robot = new Robot(inputController.initialValues);

            for (int j = 0; j < inputController.noOperations; j++)
            {
                inputController.SetNextInstructions();
                robot.Clean(inputController.instructions);
            }

            Console.WriteLine("=> Cleaned: " + robot.GetNumberOfCleanedTiles());
               
        }

    }
}
