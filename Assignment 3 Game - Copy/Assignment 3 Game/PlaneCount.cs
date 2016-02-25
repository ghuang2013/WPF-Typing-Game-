using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Assignment_3_Game {
    class PlaneCount : GameObject {
        private int planeCount = 10;
        private TextBlock block;

        public int Count {
            get { return planeCount; }
            set {
                planeCount = value;
                block.Text = "Plane(s) Remaining: " + planeCount.ToString();
            }
        }

        public PlaneCount() {
            block = new TextBlock();
            block.Text = "Plane Remaining: " + planeCount.ToString();
            block.FontFamily = new FontFamily("Arial Black");
            block.Foreground = Brushes.DeepPink;
            block.FontSize = 35;
            block.Height = 100.0;
            block.Width = G.gameWidth;
            block.TextAlignment = TextAlignment.Center;
            GameElement = block;
            addToGame();
        }

        public override void update() {
        }
    }
}
