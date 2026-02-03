using System;
using System.Collections.Generic;
using System.Text;

namespace OxenteBurguer.Models {
    public class Lanche : Produto {
        public string PontoCarne { get; set; }
        public List<string> Adicionais { get; set; } = new List<string>();
    }
}
