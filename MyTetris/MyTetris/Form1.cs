using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTetris
{
    public partial class Form1 : Form
    {
        // игровое поле
        private int[,] gameMape = new int[16, 8];

        // размер квадратика
        private int sizeOfPixel = 35;


        // очки игры
        private int score = 0;

        // количество удаленных линий
        private int countOfline = 0;


        // отрисовка сетки карты на форме
        public void DrawGrid(Graphics graf)
        {
            for (int i = 0; i <= gameMape.GetLength(0); i++)
            {
                graf.DrawLine(new Pen(Color.Black), new Point(0, i * sizeOfPixel), new Point(280, i * sizeOfPixel));
            }

            for (int j = 0; j <= gameMape.GetLength(1); j++)
            {
                graf.DrawLine(new Pen(Color.Black), new Point(j * sizeOfPixel, 0), new Point(j * sizeOfPixel, 560));
            }
        }



        public Form1()
        {
            InitializeComponent();
        }





        private void PaintAllObject(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics);
        }
    }
}
