using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
  public abstract class Player
  {
    public Hand Hand { get; private set; }
    public bool IsStanding { get; set; }

    public Player(Hand hand)
    {
      Hand = hand;
    }

    public void Hit(Deck deck)
    {
      Hand.Add(deck.Deal(1));
    }

    public void Discard(Deck deck)
    {
      Hand.Discard(deck);
    }
  }

  public class Dealer : Player
  {
    public Dealer(Hand hand) : base(hand) { }
  }

  public class Human : Player
  {
    public int Bank { get; set; }
    public int CurrentBet { get; private set; }

    public Human(Hand hand) : base(hand)
    {
      Bank = 500;
      CurrentBet = 0;
    }

    public void FinishHand(bool win)
    {
      if (win)
      {
        if (Hand.IsBlackjack())
          Bank += (CurrentBet * 2);
        else
          Bank += CurrentBet;
      }
      else
        Bank -= CurrentBet;
      //Reset the bet amount
      CurrentBet = 0;
    }

    public void Bet(int amount)
    {
      Bank -= amount;
      CurrentBet += amount;
    }
  }
}
