using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walker
{
    internal class Labyrinth
    {
        private readonly int[,] labyrint;
        private Point position;
        public Labyrinth(int width, int height)
        {
            labyrint = new int[width, height];
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    labyrint[i, j] = 0;
                }
            }
            var rnd = new Random();
            var number = 0;
            // Построение лабиринта
            for (var i = 1; i < width - 1; i += 2)
            {
                for (var j = 1; j < height - 1; j += 2)
                {
                    if (labyrint[_ = i - 1, j] == 0)
                    {
                        number += 1;
                        labyrint[i, j] = number;
                    }
                    else
                    {
                        labyrint[i, j] = labyrint[_ = i - 1, j];
                    }
                }
                // вертикальные стены
                for (var j = 1; j < height - 1; j += 2)
                {
                    var wall = rnd.Next(0, 2);
                    if (wall == 0 && j + 2 < height)
                    {
                        var a = labyrint[i, j];
                        var b = labyrint[i, _ = j + 2];
                        if (a != b)
                        {
                            labyrint[i, _ = j + 1] = labyrint[i, j];
                            labyrint[i, _ = j + 2] = labyrint[i, j];
                        }
                    }
                }
                // Горизонтальные стенки
                for (var j = 1; j < height - 1; j += 2)
                {
                    if (i + 1 == width - 1)
                    {
                        break;
                    }
                    var wall = rnd.Next(0, 2);
                    if (wall == 1)
                    {
                        labyrint[_ = i + 1, j] = labyrint[i, j];
                    }
                    if (wall == 0)
                    {
                        int countA = 0, countB = 0;
                        for (var a = 1; a < height - 1; a += 2)
                        {
                            if (labyrint[i, j] == labyrint[i, a])
                            {
                                countA += 1;
                            }
                            if (labyrint[i, j] == labyrint[_ = i + 1, a])
                            {
                                countB += 1;
                            }
                        }
                        if (countB > countA - 2)
                        {
                            labyrint[_ = i + 1, j] = labyrint[i, j];
                        }
                    }
                }
            }
            //Заполннение последней строки
            for (var j = 1; j < height - 1; j += 2)
            {
                if (j + 2 < height)
                {
                    var a = labyrint[width - 2, j];
                    var b = labyrint[width - 2, _ = j + 2];
                    if (a != b)
                    {
                        labyrint[width - 2, _ = j + 1] = labyrint[width - 2, j];
                    }
                }
            }
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (labyrint[i, j] == 0)
                    {
                        labyrint[i, j] = 1;
                    }
                    else
                    {
                        labyrint[i, j] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Установка началтног положения
        /// </summary>
        /// <returns></returns>
        public List<int> Walking()
        {
            var section = new List<int>();
            position.X = 1;
            position.Y = 1;
            for (var x = position.X + 3; x >= position.X; x--)
            {
                for (var y = position.Y - 1; y <= position.Y + 1; y++)
                {
                    section.Add(labyrint[x, y]);
                }
            }
            return section;
        }

        /// <summary>
        /// Изменеие позиции игрока по кнопкам
        /// </summary>
        /// <param name="key">Код нажатой клавиши</param>
        /// <returns></returns>
        public List<int> Walking(int key)
        {
            var section = new List<int>();
            switch (key)
            {
                case (int)ConsoleKey.RightArrow:
                    if (labyrint[position.X, position.Y + 1] != 1)
                    {
                        position.Y++;
                    }
                    break;
                case (int)ConsoleKey.LeftArrow:
                    if (labyrint[position.X, position.Y - 1] != 1)
                    {
                        position.Y--;
                    }
                    break;
                case (int)ConsoleKey.UpArrow:
                    if (labyrint[position.X + 1, position.Y] != 1)
                    {
                        position.X++;
                    }
                    break;
                case (int)ConsoleKey.DownArrow:
                    if (labyrint[position.X - 1, position.Y] != 1)
                    {
                        position.X--;
                    }
                    break;
            }
            for (var x = position.X + 3; x >= position.X; x--)
            {
                for (var y = position.Y - 1; y <= position.Y + 1; y++)
                {
                    try
                    {
                        section.Add(labyrint[x, y]);
                    }
                    catch (Exception)
                    {
                        section.Add(1);
                    }
                }
            }
            return section;
        }
    }
}
