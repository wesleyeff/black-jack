using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
  public abstract class Player
  {
    public Hand Hand { get; private set; }
    public Deck Deck { get; private set; }
    public bool IsStanding { get; set; }

    public Player(Hand hand, Deck deck)
    {
      Hand = hand;
      Deck = deck;
    }

    public void Hit()
    {
      Hand.Add(Deck.Deal(1));
    }

    public void Discard()
    {
      Hand.Discard(Deck);
    }
  }

  public class Dealer : Player
  {
    public Dealer(Hand hand, Deck deck) : base(hand, deck) { }

    public void PlayHand()
    {
      while (Hand.GetPoints() < 17) // for now just hit until the total is >= 17
      {
        Hit();
      }
      IsStanding = true;
    }
  }

  public class Human : Player
  {
    public int Bank { get; set; }
    public int CurrentBet { get; private set; }

    public Human(Hand hand, Deck deck) : base(hand, deck)
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
      //Reset the bet amount and discard hand
      CurrentBet = 0;
      Hand.Discard(Deck);
    }

    public void Bet(int amount)
    {
      Bank -= amount;
      CurrentBet += amount;
    }
  }
}
