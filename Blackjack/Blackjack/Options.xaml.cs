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
         int decks = 0;
         bool softdeal = false;
         string tie;

        public Options()
        {
            InitializeComponent();
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
            tie = "dealer";
        }

        private void radioButton2_Checked(object sender, RoutedEventArgs e)
        {
            tie = "player";
        }

        private void radioButton3_Checked(object sender, RoutedEventArgs e)
        {
            tie = "push";
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
            radioButton1.IsChecked = true;
            radioButton4.IsChecked = true;
            radioButton6.IsChecked = true;
            MainWindow.decks = decks;
            MainWindow.softdeal = softdeal;
            MainWindow.tie = tie;
            this.Close();
        }

       

    }
}
