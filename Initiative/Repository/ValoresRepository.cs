#region Using

using Initiative.Models;
using System.Data.Odbc;

#endregion

namespace Initiative.Repository
{
    public class ValoresRepository
    {
        #region Consultas

        public List<ValoresModel> GetValores(string conexao)
        {
            var valores = new List<ValoresModel>();

            OdbcCommand command = new($@"SELECT * FROM Valores;");

            using (OdbcConnection connection = new(conexao))
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

                connection.Close();
            }

            return valores;
        }

        #endregion

        #region Updates

        public void AtualizaValores(ValoresModel valor, string conexao)
        {
            OdbcCommand command = new($@"UPDATE Valores SET Decomposto='{valor.Decomposto}' WHERE Valores = {Convert.ToString(valor.Valores).Replace(",", ".")};");

            using (OdbcConnection connection = new(conexao))
            {
                command.Connection = connection;
                connection.Open();

                using (var reader = command.ExecuteReader()) { }

                connection.Close();
            }
        }

        #endregion
    }
}