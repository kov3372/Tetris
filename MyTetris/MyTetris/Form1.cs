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


        // метод синхронизацыи масива фигуры и масива поля
        public void Sunchro()
        {

            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(1); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {
                    if (kvadrat.body[i - kvadrat.y, j - kvadrat.x] != 0)
                    {
                        gameMape[i, j] = kvadrat.body[i - kvadrat.y, j - kvadrat.x];
                    }                    
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


        // проверка на то что не выходит ли фиугра за правую гарницу карты или не лежит какая то фигура с права
        // Нужно реализовать после правлеьной реализаций функций ChecknextStep()
        public bool CheckRightside()
        {
            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(1); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {
                    int g1 = i - kvadrat.y;
                    int g2 = j - kvadrat.x;

                    if (kvadrat.body[g1,g2] != 0)
                    {
                        if (j + 1 > 7 || j + 1 < 0)
                            return true;

                        if(gameMape[i,j + 1 ] != 0)
                        {
                            if (g2 + 1 >= kvadrat.body.GetLength(0) || g1 + 1 < 0)
                                return true;

                            if (kvadrat.body[g1, g2 + 1] == 0)
                                return true;
                        }
                        
                    }
                }
            }
           return false;
        }

        // проверка на то что не выходит ли фиугра за левую гарницу карты или не лежит какая то фигура с права
        // Нужно реализовать после правлеьной реализаций функций ChecknextStep()
        public bool CheckLefttside()
        {
            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(1); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {
                    int g1 = i - kvadrat.y;
                    int g2 = j - kvadrat.x;

                    if (kvadrat.body[g1, g2] != 0)
                    {
                        if (j  -1 > 7 || j  -1 < 0)
                            return true;

                       if (gameMape[i, j+1* -1] != 0)
                        {
                            if (g2 +1*-1 >= kvadrat.body.GetLength(0) || g1 + 1 *-1< 0)
                                return true;

                            if (kvadrat.body[g1, g2 + 1*-1] == 0)
                                return true;
                        }                       
                    }
                }
            }
            return false;
        }

        // проверка на то что не выходит ли фигура за пределы масива или не лежит под ней какая то другая фигура
        public bool ChecknextStep()
        {
            for (int i = kvadrat.y  + kvadrat.body.GetLength(0) - 1; i >= kvadrat.y; i--)
            {
                for (int j = kvadrat.x ; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {
                    int g1 = i - kvadrat.y;
                    int g2 = j - kvadrat.x;


                    if (kvadrat.body[g1, g2] !=0)
                    {
                        if ( i + 1 == 16)
                        {
                            return true;
                        }

                        if (gameMape[i+ 1, j] != 0 )
                        {
                            return true;
                        }
                    }                                   
                }
            }
            return false;
        }

        //  метод проверяет возможна ли ротация фиугры
        public bool IsRotationPossible()
        {
            for (int i = kvadrat.y; i < kvadrat.y + kvadrat.body.GetLength(1); i++)
            {
                for (int j = kvadrat.x; j < kvadrat.x + kvadrat.body.GetLength(0); j++)
                {

                    if (j >= 0 && j <= 7)
                    {
                        if(gameMape[i,j] == 0 && kvadrat.body[i - kvadrat.y , j - kvadrat.x]  == 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public Form1()
        {
            InitializeComponent();                   
            // реализация таймера
            timer1.Interval = 1000;
            timer1.Tick += new EventHandler(update);
            timer1.Start();

            // реализация движение
            this.KeyUp += new KeyEventHandler(key);
        }

        // контроль клавиш
        private void key(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // верчение фигуры 
                case Keys.Space:

                    ResetArea();
                    kvadrat.Rotate();
                    Sunchro();
                    Invalidate();
                    break;

                 // движение фиугры влево
                case Keys.Left:
                  
                
                        ResetArea();
                        kvadrat.Leght();
                        Sunchro();
                        Invalidate();
                    
                    break;

                 // движение фигуры вправо
                case Keys.Right:
                    if(!CheckRightside())
                    {
                        ResetArea();
                        kvadrat.Right();
                        Sunchro();
                        Invalidate();
                    }
                                                                          
                    break;
            }
        }

        // очистка заполненых фигур
        public void DeleteFilledRows()
        {
            // количество заполненых клеток в ряду
            int count;
            // колическто удаленных линий
            int countOfDeletLine = 0;


            for (int i = 0; i < gameMape.GetLength(0); i++)
            {
                count = 0;
                for (int j = 0; j < gameMape.GetLength(1); j++)
                {
                    if(gameMape[i,j] !=0)
                    {
                        count++;
                    }
                }
                if (count == 8)
                {
                    countOfDeletLine++;
                    for (int p = i; p >= 1; p--)
                    {
                        for (int p1 = i; p1 < gameMape.GetLength(1); p1++)
                        {
                            gameMape[p , p1] = gameMape[p-1, p1];
                        }
                    }
                }
            }
            score += (10 * countOfDeletLine);
        }




        // метод 
        private void update(object sender, EventArgs e)
        {
            ResetArea();
            if(!ChecknextStep())
            {
                   kvadrat.Movedown();     
            }
            else
            {
                
                Sunchro();
                DeleteFilledRows();
                kvadrat = new StandartShape(3,0);
            }


            Sunchro();                 
            Invalidate();

        }


        // функция ресования фигур
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



        /*
          for (int i = kvadrat.y  + (kvadrat.body.GetLength(1) - 1); i >= kvadrat.y; i--)
            {
                for (int j = kvadrat.x ; j <  kvadrat.body.GetLength(0); j++)
                {
                    if(kvadrat.body[i - kvadrat.y, j - kvadrat.x] !=0)
                    {
                        if ( i + 1 == 16)
                        {
                            return true;
                        }

                        if (kvadrat.body[i + 1, j] != 0 )
                        {
                            return true;
                        }
                    }
                    else
                    {
                        continue;
                    }
                    
                }
            }
            return false;
         
         */
    }

}
