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
    private int bet_amount;

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
      GetReadyToBet();


      bet_amount = 0;
      textBox1.Text = bet_amount.ToString();
      textBox2.Text = human.Bank.ToString();
    }

    private void GetReadyToBet()
    {
        DealButton.IsEnabled = false;
        HitButton.IsEnabled = false;
        StandButton.IsEnabled = false;
        human.Discard();
        dealer.Discard();
        DisplayHandCards(HumanPanel, human.Hand);
        DisplayHandCards(DealerPanel, dealer.Hand);
        BetButton.IsEnabled = true;
    }

    private void GetReadyToDeal()
    {
      DealButton.IsEnabled = true;
      HitButton.IsEnabled = false;
      StandButton.IsEnabled = false;
      BetButton.IsEnabled = false;
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
        human.Hand.Add(deck.Deal(2));
      dealer.Hand.Add(deck.Deal(2));
      
      DisplayHandCards(HumanPanel, human.Hand);
      DisplayHandCards(DealerPanel, dealer.Hand);

      if (human.Hand.IsBlackjack() && dealer.Hand.IsBlackjack())
      {
          // push
      }
      if (human.Hand.IsBlackjack())
      {
          human.FinishHand(true);
      }
      if (dealer.Hand.IsBlackjack())
      {
          human.FinishHand(false);
      }

      //make the hit/stand buttons available and disable the deal button
      GetReadyToPlayHand();
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
        if (human.Hand.IsBust())
        {
            human.FinishHand(false);
            GetReadyToBet();
        }
      DisplayHandCards(HumanPanel, human.Hand);
    }

    private void StandButton_Click(object sender, RoutedEventArgs e)
    {
      human.IsStanding = true;

      //Play the dealer's hand
      PlayDealerHand();


      if (human.IsStanding && dealer.IsStanding)
      {
          CompareHands();

      }
    }

    private void CompareHands()
    {
        if ((human.Hand.IsBust()) || (human.Hand.IsBlackjack() && dealer.Hand.IsBlackjack()) || (human.Hand.GetPoints() == dealer.Hand.GetPoints()))
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

    private void UpdateBankAndBetAmount()
    {
      textBox2.Text = human.CurrentBet.ToString();
      textBox1.Text = human.Bank.ToString();
    }

    private void BetButton_Click(object sender, RoutedEventArgs e)
    {
//        Console.WriteLine(bet_amount);
        human.Bet(bet_amount);
        textBox2.Text = human.Bank.ToString();
        bet_amount = 0;
        textBox1.Text = bet_amount.ToString();
        GetReadyToDeal();
    }

    private void bet_one_Click(object sender, RoutedEventArgs e)
    {
        bet_amount += 1;
//        Console.WriteLine(bet_amount);
        textBox1.Text = bet_amount.ToString();
    }

    private void bet_five_Click(object sender, RoutedEventArgs e)
    {
        bet_amount += 5;
//        Console.WriteLine(bet_amount);
        textBox1.Text = bet_amount.ToString();
    }

    private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
    {
        bet_amount = Convert.ToInt32(textBox1.Text);
        textBox2.Text = human.Bank.ToString();
    }

    private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
  }
}
