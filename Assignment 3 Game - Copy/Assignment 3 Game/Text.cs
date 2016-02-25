using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Globalization;
using System.Windows;
using System.IO;
using System.Windows.Documents;

namespace Assignment_3_Game {
    class Text : GameObject {

        private StreamReader freader = new StreamReader(G.fileRoot + "text.txt");
        private TextBlock sentence;
        private TextPointer begin;

        private List<string> tokens;

        public bool equals(string typed) {
            int length = typed.Length;
            if (length > sentence.Text.Length)
                return false;
            if (typed != "" && typed == sentence.Text.Substring(0, length)) {
                return true;
            }
            return false;
        }

        public TextBlock Sentence {
            get {
                return sentence;
            }
        }

        public Text() {
            GameElement = initializeTextBlock();
            begin = sentence.ContentStart;
        }

        private TextBlock initializeTextBlock() {
            sentence = new TextBlock();
            sentence.Foreground = new SolidColorBrush(Colors.Black);
            sentence.FontSize = 20;
            sentence.FontFamily = new FontFamily("Script MT Bold");
            sentence.Text = "Game Starts";
            sentence.Width = 1000;
            sentence.Height = 200;
            return sentence;
        }

        public List<string> getWords() {
            tokens = new List<string>(sentence.Text.Split());
            return tokens;
        }

        public override void update() {
            string line = string.Empty;
            if ((line = freader.ReadLine()) != null && line != "") {
                sentence.Text = line;
            } else {
                freader.BaseStream.Seek(0, SeekOrigin.Begin);
            }
        }

        public void highLight(string s) {
            //if (s.Length > sentence.Text.Length) return;
            TextRange range = new TextRange(begin, begin.GetPositionAtOffset(s.Length));
            range.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Salmon);
            range.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Brown);
        }
    }
}
