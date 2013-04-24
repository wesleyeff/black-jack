﻿using System;
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
      human.Discard();
      dealer.Discard();
      DisplayHandCards(HumanPanel, human.Hand);
      DisplayHandCards(DealerPanel, dealer.Hand);
      BetButton.IsEnabled = true;
      betAmount = human.CurrentBet;
      BetTextbox.Text = betAmount.ToString();
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
      Console.WriteLine("Dealer playing");
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
        if (MainWindow.tie == Tie.Push)
        {
          // do the push
          FinishHand(null);
        }
        else if (MainWindow.tie == Tie.Dealer)
        {
          FinishHand(false);
        }
        else // Player
        {
          FinishHand(true);
        }
      }
      else if (human.Hand.IsBust() || dealer.Hand.IsBlackjack())
      {
        //human loses
        FinishHand(false);
      }
      else if (human.Hand.GetPoints() > dealer.Hand.GetPoints() || dealer.Hand.IsBust())
      {
        //human wins
        FinishHand(true);
      }
      else
      {
        //human loses
        FinishHand(false);
      }
    }

    private void FinishHand(bool? win)
    {
      human.FinishHand(win);
      UpdateBankAndBetAmount();
      dealer.Discard();
      DisplayHandCards(DealerPanel, dealer.Hand);
      DisplayHandCards(HumanPanel, human.Hand);
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
        double Num;
        bool isNum = double.TryParse(Str, out Num);
        if (isNum)
            betAmount = Convert.ToInt32(Str);
        else
            MessageBox.Show("Invalid number");

    }
  }
}
