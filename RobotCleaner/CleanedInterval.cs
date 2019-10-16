namespace RobotCleaner
{
    public struct CleanedInterval
    {
        public int yMax { get; private set; }
        public int yMin { get; private set; }
        public int xMax { get; private set; }
        public int xMin { get; private set; }
        public CleanedInterval(int xMax, int xMin, int yMax, int yMin)
        {
            this.xMax = xMax > xMin ? xMax : xMin;
            this.xMin = xMin < xMax ? xMin : xMax;
            this.yMax = yMax > yMin ? yMax : yMin;
            this.yMin = yMin < yMax ? yMin : yMax;
        }
    }
}
