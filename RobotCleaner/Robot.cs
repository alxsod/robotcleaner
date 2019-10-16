using System;
using System.Collections.Generic;

namespace RobotCleaner
{
    public class Robot
    {
        private MemoryUnit memoryUnit;
        private Coordinates intervalEnd;
        private Coordinates intervalStart;
        private const int MaxVal = 100000;
        private const int MinVal = -100000;

        public Robot(Coordinates initialValues)
        {
            this.intervalStart = initialValues;
            this.intervalEnd = initialValues;
            memoryUnit = new MemoryUnit();
        }

        public long GetNumberOfCleanedTiles()
        {
            return memoryUnit.GetCleanedArea();
        }

        public void Clean(Instructions instructions)
        {
            this.intervalStart = this.intervalEnd;
            switch (instructions.direction)
            {
                case "N":
                    this.intervalEnd = CleanY(1, instructions.noSteps, this.intervalEnd);
                    break;
                case "S":
                    this.intervalEnd = CleanY(-1, instructions.noSteps, this.intervalEnd);
                    break;
                case "E":
                    this.intervalEnd = CleanX(1, instructions.noSteps, this.intervalEnd);
                    break;
                case "W":
                    this.intervalEnd = CleanX(-1, instructions.noSteps, this.intervalEnd);
                    break;
                default:
                    Console.WriteLine("None");
                    break;
            }
            memoryUnit.AllocateMemory(this.intervalStart, this.intervalEnd);
        }

        private Coordinates CleanX(int sign, int noSteps, Coordinates coord)
        {
            int xVal = coord.x + sign * noSteps;
            return new Coordinates(AtWall(xVal), coord.y);
        }

        private Coordinates CleanY(int sign, int noSteps, Coordinates coord)
        {
            int yVal = coord.y + sign * noSteps;
            return new Coordinates(coord.x,AtWall(yVal));
        }

        private int AtWall(int val) { 
            if(val >= 0)
            {
                return val > MaxVal ? MaxVal : val;
            }
            else
            {
                return val < MinVal ? MinVal : val;
            }
        }
    }
}
