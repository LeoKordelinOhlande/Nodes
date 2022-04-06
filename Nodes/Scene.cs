using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nodes
{
    public class Scene : Microsoft.Xna.Framework.Game
    {
        protected Scene? returnScene = null;
        public Scene? Run(byte fuckYouCS)
        {
            Run();
            return returnScene;
        }
    }
}
