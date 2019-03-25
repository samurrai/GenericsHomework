using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsHomework
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public List<Card> Deck { get; set; }
        
        public Game(int playersAmount)
        {
            Random random = new Random();
            Deck = new List<Card>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Deck.Add(new Card()
                    {
                        Suit = (Suits)j,
                        Type = (CardTypes)i,
                    });
                }
            }
            if (playersAmount < 2)
            {
                playersAmount = 2;
            }
            else if (playersAmount > 6)
            {
                playersAmount = 6;
            }
            Players = new List<Player>();
            for (int i = 0; i < playersAmount; i++)
            {
                Players.Add(new Player());
            }
            for (int i = 0; i < Deck.Count; i++)
            {
                var tmp = Deck[i];
                int pos = random.Next(0, Deck.Count);
                Deck[i] = Deck[pos];
                Deck[pos] = tmp;
            }
            for (int i = 0; i < playersAmount; i++)
            {
                Players[i].Cards = new List<Card>();
                for (int j = 0; j < Deck.Count / playersAmount; j++)
                {
                    Players[i].Cards.Add(Deck[j + ((Deck.Count / playersAmount) * i)]);
                }
            }
        }
        public void Start()
        {
            List<Card> field = new List<Card>();
            bool isGame = true;
            while (isGame)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    Console.WriteLine($"Player #{i + 1}:\n");
                    foreach (var card in Players[i].Cards)
                    {
                        Console.WriteLine($"{card.Suit} {card.Type}");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                }
                Console.WriteLine("Нажмите Enter для хода");
                Console.ReadLine();
                Random random = new Random();
                for(int i = 0; i < Players.Count; i++)
                {
                    int randPos = random.Next(0, Players[i].Cards.Count);
                    Console.WriteLine($"Player #{i + 1} - {Players[i].Cards[randPos].Suit} {Players[i].Cards[randPos].Type}");
                    field.Add(Players[i].Cards[randPos]);
                }
                int max = 0;
                for (int i = 0; i < field.Count; i++)
                {
                    if ((int)field[max].Type < (int)field[i].Type)
                    {
                        max = i;   
                    }
                }
                Console.ReadLine();
                for (int i = 0; i < Players.Count; i++)
                {
                    for (int j = 0; j < Players[i].Cards.Count; j++)
                    {
                        if (Players[i].Cards[j] == field[max])
                        {
                            Players[i].Cards.AddRange(field);
                            foreach (var card in Players[i].Cards)
                            {
                                Console.WriteLine(card.Suit + " " + card.Type);
                            }
                            field.RemoveAt(max);
                            break;
                        }
                        else
                        {
                            for (int k = 0; k < field.Count; k++)
                            {
                                if (Players[i].Cards[j] == field[k])
                                {
                                    Players[i].Cards.RemoveAt(j);
                                    field.RemoveAt(k);
                                    break;
                                }
                            }
                        }
                    }
                }
                for(int i = 0; i < Players.Count; i++)
                { 
                    if (Players[i].Cards.Count == 36) {
                        Console.WriteLine($"Player #{i + 1} победил!");
                        Console.WriteLine("Нажмите Enter для выхода");
                        Console.ReadLine();
                        isGame = false;
                    }
                }
                Console.Clear();
            }
        }
    }
}
