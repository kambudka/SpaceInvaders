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
    public interface iEnemyShip
    {       
        void MoveTo(int x, int y);
        int GetArmor();
        int GetGunDmg();
        int GetSpeed();
        Image GetImage();
        int GetLifes();
        void RemoveLife(int gundmg);
    }
}
