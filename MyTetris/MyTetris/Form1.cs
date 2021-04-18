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


        // сама фигура
        StandartShape kvadrat = new StandartShape(3,0);


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


        // рисуем фигурку
        public void Drawfigure(Graphics g)
        {
            //  Rectangle kvadrat = new Rectangle(x * sizeOfPixel, y * sizeOfPixel, sizeOfPixel, sizeOfPixel);
            for (int i = 0; i < gameMape.GetLength(0); i++)
            {
                for (int j = 0; j < gameMape.GetLength(1); j++)
                {
                    if (gameMape[i, j] == 1)
                    {
                        g.FillRectangle(Brushes.Red, j * sizeOfPixel + 1, i * sizeOfPixel + 1, sizeOfPixel - 1, sizeOfPixel - 1);
                    }
                }
            }
        }


        // метод синхронизацыи масива фигуры и поля
        public void Sunchro()
        {

            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(1); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {

                    if (kvadrat.body[i - kvadrat.y, j - kvadrat.x] != 0)
                        gameMape[i, j] = kvadrat.body[i - kvadrat.y, j - kvadrat.x];
                }
            }
        }

        // очистка тереторий
        public void ResetArea()
        {
            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(1); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {

                    if (i >= 0 && j >= 0 && i < 16 && j < 8)
                        if (kvadrat.body[i - kvadrat.y, j - kvadrat.x] != 0)
                            gameMape[i, j] = 0;
                }
            }
        }


        // проверка на то что не выходит ли фигура за пределы масива или не лежит под ней какая то другая фигура
        public bool ChecknextStep()
        {
            for (int i = kvadrat.y + kvadrat.body.GetLength(1); i >= kvadrat.y; i--)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {
                    if(kvadrat.body[i+1,j] != 0 || i + 1 == 16)
                    {
                        return true;
                    }
                }
            }
            return false;


        }



        public Form1()
        {
            InitializeComponent();
           
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(update);
            timer1.Start();
        }


        // метод 
        private void update(object sender, EventArgs e)
        {
            Sunchro();
            Invalidate();

        }

        private void PaintAllObject(object sender, PaintEventArgs e)
        {
            // отрисовка сетки
            DrawGrid(e.Graphics);
            // отрисовка фигуры
            Drawfigure(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
