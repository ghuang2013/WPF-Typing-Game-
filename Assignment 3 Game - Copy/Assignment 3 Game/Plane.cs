using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assignment_3_Game {
    class Plane : GameObject {
        private double scaleIndex;
        private bool zoomIn = true;

        public Plane() {
            if (G.chance(0.2)) {
                GameElement = useImage(G.root + "plane3.png");
            } else if (G.chance(0.4)) {
                GameElement = useImage(G.root + "plane2.png");
            } else {
                GameElement = useImage(G.root + "plane.png");
            } 
            Y = initializealtitude();
            X = initializeHorizontalPosition(GameElement);
            scaleIndex = G.nextDouble(0.001, 0.005);
            Angle = G.nextDouble(0, 40);
            dY = 0.5;
        }

        public double ScaleIndex {
            get {
                return scaleIndex;
            }
        }

        public static double initializeHorizontalPosition(FrameworkElement gameElement) {
            return G.nextDouble(0 + gameElement.Width / 2,
                G.gameWidth - gameElement.Width / 2);
        }

        public static double initializealtitude() {
            return G.nextDouble(G.gameHeight / 2, G.gameHeight);
        }

        public override void update() {
            Y -= dY;
            zoomToggle();
            if (!isActive) {
                Y = initializealtitude();
                X = initializeHorizontalPosition(GameElement);
            }
            fly();
        }

        private void zoomToggle() {
            if (zoomIn) {
                Scale += scaleIndex;
                if (Scale >= 1) {
                    zoomIn = false;
                }
            } else {
                Scale -= scaleIndex;
                if (Scale <= 0.5) {
                    zoomIn = true;
                }
            }
        }
    }
}
