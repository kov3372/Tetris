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
      
        
           
       // метод который пробегал по фигуре(работает)
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
      
        // метод отвечает за стиканье отдельного квадратика в жидкой фигуре(работает)
        public void Fallingdown(int[,] gameMape, int p1)
        {          
            for (int i = y; i < gameMape.GetLength(0); i++)
            {
                int g1 = i - y;
                int g2 = p1 - x;

                                          
                    if (g1  >= body.GetLength(1) - 1)
                    {       
                        if(g1-1 == body.GetLength(1) - 1)
                        {
                             body[g1-1, g2] = 0;
                        }


                        if (i == 15 || gameMape[i + 1, p1] != 0)
                        {                         
                            gameMape[i, p1] = 2;
                            break;
                        }
                        else
                        {
                           gameMape[i, p1] = 0;
                           gameMape[i + 1, p1] = 2;
                           continue;
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
                       
                        gameMape[i, p1] = 0;
                        body[g1, g2] = 0;
                        body[g1 + 1, g2] = 2;
                                                                                
                    }
                }
                else
                {
                    continue;
                }


            }
        }

      //  метод синхронизацыи масива фигуры и масива поля(работает)
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
         
      
     
    }
}
