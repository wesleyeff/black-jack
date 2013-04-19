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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Blackjack
{
    /// <summary>
    /// Interaction logic for WelcomeScreen.xaml
    /// </summary>
    public partial class WelcomeScreen : Page
    {
        public WelcomeScreen()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new Options();
            newWindow.Owner = Application.Current.MainWindow;
            newWindow.ShowDialog();
        }

        // Rules click event
        private void rules_Click(object sender, RoutedEventArgs e)
        {
            var rulesWindow = new Rules();
            rulesWindow.Owner = Application.Current.MainWindow;
            rulesWindow.ShowDialog();
        }

        private void game_board_Click(object sender, RoutedEventArgs e)
        {
            var gameBoardPage = new GameBoard();
            Application.Current.MainWindow.Content = gameBoardPage;
        }
    }
}
