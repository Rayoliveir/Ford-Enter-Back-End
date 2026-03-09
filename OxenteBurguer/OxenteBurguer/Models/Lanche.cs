using System;
using System.Collections.Generic;
using System.Text;

namespace OxenteBurguer.Models {
    public class Lanche : Produto {
        public string PontoCarne { get; set; } = string.Empty;
        public List<string> Adicionais { get; set; } = new List<string>();
    }
}
