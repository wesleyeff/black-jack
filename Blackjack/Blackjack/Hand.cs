using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
  public class Hand
  {
    public List<Card> Cards { get; private set; }

    public Hand()
    {
      Cards = new List<Card>();
    }

    public Hand(List<Card> newHand)
    {
      newHand.Sort();
      Cards = newHand;
    }

    public int GetPoints()
    {
      int total = 0;
      foreach (Card c in Cards)
      {
        if ((int)c.Rank >= 10)
          total += 10;
        else if (c.Rank == Rank.Ace)
          total += 11;
        else
          total += (int)c.Rank;
      }
      if (total <= 21)
        return total;
      int aces = Cards.Where(c => c.Rank == Rank.Ace).Count();
      if (aces > 0)
      {
        while (total > 21 && aces > 0)
        {
          total -= 10;
          aces--;
        }
      }
      return total;
    }

    public void Add(List<Card> cards)
    {
      Cards.AddRange(cards);
    }

    public void Discard(Deck deck)
    {
      deck.Discard(Cards);
      Cards.Clear();
    }

    public bool IsBlackjack()
    {
      return Cards.Count == 2 && GetPoints() == 21;
    }

    public bool IsFlush()
    {
      Suit suit = Cards[0].Suit;
      for (int i = 1; i < Cards.Count(); i++)
      {
        if (Cards[i].Suit != suit) return false;
      }

      return true;
    }

    public bool IsStraight()
    {
      for (int i = 1; i < Cards.Count(); i++)
      {
        if (Cards[i].Rank != Cards[i - 1].Rank + 1) //if the current rank is not one more than the previous rank
          return false;
      }
      return true;
    }

    public override string ToString()
    {
      StringBuilder result = new StringBuilder();
      foreach (Card c in Cards)
      {
        result.Append(String.Format("{0, -9}", c));
      }

      bool isFlush = IsFlush();
      bool isStraight = IsStraight();

      if (isFlush && isStraight)
      {
        result.Append(String.Format("{0, -9}", "Straight Flush"));
      }
      else
      {
        if (isFlush) result.Append(String.Format("{0, -9}", "Flush"));
        if (isStraight) result.Append(String.Format("{0, -9}", "Straight"));
      }

      return result.ToString();
    }
  }
}
