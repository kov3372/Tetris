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
        private static int[,] figure1 = new int[2, 2]
        {
             { 1, 1 },
             { 1, 1 }
        };

        private static int[,] figure2 = new int[3, 3]
        {
             { 1, 0, 0 },
             { 1, 1, 1 },
             { 0, 0, 0 }
        };

        private static int[,] figure3 = new int[3, 3]
        {
             { 0, 1, 0 },
             { 1, 1, 1 },
             { 0, 0, 0 }
        };

        private static int[,] figure4 = new int[3, 3]
         {
             { 0, 1, 1 },
             { 1, 1, 0 },
             { 0, 0, 0 }
         };

        private static int[,] figure5 = new int[4, 4]
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
