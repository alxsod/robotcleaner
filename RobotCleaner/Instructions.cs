namespace RobotCleaner
{
    public class Instructions
    {
        public string direction { get; set; }
        public int noSteps { get; set; }

        public Instructions(string direction, int noSteps)
        {
            this.direction = direction;
            this.noSteps = noSteps;
        }

        public Instructions()
        {
        }
    }
}
