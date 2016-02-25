using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Assignment_3_Game {
    class TimerText:GameObject {
        private TextBlock block;

        public TimerText() {
            block = new TextBlock();
            block.Text = "Count Down: ";
            block.FontFamily = new FontFamily("Arial Black");
            block.Foreground = Brushes.WhiteSmoke;
            block.FontSize = 35;
            block.Height = 100.0;
            block.Width = G.gameWidth;
            block.TextAlignment = TextAlignment.Right;
            GameElement = block;
            addToGame();
        }

        public string CountDown {
            get { return block.Text; }
            set {
                block.Text = "Count Down: " + value;
            }
        }

        public override void update() {
        }
    }
}
