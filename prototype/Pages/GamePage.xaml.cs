using System.Collections.ObjectModel;
using prototype.Classes;

namespace prototype.Pages;

public partial class GamePage : ContentPage
{
	ObservableCollection<Card> hand;
	public GamePage()
	{
		InitializeComponent();
		hand = new ObservableCollection<Card>()
		{
            new Card(Card.Ranks.Ace, Card.Suits.Hearts),
            new Card(Card.Ranks.Three, Card.Suits.Spades),
			new Card(Card.Ranks.Four, Card.Suits.Hearts),
        };

        DealerHand.ItemsSource = hand;
		PlayerHand.ItemsSource = hand;

		ValueOfDealersHand.Text = "Dealer's Hand: " + ValueOfHand(hand).ToString();
		ValueOfPlayersHand.Text = "Player's Hand: " + ValueOfHand(hand).ToString();
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
}
