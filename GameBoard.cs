using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameHangMan
{
    class GameBoard
    {
        Canvas _canvas;

        TextBlock level;
        Button newGame;
        Button exit;
        Words _word;
        RadioButton levelEasy;
        RadioButton levelHard;
        Alphabet _alph;
        public static bool _levelIsSelected = false;
        int _buttonHeight = 45;
        int _buttonWidth = 80;
        int _radioButtHW = 30;

        public GameBoard(Canvas MyCanvas, Words Word, Alphabet Alph)
        {
            _canvas = MyCanvas;
            _word = Word;
            _alph = Alph;
        }

        //creating control buttons on canvas
        public void AddControlButtons()
        {
            _word.TopPos = 15;
            _word.LeftPos = 255;
            level = new TextBlock();
            level.Height = _buttonHeight;
            level.Width = _buttonWidth * 1.25;
            level.Text = "LEVEL";
            Canvas.SetTop(level, _word.TopPos);
            Canvas.SetLeft(level, _word.LeftPos);
            _canvas.Children.Add(level);

            _word.TopPos = 550;
            _word.LeftPos = 1220;
            exit = new Button();
            exit.Height = _buttonHeight;
            exit.Width = _buttonWidth;
            exit.Content = "EXIT";
            Canvas.SetTop(exit, _word.TopPos);
            Canvas.SetLeft(exit, _word.LeftPos);
            _canvas.Children.Add(exit);
            exit.Click += new RoutedEventHandler(exit_click);

            _word.TopPos = _word.LeftPos = 10;
            newGame = new Button();
            newGame.Height = _buttonHeight;
            newGame.Width = 145;
            newGame.Content = "NEW GAME";
            Canvas.SetTop(newGame, _word.TopPos);
            Canvas.SetLeft(newGame, _word.LeftPos);
            _canvas.Children.Add(newGame);
            newGame.Click += new RoutedEventHandler(newGame_click);

            levelEasy = new RadioButton();
            levelEasy.Height = _radioButtHW;
            levelEasy.Width = _radioButtHW;
            levelEasy.Content = "EASY";
            Canvas.SetTop(levelEasy, 10);
            Canvas.SetLeft(levelEasy, 350);
            _canvas.Children.Add(levelEasy);
            levelEasy.Checked += new RoutedEventHandler(getSelectedRB1_Checked);

            _word.TopPos = 40;
            _word.LeftPos = 350;
            levelHard = new RadioButton();
            levelHard.Height = _radioButtHW;
            levelHard.Width = _radioButtHW;
            levelHard.Content = "HARD";
            Canvas.SetTop(levelHard, _word.TopPos);
            Canvas.SetLeft(levelHard, _word.LeftPos);
            _canvas.Children.Add(levelHard);
            levelHard.Checked += new RoutedEventHandler(getSelectedRB2_Checked);
        }

        private void newGame_click(Object sender, RoutedEventArgs e)
        {
            Button newGame = sender as Button;
            if (_levelIsSelected)
            {
                Alphabet.gameFinished = false;
                _alph.ButtonsNotPressed();
                _word.DeleteMyWord();
                _word.RemoveImage();
                _levelIsSelected = false;
                levelEasy.IsEnabled = true;
                levelHard.IsEnabled = true;
                levelEasy.IsChecked = false;
                levelHard.IsChecked = false;
            }
            return;
        }

        private void exit_click(Object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        private void getSelectedRB1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton easyLevel = sender as RadioButton;

            if (levelHard.IsChecked == true)
                return;
            else
            {
                _word.GetRandomWordLevel(1);
                _levelIsSelected = true;
                levelHard.IsEnabled = false;
            }
        }

        private void getSelectedRB2_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton hardLevel = sender as RadioButton;

            if (levelEasy.IsChecked == true)
                return;
            else
            {
                _word.GetRandomWordLevel(2);
                _levelIsSelected = true;
                levelEasy.IsEnabled = false;
            }
        }
    }
}
