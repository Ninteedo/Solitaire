using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Spot
    {
        public Spot(Card.Pile pile, int height = 0, int column = 0)
        {
            _pile = pile;
            _height = height;
            _column = column;
        }

        private readonly Card.Pile _pile;
        private readonly int _height;
        private readonly int _column;

        public Card.Pile GetPile()
        {
            return _pile;
        }

        public int GetHeight()
        {
            return _height;
        }

        public int GetColumn()
        {
            return _column;
        }
    }
}
