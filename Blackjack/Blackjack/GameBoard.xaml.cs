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
    /// Interaction logic for GameBoard.xaml
    /// </summary>
    public partial class GameBoard : Page
    {
      private Deck deck;
      private Dealer dealer;
      private Human human;

        public GameBoard()
        {
          InitializeComponent();

          //initialize players and deck
          deck = new Deck();
          deck.Shuffle();
          dealer = new Dealer(new Hand());
          human = new Human(new Hand());

          //disable some buttons until hand is dealt
          HitButton.IsEnabled = false;
          StandButton.IsEnabled = false;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var welcome_screen = new WelcomeScreen();
            Application.Current.MainWindow.Content = welcome_screen;
        }

        private void DealButton_Click(object sender, RoutedEventArgs e)
        {
          //deal 2 cards to each player
          dealer.Hand.Add(deck.Deal(2));
          human.Hand.Add(deck.Deal(2));
          //TODO: do something to display the dealt cards on the GUI

          //make the hit/stand buttons available and disable the deal button
          HitButton.IsEnabled = true;
          StandButton.IsEnabled = true;
          DealButton.IsEnabled = false;

          //TODO: possibly need to check for blackjack here?
        }

        private void HitButton_Click(object sender, RoutedEventArgs e)
        {
          human.Hand.Add(deck.Deal(1));
          //TODO: do something to display the new card to user
        }
    }
}
