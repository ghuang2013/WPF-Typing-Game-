using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Assignment_3_Game {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private Background background = new Background();
        private GameEngine gameEngine = G.gameEngine;
        private MediaPlayer keyStroke = G.SetupSound(G.root + "key.wav");

        public TextBox TypeBox {
            get { return textBox; }
            set { textBox = value; }
        }

        public MainWindow() {
            InitializeComponent();
            WindowState = WindowState.Maximized;

            mainGrid.Height = Height;
            mainGrid.Width = Width;

            G.canvas = new Canvas();
            G.canvas.Height = mainGrid.Height;
            G.canvas.Width = mainGrid.Width;
            mainGrid.Children.Add(background.GameElement);

            gameEngine.setUpGame();
            textBox.KeyUp += typing_event;
            mainGrid.Children.Add(G.canvas);

            KeyUp += window_keyed;
        }

        private void window_keyed(object sender, KeyEventArgs e) {
            if (e.Key == Key.Escape) {
                gameEngine.resetGame(gameEngine.currentRound);
                gameEngine.setUpGame();
            }
        }

        private void typing_event(object sender, KeyEventArgs e) {
            string typed = textBox.Text;
            keyStroke.Stop();
            keyStroke.Play();
            keyStroke.Volume = 0.2;
            gameEngine.textArea_type(textBox, typed);
        }
    }
}
