using System.Collections.ObjectModel;
using prototype.Classes;

namespace prototype.Pages;

public partial class GamePage : ContentPage
{
	ObservableCollection<Card> card;
	public GamePage()
	{
		InitializeComponent();
		card = new ObservableCollection<Card>()
		{
			new Card{ID = "10_of_Spades", Image = "https://publicdomainvectors.org/photos/nicubunu_Ornamental_deck_10_of_spades.png"},
            new Card{ID = "10_of_Spades", Image = "https://publicdomainvectors.org/photos/nicubunu_Ornamental_deck_10_of_spades.png"},
        };

        DealerHand.ItemsSource = card;
		PlayerHand.ItemsSource = card;
    }
}
