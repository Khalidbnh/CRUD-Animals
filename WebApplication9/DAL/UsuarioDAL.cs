using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.Data.SqlClient;
using WebApplication9.Models;
using WebApplication9.Tools;

namespace WebApplication9.DAL
{
    public class UsuarioDAL
    {
        private readonly string connecionString = "Data Source =85.208.21.117,54321;" +
                                         "Initial Catalog=KhalidAnimales;" +
                                         "User ID=sa;" + "Password=Sql#123456789;";

        public Usuario GetUsuarioLogin(string username, string password)
        {
            using (SqlConnection sql = new SqlConnection(connecionString))
            {
                string sqlQuery = "SELECT * FROM Usuario WHERE Username = @Username";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sql);
                sqlCommand.Parameters.AddWithValue("@Username", username);

                sql.Open();

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var passwordHash = (byte[])reader["PasswordHash"];
                        var passwordSalt = (byte[])reader["PasswordSalt"];

                        Console.WriteLine("PasswordHash length: " + passwordHash.Length);
                        Console.WriteLine("PasswordSalt length: " + passwordSalt.Length);

                        if (passwordHash == null || passwordSalt == null)
                        {
                            // Log or throw an exception if hash or salt is missing
                            throw new Exception("PasswordHash or PasswordSalt is missing in the database.");
                        }

                        // Verify the password
                        if (PasswordHelper.VerifyPasswordHash(password, passwordHash, passwordSalt))
                        {
                            Console.WriteLine("Password verification succeeded.");
                            return new Usuario
                            {
                                IdUsuario = (int)reader["IdUsuario"],
                                UserName = (string)reader["UserName"],
                                Apellido = reader["Apellido"] as string ?? string.Empty,
                                Email = reader["Email"] as string ?? string.Empty,
                                FechaNacimiento = reader["FechaNacimiento"] != DBNull.Value
                                    ? (DateTime?)reader["FechaNacimiento"]
                                    : null,
                                Telefono = reader["Telefono"] as string ?? string.Empty,
                                Direccion = reader["Direccion"] as string ?? string.Empty,
                                Ciudad = reader["Ciudad"] as string ?? string.Empty,
                                Estado = reader["Estado"] as string ?? string.Empty,
                                CodigoPostal = reader["CodigoPostal"] as string ?? string.Empty,
                                FechaRegistro = reader["FechaRegistro"] != DBNull.Value
                                    ? (DateTime)reader["FechaRegistro"]
                                    : DateTime.MinValue,
                                Activo = reader["Activo"] != DBNull.Value && (bool)reader["Activo"]
                            };
                        }
                        else
                        {
                            
                            Console.WriteLine("Password verification failed.");
                                
                            // Log or return null if password verification fails
                            return null;
                        }
                    }
                }
            }

            // Return null if user not found
            return null;
        }


        public void CreateUsuario(Usuario usuario, string password)
        {
            //Generating el hash y el salt para la contraseña
            PasswordHelper.CreatePasswordHash(password, out byte[] passwordHash, out  byte[] passwordSalt);


            using(SqlConnection sqlConnection = new SqlConnection(connecionString))
            {
                string sQlQuery = "INSERT INTO Usuario " +
                    "(UserName, PasswordHash, PasswordSalt, Apellido, Email, FechaNacimiento, Telefono, Direccion, Ciudad, Estado, CodigoPostal, FechaRegistro, Activo)" +
                    "VALUES (@UserName, @PasswordHash, @PasswordSalt, @Apellido, @Email, @FechaNacimiento, @Telefono, @Direccion, @Ciudad, @Estado, @CodigoPostal, @FechaRegistro, @Activo)";
                SqlCommand command = new SqlCommand(sQlQuery, sqlConnection);

                command.Parameters.AddWithValue("@UserName", usuario.UserName);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@PasswordSalt", passwordSalt);
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
