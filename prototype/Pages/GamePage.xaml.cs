using System.Collections.ObjectModel;
using System.Diagnostics;
using prototype.Classes;

namespace prototype.Pages;

public partial class GamePage : ContentPage
{
    Deck GameDeck;
    Player PersonPlayer;
	Player DealerPlayer;
    int Bet = 0;
    int PlayerMoney = 1000;
	public GamePage()
	{
		InitializeComponent();
        GameDeck = new Deck();
        PersonPlayer = new Player();
		DealerPlayer = new Player();
        YourMoney.Text = "Your Money: $" + PlayerMoney;
        StandButton.IsEnabled = false;
        HitButton.IsEnabled = false;
        BeginGameButton.IsEnabled = true;
        PlaceBetButton.IsEnabled = true;
    }

    private void BeginGame(object sender, EventArgs e)
    {
        if(Bet <= 0)
        {
            GameInformationLabel.Text = "Insufficient Bet Amount, $" + Bet + " < $" + "0. ";
            return;
        }
        BeginGameButton.IsEnabled = false;
        PlaceBetButton.IsEnabled = false;
        StandButton.IsEnabled = true;
        HitButton.IsEnabled = true;
        PersonPlayer.Hand.Clear();
        DealerPlayer.Hand.Clear();
        GameViewPlayersHand.ItemsSource = PersonPlayer.Hand;
        GameViewDealersHand.ItemsSource = DealerPlayer.Hand;
        ValueOfPlayersHand.Text = "";
        ValueOfDealersHand.Text = "";
        PersonPlayer.Hand.Add(GameDeck.Cards.Pop());
        PersonPlayer.Hand.Add(GameDeck.Cards.Pop());
        DealerPlayer.Hand.Add(GameDeck.Cards.Pop());
        DealerPlayer.Hand.Add(GameDeck.Cards.Pop());
        GameViewPlayersHand.ItemsSource = PersonPlayer.Hand;
        GameViewDealersHand.ItemsSource = DealerPlayer.Hand;
        ValueOfPlayersHand.Text = PersonPlayer.HandValue.ToString();
        ValueOfDealersHand.Text = DealerPlayer.HandValue.ToString();
    }

    private void DetermineWinCondition()
    {
        if (PersonPlayer.HandValue > DealerPlayer.HandValue)
        {
            GameInformationLabel.Text = "You Win! +$" + Bet * 2;
            PlayerMoney += Bet * 2;
        }
        else if (PersonPlayer.HandValue < DealerPlayer.HandValue)
        {
            GameInformationLabel.Text = "You Lose. -$" + Bet;
        }
        else
        {
            GameInformationLabel.Text = "Draw!";
            PlayerMoney += Bet;
        }
        YourMoney.Text = "Your Money: $" + PlayerMoney;
        Bet = 0;
        StandButton.IsEnabled = false;
        HitButton.IsEnabled = false;
        BeginGameButton.IsEnabled = true;
        PlaceBetButton.IsEnabled= true;
    }

    private void PlayerStands(object sender, EventArgs e)
    {
        DetermineWinCondition();
    }

    private void PlayerHits(object sender, EventArgs e)
    {
        PersonPlayer.Hand.Add(GameDeck.Cards.Pop());
        ValueOfPlayersHand.Text = PersonPlayer.HandValue.ToString();
    }

    async void PlaceBet(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string result = await DisplayPromptAsync("Bet", "Place your bet.");
        int tmp = 0;
        int previousBet = Bet;
        PersonPlayer.Hand.Clear();
        DealerPlayer.Hand.Clear();
        GameViewPlayersHand.ItemsSource = PersonPlayer.Hand;
        GameViewDealersHand.ItemsSource = DealerPlayer.Hand;
        try
        {
            tmp = Int16.Parse(result);
            if (tmp < 0)
                throw new ArithmeticException("Insufficient Bet Amount, $" + tmp + " < $" + "0. ");
            if (PlayerMoney + previousBet - tmp < 0)
                throw new ArithmeticException("Insufficient Funds, $" + tmp + " > $" + PlayerMoney + ". ");
        }
        catch (Exception ex)
        {
            GameInformationLabel.Text = ex.Message + " Pot: $" + previousBet;
            return;
        }
        Bet = tmp;
        PlayerMoney += previousBet;
        PlayerMoney -= Bet;
        GameInformationLabel.Text = "Pot: $" + Bet;
        YourMoney.Text = "Your Money: $" + PlayerMoney;
    }
}
