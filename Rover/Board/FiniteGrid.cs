using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rover.Board
{
    public class FiniteGrid : Board
    {
        private readonly int _width;
        private readonly int _height;

        private readonly Dictionary<int, (int x, int y)> _peices;
        private int _lastPeice = 0;

        public FiniteGrid(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentException("Grid width and height must be positive.");

            _width = width;
            _height = height;

            _peices = new Dictionary<int, (int x, int y)>();
        }

        public (int x, int y) GetPosition(int peice)
        {
            AssertPeiceExists(peice);
            return _peices[peice];
        }

        public bool IsOffBoard(int peice)
        {
            var pos = GetPosition(peice);

            return pos.x < 0 || pos.y < 0 || pos.x > _width || pos.y > _height;
        }

        public bool IsOverlapping(int peice)
        {
            var pos = GetPosition(peice);

            return _peices.Values.Where(p => p.Equals(pos)).Count() != 1;
        }

        public void MoveTo(int peice, int x, int y)
        {
            AssertPeiceExists(peice);
            _peices[peice] = (x, y);
        }

        public void MoveRelative(int peice, int xDiff, int yDiff)
        {
            var pos = GetPosition(peice);
            MoveTo(peice, pos.x + xDiff, pos.y + yDiff);
        }

        public int Place(int x, int y)
        {
            var peice = ++_lastPeice;

            _peices.Add(peice, (x, y));

            return peice;
        }

        private void AssertPeiceExists(int peice)
        {
            if (!_peices.ContainsKey(peice))
            {
                throw new InvalidOperationException("Peice does not exist");
            }
        }
    }
}
