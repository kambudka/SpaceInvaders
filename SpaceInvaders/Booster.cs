using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SpaceInvaders
{
    public class FlyingBooster
    {
        // State information used in the task.
        Booster booster;
        double x;
        Random random = new Random();
        double y;bool used=false;
        PlayerShip player;
        
        // The constructor obtains the state information.
        public FlyingBooster(Booster booster, PlayerShip player)
        {
            this.player = player;
            this.booster = booster;
        }

        // The thread procedure performs the task, such as formatting
        // and printing a document.
        public void ThreadProc()
        {
            y = 0;
            x = random.Next(1, 600);
            while (true)
            {

                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    booster.Fly(x, y);
                    
                    if (y > player.y && y < player.y + 50 && x > player.x && x < player.x + 50)
                    {
                        used = true;
                        player.gundmg += 10;
                        
                    }
                }));
                
                    y += 10;

                if (y > 600)
                    used = true;
                if (used == true)
                {
                    Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        booster.dynamicImage.Source = null;
                    }));
                    break;
                }
                    Thread.Sleep(100);

            }

        }
    }

    public class Booster
    {
        double x, y;
        public Image dynamicImage;
        string improvementType;
        public Design Design
        {
            get { return Designs.getDesign("Booster"); }
        }
        public Booster(Canvas mapa,string type)
        {
            improvementType = type;
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                dynamicImage = new Image();

                dynamicImage.Width = 50;
                dynamicImage.Height = 50;

                dynamicImage.Source = Design.texture;
                mapa.Children.Add(dynamicImage);
            }));
        }
        public void Fly(double x, double y)
        {
            Canvas.SetTop(dynamicImage, y);
            Canvas.SetLeft(dynamicImage, x);

        }
    }
}
