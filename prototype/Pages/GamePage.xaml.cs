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
			new Card(Card.Ranks.Three, Card.Suits.Spades),
			new Card(Card.Ranks.Four, Card.Suits.Hearts),
        };

        DealerHand.ItemsSource = hand;
		PlayerHand.ItemsSource = hand;
    }
}
