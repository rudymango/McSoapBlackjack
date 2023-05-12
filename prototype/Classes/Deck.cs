namespace prototype.Classes
{
    public class Deck
    {
        public Stack<Card> Cards { get; set; }
        public Deck()
        {
            Cards = new Stack<Card>();
            for (int i = 1; i <= 13; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    Card tmp = new Card((Card.Ranks)i, (Card.Suits)j);
                    Cards.Push(tmp);
                }
            }
            List<Card> temporaryList = new List<Card>();
            temporaryList = Cards.Shuffle();
            Cards = temporaryList.Shuffle();
        }
    }

    public static class DeckMethods
    {
        public static List<T> Shuffle<T>(this Stack<T> stack)
        {
            List<T> list = stack.ToList();
            return list;
        }

        public static Stack<T> Shuffle<T>(this List<T> list)
        {
            Random rnd = new Random();
            for (int i = 0; i < list.Count; i++)
            {
                int num = rnd.Next(list.Count);
                T temp = list[i];
                list[i] = list[num];
                list[num] = temp;
            }
            return list.ToStack();
        }

        public static Stack<T> ToStack<T>(this List<T> list)
        {
            Stack<T> stack = new Stack<T>();
            foreach (T t in list)
                stack.Push(t);
            return stack;
        }
    }
}
