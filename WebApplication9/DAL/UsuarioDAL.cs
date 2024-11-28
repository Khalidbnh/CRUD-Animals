using System.Data.SqlClient;
using WebApplication9.Models;

namespace WebApplication9.DAL
{
    public class UsuarioDAL
    {
        private readonly string connecionString = "Data Source =85.208.21.117,54321;" +
                                         "Initial Catalog=KhalidAnimales;" +
                                         "User ID=sa;" + "Password=Sql#123456789;";

        public Usuario GetUsuarioLogin(string username, string password)
        {
            using(SqlConnection sql = new SqlConnection(connecionString))
            {
                string sqlQuery = "SELECT * FROM Usuario WHERE Username = @Username AND Pass = @Password";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sql);
                sqlCommand.Parameters.AddWithValue("@Username", username);
                sqlCommand.Parameters.AddWithValue("@Password", password);

                sql.Open();

                using(var reader = sqlCommand.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        return new Usuario
                        {
                            IdUsuario = (int)reader["IdUsuario"],
                            UserName = (string)reader["UserName"],
                            Password = (string)reader["Pass"],
                            Apellido = (string)reader["Apellido"],
                            Email = reader["Email"] as string,
                            FechaNacimiento = (DateOnly)reader["FechaNacimiento"],
                            Telefono = reader["Telefono"] as string,
                            Direccion = (string)reader["Direccion"],
                            Ciudad = (string)reader["Ciudad"],
                            Estado = reader["Estado"] as string,
                            CodigoPostal = (string)reader["CodigoPostal"],
                            FechaRegistro = (DateTime)reader["FechaRegistro"],
                            Activo = (bool)reader["Activo"]

                        };
                    }
                }
            }


            return null;
        }
    }
}
