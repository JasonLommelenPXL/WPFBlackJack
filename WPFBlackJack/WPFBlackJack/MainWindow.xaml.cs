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

namespace WPFBlackJack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private void StartNewGame(object sender, RoutedEventArgs e)
        {
            // START GAME
            // 2 KAARTEN OPEN DICHT (VOOR BANK EN SPELER)
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
    }
}