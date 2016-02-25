using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3_Game {
    class TextArea:GameObject {

        public TextArea() {
            GameElement = useImage(G.root + "dialog.png");
            Y = G.gameHeight - 100;
            X = G.gameWidth / 2;
            addToGame();
        }

        public override void update() {

        }
    }
}
