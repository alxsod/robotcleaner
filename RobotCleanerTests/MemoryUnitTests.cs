using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Constraints;
using RobotCleaner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotCleaner.Tests
{
    [TestClass()]
    public class MemoryUnitTests
    {

        [TestMethod]
        public void TestSortIntervalList()
        {
            CleanedInterval in1 = new CleanedInterval(1, 1, 3, 4);
            CleanedInterval in2 = new CleanedInterval(-100, -300, 0, 0);
            CleanedInterval in3 = new CleanedInterval(-323, -323, 10, 100);
            CleanedInterval in4 = new CleanedInterval(1000, 1000, -30000, -40000);
            List<CleanedInterval> inList = new List<CleanedInterval>();
            inList.Add(in1); inList.Add(in2); inList.Add(in3); inList.Add(in4);
            List<CleanedInterval> sortedList = inList.OrderBy(o => o.xMin).ToList();

            Assert.IsTrue(sortedList.IndexOf(in3) == 0);
            Assert.IsTrue(sortedList.IndexOf(in2) == 1);
            Assert.IsTrue(sortedList.IndexOf(in1) == 2);
            Assert.IsTrue(sortedList.IndexOf(in4) == 3);
        }

        [TestMethod]
        public void TestFindExtremum()
        {
            CleanedInterval in1 = new CleanedInterval(1, 1, 3, 4);
            CleanedInterval in2 = new CleanedInterval(-100, -300, 0, 0);
            CleanedInterval in3 = new CleanedInterval(-323, -323, 10, 100);
            CleanedInterval in4 = new CleanedInterval(1000, 1000, -30000, -40000);
            List<CleanedInterval> inList = new List<CleanedInterval>();
            inList.Add(in1); inList.Add(in2); inList.Add(in3); inList.Add(in4);
            List<CleanedInterval> sortedList = inList.OrderBy(o => o.xMin).ToList();

            int minVal = sortedList[0].xMin;

            int maxVal = Int32.MinValue;
            for(int j = 0; j < sortedList.Count; j++)
            {
                maxVal = sortedList[j].xMax > maxVal ? sortedList[j].xMax : maxVal;
            }

            Console.WriteLine(maxVal);
            Assert.IsTrue(maxVal == 1000);
        }

        [TestMethod]
        public void TestLineSweepX()
        {
            CleanedInterval in1 = new CleanedInterval(10, 12, 22, 22);
            CleanedInterval in2 = new CleanedInterval(12, 12, 22, 23);
            List<CleanedInterval> inList = new List<CleanedInterval>();
            inList.Add(in1); inList.Add(in2); //inList.Add(in3); inList.Add(in4);
            List<CleanedInterval> sortedListOfIntervals = inList.OrderBy(o => o.xMin).ToList();
            int minVal = sortedListOfIntervals[0].xMin;

            int maxVal = Int32.MinValue;
            for (int j = 0; j < sortedListOfIntervals.Count; j++)
            {
                maxVal = sortedListOfIntervals[j].xMax > maxVal ? sortedListOfIntervals[j].xMax : maxVal;
            }
            //List<CleanedInterval> sortedListOfIntervals = SortIntervals(memory);
            long noCleanedTiles = 0;
            for (int x = minVal; x <= maxVal; x++)
            {
                HashSet<int> coordsOnAxis = new HashSet<int>();
                //Refactor inner part in actual program pls.
                for(int k = 0; k <sortedListOfIntervals.Count; k++) 
                {
                    CleanedInterval interval = sortedListOfIntervals[k];
                    if (interval.xMax < x)
                    {
                        sortedListOfIntervals.RemoveAt(k);
                    }
                    if ((interval.xMin <= x && interval.xMax >= x)
                        && interval.yMax == interval.yMin)
                    {
                        coordsOnAxis.Add(interval.yMax);
                    }
                    else if ((interval.xMin <= x && interval.xMax >= x)
                      && interval.yMax != interval.yMin)
                    {

                        for (int j = interval.yMin; j <= interval.yMax; j++)
                        {
                            coordsOnAxis.Add(j);
                        }
                    }
                }
                noCleanedTiles += coordsOnAxis.Count;
                Console.WriteLine(noCleanedTiles);

            }
            Console.Write(noCleanedTiles);
            Assert.IsTrue(noCleanedTiles == 4);
        }
    }
}