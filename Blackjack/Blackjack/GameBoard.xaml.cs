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
      dealer = new Dealer(new Hand(), deck);
      human = new Human(new Hand(), deck);

      //Get ready to play
      UpdateBankAndBetAmount();
      GetReadyToDeal();
    }

    private void GetReadyToDeal()
    {
      DealButton.IsEnabled = true;
      HitButton.IsEnabled = false;
      StandButton.IsEnabled = false;
    }

    private void GetReadyToPlayHand()
    {
      DealButton.IsEnabled = false;
      HitButton.IsEnabled = true;
      StandButton.IsEnabled = true;
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
      
      DisplayHandCards(HumanPanel, human.Hand);
      DisplayHandCards(DealerPanel, dealer.Hand);

      //make the hit/stand buttons available and disable the deal button
      GetReadyToPlayHand();

      //Play the dealer's hand
      PlayDealerHand();
    }

    private void PlayDealerHand()
    {
      dealer.PlayHand();
      DisplayHandCards(DealerPanel, dealer.Hand);
    }

    private void DisplayHandCards(StackPanel panel, Hand hand)
    {
      panel.Children.Clear();
      Image image;
      BitmapImage src;
      foreach (Card c in hand.Cards)
      {
        image = new Image();
        src = new BitmapImage();
        src.BeginInit();
        src.UriSource = new Uri(c.Path, UriKind.Relative);
        src.CacheOption = BitmapCacheOption.OnLoad;
        src.EndInit();
        image.Source = src;
        panel.Children.Add(image);
      }
    }

    private void HitButton_Click(object sender, RoutedEventArgs e)
    {
      human.Hit();
      DisplayHandCards(HumanPanel, human.Hand);
    }

    private void StandButton_Click(object sender, RoutedEventArgs e)
    {
      human.IsStanding = true;
      if (human.IsStanding && dealer.IsStanding)
      {
        if ((human.Hand.IsBust() && dealer.Hand.IsBust()) || (human.Hand.IsBlackjack() && dealer.Hand.IsBlackjack()) || (human.Hand.GetPoints() == dealer.Hand.GetPoints()))
        {
          //it's a tie, do something
        }
        else if (human.Hand.IsBust() || dealer.Hand.IsBlackjack())
        {
          //human loses
          human.FinishHand(false);
        }
        else if (human.Hand.GetPoints() > dealer.Hand.GetPoints() || dealer.Hand.IsBust())
        {
          //human wins
          human.FinishHand(true);
        }
        else
        {
          //human loses
          human.FinishHand(false);
        }
        UpdateBankAndBetAmount();
        dealer.Discard();
        DisplayHandCards(DealerPanel, dealer.Hand);
        DisplayHandCards(HumanPanel, human.Hand);
        GetReadyToDeal();

      }
    }

    private void UpdateBankAndBetAmount()
    {
      BetAmountLabel.Content = human.CurrentBet;
      BankAmountLabel.Content = human.Bank;
    }
  }
}
