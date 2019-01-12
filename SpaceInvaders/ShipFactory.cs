using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    abstract public class ShipFactory
    {
        public abstract iEnemyShip GetVehicle(string Ship);
    }

    public class ConcreteShipFactory : ShipFactory
    {
        public override iEnemyShip GetVehicle(string Ship)
        {
            switch (Ship)
            {
                case "Cruiser":
                    //return new Cruiser();
                case "Destroyer":
                    //return new Destroyer();
                default:
                    throw new ApplicationException(string.Format("Ship '{0}' cannot be created", Ship));
            }
        }
    }
}
