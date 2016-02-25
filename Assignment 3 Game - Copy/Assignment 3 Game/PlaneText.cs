using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace Assignment_3_Game {
    class PlaneText : GameObject, Observer {
        private TextBlock message = new TextBlock();
        private SolidColorBrush brush = new SolidColorBrush(Colors.Black);
        private double scaleIndex;
        private Plane plane;
        private static int OriginalFontSize = 25;
        public static List<PlaneText> planesAndTexts = new List<PlaneText>();
        public string nextText = string.Empty;

        public PlaneText(Plane plane) {
            this.plane = plane;
            setUpTextBlock();
            GameElement = message;
            X = initializeHorizontalPosition(plane);
            Y = initializeAltitude(plane);
            dY = plane.dY;
            scaleIndex = plane.ScaleIndex;
            planesAndTexts.Add(this);
            addToGame();
        }

        public override void removeFromGame(){
            base.removeFromGame();
            TargetPlane.makeInactive();
        }

        public Plane TargetPlane {
            get { return plane; }
        }

        public void setMessageColor(Color c) {
            brush.Color = c;
            message.Foreground = brush;
        }

        public void incMessageSize() {
            message.FontSize = OriginalFontSize + 10;
        }

        public override void update() {           
            if (!isActive) {
                updateText();
                Y = initializeAltitude(plane);
                X = initializeHorizontalPosition(plane);
            }
            plane.update();
            Y -= dY;
            fly();
        }

        public string Text {
            get { return message.Text; }
        }

        public void updateText() {
            message.Text = nextText;
            setMessageColor(Colors.Black);
            message.FontSize = OriginalFontSize;
        }

        public void setNextText(string s) {
            nextText = s;
        }

        public void highLight(string s) {
            string unhighlighted = message.Text.Substring(s.Length);
            message.Inlines.Clear();
            message.Inlines.Add(new Run(s) {
                Foreground = Brushes.Orange
            });
            message.Inlines.Add(new Run(unhighlighted) {
                Foreground = Brushes.Snow
            });
        }

        private static double initializeHorizontalPosition(Plane plane) {
            return plane.X;
        }

        private static double initializeAltitude(Plane plane) {
            return plane.Y + plane.GameElement.Height / 2;
        }

        private void setUpTextBlock() {
            message.Foreground = brush;
            message.FontSize = OriginalFontSize;
            message.FontFamily = new FontFamily("PMingLiU-ExtB Bold");
            message.Text = "";
            message.Height = plane.GameElement.Width;
            message.Width = plane.GameElement.Height;
        }
    }
}
