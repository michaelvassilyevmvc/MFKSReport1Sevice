using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CheckDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                using (IDbConnection db = new SqlConnection(connectionString))
                    {
                    //                        var query = @"INSERT INTO dbo.Report1 (JSON_Value)
                    //Select [dbo].[udf-Str-JSON](1,1,(SELECT TOP (10) LastName, FirstName, MiddleName
                    //  FROM [AdventureWorks2014].[Person].[Person]
                    //FOR XML RAW))";
                    var query = "SELECT TOP(1) JSON_Value FROM dbo.Report1";

                        var json =  db.Query<string>(query);
                        
                    }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
