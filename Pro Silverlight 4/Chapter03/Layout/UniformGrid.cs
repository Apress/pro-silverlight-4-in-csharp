using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Layout
{
    public class UniformGrid : Panel
    {
        public int Columns { get; set; }
        public int Rows { get; set; }

        private int realColumns;
        private int realRows;

        private void CalculateColumns()
        {
            // Count the elements, and don't do anything
            // if the panel is empty.
            double elementCount = this.Children.Count;
            if (elementCount == 0) return;

            realRows = Rows;
            realColumns = Columns;

            // If the Rows and Columns properties were set, use them.
            if ((realRows != 0) && (realColumns != 0))
            {
                return;
            }
            // If neither property was set, start by calculating the columns.
            if ((realColumns == 0) && realRows == 0)
            {
                realColumns = (int)Math.Ceiling(Math.Sqrt(elementCount));
            }
            // If only Rows is set, calculate Columns.
            if (realColumns == 0)
            {
                realColumns = (int)Math.Ceiling(elementCount / realRows);
            }
            // If only Columns is set, calculate Rows.
            if (realRows == 0)
            {
                realRows = (int)Math.Ceiling(elementCount / realColumns);
            }

        }

        protected override Size MeasureOverride(Size constraint)
        {
            // Determine the dimensions of the grid.
            CalculateColumns();

            // Share out the available space equally.
            Size childConstraint = new Size(constraint.Width / realColumns, constraint.Height / realRows);

            // Keep track of the largest requested dimensions for any element.
            Size largestCell = new Size();

            // Examine all the elements in this panel.
            foreach (UIElement child in this.Children)
            {
                // Get the desired size of the child.
                child.Measure(childConstraint);

                // Record the largest requested dimensions.
                largestCell.Height = Math.Max(largestCell.Height, child.DesiredSize.Height);
                largestCell.Width = Math.Max(largestCell.Width, child.DesiredSize.Width);
            }

            // Take the largest requested element width and height, and use
            // those to calculate the maximum size of the grid.
            // If there are more elements than cells, extra elements are ignored.
            return new Size(largestCell.Width * realColumns, largestCell.Height * realRows);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            // Calculate the size of each cell.
            double cellWidth = arrangeSize.Width / realColumns;
            double cellHeight = arrangeSize.Height / realRows;

            // Determine the placement for each child.
            Rect childBounds = new Rect(0, 0, cellWidth, cellHeight);

            // Examine all the elements in this panel.
            foreach (UIElement child in this.Children)
            {
                // Position the child.
                child.Arrange(childBounds);

                // Move the bounds to the next position.                
                childBounds.X += cellWidth;
                if (childBounds.X >= cellWidth * realColumns)
                {
                    // Move to the next row.
                    childBounds.Y += cellHeight;
                    childBounds.X = 0;

                    // If there are more elements than cells,
                    // hide extra elements.
                    if (childBounds.Y >= cellHeight * realRows)
                        childBounds = new Rect(0, 0, 0, 0);
                }
            }

            // Return the size this panel actually occupies.                        
            return arrangeSize;
        }
    }
}