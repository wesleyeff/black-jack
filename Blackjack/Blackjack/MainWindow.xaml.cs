using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    /// 

    public enum Tie : byte { Push, Human, Dealer }

    public partial class MainWindow : Window
    {
        public static int decks = 1;
        public static bool softdeal = true;
        public static Tie tie;
        

        public MainWindow()
        {
            InitializeComponent();
            var welcome_screen = new WelcomeScreen();
            this.Content = welcome_screen;
        }
    }
}
