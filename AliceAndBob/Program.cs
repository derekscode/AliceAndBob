using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AliceAndBob
{
    class Program
    {
        public static List<char> aliceChar = new List<char>();
        public static List<char> bobChar = new List<char>();

        public static List<int> AliceList = new List<int>();
        public static List<int> BobList = new List<int>();

        public static int BobFinalCard = 0;
        public static int AliceFinalCard = 0;


        static void Main(string[] args)
        {
            PopulateLists();

            AliceList = ConvertCharToInt(aliceChar);
            BobList = ConvertCharToInt(bobChar);


            while (AliceList.Count > 0 && BobList.Count > 0)
            {
                Compare();
            }

            DisplayAnswers();


        }

        private static void PopulateLists()
        {
            //alice 
            char[] alice = null;
            using (StreamReader reader = new StreamReader("Alice.txt"))
            {
                alice = reader.ReadToEnd().ToCharArray();
                reader.Close();
            }
            foreach (var card in alice)
            {
                if (card.Equals('\n'))
                {
                    break;
                }
                aliceChar.Add(card);
            }

            //bob
            char[] bob = null;
            using (StreamReader reader = new StreamReader("Bob.txt"))
            {
                bob = reader.ReadToEnd().ToCharArray();
                reader.Close();
            }
            foreach (var card in bob)
            {
                if (card.Equals('\n'))
                {
                    break;
                }
                bobChar.Add(card);
            }

        }

        static List<int> ConvertCharToInt(List<char> charList)
        {
            var ConvertedToInts = new List<int>();
            foreach (var item in charList)
            {
                switch (item)
                {
                    case ('K'):
                        ConvertedToInts.Add(13);
                        break;
                    case ('Q'):
                        ConvertedToInts.Add(12);
                        break;
                    case ('J'):
                        ConvertedToInts.Add(11);
                        break;
                    case ('T'):
                        ConvertedToInts.Add(10);
                        break;
                    case ('A'):
                        ConvertedToInts.Add(1);
                        break;
                    case ('\n'):
                        break;
                    default:
                        ConvertedToInts.Add((int)Char.GetNumericValue(item));
                        break;
                }

            }

            return ConvertedToInts;

        }

        private static void Compare()
        {
            int aliceCard = AliceList.First();
            AliceList.RemoveAt(0);

            int bobCard = BobList.First();
            BobList.RemoveAt(0);


            if (bobCard > aliceCard)
            {
                BobList.Add(bobCard);
                BobList.Add(aliceCard);
            }
            else if (aliceCard > bobCard)
            {
                AliceList.Add(aliceCard);
                AliceList.Add(bobCard);
            }
            else
            {
                //tie, so discard both cards 
            }
            if (AliceList.Count == 1 || BobList.Count == 1)
            {
                AliceFinalCard = AliceList.First();
                BobFinalCard = BobList.First();

            }


        }

        private static void DisplayAnswers()
        {
            var winner = "";
            var winnerList = new List<int>();
            int winnerDeckSize = 0;
            string lastTwoCardsPlayed = "";


            if (AliceList.Count == 0)
            {
                winner = "Bob";
                winnerList = BobList;
            }
            else if (BobList.Count == 0)
            {
                winner = "Alice";
                winnerList = AliceList;
            }

            winnerDeckSize = winnerList.Count;

            if (winner == "Alice")
            {
                lastTwoCardsPlayed = AliceFinalCard.ToString() + BobFinalCard.ToString();
            }
            else
            {
                lastTwoCardsPlayed = BobFinalCard.ToString() + AliceFinalCard.ToString();
            }

            Console.WriteLine("Winner:" + winner);
            Console.WriteLine("WinnerDeckSize: " + winnerDeckSize);
            Console.WriteLine("LastTwoCardsPlayed: " + lastTwoCardsPlayed);
            Console.ReadLine();

        }



    }
}
