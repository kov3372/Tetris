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
        public void StartGame()
        {
            kvadrat = new Liquidshape(3, 0);
            normalInterval = 1000;
            fastInterval = 300;
            score = 0;
            deadLines = 0;

            timer1.Interval = normalInterval;
            timer1.Tick += new EventHandler(update);
            timer1.Start();

            
            

            label1.Text = "линий  " + deadLines;
            label2.Text = "очки  " + score;

           

        }


        // игровое поле
        private int[,] gameMape = new int[16, 8];

        // размер квадратика
        private int sizeOfPixel = 35;
       
       
        // сама фигура
        Shape kvadrat = new Liquidshape(3, 0);

        // обычный интервал падения
        int normalInterval ;

        // ускореный интервал падения
        int fastInterval ;

        // очки игры
        private int score = 0;

        // количесво убраных линий
        private int deadLines = 0;

        // отрисовка сетки карты на форме (работает)
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

        // рисуем фигурку (работает)
        public void Drawfigure(Graphics g)
        {           
            for (int i = 0; i < gameMape.GetLength(0); i++)
            {
                for (int j = 0; j < gameMape.GetLength(1); j++)
                {
                    if (gameMape[i, j] == 1)
                    {
                        g.FillRectangle(Brushes.Green, j * sizeOfPixel + 1, i * sizeOfPixel + 1, sizeOfPixel - 1, sizeOfPixel - 1);
                    }

                    if (gameMape[i, j] == 2)
                    {
                        g.FillRectangle(Brushes.Blue, j * sizeOfPixel + 1, i * sizeOfPixel + 1, sizeOfPixel - 1, sizeOfPixel - 1);
                    }
                }
            }
        }
    
        // очистка заполненых рядов (работате)
        public void DeleteFilledRows()
        {
            for (int i = 0; i < gameMape.GetLength(0); i++)
            {
                int count = 0;
                for (int j = 0; j < gameMape.GetLength(1); j++)
                {

                    if (gameMape[i, j] != 0)
                        count++;
                }
                if (count == 8)
                {
                    deadLines++;
                    for (int g = i; g >= 1; g--)
                    {
                        for (int e = 0; e < gameMape.GetLength(1); e++)
                        {
                            gameMape[g, e] = gameMape[g - 1, e];
                        }
                    }
                }
            }
            for (int i = 0; i < deadLines; i++)
            {
                score += 10 * (i + 1);
            }

            label1.Text = "линий  " + deadLines;
            label2.Text = "очки  " + score;
        }


        // очистка карты при проиграще
        public void ClearMap()
        {
            for (int i = 0; i < gameMape.GetLength(0); i++)
            {
                for (int j = 0; j < gameMape.GetLength(1); j++)
                {
                    gameMape[i, j] = 0;
                   
                }
            }
        }


        public Form1()
        {
            InitializeComponent();
            StartGame();
            this.KeyUp += new KeyEventHandler(key);
                 
        }

        // контроль клавиш
        private void key(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                
                case Keys.Space:
                if(!kvadrat.RoteshenIsPossible(gameMape))
                {
                        // очистили
                        kvadrat.ResetArea(gameMape);
                        // подвинули
                        kvadrat.Rotate(gameMape);
                        // синхронизировали
                        kvadrat.SyncShapeWithMap(gameMape);
                        Invalidate();
                }
                break;
                 
              //ускоряем падение
               case Keys.Down:
               timer1.Interval = fastInterval;                 
               break;

               // возвращяем обычный темп падения
                case Keys.Up:
                timer1.Interval  = normalInterval;
                break;

                case Keys.Left:   
                if(!kvadrat.CheckLefttside(gameMape))
                {
                        // очистили
                        kvadrat.ResetArea(gameMape);
                        // подвинули
                        kvadrat.Leght();
                        // синхронизировали
                        kvadrat.SyncShapeWithMap(gameMape);
                        Invalidate();
                 }
                break;
               
                case Keys.Right:
                if (!kvadrat.CheckRightside(gameMape))
                {
                  // очистили
                  kvadrat.ResetArea(gameMape);
                  // подвинули
                  kvadrat.Right();
                 // синхронизировали
                  kvadrat.SyncShapeWithMap(gameMape);
                 Invalidate();
                }                   
                break;


                case Keys.Enter:
                    if (timer1.Enabled)
                    {
                        label3.Text = "Продолжить";
                        timer1.Stop();
                    }
                    else
                    {
                        label3.Text = "Пауза";
                        timer1.Start();
                    }
                    break;
            }
        }

              
        private void update(object sender, EventArgs e)
        {
            kvadrat.ResetArea(gameMape);
            if(!kvadrat.ChecknextStep(gameMape))
            {
                kvadrat.Movedown();
            }
            else
            {
               kvadrat.SyncShapeWithMap(gameMape);
               DeleteFilledRows();

              switch (new Random().Next(1,3))
              {
                   case 1:
                  kvadrat = new StandartShape(3, 0);
                  break;

                   case 2:
                  kvadrat = new Liquidshape(3, 0);
                  break;
              }

                if(kvadrat.ChecknextStep(gameMape))
                {
                    ClearMap();
                    timer1.Tick -= new EventHandler(update);
                    timer1.Stop();
                    MessageBox.Show("ваш результат" + " " + score);
                    StartGame();
                }
                     
            }

            kvadrat.SyncShapeWithMap(gameMape);
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

        private void button1_Click(object sender, EventArgs e)
        {
            
           
        }
    }



}
