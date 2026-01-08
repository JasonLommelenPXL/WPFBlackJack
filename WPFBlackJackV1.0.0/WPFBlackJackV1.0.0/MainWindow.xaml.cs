using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WPFBlackJackV1._0._0
{
    public partial class MainWindow : Window
    {
        private Random _random = new Random();

        private int _credits = 500;
        private int _currentBet = 0;

        private int _deckIndex = 0;

        // Template deck (52 kaarten)
        private Card[] _deckTemplate = new Card[]
        {
            // CLUBS
            new Card { ImageUrl="images/cards/clubs_A.png", Value=new[] {1,11}},
            new Card { ImageUrl="images/cards/clubs_2.png", Value=new[] {2}},
            new Card { ImageUrl="images/cards/clubs_3.png", Value=new[] {3}},
            new Card { ImageUrl="images/cards/clubs_4.png", Value=new[] {4}},
            new Card { ImageUrl="images/cards/clubs_5.png", Value=new[] {5}},
            new Card { ImageUrl="images/cards/clubs_6.png", Value=new[] {6}},
            new Card { ImageUrl="images/cards/clubs_7.png", Value=new[] {7}},
            new Card { ImageUrl="images/cards/clubs_8.png", Value=new[] {8}},
            new Card { ImageUrl="images/cards/clubs_9.png", Value=new[] {9}},
            new Card { ImageUrl="images/cards/clubs_10.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/clubs_J.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/clubs_Q.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/clubs_K.png", Value=new[] {10}},

            // DIAMONDS
            new Card { ImageUrl="images/cards/diamonds_A.png", Value=new[] {1,11}},
            new Card { ImageUrl="images/cards/diamonds_2.png", Value=new[] {2}},
            new Card { ImageUrl="images/cards/diamonds_3.png", Value=new[] {3}},
            new Card { ImageUrl="images/cards/diamonds_4.png", Value=new[] {4}},
            new Card { ImageUrl="images/cards/diamonds_5.png", Value=new[] {5}},
            new Card { ImageUrl="images/cards/diamonds_6.png", Value=new[] {6}},
            new Card { ImageUrl="images/cards/diamonds_7.png", Value=new[] {7}},
            new Card { ImageUrl="images/cards/diamonds_8.png", Value=new[] {8}},
            new Card { ImageUrl="images/cards/diamonds_9.png", Value=new[] {9}},
            new Card { ImageUrl="images/cards/diamonds_10.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/diamonds_J.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/diamonds_Q.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/diamonds_K.png", Value=new[] {10}},

            // HEARTS
            new Card { ImageUrl="images/cards/hearts_A.png", Value=new[] {1,11}},
            new Card { ImageUrl="images/cards/hearts_2.png", Value=new[] {2}},
            new Card { ImageUrl="images/cards/hearts_3.png", Value=new[] {3}},
            new Card { ImageUrl="images/cards/hearts_4.png", Value=new[] {4}},
            new Card { ImageUrl="images/cards/hearts_5.png", Value=new[] {5}},
            new Card { ImageUrl="images/cards/hearts_6.png", Value=new[] {6}},
            new Card { ImageUrl="images/cards/hearts_7.png", Value=new[] {7}},
            new Card { ImageUrl="images/cards/hearts_8.png", Value=new[] {8}},
            new Card { ImageUrl="images/cards/hearts_9.png", Value=new[] {9}},
            new Card { ImageUrl="images/cards/hearts_10.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/hearts_J.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/hearts_Q.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/hearts_K.png", Value=new[] {10}},

            // SPADES
            new Card { ImageUrl="images/cards/spades_A.png", Value=new[] {1,11}},
            new Card { ImageUrl="images/cards/spades_2.png", Value=new[] {2}},
            new Card { ImageUrl="images/cards/spades_3.png", Value=new[] {3}},
            new Card { ImageUrl="images/cards/spades_4.png", Value=new[] {4}},
            new Card { ImageUrl="images/cards/spades_5.png", Value=new[] {5}},
            new Card { ImageUrl="images/cards/spades_6.png", Value=new[] {6}},
            new Card { ImageUrl="images/cards/spades_7.png", Value=new[] {7}},
            new Card { ImageUrl="images/cards/spades_8.png", Value=new[] {8}},
            new Card { ImageUrl="images/cards/spades_9.png", Value=new[] {9}},
            new Card { ImageUrl="images/cards/spades_10.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/spades_J.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/spades_Q.png", Value=new[] {10}},
            new Card { ImageUrl="images/cards/spades_K.png", Value=new[] {10}},
        };

        // Werk-deck voor de ronde (geshuffeld)
        private Card[] _deck;

        public MainWindow()
        {
            InitializeComponent();

            _deck = new Card[_deckTemplate.Length];
            CopyTemplateToDeck();
            ShuffleDeck();

            creditsTextBlock.Text = _credits.ToString();
            remainingCardsTextBlock.Text = "52";

            actionsGrid.Visibility = Visibility.Collapsed;
            playerBetPanel.Visibility = Visibility.Collapsed;
            resultGrid.Visibility = Visibility.Collapsed;
        }

        private void AddImageToStackPanel(StackPanel panel, Card card, bool isVisible)
        {
            card.IsVisible = isVisible;

            Image image = new Image();
            image.Width = 120;
            image.Height = 170;
            image.Stretch = Stretch.Uniform;
            image.Margin = new Thickness(5, 0, 5, 0);

            image.Tag = card;

            // Toon front of back afhankelijk van IsVisible
            string path;
            if (isVisible)
                path = card.ImageUrl;
            else
                path = "images/cards/back.png";

            image.Source = new BitmapImage(new Uri(path, UriKind.Relative));

            panel.Children.Add(image);
        }

        private void StartNewGame(object sender, RoutedEventArgs e)
        {
            StartNextRoundCore();
        }

        private void StartNextRound(object sender, RoutedEventArgs e)
        {
            StartNextRoundCore();
        }

        private void StartNextRoundCore()
        {
            resultGrid.Visibility = Visibility.Collapsed;
            actionsGrid.Visibility = Visibility.Collapsed;

            playerBetPanel.Visibility = Visibility.Visible;
            betTextBox.Text = "";

            playerStack.Children.Clear();
            bankStack.Children.Clear();

            CopyTemplateToDeck();
            ShuffleDeck();
            _deckIndex = 0;
            _currentBet = 0;

            // 2 kaarten speler (1 open, 1 dicht)
            DealCardTo(playerStack, true);
            DealCardTo(playerStack, false);

            // 2 kaarten bank (1 open, 1 dicht)
            DealCardTo(bankStack, true);
            DealCardTo(bankStack, false);

            RefreshUI();
        }

        private void playGame(object sender, RoutedEventArgs e)
        {
            creditsTextBlock.Text = _credits.ToString();

            int playerBet;
            int.TryParse(betTextBox.Text, out playerBet);

            if (playerBet > _credits)
            {
                MessageBox.Show("Not possible, your bet is higher than your value.");
                return;
            }

            _currentBet = playerBet;
            _credits -= _currentBet;
            creditsTextBlock.Text = _credits.ToString();

            // Na bet: tweede kaarten worden zichtbaar
            RevealAllCards(playerStack);
            RevealAllCards(bankStack);

            playerBetPanel.Visibility = Visibility.Collapsed;
            actionsGrid.Visibility = Visibility.Visible;

            RefreshUI();

            // Als speler meteen blackjack/21 of bust (kan door azen promotie) -> stop meteen
            int playerTotal = CountValueFromPanel(playerStack);
            if (playerTotal >= 21)
            {
                FinishRoundAfterPlayerTurn();
            }
        }

        private void SelectedCardButton(object sender, RoutedEventArgs e)
        {
            DealCardTo(playerStack, true);
            RefreshUI();

            // 7-card rule
            int visiblePlayerCards = CountVisibleCards(playerStack);
            int playerTotal = CountValueFromPanel(playerStack);
            if (visiblePlayerCards >= 7 && playerTotal < 21)
            {
                EndRound(true, "You win!!! (7 cards)");
                return;
            }

            // Bust or 21 ends player turn
            if (playerTotal >= 21)
            {
                FinishRoundAfterPlayerTurn();
            }
        }

        private void SelectedStopButton(object sender, RoutedEventArgs e)
        {
            FinishRoundAfterPlayerTurn();
        }

        private void FinishRoundAfterPlayerTurn()
        {
            actionsGrid.Visibility = Visibility.Collapsed;

            int playerTotal = CountValueFromPanel(playerStack);
            if (playerTotal > 21)
            {
                EndRound(false, "You lost!!!");
                return;
            }

            // Bank turn: draw on 16 or lower, stop on 17+
            RevealAllCards(bankStack);
            RefreshUI();

            int bankTotal = CountValueFromPanel(bankStack);
            while (bankTotal <= 16)
            {
                DealCardTo(bankStack, true);
                bankTotal = CountValueFromPanel(bankStack);
                RefreshUI();

                if (bankTotal > 21)
                    break;
            }

            // Decide
            bankTotal = CountValueFromPanel(bankStack);
            playerTotal = CountValueFromPanel(playerStack);

            if (bankTotal > 21)
            {
                EndRound(true, "You win!!! (bank bust)");
                resultGrid.Background = Brushes.Green;
                return;
            }

            if (playerTotal > bankTotal)
            {
                EndRound(true, "You win!!!");
                resultGrid.Background = Brushes.Green;
                return;
            }

            if (playerTotal == bankTotal)
            {
                EndRound(null, "Push!");
                resultGrid.Background = Brushes.Gray;
                return;
            }

            EndRound(false, "You lost!!!");
            resultGrid.Background = Brushes.Red;
        }

        private void EndRound(bool? playerWon, string message)
        {
            // Credits afhandelen (bet is al afgetrokken bij Play)
            if (playerWon == true)
            {
                _credits += (_currentBet * 2);
            }
            else if (playerWon == null)
            {
                _credits += _currentBet;
            }

            creditsTextBlock.Text = _credits.ToString();

            resultTextBlock.Text = message;
            resultGrid.Visibility = Visibility.Visible;
        }

        private void SelectedEndButton(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SelectedNextButton(object sender, RoutedEventArgs e)
        {
            StartNextRoundCore();
        }

        private void DealCardTo(StackPanel panel, bool isVisible)
        {
            if (_deckIndex >= _deck.Length)
                return;

            Card card = _deck[_deckIndex];
            _deckIndex++;

            AddImageToStackPanel(panel, card, isVisible);
            remainingCardsTextBlock.Text = (_deck.Length - _deckIndex).ToString();
        }

        private int CountVisibleCards(StackPanel panel)
        {
            int count = 0;

            for (int i = 0; i < panel.Children.Count; i++)
            {
                Image img = (Image)panel.Children[i];
                Card card = (Card)img.Tag;

                if (card.IsVisible)
                    count++;
            }

            return count;
        }

        private int CountValueFromPanel(StackPanel panel)
        {
            int total = 0;
            int aces = 0;

            for (int i = 0; i < panel.Children.Count; i++)
            {
                Image img = (Image)panel.Children[i];
                Card card = (Card)img.Tag;

                if (card.IsVisible == false)
                    continue;

                if (card.Value.Length == 2) // Ace {1,11}
                {
                    total += 1;
                    aces++;
                }
                else
                {
                    total += card.Value[0];
                }
            }

            while (aces > 0 && total + 10 <= 21)
            {
                total += 10;
                aces--;
            }

            return total;
        }

        private void RevealAllCards(StackPanel panel)
        {
            for (int i = 0; i < panel.Children.Count; i++)
            {
                Image img = (Image)panel.Children[i];
                Card card = (Card)img.Tag;
                card.IsVisible = true;
            }
        }

        private void RefreshImagesInPanel(StackPanel panel)
        {
            for (int i = 0; i < panel.Children.Count; i++)
            {
                Image img = (Image)panel.Children[i];
                Card card = (Card)img.Tag;

                string path;
                if (card.IsVisible)
                    path = card.ImageUrl;
                else
                    path = "images/cards/back.png";

                img.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            }
        }

        private void CopyTemplateToDeck()
        {
            for (int i = 0; i < _deckTemplate.Length; i++)
            {
                // Nieuwe Card objecten zodat IsVisible niet “blijft hangen” tussen rondes
                Card c = new Card();
                c.ImageUrl = _deckTemplate[i].ImageUrl;
                c.Value = _deckTemplate[i].Value;
                c.IsVisible = false;

                _deck[i] = c;
            }
        }

        private void ShuffleDeck()
        {
            for (int i = _deck.Length - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);

                Card temp = _deck[i];
                _deck[i] = _deck[j];
                _deck[j] = temp;
            }
        }

        private void RefreshUI()
        {
            playerPointsTextBlock.Text = CountValueFromPanel(playerStack).ToString();
            bankPointsTextBlock.Text = CountValueFromPanel(bankStack).ToString();
            remainingCardsTextBlock.Text = (_deck.Length - _deckIndex).ToString();

            // Update images (front/back) volgens Card.IsVisible
            RefreshImagesInPanel(playerStack);
            RefreshImagesInPanel(bankStack);
        }

        private void RefreshUI(object sender, RoutedEventArgs e)
        {

        }
    }
}
