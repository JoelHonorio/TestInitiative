using Initiative.Models;
using System.Data.Odbc;

namespace Initiative.Repository
{
    public class ValoresRepository
    {
        public List<ValoresModel> GetValores(string con)
        {
            var valores = new List<ValoresModel>();

            OdbcCommand command = new($@"SELECT * FROM Valores;");

            using (OdbcConnection connection = new(con))
            {
                command.Connection = connection;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var Valore = new ValoresModel
                        {
                            Valores = Convert.ToDecimal(reader.GetValue(0)),
                            Decomposto = reader.GetValue(1).ToString()
                        };

                        valores.Add(Valore);
                    }
                };
            }

            return valores;
        }

        public void AtualizaValores(ValoresModel valores, string con)
        {
            OdbcCommand command = new($@"UPDATE Valores SET Decomposto='{valores.Decomposto}' WHERE Valores = {Convert.ToString(valores.Valores).Replace(",", ".")};");

            using (OdbcConnection connection = new(con))
            {
                command.Connection = connection;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {

                }
            }
        }
    }
}