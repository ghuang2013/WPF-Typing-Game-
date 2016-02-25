using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Assignment_3_Game {
    class TextProcessor : Observable {
        private StreamReader freader = new StreamReader(@"..\..\text.txt");
        private DispatcherTimer timer = new DispatcherTimer();
        private string line = string.Empty;
        private List<string> tokens = new List<string>();
        private List<Observer> observers = new List<Observer>();
        private Stack<Missile> missiles = new Stack<Missile>();

        public TextProcessor() {
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public bool checkCorrectness(string typed) {
            for (int i = 0; i < observers.Count; ++i) {
                PlaneText textOnPlane = (PlaneText)observers[i];
                if (textOnPlane.Y < 100) continue;
                else {
                    if (textOnPlane.Text.Length < typed.Length) {
                        continue;
                    } else if (typed == (textOnPlane.Text).Substring(0, typed.Length)) {
                        ((PlaneText)textOnPlane).highLight(typed);
                        if (typed.Length == textOnPlane.Text.Length) {
                            ((PlaneText)textOnPlane).incMessageSize();
                            missiles.Push(new Missile());
                            missiles.Peek().makeCollide((PlaneText)textOnPlane);
                            missiles.Pop();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void registerObserver(Observer o) {
            observers.Add(o);
        }

        public void removeObserver(Observer o) {
            observers.Remove(o);
        }

        public void notifyObservers() {
            int i = 0;
            foreach (Observer observer in observers) {
                if (i < tokens.Count) {
                    ((PlaneText)observer).setNextText(tokens[i]);
                    i++;
                } else {
                    ((PlaneText)observer).setNextText(tokens[i - 1]);
                }
            }
        }

        private void timer_Tick(object sender, EventArgs e) {
            timer_Imp();
        }

        public void timer_Imp(bool firstTime = false) {
            update();
            if (line != null) {
                tokens.Clear();
                tokens.AddRange(line.Split(new char[] { ',', '.', '(', ')', ' ' }));
                notifyObservers();
            }
            if (firstTime) {
                foreach (Observer observer in observers) {
                    ((PlaneText)observer).updateText();
                }
            }
        }

        public void update() {
            if ((line = freader.ReadLine()) == null || line == "") {
                freader.BaseStream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}
