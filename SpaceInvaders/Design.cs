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
    public class Design
    {
        public BitmapImage texture;

        public Design(Uri path,string name)
        {
            texture = new BitmapImage();
            texture.BeginInit();
            texture.UriSource = path;
            texture.EndInit();
        }
    }
}
