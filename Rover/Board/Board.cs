using System;
using System.Collections.Generic;
using System.Text;

namespace Rover.Board
{
    /// <summary>
    /// Represents a surface on which peices may be placed.
    /// </summary>
    public interface Board
    {
        /// <summary>
        /// Place a peice on the board.
        /// </summary>
        /// <param name="x">The x position of the peice.</param>
        /// <param name="y">The y position of the peice.</param>
        /// <returns>A reference number that uniquely identifies a peice.</returns>
        int Place(int x, int y);

        /// <summary>
        /// Gets the position of a peice.
        /// </summary>
        /// <param name="peice">The reference number of the peice.</param>
        /// <returns>The coordinates where the peice is located.</returns>
        (int x, int y) GetPosition(int peice);

        /// <summary>
        /// Moves a piece to the supplied coordinates.
        /// </summary>
        /// <param name="peice">The reference number of the peice.</param>
        /// <param name="x">The new x position of the peice.</param>
        /// <param name="y">The new y position of the peice.</param>
        void MoveTo(int peice, int x, int y);

        /// <summary>
        /// Moves a piece relative to its current location.
        /// </summary>
        /// <param name="peice">The reference number of the peice.</param>
        /// <param name="xDiff">The displacement of the piece on the x-axis.</param>
        /// <param name="yDiff">The displacement of the piece on the y-axis.</param>
        void MoveRelative(int peice, int xDiff, int yDiff);

        /// <summary>
        /// Determines if a peice is in the same position as any other peice.
        /// </summary>
        /// <param name="peice">The reference number of the peice.</param>
        /// <returns>True if the peice is in the same position as another peice.</returns>
        bool IsOverlapping(int peice);

        /// <summary>
        /// Determines if a peice is out of the boards boundries.
        /// </summary>
        /// <param name="peice">The reference number of the peice.</param>
        /// <returns>True if the peice is out of the boards boundries.</returns>
        bool IsOffBoard(int peice);
    }
}
