using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover.Tests
{
    [TestFixture]
    public class MissionControlTests
    {
        [Test]
        public void SetPlateauSize_PlateauExists_Throws()
        {
            var control = new MissionControl();
            control.SetPlateauSize(1, 1);
            Assert.Throws<ArgumentException>(() => control.SetPlateauSize(1, 1));
        }

        [Test]
        public void Land_WithDuplicateName_Throws()
        {
            var control = CreateMissionControl();
            control.Land("A", 1, 1, Orientation.North);

            Assert.Throws<InvalidOperationException>(
                () => control.Land("A", 1, 1, Orientation.North));
        }

        [Test]
        public void Land_WithUniqueName_AddsRover()
        {
            var control = CreateMissionControl();
            control.Land("A", 1, 1, Orientation.North);

            Assert.AreEqual("A", control.RoverStatus.Single().name);
        }

        [Test]
        public void Instruct_LeftTurn_RoverTurnsLeft()
        {
            var (control, name, _, _, initialOrientation) = CreateMissionControlWithRover();

            control.Instruct(name, new List<RoverCommands>() { RoverCommands.Left });

            Assert.AreEqual(Orientation.West, control.RoverStatus.Single().orientation);
        }

        [Test]
        public void Instruct_RightTurn_RoverTurnsRight()
        {
            var (control, name, _, _, initialOrientation) = CreateMissionControlWithRover();

            control.Instruct(name, new List<RoverCommands>() { RoverCommands.Right });

            Assert.AreEqual(Orientation.East, control.RoverStatus.Single().orientation);
        }

        [Test]
        public void Instruct_Move_RoverMovesForward()
        {
            var (control, name, _, y, _) = CreateMissionControlWithRover();

            control.Instruct(name, new List<RoverCommands>() { RoverCommands.Move });

            Assert.AreEqual(y + 1, control.RoverStatus.Single().y);
        }

        private (MissionControl, string, int, int, Orientation) CreateMissionControlWithRover(
            int width = 5, 
            int height = 5)
        {
            string roverName = "Rover";
            int roverX = 1;
            int roverY = 1;
            Orientation roverOrientation = Orientation.North;

            var ret = CreateMissionControl();
            ret.Land(roverName, roverX, roverY, roverOrientation);
            return (ret, roverName, roverX, roverY, roverOrientation);
        }

        private MissionControl CreateMissionControl(int width = 5, int height = 5)
        {
            var ret = new MissionControl();
            ret.SetPlateauSize(width, height);
            return ret;
        }
    }
}
