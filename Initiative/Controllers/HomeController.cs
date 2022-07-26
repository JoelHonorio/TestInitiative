using Initiative.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Initiative.Controllers
{
    public class HomeController : Controller
    {
        public readonly MoedaRepository _moeda;
        public readonly ValoresRepository _valores;
        public readonly IConfiguration _configuration;

        public HomeController(MoedaRepository moeda, ValoresRepository valores, IConfiguration configuration)
        {
            _moeda = moeda;
            _valores = valores;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string con = _configuration["ConnectionStrings:connection"];
            var valores = _valores.GetValores(con);
            var moedas = _moeda.GetMoeda(con);
            var decomposto = "";
            int cont;

            foreach (var valor in valores)
            {
                if (string.IsNullOrEmpty(valor.Decomposto))
                {
                    var valorCopy = valor.Valores;

                    foreach (var moeda in moedas)
                    {
                        for (cont = 0; valor.Valores >= moeda.Valor; cont++)
                        {
                            valor.Valores -= moeda.Valor;
                        }

                        if (cont > 0)
                        {
                            decomposto = decomposto + cont + " " + moeda.Formato + "(s) de " + moeda.Descritivo + ";";
                        }
                    }

                    valor.Decomposto = decomposto;
                    valor.Valores = valorCopy;
                    decomposto = "";

                    foreach (var vlr in valores)
                    {
                        _valores.AtualizaValores(vlr, con);
                    }
                }
            }

            return View(valores);
        }
    }
}