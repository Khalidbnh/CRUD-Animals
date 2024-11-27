using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using WebApplication9.Models;

namespace WebApplication9.DAL
{
    public class DAL_Animal
    {
        private string connecionString = "Data Source =85.208.21.117,54321;" + 
                                         "Initial Catalog=KhalidAnimales;" +
                                         "User ID=sa;" + "Password=Sql#123456789;";

        public List<AnimalModel> GetAll()
        {
            List<AnimalModel> animals = new List<AnimalModel>();
            DAL_TipoAnimal tipoAnimal = new DAL_TipoAnimal();

            using (SqlConnection conn = new SqlConnection(connecionString))
            {
                string sqlQuery = "SELECT * FROM Animal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                conn.Open();
                SqlDataReader sqlDataReader = cmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    AnimalModel animal = new AnimalModel()
                    {
                        IdAnimal = Convert.ToInt32(sqlDataReader["IdAnimal"]),
                        NombreAnimal = sqlDataReader["NombreAnimal"].ToString(),
                        Raza = sqlDataReader["Raza"].ToString(),
                        RIdTipoAnimal = Convert.ToInt32(sqlDataReader["RIdTipoAnimal"]),
                        FechaNacimiento = sqlDataReader["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(sqlDataReader["FechaNacimiento"]),
                        TipoAnimal = tipoAnimal.GetById(Convert.ToInt32(sqlDataReader["RIdTipoAnimal"]))
                    };
                    animals.Add(animal);
                }

            }
            return animals;    
        }

        public AnimalModel GetById(int id)
        {
            AnimalModel animal = null;
            DAL_TipoAnimal tipoAnimalDAL = new DAL_TipoAnimal();

            using (SqlConnection conn = new SqlConnection(connecionString))
            {
                string sqlQuery = "SELECT * FROM Animal WHERE IdAnimal = @IdAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdAnimal", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    animal = new AnimalModel
                    {
                        IdAnimal = Convert.ToInt32(reader["IdAnimal"]),
                        NombreAnimal = reader["NombreAnimal"].ToString(),
                        Raza = reader["Raza"]?.ToString(),
                        RIdTipoAnimal = Convert.ToInt32(reader["RIdTipoAnimal"]),
                        FechaNacimiento = reader["FechaNacimiento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["FechaNacimiento"]),
                        TipoAnimal = tipoAnimalDAL.GetById(Convert.ToInt32(reader["RIdTipoAnimal"]))
                    };
                }
            }

            return animal;
        }

        public void Insert(AnimalModel animal)
        {
            using (SqlConnection conn = new SqlConnection(connecionString))
            {
                string sqlQuery = "INSERT INTO Animal (NombreAnimal, Raza, RIdTipoAnimal, FechaNacimiento) VALUES (@NombreAnimal, @Raza, @RIdTipoAnimal, @FechaNacimiento)";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                cmd.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                cmd.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento.HasValue ? (object)animal.FechaNacimiento.Value : DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(AnimalModel animal)
        {
            using (SqlConnection conn = new SqlConnection(connecionString))
            {
                string sqlQuery = "UPDATE Animal SET NombreAnimal = @NombreAnimal, Raza = @Raza, RIdTipoAnimal = @RIdTipoAnimal, FechaNacimiento = @FechaNacimiento WHERE IdAnimal = @IdAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
                cmd.Parameters.AddWithValue("@NombreAnimal", animal.NombreAnimal);
                cmd.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RIdTipoAnimal", animal.RIdTipoAnimal);
                cmd.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento.HasValue ? (object)animal.FechaNacimiento.Value : DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connecionString))
            {
                string sqlQuery = "DELETE FROM Animal WHERE IdAnimal = @IdAnimal";
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@IdAnimal", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
