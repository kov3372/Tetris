using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    class Liquidshape : Shape
    {
        public Liquidshape(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.body = figure1;
        }


        public int[,] figure1 = new int[3, 3]
        {
             { 2, 0, 0, },
             { 0, 2, 0, },
             { 0, 0, 2, },
           
        };
      
        
        public bool WasContact(int[,] gameMape  )
        {
            bool flag = false;

            for(int i = y; i < y + body.GetLength(1); i++)
            {
                for (int j = x; i < x + body.GetLength(1); j++)
                {
                    if (body[i - y, j - x] != 0)
                    {
                        if(gameMape[i+1,j] != 0 || i==15)
                        {
                            flag = true;
                        }
                    }
                }
            }

            return flag;
        }


        /*
        *
          public void Fallingdown(int[,] gameMape  )
        {
            for(int i = y; i < gameMape.GetLength(0); i++)
            {
                for (int j = x; j < x + body.GetLength(1);j++)
                {
                    if(body[i - y, i -x ] != 0)
                    {
                        if( gameMape[i +1 , j] !=0 || i == 15)
                        {

                        }
                    }
                }
            }
        }
        */


            // метод который пробегал по фигуре
        public void GoOnMainfiagonal(int[,] gameMape)
        {
            for (int i = y; i < y + body.GetLength(1); i++)
            {
                for (int j = x; j < x + body.GetLength(0); j++)
                {
                    if(body[i - y, j - x] != 0 )
                    {
                       Fallingdown(gameMape, j);
                    }

                }
            }
        }



        // метод  который осуществлял падение квадратика но проблема в том что выход за пределы масива 
        /*
           public void Fallingdown(int[,] gameMape, int p1  )
           {
               for(int i = y; i < gameMape.GetLength(0); i++)
               {

                       if(body[i - y, p1 -x ] != 0)
                       {
                       int g1 = i - y;
                       int g2 = p1 - x;

                       if (i == 15 || gameMape[i + 1, p1] != 0)
                           {
                                gameMape[i, p1] = body[g1, g2];
                           }
                           else
                           {

                               body[g1, g2] = 0;
                               body[g1 + 1, g2] = 2;
                               gameMape[i, p1] = 0;
                           //   int debag = body[i - y + 1, p1 - x];

                           continue;
                           }
                       }

               }
           }

       */

        public void Fallingdown(int[,] gameMape, int p1)
        {
            for (int i = y; i < gameMape.GetLength(0); i++)
            {
                int g1 = i - y;
                int g2 = p1 - x;



               
                                if (g1 >= body.GetLength(1) - 1)
                                {
                                  ///  body[g1, g2] = 0;

                                    if (i == 15 || gameMape[i + 1, p1] != 0)
                                    {
                                          gameMape[i, p1] = 2;
                                          break;
                                    }
                                    else
                                    {
                                         
                                          gameMape[i, p1] = 0;
                                          gameMape[i + 1, p1] = 2;
                                    }

                                  
                                }
               


                if (body[i - y, p1 - x] != 0 )
                {
                    
                    if (i == 15 || gameMape[i + 1, p1] != 0)
                    {
                        gameMape[i, p1] = body[g1, g2];
                        break;
                    }
                    else
                    {
                      
                       
                            body[g1, g2] = 0;
                            body[g1 + 1, g2] = 2;
                            gameMape[i, p1] = 0;
                        

                       
                        //   int debag = body[i - y + 1, p1 - x];

                        continue;
                    }
                }


            }
        }


        // нужно реализовать
        public override void SyncShapeWithMap(int[,] gameMape)
        {
            for (int i = y; i < y + body.GetLength(1); i++)
            {
                for (int j = x; j < x + body.GetLength(0); j++)
                {
                    int g1 = i - y;
                    int g2 = j - x;                   
                        if (body[g1, g2] != 0)
                        {

                           if (i == 15 || gameMape[i + 1, j] != 0)
                           {
                                GoOnMainfiagonal(gameMape);
                                  break;
                           }
                           else
                           {
                                gameMape[i, j] = body[g1, g2];
                           }

                                                                                                                                           
                        }
                        else
                        {
                            continue;
                        }                                    
                }
            }

        }

        // нужно реализовать
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

        // нужно реализовать
        public override bool ChecknextStep(int[,] gameMape)
        {
            for (int i = y + body.GetLength(0) - 1; i >= y; i--)
            {
                for (int j = x; j < x + body.GetLength(0); j++)
                {
                    int g1 = i - y;
                    int g2 = j - x;

                    if (body[g1, g2] != 0 )
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

        // нужно реализовать
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

        // нужно реализовать
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

        // нужно реализовать
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

        // нужно реализовать
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
    }
}
