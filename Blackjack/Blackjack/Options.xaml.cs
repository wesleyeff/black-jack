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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Options : Window
    {
         int decks = MainWindow.decks;
         bool softdeal = MainWindow.softdeal;
         Tie tie = MainWindow.tie;

        

        public Options()
        {
            InitializeComponent();
            if (decks == 1) { radioButton6.IsChecked = true; }
            if (decks == 2) { radioButton7.IsChecked = true; }
            if (decks == 5) { radioButton8.IsChecked = true; }
            if (softdeal == true) { radioButton4.IsChecked = true; }
              else { radioButton5.IsChecked = true; }
            if (tie == Tie.Dealer) { radioButton1.IsChecked = true; }
            if (tie == Tie.Human) { radioButton2.IsChecked = true; }
            if (tie == Tie.Push) { radioButton3.IsChecked = true; }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
         //   MessageBox.Show("decks = "+decks+"\nsoftdeal = "+ softdeal+"\ntie = "+tie );
            MainWindow.decks = decks;
            MainWindow.softdeal = softdeal;
            MainWindow.tie = tie;
            this.Close();
        }

        private void radioButton1_Checked_1(object sender, RoutedEventArgs e)
        {
            tie = Tie.Dealer;
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            tie = Tie.Human;
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            tie = Tie.Push;
        }

        private void radioButton4_Checked(object sender, RoutedEventArgs e)
        {
            softdeal = true;
        }

        private void radioButton5_Checked(object sender, RoutedEventArgs e)
        {
            softdeal = false;
        }

        private void radioButton6_Checked(object sender, RoutedEventArgs e)
        {
            decks = 1;
        }

        private void radioButton7_Checked(object sender, RoutedEventArgs e)
        {
            decks = 2;
        }

        private void radioButton8_Checked(object sender, RoutedEventArgs e)
        {
            decks = 5;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
