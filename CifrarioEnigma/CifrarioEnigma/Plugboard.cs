using System;
using System.Collections.Generic;
using System.Drawing;

namespace CifrarioEnigma
{
    public class Plugboard
    {
        public char[] SwappedConfiguration { get; set; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        public SortedDictionary<string, string> _checked { get; set; }
        public List<PlugboardColor> _colors { get; set; }
        public string FirstChar { get; set; }
        public string SecondChar { get; set; }
        public int OnSelection { get; set; }

        public Plugboard()
        {
            _colors = new List<PlugboardColor>
            {
                new PlugboardColor(Color.LightBlue),
                new PlugboardColor(Color.LightCoral),
                new PlugboardColor(Color.LightCyan),
                new PlugboardColor(Color.LightGray),
                new PlugboardColor(Color.LightGreen),
                new PlugboardColor(Color.LightPink),
                new PlugboardColor(Color.LightSalmon),
                new PlugboardColor(Color.LightSeaGreen),
                new PlugboardColor(Color.LightSkyBlue),
                new PlugboardColor(Color.LightSlateGray),
                new PlugboardColor(Color.LightSteelBlue),
                new PlugboardColor(Color.LightYellow),
                new PlugboardColor(Color.BurlyWood)
            };
        }

        public void SwapChars(string first, string second)
        {
            var index1 = Array.IndexOf(this.SwappedConfiguration, first.ToCharArray()[0]);
            var index2 = Array.IndexOf(this.SwappedConfiguration, second.ToCharArray()[0]);

            var tmp = this.SwappedConfiguration[index1];
            this.SwappedConfiguration[index1] = this.SwappedConfiguration[index2];
            this.SwappedConfiguration[index2] = tmp;
        }
    }

    public class PlugboardColor
    {
        public Color CurrentColor { get; set; }
        public bool Used { get; set; } = false;

        public PlugboardColor(Color color)
        {
            this.CurrentColor = color;
        }

        public PlugboardColor()
        {

        }
    }
}
