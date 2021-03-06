﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blackjack
{
  public class Deck
  {
    public List<Card> Cards { get; private set; }

    private List<Card> DiscardPile { get; set; }

    public Deck()
    {
      Cards = NewDeck();
      DiscardPile = new List<Card>();
    }

    public Deck(List<Card> c)
    {
      Cards = c;
      DiscardPile = new List<Card>();
    }

    public void Shuffle()
    {
      // put the discard pile back into the deck and shuffle
      Cards.AddRange(DiscardPile);
      DiscardPile.Clear();

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
      //if the deck is getting low, shuffle in the discard pile
      if (Cards.Count() < 20)
        Shuffle();

      return hand;
    }

    public void Discard(List<Card> cards) {
      DiscardPile.AddRange(cards);
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
