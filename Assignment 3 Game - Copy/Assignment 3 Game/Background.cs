using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3_Game {
    class Background : GameObject {
        public Background() {
            GameElement = useImage(G.root + "sky.jpg");
            addToGame();
        }
        public override void update() {
        }
    }
}
