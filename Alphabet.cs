using GameHangMan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace GameHangMan
{
    class Alphabet
    {
        Canvas _myCanvas;
        int buttonWH = 50;
        const int alphLength = 26;
        char alphabetStart = Char.Parse("A");
        char alphabetEnd = Char.Parse("Z");
        Char[] _alphabet = new Char[alphLength];
        int _alpIndex;
        Words _word;
        public static bool gameFinished;
        Button[] _lettersButtons;

        public Alphabet(Canvas MyCanvas, Words Words)
        {
            _myCanvas = MyCanvas;
            _word = Words;
        }

        //creating alphabet buttons
        public void LettersToArrayButton()
        {
            _lettersButtons = new Button[alphLength];
            _word.TopPos = 100;
            _word.LeftPos = 0;
            _word.ButtonDistance = 80;
            _alpIndex = 0;

            for (int i = alphabetStart; i <= alphabetEnd; i++)
            {
                _lettersButtons[_alpIndex] = new Button();
                _alphabet[_alpIndex] = (Char)i;
                _lettersButtons[_alpIndex].Content = (char)i;
                _lettersButtons[_alpIndex].Foreground = new SolidColorBrush(Colors.Red);
                _lettersButtons[_alpIndex].Background = new SolidColorBrush(Colors.Yellow);
                _lettersButtons[_alpIndex].Width = _lettersButtons[_alpIndex].Height = buttonWH;
                if (_word.LeftPos >= _myCanvas.Width - 2 * buttonWH)
                {
                    _word.TopPos = _word.TopPos + _word.ButtonDistance;
                    _word.LeftPos = 0;
                    _word.LeftPos = _word.LeftPos + 3 * _word.ButtonDistance;
                }
                else _word.LeftPos = _word.LeftPos + _word.ButtonDistance;
                Canvas.SetTop(_lettersButtons[_alpIndex], _word.TopPos);
                Canvas.SetLeft(_lettersButtons[_alpIndex], _word.LeftPos);
                _myCanvas.Children.Add(_lettersButtons[_alpIndex]);
                _lettersButtons[_alpIndex].Click += new RoutedEventHandler(btn_click);
                _alpIndex++;
            }
        }

        private void btn_click(Object sender, RoutedEventArgs e)
        {
            if (GameBoard._levelIsSelected == true && gameFinished == false)
            {
                Button btn = sender as Button;
                char PressedLetter = (char)btn.Content;
                btn.Background = new SolidColorBrush(Colors.Gray);
                btn.IsEnabled = false;
                _word.OpenGuessedLetter(PressedLetter);
            }
        }

        //enabling letters buttons after pressing new game
        public void ButtonsNotPressed()
        {
            for (int i = 0; i <= _lettersButtons.Length; i++)
            {
                if (!_lettersButtons[i].IsEnabled)
                {
                    _lettersButtons[i].IsEnabled = true;
                    _lettersButtons[i].Background = new SolidColorBrush(Colors.Yellow);
                }

                if (i == _lettersButtons.Length - 1)
                    return;
            }
        }
    }
}



