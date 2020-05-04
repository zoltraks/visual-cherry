using System;
using System.Collections.Generic;
using System.Text;

namespace Cherry.Common
{
    public class Hex
    {
        private System.Drawing.Color _Color;

        public System.Drawing.Color Color { get => _Color; set => _Color = value; }

        public Hex(System.Drawing.Color color)
        {
            _Color = color;
        }

        public Hex(byte r, byte g, byte b)
        {
            _Color = System.Drawing.Color.FromArgb(255, r, g, b);
        }

        public Hex(byte r, byte g, byte b, byte a)
        {
            _Color = System.Drawing.Color.FromArgb(a, r, g, b);
        }

        public Hex(byte r, byte g, byte b, float a)
        {
            if (a < 0)
            {
                a = 0;
            }
            else if (a > 1)
            {
                a = 1;
            }
            _Color = System.Drawing.Color.FromArgb((byte)(a * 255f), r, g, b);
        }

        public Hex(string hex)
        {
            _Color = Cherry.Common.Utility.HexToColor(hex);
        }

        public static implicit operator System.Drawing.Color(Hex hex)
        {
            return hex._Color;
        }
    }
}
