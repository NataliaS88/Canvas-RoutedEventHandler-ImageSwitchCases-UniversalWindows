using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace GameHangMan
{
    class Words
    {
        Canvas _canvas;
        int _topPos;
        int _leftPos;
        int _buttonDistance = 80;
        string _selectedWord;
        char[] lettersInWord;
        Image image = new Image() { Height = 350, Width = 1100 };
        BitmapImage bitmapImage = new BitmapImage();
        int _numberOfMistake = 0;
        int _guesedLetters = 0;
        TextBlock[] lettersBoxes;
        bool _letterIsGuessed;
        TextBox showWin;
        TextBox showLose;
        private string[] _wordsLevel1 = new string[10] { "FATHER", "MOTHER", "SON", "DAUGHTER", "GRANNY", "GRANDPA", "INFANT", "CHILD", "ADULT", "FAMILY" };
        private string[] _wordsLevel2 = new string[10] { "POLYMORFIZM", "INSTANCE", "OBJECT", "STRING", "DEFAULT", "CONSTRUCTOR", "PROPERTY", "CONSTANTA", "INTERFACE", "EXSEPTION" };
        Random _randWord = new Random();

        public Words(Canvas MyCanvas)
        {
            _canvas = MyCanvas;
        }

        public int TopPos
        {
            get { return _topPos; }
            set { _topPos = value; }
        }

        public int LeftPos
        {
            get { return _leftPos; }
            set { _leftPos = value; }
        }

        public int ButtonDistance
        {
            get { return _buttonDistance; }
            set { _buttonDistance = value; }
        }

        public int GuesedLetters
        {
            get { return _guesedLetters; }
            set { _guesedLetters = value; }
        }

        //random selection word from array after choosing level in appropriate radio button
        public void GetRandomWordLevel(int selection)
        {
            if (selection == 1)
            {
                _selectedWord = _wordsLevel1[_randWord.Next(0, 9)];
            }
            else
            {
                _selectedWord = _wordsLevel2[_randWord.Next(0, 9)];
            }
            Char[] lettersToArray = _selectedWord.ToCharArray();
            lettersInWord = new Char[lettersToArray.Length];

            for (int i = 0; i < lettersToArray.Length; i++)
            {
                lettersInWord[i] = lettersToArray[i];
            }
            lettersBoxes = new TextBlock[lettersToArray.Length];
            _leftPos = 0;
            _topPos = 300;
            for (int i = 0; i < lettersBoxes.Length; i++)
            {
                lettersBoxes[i] = new TextBlock();
                CreateMyWord(i);
            }
        }

        //creating TextBlocks for letters in selected word
        private void CreateMyWord(int placeInArr)
        {
            lettersBoxes[placeInArr].Height = 50;
            lettersBoxes[placeInArr].Width = 30;
            lettersBoxes[placeInArr].Text = "???";
            _leftPos = _leftPos + _buttonDistance;
            Canvas.SetTop(lettersBoxes[placeInArr], _topPos);
            Canvas.SetLeft(lettersBoxes[placeInArr], _leftPos);
            _canvas.Children.Add(lettersBoxes[placeInArr]);
        }

        //deletting word TextBlocks after pressing new game
        public void DeleteMyWord()
        {
            _guesedLetters = 0;
            _letterIsGuessed = false;
            for (int i = 0; i < lettersInWord.Length; i++)
                _canvas.Children.Remove(lettersBoxes[i]);
        }

        //writting in appropriate TextBlocks letter that was guessed 
        public void OpenGuessedLetter(char pressedLetter)
        {
            _letterIsGuessed = false;
            for (int i = 0; i < lettersInWord.Length; i++)
            {
                if (pressedLetter == lettersInWord[i])
                {
                    lettersBoxes[i].Text = pressedLetter.ToString();
                    lettersBoxes[i].Foreground = new SolidColorBrush(Colors.Green);
                    _guesedLetters++;
                    Vin(_guesedLetters);
                    _letterIsGuessed = true;
                }
            }
            if (_letterIsGuessed == false)
            {
                _numberOfMistake++;
                SetImage(_numberOfMistake);
            }
        }

        ////writting in appropriate TextBlocks letter that wasn't guessed when game is lost
        private void OpenNotGuessedLetter()
        {
            for (int i = 0; i < lettersInWord.Length; i++)
            {
                if (lettersBoxes[i].Text == "???")
                {
                    lettersBoxes[i].Text = lettersInWord[i].ToString();
                    lettersBoxes[i].Foreground = new SolidColorBrush(Colors.Red);
                }
            }
        }

        //setting image when pressed letter isn't guessed
        private void SetImage(int mistake)
        {
            switch (mistake)
            {
                case 1:
                    if (mistake == 1)
                        _leftPos = 300;
                    _topPos = 350;
                    bitmapImage.UriSource = new Uri("ms-appx:///Assets/HM10.png");
                    Canvas.SetTop(image, _topPos);
                    Canvas.SetLeft(image, _leftPos);
                    _canvas.Children.Add(image);
                    break;

                case 2:
                    if (mistake == 2)
                        bitmapImage.UriSource = new Uri("ms-appx:///Assets/HM9.png");
                    break;

                case 3:
                    if (mistake == 3)
                        bitmapImage.UriSource = new Uri("ms-appx:///Assets/HM8.png");
                    break;

                case 4:
                    if (mistake == 4)
                        bitmapImage.UriSource = new Uri("ms-appx:///Assets/HM7.png");
                    break;

                case 5:
                    if (mistake == 5)
                        bitmapImage.UriSource = new Uri("ms-appx:///Assets/HM6.png");
                    break;

                case 6:
                    if (mistake == 6)
                        bitmapImage = new BitmapImage(new Uri("ms-appx:///Assets/HM5.png"));
                    break;
                case 7:
                    if (mistake == 7)
                        bitmapImage.UriSource = new Uri("ms-appx:///Assets/HM4.png");
                    break;

                case 8:
                    if (mistake == 8)
                        bitmapImage.UriSource = new Uri("ms-appx:///Assets/HM3.png");
                    break;

                case 9:
                    if (mistake == 9)
                        bitmapImage.UriSource = new Uri("ms-appx:///Assets/HM2.png");
                    break;

                case 10:
                    if (mistake == 10)
                        bitmapImage.UriSource = new Uri("ms-appx:///Assets/HM1.png");
                    Lose(mistake);
                    break;

                default:
                    break;
            }
            image.Source = bitmapImage;
        }

        //writting message VIN 
        private void Vin(int guesedLetters)
        {
            if (guesedLetters == lettersBoxes.Length)
            {
                _leftPos = _topPos = 400;
                showWin = new TextBox { Height = 40, Width = 280, Background = new SolidColorBrush(Colors.White), Foreground = new SolidColorBrush(Colors.Black) };
                Canvas.SetTop(showWin, _topPos);
                Canvas.SetLeft(showWin, _leftPos);
                showWin.Text = "     Congratulation!!!\tYOU WIN!!!";
                _canvas.Children.Add(showWin);
                Alphabet.gameFinished = true;
            }
        }

        //writting message LOSE
        private void Lose(int mistake)
        {
            if (mistake == 10)
            {
                _leftPos = _topPos = 400;
                OpenNotGuessedLetter();
                showLose = new TextBox { Height = 40, Width = 280, Background = new SolidColorBrush(Colors.White), Foreground = new SolidColorBrush(Colors.Black) };
                Canvas.SetTop(showLose, _topPos);
                Canvas.SetLeft(showLose, _leftPos);
                showLose.Text = "    Your game is over!\tYOU LOSE!!!";
                _canvas.Children.Add(showLose);
                Alphabet.gameFinished = true;
            }
        }

        //deletting Image after pressing new game
        public void RemoveImage()
        {
            _canvas.Children.Remove(image);
            _numberOfMistake = 0;
            _canvas.Children.Remove(showWin);
            _canvas.Children.Remove(showLose);
        }
    }
}
