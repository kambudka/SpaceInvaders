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
        protected iEnemyShip enemyShip;
        public int armor, gunDmg;
        public int shipspeed;
        public int life;

        public Dekorator(iEnemyShip enemyShip)
        {
            this.enemyShip = enemyShip;
            this.armor = enemyShip.GetArmor();
            this.gunDmg = enemyShip.GetGunDmg();
            this.shipspeed = enemyShip.GetSpeed();
            this.life = enemyShip.GetLifes();
        }

        public virtual int GetLifes()
        {
            return life;
        }

        public void RemoveLife(int gundmg)
        {
            life -= gundmg;
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
    class UpgradeLife : Dekorator
    {
        public UpgradeLife(iEnemyShip enemyShip) : base(enemyShip) { this.life += Globals.mapCount; }


        public override int GetLifes()
        {
            return life;
        }
    }
    class UpgradeGun : Dekorator
    {
        public UpgradeGun(iEnemyShip enemyShip) : base(enemyShip) { this.gunDmg += Globals.mapCount; }

        public override int GetGunDmg()
        {
            return gunDmg;
        }
    }
    class UpgradeSpeed : Dekorator
    {
        public UpgradeSpeed(iEnemyShip enemyShip) : base(enemyShip) { this.shipspeed = shipspeed + Globals.mapCount; }

        public override int GetSpeed()
        {  
            return shipspeed;
        }
    }
}
