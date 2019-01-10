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

    public class MoveUp : iCommand
    {
        private PlayerShip _ship;

        public MoveUp(PlayerShip ship)
        {
            _ship = ship;
        }

        public void doCommand()
        {
            _ship.MoveUp();
        }
    }
    public class MoveDown : iCommand
    {
        private PlayerShip _ship;

        public MoveDown(PlayerShip ship)
        {
            _ship = ship;
        }

        public void doCommand()
        {
            _ship.MoveDown();
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
}
