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
    class Cruiser : iEnemyShip 
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        public string name;
        public int gundmg;
        public int armor;
        public double shipspeed;
        public string shiptype;
        int x, y;
        Image dynamicImage;

        public Design Design
        {
            get { return Designs.getDesign("Cruiser"); }
        }


        public Cruiser(Canvas mapa)
        {
            dynamicImage = new Image();

            dynamicImage.Width = 76;
            dynamicImage.Height = 91;

            dynamicImage.Source = Design.texture;
            mapa.Children.Add(dynamicImage);
        }


        public void MoveTo(int x, int y)
        {
            Canvas.SetTop(dynamicImage, y);
            Canvas.SetLeft(dynamicImage, x);
        }

        public void Shoot(int x, int y)
        {
            throw new NotImplementedException();
        }

        virtual public void Create()
        {

        }

        public void Test()
        {

            throw new NotImplementedException();
        }
    }
}
