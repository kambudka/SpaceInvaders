using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public class PlayerCommands
    {
       private List<iCommand> _commands = new List<iCommand>();

        public void AddCommand(iCommand command)
        {
            _commands.Add(command);
        }
        public void DoCommand(iCommand command)
        {
            command.doCommand();
        }

    }

    public class MoveLeft : iCommand
    {
        private PlayerShip _ship;

        public MoveLeft(PlayerShip ship)
        {
            _ship = ship;
        }

        public void doCommand()
        {
            _ship.MoveLeft();
        }
    }
    public class MoveRight : iCommand
    {
        private PlayerShip _ship;

        public MoveRight(PlayerShip ship)
        {
            _ship = ship;
        }

        public void doCommand()
        {
            _ship.MoveRight();
        }
    }
    public class Shoot : iCommand
    {
        private PlayerShip _ship;

        public Shoot(PlayerShip ship)
        {
            _ship = ship;
        }

        public void doCommand()
        {
            _ship.ShootGun();
        }
    }
    public class Exit : iCommand
    {
        private MainWindow _window;

        public Exit(MainWindow window)
        {
            _window = window;
        }

        public void doCommand()
        {
            _window.Exit();
        }
    }
}
