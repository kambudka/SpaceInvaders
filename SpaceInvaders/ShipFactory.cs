using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SpaceInvaders
{
    abstract public class ShipFactory
    {
        public abstract iEnemyShip GetShip(Canvas mapa,string shiptype);
        
    }

    public class ConcreteShipFactory : ShipFactory
    {
        public override iEnemyShip GetShip(Canvas mapa, string shiptype)
        {
            switch (shiptype)
            {
                case "Cruiser":
                    return new Cruiser(mapa);
                case "Destroyer":
                    return new Destroyer(mapa);
                default:
                    throw new ApplicationException(string.Format("Ship '{0}' cannot be created", shiptype));
            }
        }
    }






    //public class CruiserFactory :iEnemyShip
    //{
    //    private int gundmg;
    //    private int armor;
    //    private double shipspeed;

    //    public CruiserFactory() { }

    //    public override iEnemyShip GetShip(Canvas mapa)
    //    {
    //        Cruiser cruiser = new Cruiser(mapa);

    //        return cruiser;
    //    }
    //}

    //public class DestroyerFactory : ShipFactory
    //{
    //    private int gundmg;
    //    private int armor;
    //    private double shipspeed;

    //    public DestroyerFactory() { }

    //    public override iEnemyShip GetShip(Canvas mapa)
    //    {
    //        Destroyer destroyer = new Destroyer(mapa);

    //        return destroyer;
    //    }
    //}

}
