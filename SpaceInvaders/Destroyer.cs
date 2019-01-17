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
    class Destroyer: iEnemyShip
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        public string name;
        public int gundmg;
        public int armor;
        public int shipspeed;
        public string shiptype;
        Image texture;



        public Design Design
        {
            get { return Designs.getDesign("Destroyer"); }
        }

        public Destroyer(Canvas mapa)
        {
            texture = new Image();
            shipspeed = 5;
            texture.Width = 76;
            texture.Height = 91;

            texture.Source = Design.texture;
            mapa.Children.Add(texture);
        }

        public void MoveTo(int x, int y)
        {
            Canvas.SetTop(texture, y);
            Canvas.SetLeft(texture, x);
        }

        public void Shoot(int x, int y)
        {
            throw new NotImplementedException();
        }

        public int GetArmor()
        {
            return armor;
        }

        public int GetGunDmg()
        {
            return gundmg;
        }

        public int GetSpeed()
        {
            return shipspeed;
        }
        public Image GetImage()
        {
            return this.texture;
        }
    }
}
