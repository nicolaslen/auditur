using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auditur.Presentacion.Classes
{
    public class PageChunks
    {
        public string Text { get; set; }
        public float StartX { get; set; }
        public float Y { get; set; }
        public float RelativeY => 595.5f - Y;
        public float EndX { get; set; }
        public float EndY { get; set; }
    }
}
