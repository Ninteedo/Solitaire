using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Solitaire
{
    public partial class CardPanel : Panel
    {
        public CardPanel(ref Card myCard)
        {
            InitializeComponent();

            _myCard = myCard;
            myCard.FaceUpToggled += CardFaceUpToggled;
            myCard.LocationChanged += CardLocationChanged;

            UpdateDisplayImage();
        }

        private readonly Card _myCard;

        public Card GetCard()
        {
            return _myCard;
        }

        private void CardFaceUpToggled(object sender, EventArgs e)
        {
            UpdateDisplayImage();
        }

        private void UpdateDisplayImage()
        {
            BackgroundImage = _myCard.GetDisplayImage();
        }

        public EventHandler UpdateLocation;

        private void CardLocationChanged(object sender, EventArgs e)
        {
            UpdateLocation(this, null);
        }

        #region Mouse



        #endregion


    }
}
