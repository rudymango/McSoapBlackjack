using System.Collections.ObjectModel;
using System.Diagnostics;
using prototype.Classes;

namespace prototype.Pages;

public partial class GamePage : ContentPage
{
    Stack<Card> Deck;
    Player PersonPlayer;
	Player DealerPlayer;

	public GamePage()
	{
		InitializeComponent();
        Deck = new Stack<Card>();
        PersonPlayer = new Player();
		DealerPlayer = new Player();
    }

    private void BeginGame(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        for (int i = 1; i <= 13; i++)
        {
            for (int j = 1; j <= 4; j++)
            {
                Card tmp = new Card((Card.Ranks)i, (Card.Suits)j);
                Deck.Push(tmp);
            }
        }
        PersonPlayer.Hand.Add(Deck.Pop());
        PersonPlayer.Hand.Add(Deck.Pop());
        DealerPlayer.Hand.Add(Deck.Pop());
        DealerPlayer.Hand.Add(Deck.Pop());
        GameViewPlayersHand.ItemsSource = PersonPlayer.Hand;
        GameViewDealersHand.ItemsSource = DealerPlayer.Hand;
        ValueOfDealersHand.Text = PersonPlayer.HandValue.ToString();
        ValueOfPlayersHand.Text = PersonPlayer.HandValue.ToString();
		GameInformationLabel.Text = "Game Information";
		button.IsEnabled = false;
    }

    private void DetermineWinCondition()
    {
        if (PersonPlayer.HandValue > DealerPlayer.HandValue)
            GameInformationLabel.Text = "You Win!";
        else if (PersonPlayer.HandValue < DealerPlayer.HandValue)
            GameInformationLabel.Text = "You Lose.";
        else
            GameInformationLabel.Text = "Draw!";
    }

    private void PlayerStands(object sender, EventArgs e)
    {
        DetermineWinCondition();
        // ...
        // clear hands,
        // reshuffle,
    }

    private void PlayerHits(object sender, EventArgs e)
    {
        PersonPlayer.Hand.Add(Deck.Pop());
        ValueOfPlayersHand.Text = PersonPlayer.HandValue.ToString();
    }
}
