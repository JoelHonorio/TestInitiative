using System.ComponentModel.DataAnnotations;

namespace Initiative.Models
{
    public class ValoresModel
    {
        [Display(Name = "Valores")]
        public decimal Valores { get; set; }

        [Display(Name = "Decomposto")]
        public string? Decomposto { get; set; }
    }
}