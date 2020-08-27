using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Solitaire.Properties;

#nullable enable

namespace Solitaire
{
    public partial class FormGame : Form
    {
        public FormGame()
        {
            InitializeComponent();

            CreateMarkers();
        }

        private bool _deckCreated = false;
        private Card[] _deck;
        private CardPanel[] _cardPanels;

        #region User Interface

        private SizeF drawScale = new SizeF(1, 1);

        private Size CardSize
        {
            get
            {
                double cardSizeRatio = 65.0 / 100.0;  //width to height ratio for cards
                int desiredCardsH = 15;     //minimum number of cards horizontally
                int desiredCardsV = 6;      //minimum number of cards vertically
                int maxWidthPerCard = pnlPlayArea.Width / desiredCardsH;      //maximum width possible of cards
                int maxHeightPerCard = pnlPlayArea.Height / desiredCardsV;    //maximum height possible of cards
                
                if (maxWidthPerCard > maxHeightPerCard)
                {
                    //cards scale to height
                    return new Size((int)(maxHeightPerCard * cardSizeRatio), (int)maxHeightPerCard);
                }
                else
                {
                    //cards scale to width
                    return new Size((int)maxWidthPerCard, (int)(maxWidthPerCard / cardSizeRatio));
                }
            }
        }

        /// <summary>
        /// Returns an array of <see cref="Card"/> representing a 52 size deck of cards.
        /// </summary>
        /// <returns>52 length array of <see cref="Card"/></returns>
        private Card[] CreateDeck()
        {
            Card[] result = new Card[52];

            Card.Suites[] cardSuites =
                { Card.Suites.Clubs, Card.Suites.Diamonds, Card.Suites.Hearts, Card.Suites.Spades };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    int k = i * 13 + j - 1; //index of current card in array is calculated
                    result[k] = new Card(j, cardSuites[i]);
                }
            }

            return result;
        }

        private void CreateCardPanels()
        {
            _cardPanels = new CardPanel[52];

            for (int i = 0; i < 52; i++)
            {
                _cardPanels[i] = new CardPanel(ref _deck[i])
                {
                    Size = CardSize,
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                Controls.Add(_cardPanels[i]);
                pnlPlayArea.Controls.Add(_cardPanels[i]);
                _cardPanels[i].BringToFront();
                _cardPanels[i].UpdateLocation += MoveCardToLocation;
            }

            
        }
        
        private void SetupCards()
        {
            int i, j;

            //creates shuffle order
            int[] shuffleOrder = new int[52];
            bool[] input = new bool[52];

            Random r = new Random();

            for (i = 0; i < 52; i++)
            {
                int sanity = 60;
                j = r.Next(51);
                do
                {
                    if (!input[j])
                    {
                        shuffleOrder[i] = j;
                        input[j] = true;
                        break;
                    }

                    j += 1;
                    if (j > 51)
                    {
                        j = 0;
                    }   //loops back around to 0 when j gets too high

                    sanity -= 1;
                } while (sanity > 0);   //exits loop if infinite
            }

            int k = 0;
            //7 columns of tableau
            for (i = 1; i <= 7; i++)    
            {
                for (j = 1; j <= i; j++)
                {
                    _deck[shuffleOrder[k]].SetLocation(new Spot(Card.Location.Tableau, j, j, i));
                    //only face up if at the bottom of the tableau
                    if (i > j)
                    {
                        _deck[shuffleOrder[k]].SetFaceDown();
                    }
                    else
                    {
                        _deck[shuffleOrder[k]].SetFaceUp();
                    }
                    
                    k += 1;
                }
            }

            //rest of cards go to stock (all face down)
            for (i = k; i < 52; i++)    
            {
                _deck[shuffleOrder[i]].SetLocation(new Spot(Card.Location.Stock, i - k + 1));
                _deck[shuffleOrder[i]].SetFaceDown();
            }
        }

        #region Markers

        private Card _stockWasteMarkerCard;
        private CardPanel _stockMarker;
        private CardPanel _wasteMarker;

        private Card _tableauMarkerCard;
        private CardPanel[] _tableauMarkers;

        private Card[] _foundationMarkerCards;
        private CardPanel[] _foundationMarkers;

        /// <summary>
        /// Creates markers for Stock and Waste piles, Tableaus, and Foundations
        /// </summary>
        private void CreateMarkers()
        {
            //stock and waste markers
            _stockWasteMarkerCard = new Card(0, Card.Suites.None);
            _stockMarker = new CardPanel(ref _stockWasteMarkerCard);
            _stockMarker.Location = GetPlayAreaLocation(10, 10);
            _wasteMarker = new CardPanel(ref _stockWasteMarkerCard);
            _wasteMarker.Location = GetPlayAreaLocation(10, 30);


            //tableau markers
            _tableauMarkers = new CardPanel[7];

            _tableauMarkerCard = new Card(14, Card.Suites.None);
            for (int i = 0; i < 7; i++)
            {
                _tableauMarkers[i] = new CardPanel(ref _tableauMarkerCard);
                _tableauMarkers[i].Location = GetPlayAreaLocation(25 + i * 5, 10);
            }


            //foundation markers
            _foundationMarkerCards = new Card[4];
            _foundationMarkers = new CardPanel[4];

            for (int i = 0; i < 4; i++)
            {
                _foundationMarkerCards[i] = new Card(0, (Card.Suites) i + 1);
                _foundationMarkers[i] = new CardPanel(ref _foundationMarkerCards[i]);
                _foundationMarkers[i].Location = GetPlayAreaLocation(80, 10 + 20 * i);
            }
        }

        /// <summary>
        /// Moves a <see cref="CardPanel"/> to a location, dependent on the corresponding <see cref="Card"/>'s marker.
        /// </summary>
        /// <param name="sender">Sender must be a <see cref="Card"/>.</param>
        /// <param name="e"></param>
        private void MoveCardToLocation(object sender, EventArgs e)
        {
            Point dest;
            CardPanel cardPanelToMove = (CardPanel) sender;
            Card cardToMove = cardPanelToMove.GetCard();

            switch (cardToMove.GetLocation().GetPile())
            {
                case Card.Location.Stock:
                    dest = _stockMarker.Location;
                    break;
                case Card.Location.Waste:
                    dest = _wasteMarker.Location;
                    break;
                case Card.Location.Tableau:
                    dest = _tableauMarkers[cardToMove.GetLocation().GetColumn() - 1].Location;
                    dest.Y += (cardToMove.GetLocation().GetHeight() - 1) * CardSize.Height / 3;
                    break;
                case Card.Location.Foundation:
                    dest = _foundationMarkers[(int) cardToMove.GetSuite() - 1].Location;
                    break;
                default:
                    dest = new Point(0,0);
                    break;
            }

            cardPanelToMove.Location = dest;
            cardPanelToMove.BringToFront();
        }

        #endregion

        /// <summary>
        /// Gets a location relative to the top left of <see cref="pnlPlayArea"/>
        /// </summary>
        /// <param name="percentFromLeft"></param>
        /// <param name="percentFromTop"></param>
        /// <returns></returns>
        private Point GetPlayAreaLocation(int percentFromLeft, int percentFromTop)
        {
            return new Point(pnlPlayArea.Left + (pnlPlayArea.Width * percentFromLeft / 100), pnlPlayArea.Top + (pnlPlayArea.Height * percentFromTop / 100));
        }

        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!_deckCreated)
            {
                _deck = CreateDeck();
                CreateCardPanels();
                _deckCreated = true;
            }
            SetupCards();
        }
    }
}
