using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace Assignment_3_Game {
    class ResultText : GameObject {
        private TextBlock block;
        private bool textChanged = false;

        public string Status {
            get { return block.Text; }
            set {
                block.Text = value;
                textChanged = true;
            }
        }

        public ResultText() {
            block = new TextBlock();
            reset(1, 30);
            GameElement = block;
            Y *= 2;
            addToGame();
        }

        public void reset(int number, double second) {
            textChanged = false;
            block.Text = "Challenge #" + number + ": Clear enermy's planes in " + second + " seconds";
            block.FontFamily = new FontFamily("Arial Black");
            block.Foreground = Brushes.Black;
            block.FontSize = 30;
            block.Height = 100.0;
            block.Width = G.gameWidth;
            block.TextAlignment = TextAlignment.Center;
        }

        public override void update() {
            if (textChanged) {
                if (block.FontSize < 200) {
                    block.FontSize += 2;
                    block.Height += 5;
                    block.Width += 5;
                    X -= 2.5;
                    block.TextAlignment = TextAlignment.Center;
                    block.Foreground = Brushes.HotPink;
                }
            }
        }
    }
}
