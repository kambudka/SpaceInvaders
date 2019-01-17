using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace SpaceInvaders
{
    

    public class PlayerShip
    {

        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        private static PlayerShip instance = null;
        private static object syncLock = new object();
        public Image texture;
        public double x, y;
        public int life;
        public int armor;
        public int gundmg;
        Canvas mapa;

        private PlayerShip()
        {

        }

        public static PlayerShip Instance()
        {
                lock (syncLock)
                {
                    if (PlayerShip.instance == null)
                        PlayerShip.instance = new PlayerShip();

                    return PlayerShip.instance;
                }
        }
        public Design Design
        {
            get { return Designs.getDesign("PlayerShip"); }
        }


        public void CreateShipDynamically(Canvas mapa)
        {
            //Inicjalizacja atrybutów statku
            life = 5;
            armor = 5;
            gundmg = 5;

            texture = new Image();
            // Create Image and set its width and height  
            texture.Width = 74;
            texture.Height = 79;
            x = 200;
            y = 530; //550-79;

            texture.Source = Design.texture;
            texture.Name = "playership";

            this.mapa = mapa;
            // Add Image to Window  
            mapa.Children.Add(texture);

            Canvas.SetLeft(texture, x);
            Canvas.SetTop(texture, y);
        }


        #region Upgrades
        public void upgradegun()
        {

        } 

        public void upgradelife()
        {

        }

        public void upgradearmor()
        {


        }
        #endregion

        #region Commands
        public void MoveLeft()
        {
            if (x > 10)
            {
                x -= 10;
                Canvas.SetLeft(texture, x);
                
            }
        }
        public void MoveRight()
        {
            if (x < 690)
            { 
            x +=10;
            Canvas.SetLeft(texture, x + 10);
                Debug.WriteLine(gundmg);
            }
        }
        public void ShootGun()
        {
            PlayerMissile miss = new PlayerMissile(mapa);
            Globals.playerMissiles.Add(miss);
            FlyingPlayerMissile missile = new FlyingPlayerMissile(miss, this.x, this.y);
            Thread t = new Thread(new ThreadStart(missile.ThreadProc));
            t.Start();
        }
        #endregion
    }
}
