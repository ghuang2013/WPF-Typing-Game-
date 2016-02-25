using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Assignment_3_Game {
    /*
     * a list of constants 
     * initialize game engine
     */
    public class G {
        private static Random rnd = new Random((int)DateTime.Now.Ticks);
        public static Canvas canvas;
        public static GameEngine gameEngine = new GameEngine();

        public static double gameWidth {
            get {
                return canvas.Width;
            }
        }

        public static double gameHeight {
            get {
                return canvas.Height;
            }
        }
        public static string root = @"..\..\res\";
        public static string fileRoot = @"..\..\";

        public static double nextDouble(double min, double max) {
            return min + (rnd.NextDouble() * (max - min));
        }

        public static MediaPlayer SetupSound(string filename) {
            MediaPlayer gameSound = new MediaPlayer();
            Uri uri = new Uri(filename, UriKind.RelativeOrAbsolute);
            gameSound.Open(uri);
            return gameSound;
        }

        static public bool checkCollision(GameObject obj1, GameObject obj2) {
            if (obj2.isActive && obj1.isActive) {
                if (Math.Abs(obj1.Y - obj2.Y) < ((obj1.ScaledHeight + obj2.ScaledHeight) / 2.0)) {
                    if (Math.Abs(obj1.X - obj2.X) < ((obj1.ScaledWidth + obj2.ScaledWidth) / 2.0)) {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool chance(double probability) {
            return rnd.NextDouble() < probability;
        }
    }
}
