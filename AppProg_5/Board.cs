using System;
using System.Drawing;

class Board
{
    public Cell[,] Field { get; private set; }

    public Board()
    {
        Field = new Cell[8, 8];
        CreateBoard();
    }

    public bool IsOneColor(Cell firstCell, Cell secondCell)
    {
        return firstCell.CellColor.Equals(secondCell.CellColor);
    }

    private void CreateBoard()
    {
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                Field[i, j] = new Cell(new Point(j + 1, i + 1));
            }
        }
    }
    public void ShowBoard()
    {
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                Console.SetCursorPosition((j * 2) + 3, 0);
                Console.Write(j + 1);
                Field[i, j].DrawCell(ConsoleColor.White, ConsoleColor.Black);
            }
            Console.SetCursorPosition(0, i + 1);
            Console.Write(i + 1);
        }
    }
    public void ShowFiguresType()
    {
        var oldCursorPositionX = Console.CursorLeft;
        var oldCursorPositionY = Console.CursorTop;
        var row = 0;
        Console.SetCursorPosition(20, row);
        Console.Write("Список фигур: ");
        foreach (var type in Figure.FigureTypes)
        {
            row += 1;
            Console.SetCursorPosition(20, row);
            Console.Write(row + ": " + type);

        }
        Console.SetCursorPosition(oldCursorPositionX, oldCursorPositionY);
    }
}
