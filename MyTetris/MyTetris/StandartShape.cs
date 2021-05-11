using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    class StandartShape : Shape
    {
        // варинаты  фигур
        public int[,] figure1 = new int[2, 2]
        {
             { 1, 1 },
             { 1, 1 }
        };

        public int[,] figure2 = new int[3, 3]
        {
             { 1, 0, 0 },
             { 1, 1, 1 },
             { 0, 0, 0 }
        };

        public int[,] figure3 = new int[3, 3]
        {
             { 0, 1, 0 },
             { 1, 1, 1 },
             { 0, 0, 0 }
        };

        public int[,] figure4 = new int[3, 3]
         {
             { 0, 1, 1 },
             { 1, 1, 0 },
             { 0, 0, 0 }
         };

        public int[,] figure5 = new int[4, 4]
        {
            { 1, 0, 0, 0 },
            { 1, 0, 0, 0 },
            { 1, 0, 0, 0 },
            { 1, 0, 0, 0 }
        };


        // метод синхронизацыи масива фигуры и масива поля(работает)
        public override void SyncShapeWithMap(int[,] gameMape)
        {
            for (int i = y; i < y + body.GetLength(1); i++)
            {
                for (int j = x; j < x + body.GetLength(0); j++)
                {
                    if (body[i - y, j - x] != 0)
                    {
                        gameMape[i, j] = body[i - y, j - x];
                    }
                }
            }
        }

        // очистка тереторий (работает)
        public override void ResetArea(int[,] gameMape)
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
        public override bool ChecknextStep(int[,] gameMape)
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
        public override bool CheckRightside(int[,] gameMape)
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
        public override bool CheckLefttside(int[,] gameMape)
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

        // попытка вертеть фигуру вертит фигуру
        public override void Rotate(int[,] gameMape)
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
        public override bool RoteshenIsPossible(int[,] gameMape)
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






        // констурктор
        public StandartShape(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.body = GenetateBody();
        }


        public void ResetHape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        // метод генерации тела
        public int[,] GenetateBody()
        {
            int[,] matrix = body;

            switch (new Random().Next(1, 6))
            {
                case 1:
                    matrix = figure1;
                    break;
                case 2:
                    matrix = figure2;
                    break;
                case 3:
                    matrix = figure3;
                    break;
                case 4:
                    matrix = figure4;
                    break;
                case 5:
                    matrix = figure5;
                    break;
            }
            return matrix;
        }
        // попытка вертеть фигуру
       
    }
}
