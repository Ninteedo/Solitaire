using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
        private Card[] _deck = new Card[52];
        private CardPanel[] _cardPanels = new CardPanel[52];

        #region User Interface

        //private SizeF drawScale = new SizeF(1, 1);

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

        private CardPanel[] GetAllCardPanels()
        {
            CardPanel[] result = new CardPanel[65];

            int i = 0;

            result[i] = _stockMarker;
            i++;
            result[i] = _wasteMarker;
            i++;
            foreach (CardPanel tMarker in _tableauMarkers)
            {
                result[i] = tMarker;
                i++;
            }
            foreach (CardPanel fMarker in _foundationMarkers)
            {
                result[i] = fMarker;
                i++;
            }
            foreach (CardPanel cMarker in _cardPanels)
            {
                result[i] = cMarker;
                i++;
            }

            return result;
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
                _cardPanels[i] = new CardPanel(ref _deck[i]);
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
                    _deck[shuffleOrder[k]].SetSpot(new Spot(Card.Pile.Tableau, j, i));
                    //only face up if at the bottom of the tableau
                    if (i > j)
                    {
                        _deck[shuffleOrder[k]].SetFaceDown();
                    }
                    else
                    {
                        _deck[shuffleOrder[k]].SetFaceUp();
                        _deck[shuffleOrder[k]].CanMove = true;
                    }
                    
                    k += 1;
                }
            }

            //rest of cards go to stock (all face down)
            for (i = k; i < 52; i++)    
            {
                _deck[shuffleOrder[i]].SetSpot(new Spot(Card.Pile.Stock, i - k + 1));
                _deck[shuffleOrder[i]].SetFaceDown();
            }
        }


        #region Markers

        private Card _stockMarkerCard;
        private CardPanel _stockMarker;

        private Card _wasteMarkerCard;
        private CardPanel _wasteMarker;

        private Card[] _tableauMarkerCards;
        private CardPanel[] _tableauMarkers;

        private Card[] _foundationMarkerCards;
        private CardPanel[] _foundationMarkers;

        /// <summary>
        /// Creates markers for Stock and Waste piles, Tableaus, and Foundations
        /// </summary>
        private void CreateMarkers()
        {
            //stock and waste markers
            _stockMarkerCard = new Card(0, Card.Suites.None, new Spot(Card.Pile.Stock), true);
            _stockMarker = new CardPanel(ref _stockMarkerCard);
            _stockMarker.Location = GetPlayAreaLocation(10, 10);

            _wasteMarkerCard = new Card(0, Card.Suites.None, new Spot(Card.Pile.Waste), true);
            _wasteMarker = new CardPanel(ref _wasteMarkerCard);
            _wasteMarker.Location = GetPlayAreaLocation(10, 30);


            //tableau markers
            _tableauMarkerCards = new Card[7];
            _tableauMarkers = new CardPanel[7];

            for (int i = 0; i < 7; i++)
            {
                _tableauMarkerCards[i] = new Card(14, Card.Suites.None, new Spot(Card.Pile.Tableau, 0, i + 1), true);
                _tableauMarkers[i] = new CardPanel(ref _tableauMarkerCards[i]);
                _tableauMarkers[i].Location = GetPlayAreaLocation(25 + i * 5, 10);
            }


            //foundation markers
            _foundationMarkerCards = new Card[4];
            _foundationMarkers = new CardPanel[4];

            for (int i = 0; i < 4; i++)
            {
                _foundationMarkerCards[i] = new Card(0, (Card.Suites) i + 1, new Spot(Card.Pile.Foundation, 0, i + 1), true);
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
            const int cardPanelSplitFactor = 3;

            Point dest;
            CardPanel cardPanelToMove = (CardPanel) sender;
            Card cardToMove = cardPanelToMove.GetCard();

            if (_heldCardPanel != null && _followerCardPanels.Contains(cardPanelToMove))
            {
                dest = new Point(_heldCardPanel.Location.X,
                    _heldCardPanel.Location.Y + (_followerCardPanels.IndexOf(cardPanelToMove) + 1) * CardSize.Height /
                    cardPanelSplitFactor);
            }
            else
            {
                switch (cardToMove.GetSpot().GetPile())
                {
                    case Card.Pile.Stock:
                        dest = _stockMarker.Location;
                        break;
                    case Card.Pile.Waste:
                        dest = _wasteMarker.Location;
                        break;
                    case Card.Pile.Tableau:
                        dest = _tableauMarkers[cardToMove.GetSpot().GetColumn() - 1].Location;
                        dest.Y += (cardToMove.GetSpot().GetHeight() - 1) * CardSize.Height / cardPanelSplitFactor;
                        break;
                    case Card.Pile.Foundation:
                        dest = _foundationMarkers[(int) cardToMove.GetSuite() - 1].Location;
                        break;
                    default:
                        dest = new Point(0,0);
                        break;
                }
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

        #region Mouse Control

        private CardPanel? _heldCardPanel;
        private Point? _mouseOffset;
        private List<CardPanel> _followerCardPanels = new List<CardPanel>();

        private void CardPanelMouseDown(object sender, MouseEventArgs e)
        {
            CardPanel clickedCardPanel = (CardPanel) sender;
            Spot cardSpot = clickedCardPanel.GetCard().GetSpot();
            _followerCardPanels.Clear();

            //markers cannot move
            if (!clickedCardPanel.GetCard().IsMarker)
            {
                if (cardSpot.GetPile() == Card.Pile.Stock)
                {
                    clickedCardPanel.GetCard().SetSpot(new Spot(Card.Pile.Waste, FindTopCardInPile(new Spot(Card.Pile.Waste)).GetSpot().GetHeight() + 1));
                    clickedCardPanel.GetCard().SetFaceUp();
                }

                //if (clickedCardPanel.GetCard() == FindTopCardInPile(cardSpot))
                if (!clickedCardPanel.GetCard().GetFaceDown())
                {
                    _heldCardPanel = clickedCardPanel;
                    _mouseOffset = e.Location;

                    //finds all follower cards, revealed cards on top of the held card
                    for (int i = cardSpot.GetHeight() + 1; i <= FindTopCardInPile(
                        cardSpot).GetSpot().GetHeight(); i++)
                    {
                        Card? c = FindCardInSpot(new Spot(cardSpot.GetPile(),
                            i, cardSpot.GetColumn()));
                        if (c != null)
                        {
                            CardPanel? cPanel = FindCardPanelByCard(c);
                            if (cPanel != null)
                            {
                                _followerCardPanels.Add(cPanel);
                            }
                        }
                    }
                }
            }
            else
            {
                //when stock marker is clicked return waste cards to stock
                int j = FindTopCardInPile(new Spot(Card.Pile.Waste)).GetSpot().GetHeight();
                for (int i = j; i > 0; i--)
                {
                    Card? c = FindCardInSpot(new Spot(Card.Pile.Waste, i));
                    c?.SetSpot(new Spot(Card.Pile.Stock, j - i + 1));
                    c?.SetFaceDown();
                }
            }
        }

        private void CardPanelMouseUp(object sender, MouseEventArgs e)
        {
            if (_heldCardPanel != null)
            {
                CardDroppedByPlayer(ref _heldCardPanel);
                _heldCardPanel = null;

                for (int i = 0; i < _followerCardPanels.Count; i++)
                {
                    CardDroppedByPlayer(ref _followerCardPanels.ToArray()[i]);
                }
            }
        }

        private void MouseMoved(object sender, MouseEventArgs e)
        {
            //the location value of e is relative to the top left of the sender
            if (_heldCardPanel != null && _mouseOffset != null)
            {
                _heldCardPanel.Location = new Point(e.X + _heldCardPanel.Left - _mouseOffset.Value.X,
                    e.Y + _heldCardPanel.Top - _mouseOffset.Value.Y); 

                //moves all followers
                foreach (CardPanel cPanel in _followerCardPanels)
                {
                    cPanel.UpdateLocation(cPanel, null);
                }
            }
        }

        /// <summary>
        /// Returns true if the 2 input <see cref="CardPanel"/>s are overlapping.
        /// </summary>
        /// <param name="panel1"></param>
        /// <param name="panel2"></param>
        /// <returns></returns>
        private bool CheckForCardPanelOverlap(CardPanel panel1, CardPanel panel2)
        {
            return !((panel1.Left > panel2.Right || panel1.Top > panel2.Bottom) ||
                     (panel2.Left > panel1.Right || panel2.Top > panel1.Bottom));
        }

        #endregion

        #endregion

        #region Game Logic

        /// <summary>
        /// Returns the <see cref="Card"/> which has matching <see cref="Spot"/>, or if no matching card then returns null.
        /// </summary>
        /// <param name="targetSpot"></param>
        /// <returns></returns>
        private Card? FindCardInSpot(Spot targetSpot)
        {
            //note: 2 cards should never share an identical spot

            foreach (CardPanel cPanel in GetAllCardPanels())
            {
                Card c = cPanel.GetCard();
                if (c.GetSpot().GetPile() == targetSpot.GetPile() && c.GetSpot().GetHeight() == targetSpot.GetHeight() &&
                     c.GetSpot().GetColumn() == targetSpot.GetColumn())
                {
                    return c;
                }
            }

            //if no card in target spot then return null
            return null;
        }

        /// <summary>
        /// Returns the topmost <see cref="Card"/> in a tableau column or foundation pile
        /// </summary>
        /// <param name="spotToCheck">Looks at <see cref="Spot.GetPile()"/> and <see cref="Spot.GetColumn()"/>, but ignores <see cref="Spot.GetHeight()"/>.</param>
        /// <returns></returns>
        private Card FindTopCardInPile(Spot spotToCheck)
        {
            Card topInPile;
            switch (spotToCheck.GetPile())
            {
                case Card.Pile.Stock:
                    topInPile = _stockMarker.GetCard();
                    break;
                case Card.Pile.Waste:
                    topInPile = _wasteMarker.GetCard();
                    break;
                case Card.Pile.Tableau:
                    topInPile = _tableauMarkers[spotToCheck.GetColumn() - 1].GetCard();
                    break;
                case Card.Pile.Foundation:
                    topInPile = _foundationMarkers[spotToCheck.GetColumn() - 1].GetCard();
                    break;
                default:
                    return _stockMarker.GetCard();
                    //this is just a fallback, this shouldn't be triggered as player can only move cards to foundation and tableau
            }

            foreach (Card c in _deck)
            {
                if (c.GetSpot().GetPile() == spotToCheck.GetPile() && c.GetSpot().GetColumn() == spotToCheck.GetColumn() && c.GetSpot().GetHeight() > topInPile.GetSpot().GetHeight())
                {
                    topInPile = c;
                }
            }

            return topInPile;
        }

        /// <summary>
        /// Returns whether or not a player's move is valid.
        /// </summary>
        /// <param name="cardToMove"></param>
        /// <param name="newSpot"></param>
        /// <returns></returns>
        private bool CheckValidMove(Card cardToMove, Spot newSpot)
        {
            Card topInNewPile = FindTopCardInPile(new Spot(newSpot.GetPile(), 0, newSpot.GetColumn()));
            if (topInNewPile != null)
            {
                switch (newSpot.GetPile())
                {
                    case Card.Pile.Tableau:
                        //card must be different colour to card below and have a value 1 less than the card below, or any king in an empty tableau
                        if (cardToMove.GetValue() == topInNewPile.GetValue() - 1 &&
                            (cardToMove.IsBlack() == topInNewPile.IsRed() ||
                             topInNewPile.GetSuite() == Card.Suites.None))
                        {
                            return true;
                        }
                        break;
                    case Card.Pile.Foundation:
                        //card must be same suite as the card below and have a value 1 more than the card below
                        if (cardToMove.GetValue() == topInNewPile.GetValue() + 1 && cardToMove.GetSuite() == topInNewPile.GetSuite())
                        {
                            return true;
                        }
                        break;
                }
            }

            //defaults to false
            return false;
        }

        /// <summary>
        /// Returns a List of <see cref="Spot"/>s of places where the provided <see cref="cardToCheck"/> can move to.
        /// </summary>
        /// <param name="cardToCheck"></param>
        /// <returns></returns>
        private List<Spot> GeneratePossibleSpots(Card cardToCheck)
        {
            List<Spot> result = new List<Spot>();
            int suiteNumber = (int) cardToCheck.GetSuite();

            //checks the corresponding foundation pile, current height of foundation pile plus one
            Spot correspondingFoundation = new Spot(Card.Pile.Foundation,
                FindTopCardInPile(new Spot(Card.Pile.Foundation, 0, suiteNumber)).GetSpot().GetHeight() + 1,
                suiteNumber);
            if (CheckValidMove(cardToCheck, correspondingFoundation))
            {
                result.Add(correspondingFoundation);
            }

            //check each tableau
            Spot[] tableauSpots = new Spot[7];
            for (int i = 0; i < 7; i++)
            {
                tableauSpots[i] = new Spot(Card.Pile.Tableau,
                    FindTopCardInPile(new Spot(Card.Pile.Tableau, 0, i + 1)).GetSpot().GetHeight() + 1,
                    i + 1);
                if (CheckValidMove(cardToCheck, tableauSpots[i]))
                {
                    result.Add(tableauSpots[i]);
                }
            }

            return result;
        }

        private void CardDroppedByPlayer(ref CardPanel cardPanelMoved)
        {
            List<Spot> possibleSpots = GeneratePossibleSpots(cardPanelMoved.GetCard());
            bool invalidDrop = true;

            foreach (Spot s in possibleSpots)
            {
                //other card is one below where the new card will be if checks complete successfully
                Card? otherCard = FindCardInSpot(new Spot(s.GetPile(), s.GetHeight() - 1, s.GetColumn()));
                if (otherCard != null)
                {
                    CardPanel? otherCardPanel = FindCardPanelByCard(otherCard);
                    if (otherCardPanel != null)
                    {
                        if (CheckForCardPanelOverlap(cardPanelMoved, otherCardPanel))
                        {
                            //valid move
                            Spot oldSpot = cardPanelMoved.GetCard().GetSpot();
                            cardPanelMoved.GetCard().SetSpot(s);
                            invalidDrop = false;

                            Card topOfOldSpot = FindTopCardInPile(oldSpot);
                            if (topOfOldSpot.GetSpot().GetPile() == Card.Pile.Tableau && !topOfOldSpot.IsMarker)
                            {
                                //reveals card under card moved
                                topOfOldSpot.SetFaceUp();
                            }

                            break;
                        }
                    }
                }
            }

            if (invalidDrop)
            {
                cardPanelMoved.UpdateLocation(cardPanelMoved, null);
            }
        }

        private CardPanel? FindCardPanelByCard(Card searchCard)
        {
            //checks tableau markers
            foreach (CardPanel tMarkerPanel in _tableauMarkers)
            {
                if (tMarkerPanel.GetCard() == searchCard)
                {
                    return tMarkerPanel;
                }
            }

            //checks foundation markers
            foreach (CardPanel fMarkerPanel in _foundationMarkers)
            {
                if (fMarkerPanel.GetCard() == searchCard)
                {
                    return fMarkerPanel;
                }
            }

            //checks the deck
            foreach (CardPanel cPanel in _cardPanels)
            {
                if (cPanel.GetCard() == searchCard)
                {
                    return cPanel;
                }
            }

            return null;
        }

        #endregion

        private void StartGame()
        {
            if (!_deckCreated)
            {
                _deck = CreateDeck();
                CreateCardPanels();
                _deckCreated = true;

                CardPanel[] allCardPanels = GetAllCardPanels();

                foreach (CardPanel cPanel in allCardPanels)
                {
                    //Controls.Add(_cardPanels[i]);
                    pnlPlayArea.Controls.Add(cPanel);
                    cPanel.BringToFront();

                    cPanel.UpdateLocation += MoveCardToLocation;
                    cPanel.MouseDown += CardPanelMouseDown;
                    cPanel.MouseUp += CardPanelMouseUp;
                    cPanel.MouseMove += MouseMoved;
                    cPanel.Size = CardSize;
                    cPanel.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            
            _heldCardPanel = null;
            SetupCards();
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            StartGame();
        }
    }
}
