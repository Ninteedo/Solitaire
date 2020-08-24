using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public partial class Card : Component
    {
        #region Constructors

        public Card(int value, Suites suite)
        {
            InitializeComponent();

            _value = value;
            _suite = suite;
        }

        public Card(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        

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
            string[] valueNames = {
                "Blank", "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen",
                "King"
            };
            string[] suiteNames =
            {
                "None", "Clubs", "Diamonds", "Hearts", "Spades"
            };

            if (_value >= 0 && _value <= 13)
            {
                return valueNames[_value] + " of " + suiteNames[(int) _suite];
            }
            else
            {
                return "Unknown Card";
            }
        }

        public void SetFaceUp()
        {
            _isFaceDown = false;
        }

        public void SetFaceDown()
        {
            _isFaceDown = true;
        }
    }
}
