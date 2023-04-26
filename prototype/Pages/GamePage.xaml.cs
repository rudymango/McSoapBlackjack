using System.Collections.ObjectModel;
using prototype.Classes;

namespace prototype.Pages;

public partial class GamePage : ContentPage
{
	// Stack<Card> Deck;
	ObservableCollection<Card> PlayersHand;
	ObservableCollection<Card> DealersHand;

	public GamePage()
	{
		InitializeComponent();

		PlayersHand = new ObservableCollection<Card>();
		DealersHand = new ObservableCollection<Card>();

        GameViewDealersHand.ItemsSource = PlayersHand;
		GameViewPlayersHand.ItemsSource = DealersHand;

		ValueOfDealersHand.Text = "Dealer's Hand: " + ValueOfHand(PlayersHand).ToString();
		ValueOfPlayersHand.Text = "Player's Hand: " + ValueOfHand(DealersHand).ToString();
    }
	private int ValueOfHand(ObservableCollection<Card> hand)
	{
		int sum = 0;
		int amountOfAces = 0;

		foreach (Card card in hand)
		{
			if (card.Rank != Card.Ranks.Ace)
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
    private void BeginGame(object sender, EventArgs e)
    {
        Stack<Card> Deck = new Stack<Card>();

        for (int i = 1; i <= 13; i++)
        {
            for (int j = 1; j <= 4; j++)
            {
                Card tmp = new Card((Card.Ranks)i, (Card.Suits)j);
                Deck.Push(tmp);
            }
        }

        PlayersHand.Add(Deck.Pop());
		PlayersHand.Add(Deck.Pop());
		DealersHand.Add(Deck.Pop());
        DealersHand.Add(Deck.Pop());
    }
}
