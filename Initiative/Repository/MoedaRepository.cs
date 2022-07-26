using Initiative.Models;
using System.Data.Odbc;

namespace Initiative.Repository
{
    public class MoedaRepository
    {
        public List<MoedaModel> GetMoeda(string con)
        {
            var moedas = new List<MoedaModel>();

            OdbcCommand command = new($@"SELECT * FROM Moeda ORDER BY Valor DESC;");

            using (OdbcConnection connection = new(con))
            {
                command.Connection = connection;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var moeda = new MoedaModel
                        {
                            ID = reader.GetInt32(0),
                            Descritivo = reader.GetString(1),
                            Valor = Convert.ToDecimal(reader.GetValue(2)),
                            Formato = reader.GetString(3)
                        };

                        moedas.Add(moeda);
                    }
                };
            }

            return moedas;
        }
    }
}