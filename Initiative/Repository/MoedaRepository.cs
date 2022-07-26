#region Using

using Initiative.Models;
using System.Data.Odbc;

#endregion

namespace Initiative.Repository
{
    public class MoedaRepository
    {
        #region Consultas

        public List<MoedaModel> GetMoeda(string conexao)
        {
            var moedas = new List<MoedaModel>();

            OdbcCommand command = new($@"SELECT * FROM Moeda ORDER BY Valor DESC;");

            using (OdbcConnection connection = new(conexao))
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

                connection.Close();
            }

            return moedas;
        }

        #endregion
    }
}