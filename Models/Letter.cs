using System;
using System.Collections.Generic;
using System.Text;

namespace WordleSolver.Models
{
    public class Letter
    {
        public char Character { get; set; }
        public int? Position { get; set; }
        public List<int> NotPosPossitions { get; set; }
    }
}
