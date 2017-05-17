using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Trombinoscope
{
    public static class DAL
    {
        public static List<EmployéPhoto> GetPictureFromDataReader()
        {
            var lst = new List<EmployéPhoto>();
            var connectString = Properties.Settings.Default.Northwind;
            string queryString = @"select e.Photo, e.LastName, e.FirstName, m.LastName LNManager, m.FirstName FNManager 
                                    from Employees e left outer join Employees m on m.EmployeeID=e.ReportsTo";

            using (var connect = new SqlConnection(connectString))
            {
                var command = new SqlCommand(queryString, connect);
                connect.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var emp = new EmployéPhoto();
                        emp.Image = ConvertBytesToImageSource((Byte[])reader["Photo"]);
                        emp.Nom = (string)reader["FirstName"];
                        emp.Prenom = (string)reader["LastName"];
                        if (reader["FNManager"] != DBNull.Value)
                            emp.NomManager = (string)reader["FNManager"];
                        if (reader["LNManager"] != DBNull.Value)
                            emp.PrenomManager = (string)reader["LNManager"];



                        lst.Add(emp);
                    }
                }
            }
            return lst;

        }

        private static ImageSource ConvertBytesToImageSource(Byte[] tab)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Les images stockées dans la base Northwind ont un en-tête de 78 octets 
                // qu'il faut enlever pour pouvoir les charger correctement
                ms.Write(tab, 78, tab.Length - 78);
                ImageSource image = BitmapFrame.Create(ms, BitmapCreateOptions.None,
                                      BitmapCacheOption.OnLoad);
                return image;
            }
        }

        public static List<Employé> GetInfoEmployésFromDataReader()
        {
            var lst = new List<Employé>();
            var connectString = Properties.Settings.Default.Northwind;
            string queryString = @"select e.EmployeeID,e.LastName,e.FirstName, m.LastName LNManager, m.FirstName FNManager   from Employees e
                                    left outer join Employees m on m.EmployeeID=e.ReportsTo";

            using (var connect = new SqlConnection(connectString))
            {
                var command = new SqlCommand(queryString, connect);
                connect.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var emp = new Employé();
                        emp.Id = (int)reader["EmployeeID"];
                        emp.Nom = (string)reader["LastName"];
                        emp.Prenom = (string)reader["FirstName"];
                        emp.LstTerritoires = GetTerritoryFromData(emp.Id);

                        if (reader["FNManager"] != DBNull.Value)
                            emp.NomManager = (string)reader["FNManager"];
                        if (reader["LNManager"] != DBNull.Value)
                            emp.PrenomManager = (string)reader["LNManager"];
                        lst.Add(emp);
                    }
                }
            }

            return lst;

        }

        public static List<territoire> GetTerritoryFromData(int EmployéId)
        {
            var lst = new List<territoire>();

            var connectString = Properties.Settings.Default.Northwind;
            string queryString = @"select et.TerritoryID, TerritoryDescription from EmployeeTerritories et inner join Territories t on t.TerritoryID=et.TerritoryID where et.EmployeeID=@id";
            var par = new SqlParameter("@Id", DbType.String);
            par.Value = EmployéId;


            using (var connect = new SqlConnection(connectString))
            {
                var command = new SqlCommand(queryString, connect);
                connect.Open();

                command.Parameters.Add(par);
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var ter = new territoire();
                        ter.Id = (string)reader["TerritoryID"];
                        ter.Description = (string)reader["TerritoryDescription"];
                        lst.Add(ter);

                    }
                }
            }

            return lst;
        }

        public static void SupprimerEmploye(int empId)
        {
            var connectString = Properties.Settings.Default.Northwind;
            string queryString = @"delete Employees where EmployeeID=@Id ";
            var paramId = new SqlParameter("@Id", DbType.Int32);
            paramId.Value = empId;


            using (var connect = new SqlConnection(connectString))
            {
                connect.Open();
                var tran = connect.BeginTransaction();

                var command = new SqlCommand(queryString, connect, tran);
                command.Parameters.Add(paramId);

                try
                {
                    command.ExecuteNonQuery();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }
        }
        public static void InsertEmploye(Employé emp)
        {
            var connectString = Properties.Settings.Default.Northwind;
            string queryString = @"insert Employees(LastName, FirstName) values(@nom, @prenom)";
            var paramNom = new SqlParameter("@nom", DbType.String);
            paramNom.Value = emp.Nom;
            var paramPrenom = new SqlParameter("@prenom", DbType.String);
            paramPrenom.Value = emp.Prenom;

            using (var connect = new SqlConnection(connectString))
            {
                connect.Open();
                var tran = connect.BeginTransaction();
                var command = new SqlCommand(queryString, connect, tran);
                command.Parameters.Add(paramNom);
                command.Parameters.Add(paramPrenom);

                try
                {
                    command.ExecuteNonQuery();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }


                queryString = "select top 1 EmployeeID from Employees order by 1 desc";
                command = new SqlCommand(queryString, connect);
                emp.Id = (int)command.ExecuteScalar();
            }
        }
    }

}
