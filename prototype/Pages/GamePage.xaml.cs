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
        PersonPlayer = new Player();
		DealerPlayer = new Player();
        YourMoney.Text = "Your Money: $" + PlayerMoney;
        SetDefaultButtons();
    }

    private void BeginGame(object sender, EventArgs e)
    {
        if(Bet <= 0)
        {
            GameInformationLabel.Text = "Insufficient Bet Amount, $" + Bet + " < $" + "0. ";
            return;
        }
        GameDeck = new Deck();
        SetGameButtons();
        PersonPlayer.Hand.Add(GameDeck.Cards.Pop());
        PersonPlayer.Hand.Add(GameDeck.Cards.Pop());
        DealerPlayer.Hand.Add(GameDeck.Cards.Pop());
        Card secondCardHidden = GameDeck.Cards.Pop();
        secondCardHidden.HideCard();
        DealerPlayer.Hand.Add(secondCardHidden);
        GameViewPlayersHand.ItemsSource = PersonPlayer.Hand;
        GameViewDealersHand.ItemsSource = DealerPlayer.Hand;
        ValueOfPlayersHand.Text = PersonPlayer.HandValue.ToString();
        ValueOfDealersHand.Text = DealerPlayer.HandValue.ToString();
        if(PersonPlayer.HandValue >= 21 || DealerPlayer.HandValue >= 21)
            DetermineWinCondition();
    }

    private void DetermineWinCondition()
    {
        if(PersonPlayer.HandValue == 21 && DealerPlayer.HandValue == 21)
        {
            GameInformationLabel.Text = "Push!";
            PlayerMoney += Bet;
        }
        else if(PersonPlayer.HandValue > 21)
        {
            GameInformationLabel.Text = "Bust! -$" + Bet;
        }
        else if (DealerPlayer.HandValue > 21)
        {
            GameInformationLabel.Text = "Dealer Busts! +$" + Bet * 2;
            PlayerMoney += Bet * 2;
        }
        else if (PersonPlayer.HandValue > DealerPlayer.HandValue)
        {
            GameInformationLabel.Text = "You Win +$" + Bet * 2;
            PlayerMoney += Bet * 2;
        }
        else if (PersonPlayer.HandValue < DealerPlayer.HandValue)
        {
            GameInformationLabel.Text = "You Lose -$" + Bet;
        }
        else
        {
            GameInformationLabel.Text = "Push!";
            PlayerMoney += Bet;
        }
        ShowDealerCards();
        YourMoney.Text = "Your Money: $" + PlayerMoney;
        Bet = 0;
        SetDefaultButtons();
    }

    private void PlayerStands(object sender, EventArgs e)
    {
        ShowDealerCards();
        DealerDrawAlgorithm();
        DetermineWinCondition();
    }

    private void PlayerHits(object sender, EventArgs e)
    {
        PersonPlayer.Hand.Add(GameDeck.Cards.Pop());
        ValueOfPlayersHand.Text = PersonPlayer.HandValue.ToString();
        if (PersonPlayer.HandValue >= 21)
            DetermineWinCondition();
    }

    async void PlaceBet(object sender, EventArgs e)
    {
        Button button = (Button)sender;
        string result = await DisplayPromptAsync("Bet", "Place your bet.");
        int tmp = 0;
        int previousBet = Bet;
        try
        {
            tmp = Int16.Parse(result);
            if (tmp <= 0)
                throw new ArithmeticException("Insufficient Bet Amount, $" + tmp + " <= $" + "0. ");
            if (PlayerMoney + previousBet - tmp < 0)
                throw new ArithmeticException("Insufficient Funds, $" + tmp + " > $" + PlayerMoney + ". ");
        }
        catch (Exception ex)
        {
            GameInformationLabel.Text = ex.Message + " Pot: $" + previousBet;
            return;
        }
        PersonPlayer.Hand.Clear();
        DealerPlayer.Hand.Clear();
        GameViewPlayersHand.ItemsSource = PersonPlayer.Hand;
        GameViewDealersHand.ItemsSource = DealerPlayer.Hand;
        ValueOfPlayersHand.Text = "";
        ValueOfDealersHand.Text = "";
        Bet = tmp;
        PlayerMoney += previousBet;
        PlayerMoney -= Bet;
        GameInformationLabel.Text = "Pot: $" + Bet;
        YourMoney.Text = "Your Money: $" + PlayerMoney;
    }

    private void DealerDrawAlgorithm()
    {
        while(DealerPlayer.HandValue < 16)
        {
            DealerPlayer.Hand.Add(GameDeck.Cards.Pop());
            ValueOfDealersHand.Text = DealerPlayer.HandValue.ToString();
        }
    }

    private void SetDefaultButtons()
    {
        HitButton.IsEnabled = false;
        StandButton.IsEnabled = false;
        BeginGameButton.IsEnabled = true;
        PlaceBetButton.IsEnabled = true;
    }

    private void SetGameButtons()
    {
        HitButton.IsEnabled = true;
        StandButton.IsEnabled = true;
        BeginGameButton.IsEnabled = false;
        PlaceBetButton.IsEnabled = false;
    }

    private void ShowDealerCards()
    {
        Card tmp = DealerPlayer.Hand[1];
        tmp.ShowCard();
        DealerPlayer.Hand.RemoveAt(1);
        DealerPlayer.Hand.Add(tmp);
        GameViewDealersHand.ItemsSource = DealerPlayer.Hand;
        ValueOfDealersHand.Text = DealerPlayer.HandValue.ToString();
    }
}
