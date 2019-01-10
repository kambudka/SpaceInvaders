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
        private double x, y, z;
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



        public void CreateShipDynamically(Canvas mapa)
        {
            dynamicImage = new Image();
            // Create Image and set its width and height  
            dynamicImage.Width = 150;
            dynamicImage.Height = 100;
            x = 200;
            y = 200;

            // Create a BitmapSource  
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(@"E:\Program Files (x86)\Microsoft Visual Studio\2017\Community\Common7\IDE\inavders.PNG");
            bitmap.EndInit();

            // Set Image.Source  
            dynamicImage.Source = bitmap;
            dynamicImage.Name = "playership";


            // Add Image to Window  
            mapa.Children.Add(dynamicImage);

            Canvas.SetLeft(dynamicImage, y);
            Canvas.SetTop(dynamicImage, x);
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
            y = y - 10;
            MoveTo(x, y);
        }
        public void MoveDown()
        {
            Canvas.SetTop(dynamicImage, 250);
        }
        public void MoveLeft()
        {
            x--;
        }
        public void MoveRight()
        {
            x++;
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
