/**
 * 
 * Darrel Portzline
 * A03 - Cards
 * 2/6/2013
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cards
{
  public class Hand
  {
    public List<Card> Cards { get; private set; }

    public Hand(List<Card> newHand)
    {
      newHand.Sort();
      Cards = newHand;
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
