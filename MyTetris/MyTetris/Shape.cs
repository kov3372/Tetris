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

        // очистка тереторий(работает)
        abstract public void ResetArea(int[,] gameMape);

        // функция для проверки не выходим ли мы за границу карты или не лежит под фигурой какоя то другая фигурка (работает отлично)
        abstract public bool ChecknextStep(int[,] gameMape);


        // проверка на то что не выходит ли фигура за правую гарницу карты или не лежит какая то фигура с права (работает )
        abstract public bool CheckRightside(int[,] gameMape);

        // метод на проверку левой стороны ( работатет)
        abstract public bool CheckLefttside(int[,] gameMape);


        // попытка вертеть фигуру вертит фигуру только в 2 -е позиций
        abstract public void Rotate(int[,] gameMape);


        // метод для проверки возможна ли ротация (работате )
        abstract public bool RoteshenIsPossible(int[,] gameMape);


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
