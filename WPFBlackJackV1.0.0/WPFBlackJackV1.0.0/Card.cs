using System;
using System.Collections.Generic;
using System.Text;

namespace WPFBlackJackV1._0._0
{
    internal class Card
    {
        public string Suit { get; set; }
        public int Rank { get; set; }
        public int[] Value { get; set; }
        public string ImageUrl { get; set; }
        public bool IsVisible { get; set; }
    }
}