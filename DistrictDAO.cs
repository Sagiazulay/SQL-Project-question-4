using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTARGIL4
{
    class DistrictDAO
    { 
     private string m_con = "Data Source = sagi\\mssqlserver01;initial catalog=FaceMinistry;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
    private static readonly log4net.ILog my_logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public DistrictDAO(string con)
    {
        con = m_con;
    }
        FaceMinistryEntities entities = new FaceMinistryEntities() { };
        
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
    public void AddDistrict(string district, int id, long population)
    {
        ExecuteNonQuery($"INSERT INTO DISTRICTS (Name, District_ID, Population) VALUES ('{district}', {id}, {population});");
        my_logger.Info($"New Districts '{district}' Was Added Seccessfully");
    }
    public void UpdateDistrict(int district_id, string name)
    {
        ExecuteNonQuery($"UPDATE DISTRICTS SET Name = '{name}' WHERE ID = {district_id};");
        my_logger.Info($"District '{name}' Was Updated Seccessfully");
    }
    public void DeleteDistrict(int district_id)
    {
        ExecuteNonQuery($"DELETE FROM CITIES WHERE ID = {district_id};");
        my_logger.Info($"The District '{district_id}' Was Deleted Seccessfully");
    }
    public void GetAllDistricts()
    {
        string query = "SELECT * FROM DISTRICTS";

        try
        {
            SqlCommand cmd = new SqlCommand(query, new SqlConnection(m_con));
            cmd.CommandType = CommandType.Text;
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

            List<DISTRICT> list = new List<DISTRICT>();

            while (reader.Read() == true)
            {
                list.Add(
                    new DISTRICT
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        Name = reader["NAME"].ToString()                       
                    });
                Console.WriteLine($"{reader["ID"]}, {reader["NAME"].ToString()}");
            }
        }
        catch (Exception ex)
        {
            my_logger.Error($"Failed to get Districts from DB [SELECT * FROM DISTRICTS]. Error : {ex}");
        }


    }
    public DISTRICT GetDistrictByID(int district_id)
    {
        string query = $"SELECT * FROM DISTRICTS WHERE ID = {district_id}";

        try
        {
            SqlCommand cmd = new SqlCommand(query, new SqlConnection(m_con));
            cmd.CommandType = CommandType.Text;
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
            reader.Read();
                DISTRICT c = new DISTRICT
                {
                ID = Convert.ToInt32(reader["ID"]),
                Name = reader["NAME"].ToString()
            };
            Console.WriteLine($"{reader["ID"]}, {reader["NAME"].ToString()}");
            return c;
        }
        catch (Exception ex)
        {
            my_logger.Error($"Failed to get DISTRICT from DB [SELECT * FROM DISTRICT]. Error : {ex}");
            return null;
        }
    }
}
}
