using System;
using System.Collections.Generic;
using System.Linq;

namespace RobotCleaner
{
    /* MemoryUnit holds state of walked paths. Paths are stored in a CleanedInterval object which
     * are unfolded into actual points in the NumberOfCleanedSpots() - method. 
     * 
     */
    public class MemoryUnit
    {
        private List<CleanedInterval> memory;
        public MemoryUnit()
        {
            memory = new List<CleanedInterval>();
        }

        public void AllocateMemory(Coordinates start, Coordinates end)
        {
            CleanedInterval interval = new CleanedInterval(start.x, end.x, start.y, end.y);
            memory.Add(interval);
        }

        /* GetCleanedArea() is a publicly available method that counts the number of unique points
         * the robot passed. It makes us of a line-sweep-like algorithm that passes through 
         * the region of the plane that is populated w.r.t. x.
         */
        public long GetCleanedArea()
        {
            int minValx = GetMin(memory);
            int maxValx = GetMax(memory);
  
            return LineSweeper(minValx, maxValx);
        }

        /* LineSweeper makes use of a line-sweep-like algorithm to go through the part 
         * of the plane that the robot passed and counts the number of unique points
         * that the robot cleaned. 
         * 
         * It starts by creating a sorted list of intervals where the sorting is w.r.t. 
         * the smallest value in the x-direction. This list is then passed to a method
         * that searches for points in the intervals that intersect a specified y-slice
         * of the plane.
         */
        private long LineSweeper(int minValx, int maxValx)
        {
            long noCleanedTiles = 0;
            List<CleanedInterval> sortedListOfIntervals = SortIntervals(memory);

            for (int x = minValx; x <= maxValx; x++)
            {
               
                noCleanedTiles += NumberOfCleanedSpots(x, sortedListOfIntervals);
            }
            return noCleanedTiles;
        }

        /* NumberOfCleanedSpots calculates the number of unique 
         * points of the intervals that are present on a specified y-slice of the plane
         */
        private long NumberOfCleanedSpots(int x, List<CleanedInterval> sortedListOfIntervals)
        {
            HashSet<int> coordsOnAxis = new HashSet<int>();
            for (int k = 0; k < sortedListOfIntervals.Count; k++)
            {
                CleanedInterval interval = sortedListOfIntervals[k];
                if(interval.xMax < x)
                {
                    continue;
                }
                bool isxInInterval = interval.xMin <= x && interval.xMax >= x;
                if (isxInInterval)
                { 
                    for (int j = interval.yMin; j <= interval.yMax; j++)
                    {
                        coordsOnAxis.Add(j);
                    }
                    coordsOnAxis.Add(interval.yMax);
                }
            }
            return coordsOnAxis.Count;
        }

        /* Sorts a list of intervals w.r.t. the smallest x-value in each interval.
         * 
         */
        private List<CleanedInterval> SortIntervals(List<CleanedInterval> unsortedList)
        {
            return unsortedList.OrderBy(o => o.xMin).ToList();
        }

        private int GetMin(List<CleanedInterval> intervals)
        {
            int minVal = Int32.MaxValue;
            foreach (CleanedInterval interval in intervals)
            {
                minVal = interval.xMin < minVal ? interval.xMin : minVal;
            }
            return minVal;
         
        }

        private int GetMax(List<CleanedInterval> intervals)
        {
            int maxVal = Int32.MinValue;
            foreach (CleanedInterval interval in intervals)
            { 
                maxVal = interval.xMax > maxVal ? interval.xMax : maxVal;
            }
            return maxVal;
        }
    
    }
}
