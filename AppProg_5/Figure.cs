using System;
using System.Collections.Generic;
using System.Drawing;


abstract class Figure
{
    public Point Position { get; protected set; }
    public static List<string> FigureTypes = new List<string>()
    {
        "Queen  (Ферзь)",
        "Rook   (Ладья)",
        "Bishop (Слон)",
        "Knight (Конь)" };
    abstract public bool CanReach(Point point);
    abstract public Point CreateTwoStepPath(Point point, Board board);
}

class Queen : Figure
{
    public Queen(Point position)
    {
        Position = position;
    }
    public override bool CanReach(Point point)
    {
        return (Math.Abs(Position.X - point.X) <= 1 && Math.Abs(Position.Y - point.Y) <= 1 || (Position.X == point.X) || (Position.Y == point.Y));
    }

    public override Point CreateTwoStepPath(Point point, Board board)
    {
        foreach (var cell in board.Field)
        {
            if (CanReach(cell.Position))
            {
                var tempQueen = new Queen(cell.Position);
                if (tempQueen.CanReach(point))
                {
                    return tempQueen.Position;
                }
            }
        }
        return new Point(-1, -1);
    }
}
class Rook : Figure
{
    public Rook(Point position)
    {
        Position = position;
    }

    public override bool CanReach(Point point)
    {
        return ((Position.X == point.X) || (Position.Y == point.Y));
    }

    public override Point CreateTwoStepPath(Point point, Board board)
    {
        foreach (var cell in board.Field)
        {
            if (CanReach(cell.Position))
            {
                var tempRook = new Rook(cell.Position);
                if (tempRook.CanReach(point))
                {
                    return tempRook.Position;
                }
            }
        }
        return new Point(-1, -1);
    }
}
class Bishop : Figure
{
    public Bishop(Point position)
    {
        Position = position;
    }
    public override bool CanReach(Point point)
    {
        return (Math.Abs(Position.X - point.X) == Math.Abs(Position.Y - point.Y));
    }

    public override Point CreateTwoStepPath(Point point, Board board)
    {
        foreach (var cell in board.Field)
        {
            if (CanReach(cell.Position))
            {
                var tempBishop = new Bishop(cell.Position);
                if (tempBishop.CanReach(point))
                {
                    return tempBishop.Position;
                }
            }
        }
        return new Point(-1, -1);
    }
}
class Knight : Figure
{
    public Knight(Point position)
    {
        Position = position;
    }
    public override bool CanReach(Point point)
    {
        var dx = Math.Abs(Position.X - point.X);
        var dy = Math.Abs(Position.Y - point.Y);
        return ((dx == 1 && dy == 2) || (dx == 2 && dy == 1));
    }

    public override Point CreateTwoStepPath(Point point, Board board)
    {
        foreach (var cell in board.Field)
        {
            if (CanReach(cell.Position))
            {
                var tempKnight = new Knight(cell.Position);
                if (tempKnight.CanReach(point))
                {
                    return tempKnight.Position;
                }
            }
        }
        return new Point(-1, -1);
    }
}


