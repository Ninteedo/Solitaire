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

            UpdateDisplayImage();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private readonly Card _myCard;

        private void CardFaceUpToggled(object sender, EventArgs e)
        {
            UpdateDisplayImage();
        }

        private void UpdateDisplayImage()
        {
            BackgroundImage = _myCard.GetDisplayImage();
        }
    }
}
