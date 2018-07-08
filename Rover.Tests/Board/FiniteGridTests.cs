using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Rover.Board;

namespace Rover.Tests.Board
{
    [TestFixture]
    public class FiniteGridTests
    {
        [Test]
        public void Place_OnEmptySpace_PlacesPeice()
        {
            var board = CreateBoard();
            var peice = board.Place(3, 3);

            Assert.DoesNotThrow(() => board.GetPosition(peice));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void Place_ReturnsUniqueIdenifier(int numberToPlace)
        {
            var peices = new HashSet<int>();

            var board = CreateBoard();

            for (int i = 0; i < numberToPlace; i++)
            {
                peices.Add(board.Place(i, i));
            }

            Assert.AreEqual(numberToPlace, peices.Count);
        }

        [Test]
        public void Place_OnOccupiedSpace_Places()
        {
            var board = CreateBoard();
            var p1 = board.Place(2, 2);
            var p2 = board.Place(2, 2);

            Assert.AreEqual((2, 2), board.GetPosition(p1));
            Assert.AreEqual((2, 2), board.GetPosition(p2));
        }

        [Test]
        public void GetPosition_WhenPeiceExists_ReturnsLocation()
        {
            var board = CreateBoard();
            var peice = board.Place(2, 2);

            Assert.AreEqual((2, 2), board.GetPosition(peice));
        }

        [Test]
        public void GetPosition_WhenPeiceDoesNotExist_Throws()
        {
            var board = CreateBoard();

            Assert.Throws<InvalidOperationException>(() => board.GetPosition(1));
        }

        [Test]
        public void MoveTo_WhenSpaceEmpty_MovesPeice()
        {
            var board = CreateBoard();
            var peice = board.Place(2, 2);

            AssertMoveSuccessfull(board, peice, 3, 3);
        }

        [Test]
        public void MoveTo_WhenSpaceOccupied_MovesPeice()
        {
            var board = CreateBoard();
            var p1 = board.Place(2, 2);
            var p2 = board.Place(3, 3);

            AssertMoveSuccessfull(board, p2, 2, 2);
        }

        [Test]
        public void IsOverlapping_WhenPeiceDoesNotExist_Throws()
        {
            var board = CreateBoard();

            Assert.Throws<InvalidOperationException>(() => board.IsOverlapping(1));
        }

        [Test]
        public void IsOverlapping_NotOverlapping_ReturnsFalse()
        {
            var board = CreateBoard();
            var peice = board.Place(3, 3);

            Assert.IsFalse(board.IsOverlapping(peice));
        }

        [Test]
        public void IsOverlapping_Overlapping_ReturnsTrue()
        {
            var board = CreateBoard();
            var peice = board.Place(3, 3);
            board.Place(3, 3);

            Assert.IsTrue(board.IsOverlapping(peice));
        }

        [Test]
        public void IsOffBoard_WhenPeiceDoesNotExist_Throws()
        {
            var board = CreateBoard();

            Assert.Throws<InvalidOperationException>(() => board.IsOffBoard(1));
        }

        [Test]
        public void MoveRelative_PeiceDoesNotExist_Throws()
        {
            var board = CreateBoard();

            Assert.Throws<InvalidOperationException>(() => board.MoveRelative(1, 1, 1));
        }

        [Test]
        [TestCaseSource(nameof(MoveRelitiveTestCases))]
        public void MoveRelative_Moves(int xDiff, int yDiff)
        {
            var board = CreateBoard();
            var peice = board.Place(3, 3);

            board.MoveRelative(peice, xDiff, yDiff);
            var pos = board.GetPosition(peice);

            Assert.AreEqual(3 + xDiff, pos.x);
            Assert.AreEqual(3 + yDiff, pos.y);
        }

        public static IEnumerable<TestCaseData> MoveRelitiveTestCases
        {
            get
            {
                for (int x = -2; x <= 2; x++)
                    for (int y = -2; y <= 2; y++)
                    {
                        yield return new TestCaseData(x, y);
                    }
            }
        }

        [Test]
        [TestCase(0, 0, ExpectedResult = false)]
        [TestCase(0, 5, ExpectedResult = false)]
        [TestCase(5, 0, ExpectedResult = false)]
        [TestCase(5, 5, ExpectedResult = false)]
        [TestCase(-1, -1, ExpectedResult = true)]
        [TestCase(-1, 0, ExpectedResult = true)]
        [TestCase(0, -1, ExpectedResult = true)]
        [TestCase(6, 6, ExpectedResult = true)]
        [TestCase(5, 6, ExpectedResult = true)]
        [TestCase(6, 5, ExpectedResult = true)]
        [TestCase(2, 2, ExpectedResult = false)]
        public bool IsOffBoard_ReturnsCorrectStatus(int x, int y)
        {
            var board = CreateBoard();
            var peice = board.Place(x, y);

            return board.IsOffBoard(peice);
        }
        
        private void AssertMoveSuccessfull(Rover.Board.Board board, int peice, int x, int y)
        {
            board.MoveTo(peice, x, y);

            Assert.AreEqual((x, y), board.GetPosition(peice));
        }

        private Rover.Board.Board CreateBoard(
            int width = 5,
            int height = 5)
        {
            return new FiniteGrid(width, height);
        }
    }
}
