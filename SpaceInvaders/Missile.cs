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
    public class FlyingMissile
    {
        // State information used in the task.
        Missile missile;
        double x;
        double y;
        //bool fromPlayer;
        PlayerShip player;
        bool hitted = false;

        // The constructor obtains the state information.
        public FlyingMissile(Missile missile, double xx, double yy, PlayerShip player)
        {
            x = xx;
            y = yy;
            this.missile = missile;
            this.player = player;
        }

        public void ThreadProc()
        {

            while (true)
            {

                Application.Current.Dispatcher.Invoke((Action)(() =>
                {
                    missile.Fly(x, y);

                    if (y > player.y && y < player.y + 50 && x > player.x && x < player.x + 50) // collison player with enemy bullet
                    {
                        Debug.WriteLine("Hitted!");
                        hitted = true;
                        if (player.armor > 0) // Player has armor
                        {
                            Debug.WriteLine("Player has armor!" + player.armor);
                            player.armor -= 10; // Destroy armor
                            Debug.WriteLine("Player loses 10 armor, now: " + player.armor);
                        }
                        else if (player.armor <= 0) // Player has no armor
                        {
                            Debug.WriteLine("Player no armor, life: " + player.life);
                            player.life -= missile.dmg; // Player loses all lifes
                            MainWindow.main.Dispatcher.Invoke(new Action(delegate ()
                            {
                                MainWindow.main.LifePoints = player.life.ToString();
                            }));
                            if (player.life < 0) // GameOver
                            {
                                player.texture.Source = null;
                                MainWindow.main.Dispatcher.Invoke(new Action(delegate ()
                                {
                                    MainWindow.main.ShowScoreboard();
                                }));
                                // print GameOver
                            }
                        }
                    }
                }));

                y += 10;

                if (y > 600)
                    hitted = true;
                if (hitted == true)
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
        public int dmg;
        public Design Design
        {
            get { return Designs.getDesign("Missile1"); }
        }
        public Missile(Canvas mapa, int dmg)
        {
            this.dmg = dmg;
            Application.Current.Dispatcher.Invoke((Action)(() =>
            {
                dynamicImage = new Image();

                dynamicImage.Width = 5;
                dynamicImage.Height = 20;

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
