using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Assignment_3_Game {
    public class GameEngine {
        private List<GameObject> displayList = new List<GameObject>();
        private DispatcherTimer timer = new DispatcherTimer();
        private DispatcherTimer gameTimer = new DispatcherTimer();
        private DispatcherTimer counterDownTimer = new DispatcherTimer();
        private TextProcessor textProcessor;
        private GameObject toRemove = null;
        private ScoreText scoreText = null;
        private TextArea textbox = null;
        private PlaneCount planeCount = null;
        private ResultText resultText = null;
        private TimerText timerText = null;

        private double segundo;
        private DateTime dt = new DateTime();
        public List<Round> rounds = new List<Round> { new Round(30), new Round(20) };
        public Round currentRound;
        public int roundIndex = 0;

        public GameEngine() {
            double msPerFrame = 10.0;
            timer.Interval = new TimeSpan(0, 0, 0, 0, (int)msPerFrame);
            timer.Start();

            currentRound = rounds[roundIndex];
            segundo = currentRound.Segundo;

            gameTimer.Interval = TimeSpan.FromSeconds(segundo);
            counterDownTimer.Interval = TimeSpan.FromSeconds(1);

            counterDownTimer.Tick += count_down;
            gameTimer.Tick += round_end;
            timer.Tick += timer_Tick;
        }

        public void resetGame(Round newRounds) {
            //double msPerFrame = 10;
            //msPerFrame *= 1920.0 / G.gameWidth;
            //timer.Interval = new TimeSpan(0, 0, 0, 0, (int)msPerFrame);
            segundo = newRounds.Segundo;
            gameTimer.Interval = TimeSpan.FromSeconds(segundo);

            G.canvas.Children.Clear();
            displayList.Clear();
            PlaneText.planesAndTexts.Clear();
        }

        private void count_down(object sender, EventArgs e) {
            if (segundo >= 1) {
                segundo--;
                timerText.CountDown = dt.AddSeconds(segundo).ToString("mm:ss");
            }
        }

        private void round_end(object sender, EventArgs e) {
            if (planeCount != null) {
                if (countPlane() == 0) {
                    resultText.Status = "You Win!";
                } else {
                    resultText.Status = "You Lose!";
                }
            }
            gameTimer.Stop();
            counterDownTimer.Stop();
            timerText.CountDown = string.Empty;

            if (resultText.Status == "You Win!") {
                roundIndex++;
                if (roundIndex < rounds.Count) {
                    MessageBoxResult response = MessageBox.Show("Do you want to continue to the next round",
                        "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    switch (response) {
                        case MessageBoxResult.Yes:
                            currentRound = rounds[roundIndex];
                            resetGame(currentRound);
                            setUpGame();
                            resultText.reset(roundIndex + 1, currentRound.Segundo);
                            break;
                        case MessageBoxResult.No:
                            Quit();
                            break;
                    }
                }
            }
        }

        public void setUpGame() {
            textProcessor = new TextProcessor();

            for (int i = 0; i < 10; ++i) {
                PlaneText planetext = new PlaneText(new Plane());
            }
            foreach (PlaneText planeText in PlaneText.planesAndTexts) {
                textProcessor.registerObserver(planeText);
            }
            textProcessor.timer_Imp(true);

            textbox = new TextArea();
            scoreText = new ScoreText();
            resultText = new ResultText();
            timerText = new TimerText();
            planeCount = new PlaneCount();

            gameTimer.Start();
            counterDownTimer.Start();
        }

        public void textArea_type(TextBox textbox, string content) {
            if (textProcessor.checkCorrectness(content)) {
                textbox.Text = string.Empty;
                if (content.Length > 5) {
                    scoreText.Score += 200;
                } else {
                    scoreText.Score += 100;
                }
            }
        }

        private int countPlane() {
            int planeLeft = 0;
            foreach (GameObject go in displayList) {
                if (go is PlaneText) {
                    planeLeft++;
                }
            }
            return planeLeft;
        }

        private void timer_Tick(object sender, EventArgs e) {
            foreach (GameObject gameObj in displayList) {
                gameObj.update();
            }
            if (toRemove != null) {
                displayList.Remove(toRemove);
            }
            planeCount.Count = countPlane();
        }

        public void addToDisplayList(GameObject obj) {
            displayList.Add(obj);
        }

        public void removeFromDisplayList(GameObject gameObject) {
            toRemove = gameObject;
        }

        private void Quit() {
            Application.Current.Shutdown();
        }
    }
}
