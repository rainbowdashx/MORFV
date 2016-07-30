using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MORFV.Game
{
    class GameInstance
    {
        private static readonly GameInstance uniqueInstance = new GameInstance();
        private Random rnd;
        private Vector2 ArenaSize;
        public Entity crosshair { get; set; }
        public bool bRightKeyDown { get; set; }
        public bool bLeftKeyDown { get; set; }


        private GameInstance()
        {
            rnd = new Random();
        }

        public static GameInstance GetInstance()
        {
            return uniqueInstance;
        }

        public Random GetRandom()
        {
            return rnd;
        }

        public void SetArenaSize(Vector2 size)
        {
            ArenaSize = size;
        }

        public Vector2 GetArenaSize()
        {
            return ArenaSize;
        }
        


    }
}
