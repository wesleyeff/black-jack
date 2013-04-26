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
    private int betAmount;

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


      betAmount = 0;
      BetTextbox.Text = betAmount.ToString();
      BankTextbox.Text = human.Bank.ToString();
    }

    private void GetReadyToBet()
    {
      DealButton.IsEnabled = false;
      HitButton.IsEnabled = false;
      StandButton.IsEnabled = false;
      BetButton.IsEnabled = false;
      human.Discard();
      dealer.Discard();
      DisplayHandCards(HumanPanel, human);
      DisplayHandCards(DealerPanel, dealer);
      betAmount = human.CurrentBet;
      BetTextbox.Text = betAmount.ToString();
    }

    private void GetReadyToDeal()
    {
      DealButton.IsEnabled = true;
      HitButton.IsEnabled = false;
      StandButton.IsEnabled = false;
      BetButton.IsEnabled = false;
      dealer.FaceDown = true;
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

      DisplayHandCards(HumanPanel, human);
      DisplayHandCards(DealerPanel, dealer);

      if (human.Hand.IsBlackjack() && dealer.Hand.IsBlackjack())
      {
        // push
          DisplayMessageBox("Do The Push!");
          FinishHand(null);
      }
      else if (human.Hand.IsBlackjack())
      {
          DisplayMessageBox("Black Jack!! You Win!");
          FinishHand(true);
      }
      else if (dealer.Hand.IsBlackjack())
      {
          dealer.FaceDown = false;
          DisplayHandCards(DealerPanel, dealer);
          DisplayMessageBox("The dealer killed you dead.");
        FinishHand(false);
      }
      //make the hit/stand buttons available and disable the deal button
      else
        GetReadyToPlayHand();
    }

    private void PlayDealerHand()
    {
      Console.WriteLine("Dealer playing");
      dealer.PlayHand();
      DisplayHandCards(DealerPanel, dealer);
    }

    private void DisplayHandCards(StackPanel panel, Player player)
    {
      string path;
      panel.Children.Clear();
      Image image;
      BitmapImage src;
      for (int i = 0; i < player.Hand.Cards.Count; i++)
      {
          if (i == 0 && player.GetType() == typeof(Dealer) && ((Dealer)player).FaceDown)
          {
              path = "Images/Cards/Card-Back.png";
          }
          else
              path = player.Hand.Cards[i].Path;
          image = new Image();
          src = new BitmapImage();
          src.BeginInit();
          src.UriSource = new Uri(path, UriKind.Relative);
          src.CacheOption = BitmapCacheOption.OnLoad;
          src.EndInit();
          image.Source = src;
          panel.Children.Add(image);
      }

      foreach (Card c in player.Hand.Cards)
      {
        
      }
    }

    private void HitButton_Click(object sender, RoutedEventArgs e)
    {
        human.Hit();
        DisplayHandCards(HumanPanel, human);
      if (human.Hand.IsBust())
      {
          DisplayMessageBox("Busted!");
        human.FinishHand(false);
        GetReadyToBet();
      }
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
        if (MainWindow.tie == Tie.Push)
        {
          // do the push
            DisplayMessageBox("Do The Push!");
          FinishHand(null);
        }
        else if (MainWindow.tie == Tie.Dealer)
        {
            DisplayMessageBox("Tie goes to Dealer!");
          FinishHand(false);
        }
        else // Player
        {
            DisplayMessageBox("Tie goes to You(The Player)!");
          FinishHand(true);
        }
      }
      else if (human.Hand.IsBust() || dealer.Hand.IsBlackjack())
      {
        //human loses
          DisplayMessageBox("You are the worst!");
        FinishHand(false);
      }
      else if (human.Hand.GetPoints() > dealer.Hand.GetPoints() || dealer.Hand.IsBust())
      {
        //human wins
          DisplayMessageBox("You Are The Best!");
        FinishHand(true);
      }
      else
      {
        //human loses
          DisplayMessageBox("You are still the worst!");
        FinishHand(false);
      }
    }

    private void DisplayMessageBox(string msg)
    {
        MessageBoxButton mb = MessageBoxButton.OK;
        MessageBox.Show(msg, "Achtung!", mb);
    }

    private void FinishHand(bool? win)
    {
      human.FinishHand(win);
      UpdateBankAndBetAmount();
      dealer.Discard();
      DisplayHandCards(DealerPanel, dealer);
      DisplayHandCards(HumanPanel, human);
      GetReadyToBet();
    }

    private void UpdateBankAndBetAmount()
    {
      BetTextbox.Text = human.CurrentBet.ToString();
      BankTextbox.Text = human.Bank.ToString();
    }

    private void BetButton_Click(object sender, RoutedEventArgs e)
    {
      human.Bet(betAmount);
      UpdateBankAndBetAmount();
      betAmount = 0;
      GetReadyToDeal();
    }

    private void bet_one_Click(object sender, RoutedEventArgs e)
    {
      betAmount += 1;
      BetTextbox.Text = betAmount.ToString();
    }

    private void bet_five_Click(object sender, RoutedEventArgs e)
    {
      betAmount += 5;
      BetTextbox.Text = betAmount.ToString();
    }

    private void BetTextbox_TextChanged(object sender, TextChangedEventArgs e)
    {
 //        bet_amount = Convert.ToInt32(textBox1.Text);
 //        textBox2.Text = human.Bank.ToString();
        string Str = BetTextbox.Text.Trim();
        int Num;
        bool isNum = int.TryParse(Str, out Num);
        if (isNum)
            betAmount = Num;
        else
            MessageBox.Show("Invalid number");
        if (Num > 0)
            BetButton.IsEnabled = true;
    }
  }
}
