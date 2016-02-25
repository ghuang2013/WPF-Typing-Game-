using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3_Game {
    class Explosion:GameObject {
        public Explosion() {
            GameElement = useImage(G.root + "explosion.png");
            addToGame();
            X = -200;
            Y = -200;
        }

        public override void update() {
            
        }
    }
}
