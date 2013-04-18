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
    public Human(Hand hand) : base(hand) { }
    //TODO: needs a bank property

    //TODO: this needs to be implemented once we have a Bank class
    public void Bet(int amount)
    {

    }
  }
}
