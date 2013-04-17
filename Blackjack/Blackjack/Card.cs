using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
  public enum Suit { Spades = 9824, Clubs = 9827, Hearts = 9829, Diamonds = 9830 }

  public enum Rank { Ace = 1, Deuce = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13 }

  public struct Card : IComparable<Card>
  {
    public Rank Rank { get; private set; }
    public Suit Suit { get; private set; }

    public Card(Rank rank, Suit suit) : this()
    {
      Rank = rank;
      Suit = suit;
    }

    public override string ToString()
    {
      return string.Format("{0} {1}", (char)Suit, Rank);
    }

    public int CompareTo(Card c)
    {
      if (this.Rank == c.Rank)
        return this.Suit.CompareTo(c.Suit);
      return this.Rank.CompareTo(c.Rank);
    }
  }
}
