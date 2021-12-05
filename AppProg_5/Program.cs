using System;
using System.Drawing;

namespace AppProg_5
{
    class Program
    {
        public static Point InputeCoordinate(bool isFriendly, MyStupidLogger logger)
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
                            logger.CallToInfo(System.Reflection.MethodBase.GetCurrentMethod().Name + "\nВвод пользователя принят.");
                        }
                        else
                        {
                            Console.Write("Ошибка ввода. Повторите ввод. Числа должны быть в интервале [1,8]: ");
                            logger.CallToError(System.Reflection.MethodBase.GetCurrentMethod().Name + "\nВвод пользователя отклонён: выход за интервал [1,8].");
                        }

                    }
                    else
                    {
                        Console.Write("Ошибка ввода. Повторите ввод. Первое число отвечает за X, второе за Y: ");
                        logger.CallToError(System.Reflection.MethodBase.GetCurrentMethod().Name + "\nВвод пользователя отклонён: Ввод не числа.");
                    }
                }
                else
                {
                    Console.Write("Ошибка ввода. Повторите ввод. Вы ввели лишь одно число: ");
                    logger.CallToError(System.Reflection.MethodBase.GetCurrentMethod().Name + "\nВвод пользователя отклонён: Ввод лишь одного числа.");
                }
            }
            return new Point(x, y);
        }

        public static int InputeFigureType(MyStupidLogger logger)
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
                        logger.CallToInfo(System.Reflection.MethodBase.GetCurrentMethod().Name + "\nВвод пользователя принят.");
                    }
                    else
                    {
                        Console.Write("Ошибка ввода. Повторите ввод. Число должнщ быть в интервале [1,4]: ");
                        logger.CallToError(System.Reflection.MethodBase.GetCurrentMethod().Name + "\nВвод пользователя отклонён: выход за интервал [1,4].");
                    }
                }
                else
                {
                    Console.Write("Ошибка ввода. Повторите ввод. Вы ввели не число: ");
                    logger.CallToError(System.Reflection.MethodBase.GetCurrentMethod().Name + "\nВвод пользователя отклонён: Ввод не числа.");
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
            var logger = new MyStupidLogger("log.txt");

            logger.StartStopWatch();
            var board = new Board();
            logger.StopStopWatch("Игровое поле успешно создано.");
            board.ShowBoard();
            board.ShowFiguresType();

            var friendlyPoint = InputeCoordinate(true, logger);
            logger.StartStopWatch();
            var activeFriendCell = board.Field[friendlyPoint.Y - 1, friendlyPoint.X - 1];
            activeFriendCell.SetColor(ConsoleColor.Green);
            logger.StopStopWatch("Атакующая фигура успешно создана.");
            var index = InputeFigureType(logger) - 1;
            var friendlyFigure = SelectFigure(index, friendlyPoint);
            activeFriendCell.Figure = friendlyFigure;
            logger.CallToInfo("Тип атакующей фигуры успешно назначен.");

            var hostilePoint = InputeCoordinate(false, logger);

            logger.StartStopWatch();
            var activeHostileCell = board.Field[hostilePoint.Y - 1, hostilePoint.X - 1];
            activeHostileCell.SetColor(ConsoleColor.Red);
            logger.StopStopWatch("Атакуемая фигура успешно создана.");

            logger.CallToInfo("Вычисляю ответы...");
            logger.StartStopWatch();
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
            logger.StopStopWatch("Вычисления завершены");
            logger.СallToFinishWork();
            Console.ReadKey();
        }
    }
}
