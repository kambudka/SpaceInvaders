using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SpaceInvaders
{

    public class FlyingPlayerMissile
    {
        // State information used in the task.
        PlayerMissile missile;
        public double x;
        public double y;
        
        bool used = false;
        // The constructor obtains the state information.
        public FlyingPlayerMissile(PlayerMissile missile, double xx, double yy)
        {
            x = xx;
            y = yy;
            this.missile = missile;
        }

        // The thread procedure performs the task, such as formatting
        // and printing a document.
        public void ThreadProc()
        {
            

            while (true)
            {

                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    missile.PlayerMissileFly(x, y);
                    used = true;
                    for (int i = 0; i < Globals.playerMissiles.Count(); i++)
                    {
                        if (Globals.playerMissiles[i].Equals(missile))
                        {
                            used = false;
                            break;
                        }
                    }
                    
                }));
                
                    y -= 10;
                

                if (y < 0 || y > 600)
                {
                    Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        
                        missile.dynamicImage.Source = null;
                    }));
                    break;
                }
                if (used == true)
                    break;
                Thread.Sleep(100);

            }

        }
    }

    public class PlayerMissile
    {
       public double x, y;
        public Image dynamicImage;

        public Design Design
        {
            get { return Designs.getDesign("Missile1"); }
        }
        public PlayerMissile(Canvas mapa)
        {
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                dynamicImage = new Image();

                dynamicImage.Width = 10;
                dynamicImage.Height = 40;

                dynamicImage.Source = Design.texture;
                mapa.Children.Add(dynamicImage);
            }));
        }
        public void PlayerMissileFly(double x, double y)
        {
            Canvas.SetTop(dynamicImage, y);
            Canvas.SetLeft(dynamicImage, x);
            this.x = x;
            this.y = y;

        }
    }
}

