#region Using

using Initiative.Repository;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace Initiative.Controllers
{
    public class HomeController : Controller
    {
        #region Injeção de dependências

        public readonly MoedaRepository _moeda;
        public readonly ValoresRepository _valores;
        public readonly IConfiguration _configuration;

        public HomeController(MoedaRepository moeda, ValoresRepository valores, IConfiguration configuration)
        {
            _moeda = moeda;
            _valores = valores;
            _configuration = configuration;
        }

        #endregion

        public IActionResult Index()
        {
            string conexao = _configuration["ConnectionStrings:connection"];
            var valores = _valores.GetValores(conexao);
            var moedas = _moeda.GetMoeda(conexao);
            var decomposto = "";
            int contador;

            foreach (var valor in valores)
            {
                if (string.IsNullOrEmpty(valor.Decomposto))
                {
                    var copiaValor = valor.Valores;

                    foreach (var moeda in moedas)
                    {
                        for (contador = 0; valor.Valores >= moeda.Valor; contador++)
                        {
                            valor.Valores -= moeda.Valor;
                        }

                        if (contador > 0)
                        {
                            decomposto = decomposto + contador + " " + moeda.Formato + "(s) de " + moeda.Descritivo + ";";
                        }
                    }

                    valor.Decomposto = decomposto;
                    valor.Valores = copiaValor;
                    decomposto = "";

                    foreach (var vlr in valores)
                    {
                        _valores.AtualizaValores(vlr, conexao);
                    }
                }
            }

            return View(valores);
        }
    }
}