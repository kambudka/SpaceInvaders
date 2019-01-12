using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        Image dynamicImage;
        private double x, y;
        public int life;
        public int armor;
        public int gundmg;

        public PlayerShip()
        {

        }

        public static PlayerShip Instance
        {
            get
            {
                lock (syncLock)
                {
                    if (PlayerShip.instance == null)
                        PlayerShip.instance = new PlayerShip();

                    return PlayerShip.instance;
                }
            }
        }
        public Design Design
        {
            get { return Designs.getDesign("PlayerShip"); }
        }


        public void CreateShipDynamically(Canvas mapa)
        {
            //Inicjalizacja atrybutów statku
            life = 1;
            armor = 0;
            gundmg = 1;


            dynamicImage = new Image();
            // Create Image and set its width and height  
            dynamicImage.Width = 74;
            dynamicImage.Height = 79;
            x = 200;
            y = 550-79;

            dynamicImage.Source = Design.texture;
            dynamicImage.Name = "playership";


            // Add Image to Window  
            mapa.Children.Add(dynamicImage);

            Canvas.SetLeft(dynamicImage, x);
            Canvas.SetTop(dynamicImage, y);
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

        #region Movement
        public void MoveUp()
        {
            //Canvas.SetTop(dynamicImage, 50);
        }
        public void MoveDown()
        {
            //Canvas.SetTop(dynamicImage, 250);
        }
        public void MoveLeft()
        {
            if (x > 10)
            {
                x -= 10;
                Canvas.SetLeft(dynamicImage, x);
            }
        }
        public void MoveRight()
        {
            if (x < 690)
            { 
            x +=10;
            Canvas.SetLeft(dynamicImage, x + 10);
            }
        }
        public void ShootGun()
        {
            Canvas.SetLeft(dynamicImage, 250);
        }

        public void MoveTo(double newX, double newY)
        {
            var top = Canvas.GetTop(dynamicImage);
            var left = Canvas.GetLeft(dynamicImage);
            TranslateTransform trans = new TranslateTransform();
            dynamicImage.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(top, newX - top, TimeSpan.FromSeconds(1));
            //DoubleAnimation anim2 = new DoubleAnimation(left, newY - left, TimeSpan.FromSeconds(1));
            trans.BeginAnimation(TranslateTransform.XProperty, anim1);
            //trans.BeginAnimation(TranslateTransform.YProperty, anim2);
        }
        #endregion
    }
}
