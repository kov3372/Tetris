using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
   abstract class Shape
    {
        public int x;
        public int y;
        public int[,] body;
     


        // метод сонхронизаций масива поля и масива фигурки(работает)
        abstract public void SyncShapeWithMap(int[,] gameMape);

        // очистка тереторий(работает) Одинаково работает для обоих фигур в подкласах не требует переопределения
        public void ResetArea(int[,] gameMape)
        {
            for (int i = y; i < y + body.GetLength(1); i++)
            {
                for (int j = x; j < x + body.GetLength(0); j++)
                {
                    if (i >= 0 && j >= 0 && i < 16 && j < 8)
                        if (body[i - y, j - x] != 0)
                            gameMape[i, j] = 0;
                }
            }
        }

        // функция для проверки не выходим ли мы за границу карты или не лежит под фигурой какоя то другая фигурка (работает отлично)
        public bool ChecknextStep(int[,] gameMape)
        {
            
                for (int i = y + body.GetLength(0) - 1; i >= y; i--)
                {
                    for (int j = x; j < x + body.GetLength(0); j++)
                    {
                        int g1 = i - y;
                        int g2 = j - x;

                        if (body[g1, g2] != 0)
                        {
                            if (i + 1 == 16)
                            {
                                return true;
                            }

                            if (gameMape[i + 1, j] != 0)
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            
        }

        // проверка на то что не выходит ли фигура за правую гарницу карты или не лежит какая то фигура с права (работает )
        public bool CheckRightside(int[,] gameMape)
        {
            for (int i = y; i < y + body.GetLength(1); i++)
            {
                for (int j = x; j < x + body.GetLength(0); j++)
                {
                    int g1 = i - y;
                    int g2 = j - x;

                    if (body[g1, g2] != 0)
                    {
                        if (j + 1 > 7 || j + 1 < 0)
                            return true;

                        if (gameMape[i, j + 1] != 0)
                        {

                            if (g2 + 1 >= body.GetLength(0) || g2 + 1 < 0)
                                return true;

                            if (body[g1, g2 + 1] == 0)
                                return true;
                        }
                    }
                }
            }
            return false;
        }

        // метод на проверку левой стороны ( работатет)
        public bool CheckLefttside(int[,] gameMape)
        {
            for (int i = y; i < y + body.GetLength(1); i++)
            {
                for (int j = x; j < x + body.GetLength(0); j++)
                {
                    int g1 = i - y;
                    int g2 = j - x;

                    if (body[g1, g2] != 0)
                    {
                        if (j - 1 > 7 || j - 1 < 0)
                            return true;

                        if (gameMape[i, j - 1] != 0)
                        {
                            if (g2 - 1 < 0 || g2 - 1 > body.GetLength(0))
                                return true;


                            if (body[g1, g2 - 1] == 0)
                                return true;

                        }
                    }
                }
            }
            return false;
        }

        // попытка вертеть фигуру вертит фигуру только в 2 -е позиций
        public void Rotate(int[,] gameMape)
        {
            int[,] tempMatrix = new int[body.GetLength(0), body.GetLength(1)];
            for (int i = 0; i < body.GetLength(0); i++)
            {
                for (int j = 0; j < body.GetLength(0); j++)
                {
                    tempMatrix[i, j] = body[j, (body.GetLength(0) - 1) - i];
                }
            }
            body = tempMatrix;
            int offset1 = (8 - (x + body.GetLength(0)));
            if (offset1 < 0)
            {
                for (int i = 0; i < Math.Abs(offset1); i++)
                    Leght();
            }

            if (x < 0)
            {
                for (int i = 0; i < Math.Abs(x) + 1; i++)
                    Right();
            }
        }

        // метод для проверки возможна ли ротация (работате )
        public bool RoteshenIsPossible(int[,] gameMape)
        {
            for (int i = y; i < y + body.GetLength(1); i++)
            {
                for (int j = x; j < x + body.GetLength(0); j++)
                {
                    if (j >= 0 && j <= gameMape.GetLength(1) - 1)
                    {
                        if (gameMape[i, j] != 0 && body[i - y, j - x] == 0)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Movedown()
        {           
            y++;
        }

        public void Leght()
        {           
           x--;
        }

        public void Right()
        {          
             x++;
        }


    }
}
