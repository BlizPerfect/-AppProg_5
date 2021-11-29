using System;
using System.Drawing;

enum CellColor
{
    black,
    white,
}

class Cell
{
    public static string VisualFormat = "██";
    public Point Position { get; private set; }
    public Point DrawPosition { get; private set; }
    public CellColor CellColor { get; private set; }
    public Figure Figure { get; set; }

    public Cell(Point position)
    {
        Position = position;
        DrawPosition = new Point(Position.X * 2, Position.Y);
        SetCellColor();
    }


    private void SetCellColor()
    {
        if ((Position.Y % 2) == 0)
        {
            if ((Position.X % 2) == 0)
            {
                CellColor = CellColor.white;
            }
            else
            {
                CellColor = CellColor.black;
            }
        }
        else
        {
            if ((Position.X % 2) != 0)
            {
                CellColor = CellColor.white;
            }
            else
            {
                CellColor = CellColor.black;
            }
        }
    }

    public void DrawCell(ConsoleColor firstColor, ConsoleColor secondColor)
    {
        var oldCursorPositionX = Console.CursorLeft;
        var oldCursorPositionY = Console.CursorTop;
        Console.SetCursorPosition(DrawPosition.X, DrawPosition.Y);
        if ((Position.Y % 2) == 0)
        {
            if ((Position.X % 2) == 0)
            {
                Console.ForegroundColor = firstColor;
            }
            else
            {
                Console.ForegroundColor = secondColor;
            }
        }
        else
        {
            if ((Position.X % 2) != 0)
            {
                Console.ForegroundColor = firstColor;
            }
            else
            {
                Console.ForegroundColor = secondColor;
            }
        }
        Console.Write(VisualFormat);
        Console.SetCursorPosition(oldCursorPositionX, oldCursorPositionY);
        Console.ResetColor();
    }

    public void SetColor(ConsoleColor color)
    {
        DrawCell(color, color);
    }
}
