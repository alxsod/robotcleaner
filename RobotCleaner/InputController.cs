using System;

namespace RobotCleaner
{
    
    class InputController
    {
        
        public Coordinates initialValues { get; private set; }
        public int noOperations { get; private set; }
        public Instructions instructions { get; private set; }
        public InputController()
        {
            this.instructions = new Instructions();  
        }

        public void Initialize()
        {
            noOperations = Convert.ToInt32(Console.ReadLine());
            string[] secondInput = Console.ReadLine().Split(" ");
            initialValues = new Coordinates(Convert.ToInt32(secondInput[0]), 
                Convert.ToInt32(secondInput[1]));
        }

        public void SetNextInstructions()
        {
            string[] nextOperations = Console.ReadLine().Split(" ");
            instructions = new Instructions(nextOperations[0], Convert.ToInt32(nextOperations[1]));
        }


    }
}
