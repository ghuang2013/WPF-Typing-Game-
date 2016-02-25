using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Assignment_3_Game {
    class Missile : GameObject {
        private PlaneText collidedText = null;
        private enum State { NONE, APPROACH, HIT, EXPLODE };
        private State state = State.NONE;
        private MediaPlayer missileSound = null;
        private Explosion explosion;

        public Missile() {
            GameElement = useImage(G.root + "missile.png");
            X = -100;
            Y = -100;
            addToGame();
            missileSound = G.SetupSound(G.root + "EXPLODE.wav");
            missileSound.Volume = 0.2;
            explosion = new Explosion();
        }

        public void makeCollide(PlaneText planeandText) {
            state = State.APPROACH;
            collidedText = planeandText;
            Y = collidedText.Y;
        }

        public override void update() {
            switch (state) {
                case State.APPROACH:
                    X += collidedText.X / 100;
                    Y -= 0.8;
                    if (G.checkCollision(this, collidedText)) {
                        state = State.HIT;
                        X = -100;
                        Y = -100;
                        explosion.X = collidedText.X;
                        explosion.Y = collidedText.Y;
                        missileSound.Stop();
                        missileSound.Play();
                    } else if (!isActive) {
                        X = -100;
                        Y = -100;
                    }
                    break;
                case State.HIT:
                    if (collidedText.TargetPlane.Y > G.gameHeight) {
                        collidedText.removeFromGame();
                        state = State.EXPLODE;
                    } else {
                        collidedText.TargetPlane.X += 1;
                        collidedText.TargetPlane.Y += 5;
                        collidedText.TargetPlane.Angle += 5;
                        explosion.Scale += G.nextDouble(0.01, 0.02);
                    }
                    break;
                case State.EXPLODE:
                    explosion.makeInactive();
                    state = State.NONE;
                    break;
            }
        }
    }
}
