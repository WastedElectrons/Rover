using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover.Tests
{
    [TestFixture]
    public class MarsRoverTests
    {
        [Test]
        public void TurnLeft_RotatesRover()
        {
            (var rover, _) = CreateRover();

            foreach (var o in InOrderOrientations.Reverse().Skip(1))
            {
                rover.TurnLeft();
                Assert.AreEqual(o, rover.Orientation);
            }
        }

        [Test]
        public void TurnRight_RotatesRover()
        {
            (var rover, _) = CreateRover();

            foreach (var o in InOrderOrientations.Skip(1))
            {
                rover.TurnRight();
                Assert.AreEqual(o, rover.Orientation);
            }
        }

        [Test]
        [TestCase(Orientation.North)]
        [TestCase(Orientation.East)]
        [TestCase(Orientation.South)]
        [TestCase(Orientation.West)]
        public void Move_MovesInOrientationDirection(Orientation orientation)
        {
            (var rover, var board) = CreateRover(orientation);

            rover.Move();

            board.VerifyMovedInDirection(orientation);
        }

        [Test]
        [TestCaseSource(nameof(RoverActions))]
        public void Action_RoverGoesOffBoard_ThowsException(Action<MarsRover> action)
        {
            (var rover, var board) = CreateRover();
            board.SetOffBoard();

            AssertThrowsRoverException(() => action(rover));
        }

        [Test]
        [TestCaseSource(nameof(RoverActions))]
        public void Action_RoverCrashed_ThowsException(Action<MarsRover> action)
        {
            (var rover, var board) = CreateRover();
            board.SetCrashed();

            AssertThrowsRoverException(() => action(rover));
        }

        private static IEnumerable<Action<MarsRover>> RoverActions
        {
            get
            {
                yield return (MarsRover r) => r.TurnLeft();
                yield return (MarsRover r) => r.TurnRight();
                yield return (MarsRover r) => r.Move();
            }
        }

        private static IEnumerable<Orientation> InOrderOrientations
        {
            get
            {
                yield return Orientation.North;
                yield return Orientation.East;
                yield return Orientation.South;
                yield return Orientation.West;
                yield return Orientation.North;
            }
        }

        private static void AssertThrowsRoverException(Action action)
        {
            Assert.Throws<RoverException>(() => action());
        }

        private static (MarsRover, Board.MockBoard) CreateRover(
            Orientation orientation = Orientation.North)
        {
            var board = new Board.MockBoard();

            return (new MarsRover(1, 1, orientation, board.Object), board);
        }
    }
}
