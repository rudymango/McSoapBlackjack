using System.Collections.ObjectModel;

namespace prototype.Classes
{
    internal class Player
    {
        int Money { get; set; }
        public ObservableCollection<Card> Hand { get; set; }
        public int HandValue 
        { 
            get
            {
                return ValueOfHand(this.Hand);
            }
            private set
            {

            }
        }

        public Player()
        {
            Money = 1000;
            Hand = new ObservableCollection<Card>();
            HandValue = ValueOfHand(Hand);
        }

        private int ValueOfHand(ObservableCollection<Card> hand)
        {
            int sum = 0;
            int amountOfAces = 0;

            if (hand.Count > 0)
            {
                foreach (Card card in hand)
                {
                    if (card.IsHidden)
                        break;
                    else if (card.Rank != Card.Ranks.Ace)
                        sum += card.Value;
                    else
                        amountOfAces += 1;
                }

                for (int i = 0; i < amountOfAces; i++)
                {
                    if (sum + 11 <= 21)
                        sum += 11;
                    else
                        sum += 1;
                }

                return sum;
            }

            return 0;
        }
    }
}
