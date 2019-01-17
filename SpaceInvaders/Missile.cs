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
    public class FlyingMissile
    {
        // State information used in the task.
        Missile missile;
        double x;
        double y;
        bool fromPlayer;
        // The constructor obtains the state information.
        public FlyingMissile(Missile missile, double xx, double yy)
        {
            x = xx;
            y = yy;
            this.missile = missile;
        }

        // The thread procedure performs the task, such as formatting
        // and printing a document.
        public void ThreadProc()
        {   if (y > 400)
                fromPlayer = true;
            else
                fromPlayer = false;

            while (true)
            {

                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    missile.Fly(x, y);
                }));
                if (fromPlayer == true)
                    y -= 10;
                else
                    y += 10;

                if (y < 0 || y > 600)
                {
                    Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        missile.dynamicImage.Source = null;
                }));
                break;
                }
                Thread.Sleep(100);

            }
            
        }
    }

    public class Missile
    {
        //MainWindow Form = Application.Current.Windows[0] as MainWindow;
        double x, y;
       public Image dynamicImage;

        public Design Design
        {
            get { return Designs.getDesign("Missile2"); }
        }
        public Missile(Canvas mapa)
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
        public void Fly(double x, double y)
        {
            Canvas.SetTop(dynamicImage, y);
            Canvas.SetLeft(dynamicImage, x);
            
        }
    }
    
}
