using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    class Shape
    {
        public int x;
        public int y;
        public int[,] body;

        public void Movedown()
        {

            y++;
        }


        public void Leght()
        {
            if (x > 0)
                x--;
        }

        public void Right()
        {
            if (x < 8 - body.GetLength(0))
                x++;
        }


    }
}
