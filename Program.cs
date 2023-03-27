using System;
//Problème lorsque j'ai un as, il garde le score a 11
class BlackJack
{
    static string[] cards = new string[13] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    static string[] sign = new string[4] { "H", "D", "S", "C" };

    static string[] dealerhand = new string[] { };
    static string[] playerhand = new string[] { };

    static int dealerscore = 0;
    static int playerscore = 0;

    static string command = "";

    static void Main(string[] args)
    {
        Console.WriteLine("<---- Blackjack ---->");
        Console.WriteLine();
        Console.WriteLine("Dealer hand [" + NewCard(0) + "  ??]");
        Console.WriteLine("Your hand [" + NewCard(1) + " " + NewCard(1) + "]");
        Console.WriteLine();

        if (playerscore == 21)
        {
            Check(playerscore, dealerscore);
        }

        while (command != "n" && playerscore < 21)
        {
            Console.Write("Another card ? y/n ");
            command = Console.ReadLine();
            if (command == "n")
            {
                break;
            }
            for (int i = 0; i < playerhand.Length; i++)
            {
                Console.Write(playerhand[i] + " ");
            }
            Console.WriteLine(NewCard(1));
            Console.WriteLine();
        }

        Console.WriteLine();
        Console.Write("Dealer hand : [");
        for (int i = 0; i < dealerhand.Length; i++)

        {
            Console.Write(dealerhand[i] + " ");
        }

        if (playerscore > 21)
        {
            Console.Write(NewCard(0) + " ");
        }
        else
        {
            while (dealerscore < 17)
            {
                Console.Write(NewCard(0) + " ");
            }
        }
        Console.WriteLine("] Score : " + dealerscore);
        Console.Write("Your hand : [");

        for (int i = 0; i < playerhand.Length; i++)
        {
            Console.Write(playerhand[i] + " ");
        }
        Console.WriteLine("] Score : " + playerscore);
        Check(playerscore, dealerscore);
    }

    static string NewCard(int who)
    {
        Random random = new Random();
        int randomCard = random.Next(0, 12);
        int randomSign = random.Next(0, 3);

        if (who == 0)
        {
            Array.Resize(ref dealerhand, dealerhand.Length + 1);
            dealerhand[dealerhand.Length - 1] = sign[randomSign] + cards[randomCard];
            dealerscore = CardScore(randomCard, dealerscore);
            checkAce(dealerscore, randomCard, dealerhand);
        }
        else
        {
            Array.Resize(ref playerhand, playerhand.Length + 1);
            playerhand[playerhand.Length - 1] = sign[randomSign] + cards[randomCard];
            playerscore = CardScore(randomCard, playerscore);
            checkAce(playerscore, randomCard, playerhand);
        }

        return sign[randomSign] + cards[randomCard];
    }

    static int CardScore(int tabcolumn, int actualscore)
    {
        int score = actualscore;
        if (tabcolumn == 0)
        {
            if (actualscore >= 11)
            {
                score++;
            }
            else
            {
                score += 11;
            }
        }
        else if (tabcolumn >= 1 && tabcolumn <= 9)
        {
            score = score + tabcolumn + 1;
        }
        else
        {
            score += 10;
        }
        return score;
    }

    static void checkAce(int score, int newcard, string[] hand)
    {
        if (score > 21)
        {
            if (hand[0] == "HA" || hand[0] == "DA" || hand[0] == "SA" || hand[0] == "CA")
            {
                score -= 10;
                hand[0] = "1";
            }
            else if (hand[1] == "HA" || hand[1] == "DA" || hand[1] == "SA" || hand[1] == "CA")
            {
                score -= 10;
                hand[1] = "1";
            }
            else if (hand[hand.Length -1] == "HA" || hand[hand.Length - 1] == "DA" || hand[hand.Length - 1] == "SA" || hand[hand.Length - 1] == "CA")
            {
                score -= 10;
                hand[hand.Length - 1] = "1";
            }
        }
    }

    static void Check(int pscore, int dscore)
    {
        if (pscore > 21)
        {
            Console.WriteLine("Bust, dealer win !");
        }
        else if (dscore > 21)
        {
            Console.WriteLine("Bust, you win !");
        }
        else
        {
            if (pscore == 21)
            {
                Console.WriteLine("====== BLACKJACK !!! ======");
                Console.WriteLine("You win !");
                Environment.Exit(0);
            }
            else if (dscore == 21)
            {
                Console.WriteLine("====== BLACKJACK !!! ======");
                Console.WriteLine("Dealer win !");
                Environment.Exit(0);
            }
            else if (command == "n")
            {
                if (pscore > dscore)
                {
                    Console.WriteLine("You win !");
                }
                else if (pscore == dscore)
                {
                    Console.WriteLine("Push !");
                }
                else
                {
                    Console.WriteLine("Dealer win !");
                }
            }
        }
    }
}