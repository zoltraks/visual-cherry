using System;
using System.Collections.Generic;
using System.Text;

namespace Cherry.Common
{
    public static class Utility
    {
        public static System.Drawing.Color HexToColor(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                return System.Drawing.Color.Transparent;
            }
            if ('#' == hex[0])
            {
                hex = hex.Substring(1);
            }
            for (int i = 0, l = hex.Length; i < l; i++)
            {
                if (false
                    || hex[i] >= '0' && hex[i] <= '9'
                    || hex[i] >= 'a' && hex[i] <= 'f'
                    || hex[i] >= 'A' && hex[i] <= 'F'
                    )
                {
                    continue;
                }
                return System.Drawing.Color.Transparent;
            }
            byte r = 0;
            byte g = 0;
            byte b = 0;
            byte a = 255;
            switch (hex.Length)
            {
                default:
                    return System.Drawing.Color.Transparent;
                case 3:
                    hex = new System.String(new char[] { hex[0], hex[0], hex[1], hex[1], hex[2], hex[2] });
                    goto case 6;
                case 4:
                    hex = new System.String(new char[] { hex[0], hex[0], hex[1], hex[1], hex[2], hex[2], hex[3], hex[3] });
                    goto case 8;
                case 8:
                    a = Convert.ToByte(hex.Substring(6, 2), 16);
                    goto case 6;
                case 6:
                    r = Convert.ToByte(hex.Substring(0, 2), 16);
                    g = Convert.ToByte(hex.Substring(2, 2), 16);
                    b = Convert.ToByte(hex.Substring(4, 2), 16);
                    return System.Drawing.Color.FromArgb(a, r, g, b);
            }
        }

        public static System.Drawing.Color ColorTransition(System.Drawing.Color color_1, System.Drawing.Color color_2, double percentage)
        {
            int a, r, g, b;

            if (percentage < 0) percentage = 0;
            if (percentage > 1) percentage = 100 / percentage;
            if (percentage > 1) percentage = 1;

            a = (int)(double)(percentage * color_1.A + (1 - percentage) * color_2.A);
            r = (int)(double)(percentage * color_1.R + (1 - percentage) * color_2.R);
            g = (int)(double)(percentage * color_1.G + (1 - percentage) * color_2.G);
            b = (int)(double)(percentage * color_1.B + (1 - percentage) * color_2.B);

            return System.Drawing.Color.FromArgb(a, r, g, b);
        }
    }
}
