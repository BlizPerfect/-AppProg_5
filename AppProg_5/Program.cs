using System;
using System.Drawing;

namespace AppProg_5
{
    class Program
    {
        public static Point InputeCoordinate(bool isFriendly)
        {
            var dataString = "";
            var x = 0;
            var y = 0;
            var error = true;
            Console.Write("\nВведите координаты");
            if (isFriendly)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(" АТАКУЮЩЕЙ ");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" АТАКУЕМОЙ ");
            }
            Console.ResetColor();
            Console.Write("фигуры через пробел: ");
            while (error)
            {
                dataString = Console.ReadLine();
                var splitedDataString = dataString.Split(" ");
                if (splitedDataString.Length > 1)
                {
                    if (int.TryParse(splitedDataString[0], out x) && (int.TryParse(splitedDataString[1], out y)))
                    {
                        if ((x < 9 && x > 0) && (y < 9 && y > 0))
                        {
                            error = false;
                        }
                        else
                        {
                            Console.Write("Ошибка ввода. Повторите ввод. Числа должны быть в интервале [1,8]: ");
                        }

                    }
                    else
                    {
                        Console.Write("Ошибка ввода. Повторите ввод. Первое число отвечает за X, второе за Y: ");
                    }
                }
                else
                {
                    Console.Write("Ошибка ввода. Повторите ввод. Вы ввели лишь одно число: ");
                }

            }
            return new Point(x, y);
        }

        public static int InputeFigureType()
        {
            var result = 0;
            var dataString = "";
            var error = true;
            Console.Write("Введите тип фигуры(цифру): ");
            while (error)
            {
                dataString = Console.ReadLine();
                if (int.TryParse(dataString, out result))
                {
                    if (result < 5 && result > 0)
                    {
                        error = false;
                    }
                    else
                    {
                        Console.Write("Ошибка ввода. Повторите ввод. Число должнщ быть в интервале [1,4]: ");
                    }
                }
                else
                {
                    Console.Write("Ошибка ввода. Повторите ввод. Вы ввели не число.: ");
                }
            }
            return result;
        }

        public static Figure SelectFigure(int index, Point position)
        {
            if (index == 0)
            {
                return new Queen(position);
            }
            if (index == 1)
            {
                return new Rook(position);
            }
            if (index == 2)
            {
                return new Bishop(position);
            }
            return new Knight(position);
        }

        static void Main(string[] args)
        {
            var board = new Board();
            board.ShowBoard();
            board.ShowFiguresType();

            var friendlyPoint = InputeCoordinate(true);
            var activeFriendCell = board.Field[friendlyPoint.Y - 1, friendlyPoint.X - 1];
            activeFriendCell.SetColor(ConsoleColor.Green);
            var index = InputeFigureType() - 1;
            var friendlyFigure = SelectFigure(index, friendlyPoint);
            activeFriendCell.Figure = friendlyFigure;

            var hostilePoint = InputeCoordinate(false);
            var activeHostileCell = board.Field[hostilePoint.Y - 1, hostilePoint.X - 1];
            activeHostileCell.SetColor(ConsoleColor.Red);

            Console.Write("1) Одного ли цвета клетки? - ");
            if (board.IsOneColor(activeFriendCell, activeHostileCell))
            {
                Console.Write("Клетки одного цвета.");
            }
            else
            {
                Console.Write("Клетки не одного цвета.");
            }

            Console.Write("\n2) Угрожает ли фигура клетке [" + hostilePoint.X + ";" + hostilePoint.Y + "] - ");
            if (friendlyFigure.CanReach(hostilePoint))
            {
                Console.WriteLine("Угрожает.");
            }
            else
            {
                Console.WriteLine("Не Угрожает.\n3) Проверим возможность угрозы в два хода.");
                var tempPoint = friendlyFigure.CreateTwoStepPath(hostilePoint, board);
                if (tempPoint.Equals(new Point(-1, -1)))
                {
                    Console.WriteLine("Даже за два хода нельзя достич атакуемой фигуры.");
                }
                else
                {
                    var tempCell = board.Field[tempPoint.Y - 1, tempPoint.X - 1];
                    tempCell.SetColor(ConsoleColor.Blue);
                    Console.WriteLine("За два хода можно достич атакуемой фигуры.");
                    Console.Write("Нужно сначала сделать ход на");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(" клетку:");
                    Console.ResetColor();
                    Console.Write(" [" + tempPoint.X + "; " + tempPoint.Y + "]");
                }
            }
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
