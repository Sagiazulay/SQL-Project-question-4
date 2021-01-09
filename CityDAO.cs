using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTARGIL4
{
    class CityDAO
    {
        private string m_con = "Data Source = sagi\\mssqlserver01;initial catalog=FaceMinistry;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
        private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        FaceMinistryEntities entities = new FaceMinistryEntities() { };
        public CityDAO(string con)
        {
            con = m_con;
        }
        private int ExecuteNonQuery(string query)
        {
            int result = 0;
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (cmd.Connection = new SqlConnection(m_con))
                    {
                        cmd.Connection.Open();
                        my_logger.Debug($"Connection sting is Open");
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.CommandText = query;

                        result = cmd.ExecuteNonQuery();
                        Console.WriteLine(result);
                    }
                }
                my_logger.Info("*ExecuteNonQuery Secceeded!*");
                return result;
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Could not connect to server!{Ex}");
                return 0;
            }
        }
        public void GetCityByPopulation(long population)
        {
           entities.CITIES.Where(s => s.Population > population).ToList().ForEach(s => Console.WriteLine(JsonConvert.SerializeObject(s)));
            my_logger.Info("*GetCityByPopulation Fuct MAde It Seccessfully*");
        }
        public void AllCities()
        {
            var cities = from c in entities.CITIES
                         select c.Name.ToString();
            cities.ToList().ForEach(c => Console.WriteLine(c));
        }

        public void AddCity(string city, int district, string mayor, long population)
        {
            ExecuteNonQuery($"INSERT INTO CITIES (Name, District_ID, Mayor, Population) VALUES ('{city}', {district}, '{mayor}', {population});");
            my_logger.Info($"New City '{city}' Was Added Seccessfully");
        }

        public void UpdateCity(int city_id, string name)
        {
            ExecuteNonQuery($"UPDATE CITIES SET Name = '{name}' WHERE ID = {city_id};");
            my_logger.Info($"City '{name}' Was Updated Seccessfully");
        }

        public void DeleteCity(int city_id)
        {
            ExecuteNonQuery($"DELETE FROM CITIES WHERE ID = {city_id};");
            my_logger.Info($"The City '{city_id}' Was Deleted Seccessfully");
        }

        public void GetAllCities()
        {
            string query = "SELECT * FROM CITIES";

            try
            {
                SqlCommand cmd = new SqlCommand(query, new SqlConnection(m_con));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                List<CITy> list = new List<CITy>();

                while (reader.Read() == true)
                {
                    list.Add(
                        new CITy
                        {
                            ID = Convert.ToInt32(reader["ID"]),
                            Name = reader["NAME"].ToString(),
                            Mayor = reader["MAYOR"].ToString()
                        });
                    Console.WriteLine($"{reader["ID"]}, {reader["NAME"].ToString()}, {reader["MAYOR"].ToString()}");
                }
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Cities from DB [SELECT * FROM CITIES]. Error : {ex}");
            }
        }

        public CITy GetCityByID(int city_id)
        {
            string query = $"SELECT * FROM CITIES WHERE ID = {city_id}";

            try
            {
                SqlCommand cmd = new SqlCommand(query, new SqlConnection(m_con));
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                reader.Read();
                CITy c = new CITy
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["NAME"].ToString(),
                    Mayor = reader["MAYOR"].ToString() 
                };
                Console.WriteLine($"{reader["ID"]}, {reader["NAME"].ToString()}, {reader["MAYOR"].ToString()}");
                return c;
            }
            catch (Exception ex)
            {
                my_logger.Error($"Failed to get Cities from DB [SELECT * FROM CITIES]. Error : {ex}");
                return null;
            }
        }
    }
}
