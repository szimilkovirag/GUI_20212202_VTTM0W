using GhostHunter.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static GhostHunter.Logic.GhostHunterLogic;

namespace GhostHunter.Controller
{
    public class GameController
    {
        IGameControl control;
        public GameController(IGameControl control)
        {
            this.control = control;
        }

        public void KeyPressed(Key key)
        {
            switch (key)
            {
                case Key.W:
                    control.Move(Direction.Up);
                    break;
                case Key.S:
                    control.Move(Direction.Down);
                    break;
                case Key.A:
                    control.Move(Direction.Left);
                    break;
                case Key.D:
                    control.Move(Direction.Right);
                    break;
            }
        }
    }
}
