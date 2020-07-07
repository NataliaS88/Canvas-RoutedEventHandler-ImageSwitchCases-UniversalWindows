using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GameHangMan
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Alphabet _alph;
        Words _word;
        GameBoard _board;

        public MainPage()
        {
            this.InitializeComponent();
            _word = new Words(MyCanvas);
            _alph = new Alphabet(MyCanvas, _word);
            _board = new GameBoard(MyCanvas, _word, _alph);
            _board.AddControlButtons();
            _alph.LettersToArrayButton();
        }

        public Canvas canvas
        {
            get
            { return MyCanvas; }
            set
            { MyCanvas = value; }
        }
    }
}

