using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using static Rover.CommandLine.Parser;

namespace Rover.Tests.CommandLine
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void ParsePlateauArgs_WithValidInput_ReturnsCorrectArgs()
        {
            Assert.AreEqual(
                (1, 1),
                ParsePlateauArgs(new string[] { "1", "1" }));
        }
        
        [Test]
        [TestCase("1", "A")]
        [TestCase("1", "1", "1")]
        [TestCase("1")]
        public void ParsePlateauArgs_WithInalidInput_Throws(params string[] s)
        {
            Assert.Throws<ArgumentException>(() => ParsePlateauArgs(s));
        }

        [Test]
        public void ParseLandingArgs_WithValidInput_ReturnsCorrectArgs()
        {
            Assert.AreEqual(
                (1, 1, Orientation.North),
                ParseLandingArgs(new string[] { "1", "1", "N" }));
        }

        [Test]
        [TestCase("1", "1")]
        [TestCase("1", "1", "N", "1")]
        [TestCase("1", "A", "N")]
        public void ParseLandingArgs_WithInalidInput_Throws(params string[] s)
        {
            Assert.Throws<ArgumentException>(() => ParseLandingArgs(s));
        }

        [Test]
        public void ParseInstructionsArgs_WithValidInput_ReturnsCorrectArgs()
        {
            CollectionAssert.AreEquivalent(
                new List<RoverCommands>() { RoverCommands.Move, RoverCommands.Left },
                ParseInstructionsArgs(new string[] { "ML" }));
        }

        [Test]
        [TestCase("MR", "RM")]
        public void ParseInstructionsArgs_WithInalidInput_Throws(params string[] s)
        {
            Assert.Throws<ArgumentException>(() => ParseInstructionsArgs(s));
        }

        [Test]
        [TestCase('M', ExpectedResult = RoverCommands.Move)]
        [TestCase('m', ExpectedResult = RoverCommands.Move)]
        [TestCase('L', ExpectedResult = RoverCommands.Left)]
        [TestCase('l', ExpectedResult = RoverCommands.Left)]
        [TestCase('R', ExpectedResult = RoverCommands.Right)]
        [TestCase('r', ExpectedResult = RoverCommands.Right)]
        public RoverCommands CommandFromChar_ReturnsCommand(char c)
        {
            return CommandFromChar(c);
        }

        [Test]
        [TestCase('N', ExpectedResult = Orientation.North)]
        [TestCase('n', ExpectedResult = Orientation.North)]
        [TestCase('E', ExpectedResult = Orientation.East)]
        [TestCase('e', ExpectedResult = Orientation.East)]
        [TestCase('S', ExpectedResult = Orientation.South)]
        [TestCase('s', ExpectedResult = Orientation.South)]
        [TestCase('W', ExpectedResult = Orientation.West)]
        [TestCase('W', ExpectedResult = Orientation.West)]
        public Orientation OrientationFromChar_ReturnsOrientation(char c)
        {
            return OrientationFromChar(c);
        }

        [Test]
        public void CommandFromChar_CharIsNotACommand_Throws()
        {
            Assert.Throws<ArgumentException>(() => CommandFromChar('z'));
        }

        [Test]
        public void OrientationFromChar_OrientationIsNotACommand_Throws()
        {
            Assert.Throws<ArgumentException>(() => OrientationFromChar('z'));
        }
    }
}
