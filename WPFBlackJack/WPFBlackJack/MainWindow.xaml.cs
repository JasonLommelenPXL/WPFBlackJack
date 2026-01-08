using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPFBlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random _random = new Random(); // Inistaliseert de Random methode onder _random.

        private int _credits = 500;

        private List<Card> CreateCardList() // Initialiseren/Aanmaken van de deck. (Best voor dit steeds een list gebruiken wat handiger is dan een array die steeds opnieuw opgeroepen met worden).
        {
            return new List<Card> // Met return verbind je deze kaarten aan CreateCardList()
            {
                // CLUBS
                new Card { ImageUrl = "/images/cards/clubs_A.png", Value = new[] { 1, 11 } },
                new Card { ImageUrl = "/images/cards/clubs_2.png", Value = new[] { 2 } },
                new Card { ImageUrl = "/images/cards/clubs_3.png", Value = new[] { 3 } },
                new Card { ImageUrl = "/images/cards/clubs_4.png", Value = new[] { 4 } },
                new Card { ImageUrl = "/images/cards/clubs_5.png", Value = new[] { 5 } },
                new Card { ImageUrl = "/images/cards/clubs_6.png", Value = new[] { 6 } },
                new Card { ImageUrl = "/images/cards/clubs_7.png", Value = new[] { 7 } },
                new Card { ImageUrl = "/images/cards/clubs_8.png", Value = new[] { 8 } },
                new Card { ImageUrl = "/images/cards/clubs_9.png", Value = new[] { 9 } },
                new Card { ImageUrl = "/images/cards/clubs_10.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/clubs_J.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/clubs_Q.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/clubs_K.png", Value = new[] { 10 } },

                // DIAMONDS
                new Card { ImageUrl = "/images/cards/diamonds_A.png", Value = new[] { 1, 11 } },
                new Card { ImageUrl = "/images/cards/diamonds_2.png", Value = new[] { 2 } },
                new Card { ImageUrl = "/images/cards/diamonds_3.png", Value = new[] { 3 } },
                new Card { ImageUrl = "/images/cards/diamonds_4.png", Value = new[] { 4 } },
                new Card { ImageUrl = "/images/cards/diamonds_5.png", Value = new[] { 5 } },
                new Card { ImageUrl = "/images/cards/diamonds_6.png", Value = new[] { 6 } },
                new Card { ImageUrl = "/images/cards/diamonds_7.png", Value = new[] { 7 } },
                new Card { ImageUrl = "/images/cards/diamonds_8.png", Value = new[] { 8 } },
                new Card { ImageUrl = "/images/cards/diamonds_9.png", Value = new[] { 9 } },
                new Card { ImageUrl = "/images/cards/diamonds_10.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/diamonds_J.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/diamonds_Q.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/diamonds_K.png", Value = new[] { 10 } },

                // HEARTS
                new Card { ImageUrl = "/images/cards/hearts_A.png", Value = new[] { 1, 11 } },
                new Card { ImageUrl = "/images/cards/hearts_2.png", Value = new[] { 2 } },
                new Card { ImageUrl = "/images/cards/hearts_3.png", Value = new[] { 3 } },
                new Card { ImageUrl = "/images/cards/hearts_4.png", Value = new[] { 4 } },
                new Card { ImageUrl = "/images/cards/hearts_5.png", Value = new[] { 5 } },
                new Card { ImageUrl = "/images/cards/hearts_6.png", Value = new[] { 6 } },
                new Card { ImageUrl = "/images/cards/hearts_7.png", Value = new[] { 7 } },
                new Card { ImageUrl = "/images/cards/hearts_8.png", Value = new[] { 8 } },
                new Card { ImageUrl = "/images/cards/hearts_9.png", Value = new[] { 9 } },
                new Card { ImageUrl = "/images/cards/hearts_10.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/hearts_J.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/hearts_Q.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/hearts_K.png", Value = new[] { 10 } },

                // SPADES
                new Card { ImageUrl = "/images/cards/spades_A.png", Value = new[] { 1, 11 } },
                new Card { ImageUrl = "/images/cards/spades_2.png", Value = new[] { 2 } },
                new Card { ImageUrl = "/images/cards/spades_3.png", Value = new[] { 3 } },
                new Card { ImageUrl = "/images/cards/spades_4.png", Value = new[] { 4 } },
                new Card { ImageUrl = "/images/cards/spades_5.png", Value = new[] { 5 } },
                new Card { ImageUrl = "/images/cards/spades_6.png", Value = new[] { 6 } },
                new Card { ImageUrl = "/images/cards/spades_7.png", Value = new[] { 7 } },
                new Card { ImageUrl = "/images/cards/spades_8.png", Value = new[] { 8 } },
                new Card { ImageUrl = "/images/cards/spades_9.png", Value = new[] { 9 } },
                new Card { ImageUrl = "/images/cards/spades_10.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/spades_J.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/spades_Q.png", Value = new[] { 10 } },
                new Card { ImageUrl = "/images/cards/spades_K.png", Value = new[] { 10 } },
            };
        }

        public MainWindow()
        {
            InitializeComponent();


        }

        /// <summary>
        /// Adds an image control to a panel which displays the given card
        /// </summary>
        /// <param name="panel">The control to which the image must be added</param>
        /// <param name="card">The card that should be displayed in the image</param>
        /// <param name="isVisible">A boolean that indicates if the card should be open or not</param>
        private void AddImageToStackPanel(StackPanel panel, Card card, bool isVisible)
        {
            card.IsVisible = isVisible;

            //Maak een nieuwe Image control
            Image image = new Image();
            image.Width = 120;
            image.Height = 170;
            image.Stretch = Stretch.Uniform;
            image.Margin = new Thickness(5, 0, 5, 0);
            //Bewaar het volledige Card-object in de Tag-property van de Image control
            image.Tag = card;
            image.Source = new BitmapImage(new Uri(card.ImageUrl, UriKind.Relative));

            //Voeg de Image control toe aan het StackPanel
            panel.Children.Add(image);
        }

        private void StartNewGame(object sender, RoutedEventArgs e)
        {
            // List<Card> _deck = CreateCardList(); // Nieuwe deck met 52 kaarten die opgehaald wordt uit CreateCardList methode (AANGEROEPEN) Wordt voorlopig niet gebruikt.

            List<Card> playerCards = DrawRandomTwoCards(_random); // Nieuwe playercards (2 kaarten die gemaakt zijn in de drawrandom methode) ophalen uit de DrawRandomTwoCard methode (die parameter _random krijgt en een random geeft heeft)
            List<Card> bankCards = DrawRandomTwoCards(_random); // Nieuw bankcards (2 kaarten die gemaakt zijn in de drawrandom methode) ophalen uit de DrawRandomTwoCard methode (die parameter _random krijgt en een random geeft heeft)

            playerStack.Children.Clear(); // Maak indien er nog een stack aanwezig is leeg bij de speler
            bankStack.Children.Clear(); // Maak indien er nog een stack aanwezig is leeg bij de bank

            int i = 0; // geeft i waarde standaard 0
            foreach (Card card in playerCards) // voor elke kaart object in playercards (1 kaart + 1 kaart)
            {
                string path; // pad
                bool isVisible = (i == 0); // 1e open, 2e dicht
                if (isVisible)  // als visible
                {
                    AddImageToStackPanel(playerStack, card, true); // Toon de playerstack kaart waar.
                }
                else
                {
                    Card hiddenCard = new Card(); // toon (verborgen kaart)
                    hiddenCard.ImageUrl = "images/cards/back.png"; // door de url back.png te tonen.
                    hiddenCard.IsVisible = false;

                    AddImageToStackPanel(playerStack, hiddenCard, false);
                }
                i++;
            }

            i = 0; // idem
            foreach (Card card in bankCards) // idem 
            {
                bool isVisible = (i == 0); // 1e open, 2e dicht
                if (isVisible)
                {
                    AddImageToStackPanel(bankStack, card, true);
                } 
                else
                {
                    Card hiddenCard = new Card();
                    hiddenCard.ImageUrl = "images/cards/back.png";
                    hiddenCard.IsVisible = false;

                    AddImageToStackPanel(bankStack, hiddenCard, false);
                }
                i++;
            }

            actionsGrid.Visibility = Visibility.Visible;
            playerBetPanel.Visibility = Visibility.Visible;
            

        }

        private List<Card> DrawRandomTwoCards(Random random)
        {
            // List<Card> _deck = CreateCardList(); // nieuw deck met 52 kaarten aangemaakt door CreateCardList() aan te roepen.

            // if (_deck.Count < 2) return null;  // Controleer of er minstens twee kaarten zijn anders kan hij sowieso geen 2 kaarten trekken.

            // for (int i = 0; i < 2; i++) // TOT 2x MAX aanroepen omdat we 2 kaarten maar gaan moeten oproepen. (het aantal keren dat het dus moet gebeuren.
            // {
            //     int index = random.Next(_deck.Count); // getal index = random.volgende(_deck.tel); = 0 TOT 52 - 1 (omdat we rekenen vanaf 0 en niet 1)
            //     Card card = _deck[index]; // Geef mij een kaart van de klasse kaarten = _deck[één kaart tussen de 0-51)
            //     _deck.RemoveAt(index); // Verwijder een kaart tussen 0-51 van de _deck (52 kaarten)
            //     _deck.Add(card); // Voeg kaart toe aan "deck" terug
            // }
            // return _deck; // return de deck.

            //// FOUT WANT NU RETURNEN WIJ 104 KAARTEN

            List<Card> _deck = CreateCardList(); // Deck wordt opgehaald en aangemaakt met de 52 kaarten door de CreateCardList() methode aan te roepen.
            if (_deck.Count < 2) return null; // Controle: MINSTENS 2 KAARTEN

            List<Card> DrawRandomCard = new List<Card>(); // Maak van de kaarten een nieuwe lijst genaamd DrawRandomCard = NEW LIST is lege lijst want er is standaard nog niets.

            for (int i = 0; i < 2; i++) // 2X UITVOEREN (1 kaart + 1 kaart)
            {
                int index = random.Next(_deck.Count); // index is een random getal die we uit de deck nemen. COUNT is enorm belangrijk om een getal tussen de 0-51 te geven anders kan hij 53 geven wat niet bestaat en geeft hij een foutmelding.
                Card card = _deck[index]; // Een kaart uit de kaart klasse = _deck[de willekeurige kaarten]
                _deck.RemoveAt(index); // Verwijder de getrokken kaart met waarde index uit deck
                DrawRandomCard.Add(card); // DrawRandomCard lijst wordt een kaart aan toegevoegd.
            }
            return DrawRandomCard; // Return 2x DrawRandomCard

            // Andere manier:
            //
            // List<Card> _deck = CreateCardList();
            //
            // While (_deck < 2) 
            // {
            //
            // }
        }

        private void RefreshUI(object sender, RoutedEventArgs e)
        {
            //CLEAR GEBRUIKEN
        }

        private void SelectedCardButton(object sender, RoutedEventArgs e)
        {
            // WANNEER SELECT CARD KNOP GEDRUK DAN GEEF KAART
        }

        private void SelectedStopButton(object sender, RoutedEventArgs e)
        {
            // WANNEER SELECT STOP KNOP GEDRUKT DAN STOP EN TOON WINNAAR 
        }

        private void CountValueFromStack()
        {
            List<Card> _deck = CreateCardList();

            // Als speler of bank nieuwe kaart raapt met de card button (x:Name cardButton) dan doe card - 1 en toon dit op TextBlock (x:Name: remainingCardsTextBlock)
            int playerTotal = 0; // Spelers totaal
            int playerAces = 0; // Aantal aces van de speler

            for (int i = 0; i < playerStack.Children.Count; i++) // Voor (een geheel getal i = 0; zolang i kleiner is dan het aantal children in playerStack; verhoog i telkens met 1)
            {
                Image image = (Image)playerStack.Children[i]; // Kijkt naar het aantal images in de player stack.
                Card card = (Card)image.Tag; // Kijkt naar het aantal kaarten 

                if (card.IsVisible == false) continue; // kijkt of de card de waarde visible heeft want standaard in de lijst zo is.

                if (card.Value.Length == 2) // de kaart heeft 2 mogelijke waarden
                {
                    playerTotal += 1; // playertotaal + 1 = 1
                    playerAces++; // 
                }
                else
                {
                    playerTotal += card.Value[0];
                }
                while (playerAces > 0 && playerTotal + 10 <= 21)
                {
                    playerTotal += 10;
                    playerAces--;
                }
                bankPointsTextBlock.Text = playerTotal.ToString();
            }

            int bankTotal = 0; // Bank totaal
            int bankAces = 0; // Aantal aces van de bank

            for (int i = 0; i < bankStack.Children.Count; i++) // Voor (een geheel getal i = 0; zolang i kleiner is dan het aantal children in bankStack; verhoog i telkens met 1)
            {
                Image image = (Image)bankStack.Children[i];
                Card card = (Card)image.Tag;

                if (card.IsVisible == false) continue;

                if (card.Value.Length == 2) // Aces telt voor 2
                {
                    bankTotal += 1;
                    bankAces++;
                }
                else
                {
                    bankTotal += card.Value[0];
                }
                while (bankAces > 0 && bankTotal + 10 <= 21)
                {
                    bankTotal += 10;
                    bankAces--;
                }
                bankPointsTextBlock.Text = bankTotal.ToString();
            }
        }

        private void SelectedEndButton(object sender, RoutedEventArgs e)
        {

        }

        private void SelectedNextButton(object sender, RoutedEventArgs e)
        {

        }

        private void playGame(object sender, RoutedEventArgs e)
        {
            creditsTextBlock.Text = _credits.ToString(); // textblock 500 wordt credits 500 waarde aan gegeven.

            int.TryParse(betTextBox.Text?.ToString(), out int playerBet); // converteert bettextbox text naar een int. 
            if (playerBet > _credits) // als bet groter is dan credits
            {
                MessageBox.Show("Not possible, your bet is higher than your value."); // niet mogelijk, uw bet is hoger dan uw waarde
                return;
            }

            _credits -= playerBet; // indien wel mogelijk doe credits text box - bet
            creditsTextBlock.Text = _credits.ToString(); // nieuwe waarde.


        }
    }
}