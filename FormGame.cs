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

            _deck = CreateDeck();
            CreateCardPanels();
            SetupCards();
        }

        private readonly Card[] _deck;
        private CardPanel[] _cardPanels;

        #region "User Interface"

        /// <summary>
        /// Returns an array of <see cref="Card"/> representing a 52 size deck of cards.
        /// </summary>
        /// <returns>52 length array of <see cref="Card"/></returns>
        private static Card[] CreateDeck()
        {
            Card[] result = new Card[52];

            Card.Suites[] cardSuites =
                { Card.Suites.Clubs, Card.Suites.Diamonds, Card.Suites.Hearts, Card.Suites.Spades };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 14; j++)
                {
                    result[i * 13 + j - 1] = new Card(j, cardSuites[i]);
                    //index of current card in array is calculated
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
                    Size = new Size(65, 100),
                    Location = new Point(i * 65, 200),
                    BackgroundImageLayout = ImageLayout.Stretch
                };

                pnlTableaus.Controls.Add(_cardPanels[i]);
            }
        }

        private void SetupCards()
        {
            int[] shuffleOrder = new int[52];
            bool[] input = new bool[52];

            Random r = new Random();

            for (int i = 0; i < 52; i++)
            {
                int sanity = 60;
                int j = r.Next(51);
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

            for (int i = 1; i <= 7; i++)    //7 columns of tableau
            {
                for (int j = 1; j <= i; j++)
                {
                    _deck[k].SetLocation(Card.Location.Tableau, j,j, i);
                    k += 1;
                }
            }

            for (int i = k; i < 52; i++)    //rest of cards go to stock
            {
                _deck[i].SetLocation(Card.Location.Stock, i - k + 1);
            }
        }

        #endregion

    }
}
