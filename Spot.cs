using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire
{
    public class Spot
    {
        public Spot(Card.Location pile, int height = 0, int row = 0, int column = 0)
        {
            _pile = pile;
            _height = height;
            _column = column;
            _row = row;
        }

        private Card.Location _pile;
        private int _height;
        private int _column;
        private int _row;

        public Card.Location GetPile()
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

        public int GetRow()
        {
            return _row;
        }
    }
}
