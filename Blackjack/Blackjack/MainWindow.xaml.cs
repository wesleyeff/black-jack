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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Hand test = new Hand(new List<Card>() {
              new Card(Rank.Ace, Suit.Clubs),
              new Card(Rank.Ten, Suit.Clubs),
              new Card(Rank.Deuce, Suit.Clubs)
            });
            int total = test.GetPoints();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new Options();
            newWindow.Owner = this;
            newWindow.ShowDialog();
        }
    }
}
