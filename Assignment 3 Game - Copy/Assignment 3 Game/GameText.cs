using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Assignment_3_Game {
    class ScoreText : GameObject {
        private int score = 0;
        private TextBlock block;

        public ScoreText() {
            block = new TextBlock();
            block.Text = "Score: " + score.ToString();
            block.FontFamily = new FontFamily("Arial Black");
            block.Foreground = Brushes.YellowGreen;
            block.FontSize = 35;
            block.Height = 100.0;
            block.Width = 1000.0;
            GameElement = block;
            addToGame();
        }

        public int Score {
            get { return score; }
            set {
                score = value;
                block.Text = "Score: " + score.ToString();
            }
        }

        public override void update() {

        }
    }
}
