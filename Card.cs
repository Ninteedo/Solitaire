using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Solitaire.Properties;

namespace Solitaire
{
    public class Card
    {
        #region Constructors

        public Card(int value, Suites suite)
        {
            _value = value;
            _suite = suite;
        }

        //public Card(IContainer container)
        //{
        //    container.Add(this);

        //    InitializeComponent();
        //}

        #endregion

        #region "Card Variables"

        private readonly int _value;
        private readonly Suites _suite;
        private bool _isFaceDown;

        public enum Suites
        {
            None,
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        public int GetValue()
        {
            return _value;
        }

        public Suites GetSuite()
        {
            return _suite;
        }

        public bool GetFaceDown()
        {
            return _isFaceDown;
        }

        public string GetName()
        {
            string[] valueNames = 
            {
                "Blank", "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen",
                "King"
            };
            string[] suiteNames =
            {
                "None", "Clubs", "Diamonds", "Hearts", "Spades"
            };

            if (ValidCardValue)
            {
                return valueNames[_value] + " of " + suiteNames[(int) _suite];
            }
            else
            {
                return "Unknown Card";
            }
        }

        public event EventHandler FaceUpToggled;

        public void SetFaceUp()
        {
            _isFaceDown = false;
            FaceUpToggled(this, null);
        }

        public void SetFaceDown()
        {
            _isFaceDown = true;
            FaceUpToggled(this, null);
        }

        #endregion

        #region Images

        private Image _frontImage;
        private Image _backImage;

        /// <summary>
        /// Returns the image which currently represents the card, dependent on whether card is face down.
        /// </summary>
        /// <returns></returns>
        public Image GetDisplayImage()
        {
            if (_isFaceDown)
            {
                return _backImage;
            }
            else
            {
                return _frontImage;
            }
        }

        private void GetFrontImage()
        {
            var result = Resources.ResourceManager.GetObject(GenerateFrontImageName());
            if (result != null)
            {
                _frontImage = (Image) result;
            }
            else
            {
                _frontImage = Resources.blank;  //if correct image wasn't found then defaults to a blank card
            }
        }

        private string GenerateFrontImageName()
        {
            string[] valueNames = 
            {
                "0", "A", "_2", "_3", "_4", "_5", "_6", "_7", "_8", "_9", "_10", "J", "Q", "K"
            };  //underscores were automatically inserted for card names beginning with a number
            string[] suiteNames =
            {
                "N", "C", "D", "H", "S"
            };  //N suite is the none suite, shouldn't be used but I put it in anyway

            if (ValidCardValue)
            {
                return valueNames[_value] + suiteNames[(int) _suite] + ".png";
            }
            else
            {
                return "unknown.png";
            }
        }

        private void GetBackImage()
        {
            _backImage = Resources.blue_back;
        }

        #endregion

        private bool ValidCardValue
        {
            get { return _value >= 0 && _value <= 13; }
        }
    }
}
