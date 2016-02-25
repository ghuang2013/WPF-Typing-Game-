using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Assignment_3_Game {
    public abstract class GameObject {
        private FrameworkElement element;
        private Canvas canvas = G.canvas;
        private Effect effect;
        private RotateTransform rotateTransform;
        private ScaleTransform scaleTransform;
        private TranslateTransform translateTransform;
        private TransformGroup transformGroup;
        private BitmapImage bitmap;
        public double _dX;
        public double _dY;
        private enum Direction { EAST, WEST, SOUTH, NORTH };
        private Direction flyDirection = Direction.EAST;

        public void addToGame() {
            G.gameEngine.addToDisplayList(this);
        }

        public virtual void removeFromGame() {
            makeInactive();
            G.gameEngine.removeFromDisplayList(this);
        }

        public Effect Effect {
            get {
                return effect;
            }
            set {
                effect = value;
            }
        }

        public ScaleTransform ScaleTransform {
            get {
                return scaleTransform;
            }
        }

        public FrameworkElement GameElement {
            get { return element; }
            set {
                element = value;
                element.RenderTransform = initTransforms();
                if (canvas != null) {
                    canvas.Children.Add(element);
                }
                if (effect != null) {
                    canvas.Effect = effect;
                }
            }
        }

        public Canvas Canvas {
            get {
                return canvas;
            }
            set {
                canvas = value;
                if (element != null) {
                    canvas.Children.Add(element);
                }
            }
        }

        public FrameworkElement useImage(String filename) {
            if (bitmap == null) {
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filename, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
            }
            Image image = new Image();
            image.Source = bitmap;
            image.Stretch = Stretch.Uniform;
            image.Height = bitmap.Height;
            image.Width = bitmap.Width;
            return image;
        }

        private TransformGroup initTransforms() {
            rotateTransform = new RotateTransform(0, element.Width / 2.0,
                element.Height / 2.0);
            scaleTransform = new ScaleTransform(1, 1, element.Width / 2.0, 
                element.Height / 2.0);
            translateTransform = new TranslateTransform(0, 0);

            transformGroup = new TransformGroup();
            transformGroup.Children.Add(rotateTransform);
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);
            return transformGroup;
        }

        public double X {
            get {
                return translateTransform.X + element.Width / 2.0;
            }
            set {
                translateTransform.X = value - element.Width / 2.0;
            }
        }

        public double Y {
            get {
                return translateTransform.Y + element.Height / 2.0;
            }
            set {
                translateTransform.Y = value - element.Height / 2.0;
            }
        }

        public double dX {
            get {
                return _dX / (1920.0 / G.gameWidth);
            }
            set {
                _dX = value * (1920.0 / G.gameWidth);
            }
        }

        public double dY {
            get {
                return _dY / (1080.0 / G.gameHeight);
            }
            set {
                _dY = value * (1080.0 / G.gameHeight);
            }
        }

        public void makeInactive() {
            Y = -100000.0;
            X = -100000.0;

            dX = 0.0;
            dY = 0.0;
        }

        public double Angle {
            get {
                return rotateTransform.Angle;
            }
            set {
                rotateTransform.Angle = value;
            }
        }

        public bool isActive {
            get {
                return Y > 20;
            }
        }

        private double _Scale;

        public double Scale {
            get {
                return _Scale;
            }
            set {
                _Scale = value;
                scaleTransform.ScaleX = value * (G.gameWidth / 1920.0);
                scaleTransform.ScaleY = value * (G.gameHeight / 1080.0);
            }
        }

        public void fly() {
            switch (flyDirection) {
                case Direction.EAST:
                    Angle += G.nextDouble(0.01, 0.05);
                    if (Angle > 20) {
                        flyDirection = Direction.WEST;
                    }
                    break;
                case Direction.WEST:
                    Angle -= G.nextDouble(0.01, 0.05);
                    if (Angle < -20) {
                        flyDirection = Direction.EAST;
                    }
                    break;
            }
        }

        public double ScaledHeight {
            get {
                return element.Height * scaleTransform.ScaleY;
            }
        }

        public double ScaledWidth {
            get {
                return element.Width * scaleTransform.ScaleX;
            }
        }

        public abstract void update();
    }
}
