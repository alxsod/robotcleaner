using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotCleaner;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotCleaner.Tests
{
    [TestClass()]
    public class RobotTests
    {
        [TestMethod()]
        public void TestCleanStandard()
        {
            Coordinates initialValues = new Coordinates(10, 22);
            Robot robot = new Robot(initialValues);
            List<Instructions> instructions = new List<Instructions>();
            instructions.Add(new Instructions("E", 2));
            instructions.Add(new Instructions("N", 1));
            foreach (Instructions instruction in instructions)
            {
                robot.Clean(instruction);
            }

            long area = robot.GetNumberOfCleanedTiles();
            Assert.IsTrue(area == 4);
        }
        
        [TestMethod]
        public void TestCleanAllDirectionsWithOverlap()
        {
            Coordinates initialValues = new Coordinates(0, 0);
            Robot robot = new Robot(initialValues);
            List<Instructions> instructions = new List<Instructions>();
           
            instructions.Add(new Instructions("S", 10));
            instructions.Add(new Instructions("E", 1));
            instructions.Add(new Instructions("N", 10));
            instructions.Add(new Instructions("W", 1));
            instructions.Add(new Instructions("S", 10));
            instructions.Add(new Instructions("E", 1));
            instructions.Add(new Instructions("N", 10));

            foreach (Instructions instruction in instructions)
            {
                robot.Clean(instruction);
            }
            long area = robot.GetNumberOfCleanedTiles();
            Assert.IsTrue(area == 22);
        }

        
        [TestMethod]
        public void TestLargeInput()
        {
            Coordinates initialValues = new Coordinates(-100000, 100000);
            Robot robot = new Robot(initialValues);
            int noTiles = 1000;
            int n = 100000;
            for(int j = 0; j < noTiles; j++)
            {
                if((j % 2) == 0)
                {
                    robot.Clean(new Instructions("S", n));
                    robot.Clean(new Instructions("E", 1));
                }
                else
                {
                    robot.Clean(new Instructions("N", n));
                    robot.Clean(new Instructions("E", 1));
                }
            }
            long sizeOfGrid = n * noTiles + noTiles + 1;
            long area = robot.GetNumberOfCleanedTiles();
            Assert.IsTrue(area == sizeOfGrid);
        }

        [TestMethod]
        public void TestCleanWesternEdge()
        {
            Coordinates initialValues = new Coordinates(-100000, -100000);
            Robot robot = new Robot(initialValues);
            List<Instructions> instructions = new List<Instructions>();
            int n = 200000;
            for(int j = 0; j < n; j++)
            {
                instructions.Add(new Instructions("W", 1000));
                instructions.Add(new Instructions("N", 1));
            }
            foreach(Instructions instruction in instructions)
            {
                robot.Clean(instruction);
            }
            int sizeOfGrid = n + 1;
            long area = robot.GetNumberOfCleanedTiles();
            Assert.IsTrue(area == sizeOfGrid);
        }

        // Way slower than Western edge. Fix by bi-directional sweep?
        [TestMethod]
        public void TestCleanNorthernEdge()
        {
            Coordinates initialValues = new Coordinates(100000, 100000);
            Robot robot = new Robot(initialValues);
            List<Instructions> instructions = new List<Instructions>();
            int n = 10000;
            for (int j = 0; j < n; j++)
            {
                instructions.Add(new Instructions("W", 1));
            }
            foreach (Instructions instruction in instructions)
            {
                robot.Clean(instruction);
            }

            long area = robot.GetNumberOfCleanedTiles();
            int sizeOfGrid = n + 1;
            Assert.IsTrue(area == sizeOfGrid);
        }
    }
}