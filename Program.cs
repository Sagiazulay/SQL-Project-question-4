using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLTARGIL4
{
    class Program
    {
        static void Main(string[] args)
        {
            string con = "Data Source = sagi\\mssqlserver01;initial catalog=FaceMinistry;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";
            CityDAO cityDAO = new CityDAO(con);
            cityDAO.AddCity("Herzliya", 2, "Ilana Dayan", 100_00);
            cityDAO.GetCityByID(4);
            //using (FaceMinistryEntities entities = new FaceMinistryEntities())
            //{
            //entities.CITIES.ToList().ForEach
            //}
            //cityDAO.GetCityByPopulation(300000);
            cityDAO.AllCities();
        }
    }
}
