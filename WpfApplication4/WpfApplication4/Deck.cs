/**
 * 
 * Darrel Portzline hi
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
  public class Deck
  {
    public List<Card> Cards { get; private set; }

    public Deck()
    {
      Cards = NewDeck();
    }

    public Deck(List<Card> c)
    {
      Cards = c;
    }

    public void Shuffle()
    {
      Card temp;
      int next;
      Random r = new Random();
      for (int i = Cards.Count() - 1; i > 0; i--)
      {
        next = r.Next(i + 1);
        temp = Cards[i];
        Cards[i] = Cards[next];
        Cards[next] = temp;
      }
    }

    public List<Card> Deal(int i)
    {
      List<Card> hand = new List<Card>();
      if (i > Cards.Count())
      {
        return hand;
      }
      for (int j = 0; j < i; j++)
      {
        hand.Add(Cards[0]);
        Cards.RemoveAt(0);
      }
      return hand;
    }

    public static List<Card> NewDeck()
    {
      List<Card> deck = new List<Card>();
      foreach (Rank r in Enum.GetValues(typeof(Rank)))
      {
        foreach (Suit s in Enum.GetValues(typeof(Suit)))
        {
          deck.Add(new Card(r, s));
        }
      }
      return deck;
    }

    public override string ToString()
    {
      StringBuilder result = new StringBuilder();
      for (int i = 0; i < Cards.Count(); i++)
      {
        result.Append(String.Format("{0, -9}", Cards[i]));
        if (i % 4 == 3 && i > 0)
        {
          result.Append("\n");
        }
      }
      return result.ToString();
    }
  }
}
