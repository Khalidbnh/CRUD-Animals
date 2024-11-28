using Microsoft.CodeAnalysis.Elfie.Diagnostics;
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

                using(SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        return new Usuario
                        {
                            IdUsuario = (int)reader["IdUsuario"],
                            UserName = (string)reader["UserName"],
                            Password = (string)reader["Pass"],
                            Apellido = reader["Apellido"] as string ?? string.Empty,
                            Email = reader["Email"] as string ?? string.Empty,
                            FechaNacimiento = reader["FechaNacimiento"] != DBNull.Value
                                      ? (DateTime?)reader["FechaNacimiento"]
                                      : null, // Nullable DateTime
                            Telefono = reader["Telefono"] as string ?? string.Empty,
                            Direccion = reader["Direccion"] as string ?? string.Empty, // Safe nullable string
                            Ciudad = reader["Ciudad"] as string ?? string.Empty,
                            Estado = reader["Estado"] as string ?? string.Empty,
                            CodigoPostal = reader["CodigoPostal"] as string ?? string.Empty,
                            FechaRegistro = reader["FechaRegistro"] != DBNull.Value
                                    ? (DateTime)reader["FechaRegistro"]
                                    : DateTime.MinValue, // Default for non-nullable
                            Activo = reader["Activo"] != DBNull.Value && (bool)reader["Activo"] // Handle nullable bool
                        };
                    }
                }
            }


            return null;
        }


        public void CreateUsuario(Usuario usuario)
        {
            using(SqlConnection sqlConnection = new SqlConnection(connecionString))
            {
                string sQlQuery = "INSERT INTO Usuario " +
                    "(UserName, Pass, Apellido, Email, FechaNacimiento, Telefono, Direccion, Ciudad, Estado, CodigoPostal, FechaRegistro, Activo)" +
                    "VALUES (@UserName, @Pass, @Apellido, @Email, @FechaNacimiento, @Telefono, @Direccion, @Ciudad, @Estado, @CodigoPostal, @FechaRegistro, @Activo)";
                SqlCommand command = new SqlCommand(sQlQuery, sqlConnection);

                command.Parameters.AddWithValue("@UserName", usuario.UserName);
                command.Parameters.AddWithValue("@Pass", usuario.Password);
                command.Parameters.AddWithValue("@Apellido", usuario.Apellido ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", usuario.Email ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento.HasValue ? (object)usuario.FechaNacimiento.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Telefono", usuario.Telefono ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Direccion", usuario.Direccion ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Ciudad", usuario.Ciudad ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Estado", usuario.Estado ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@CodigoPostal", usuario.CodigoPostal ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@FechaRegistro", DateTime.Now);
                command.Parameters.AddWithValue("@Activo", true);

                sqlConnection.Open();
                command.ExecuteNonQuery();

            }
        }
    }
}
