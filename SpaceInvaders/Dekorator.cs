using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SpaceInvaders
{
    abstract class Dekorator : iEnemyShip
    {
        protected iEnemyShip enemyShip;public int armor, gunDmg; public int shipspeed;
        public Dekorator(iEnemyShip enemyShip)
        {
            this.enemyShip = enemyShip;
            this.armor = enemyShip.GetArmor();
            this.gunDmg = enemyShip.GetGunDmg();
            this.shipspeed = enemyShip.GetSpeed();
        }
        

        public virtual int GetArmor()
        {
            return this.enemyShip.GetArmor();
        }

        public virtual int GetGunDmg()
        {
            return this.enemyShip.GetGunDmg();
        }

        public virtual int GetSpeed()
        {
            return this.enemyShip.GetSpeed();
        }

        public virtual void Test()
        {
            System.Diagnostics.Debug.WriteLine(armor);
        }

        public void MoveTo(int x, int y)
        {
            enemyShip.MoveTo(x, y);
        }

        public Image GetImage()
        {
            return this.enemyShip.GetImage();
        }
    }
    class UpgradeArmor : Dekorator
    {
        public UpgradeArmor(iEnemyShip enemyShip) : base(enemyShip) { this.armor += 5; }


        public override int GetArmor()
        {
            return armor;
        }
    }
    class UpgradeGun : Dekorator
    {
        public UpgradeGun(iEnemyShip enemyShip) : base(enemyShip) { this.gunDmg += 5; }

        public override int GetGunDmg()
        {
            return gunDmg;
        }
    }
    class UpgradeSpeed : Dekorator
    {
        public UpgradeSpeed(iEnemyShip enemyShip) : base(enemyShip) { this.shipspeed = shipspeed + 20; }

        public override int GetSpeed()
        {  
            return shipspeed;
        }
    }
}
