using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace MFKSReposrtService
{
    public partial class MFKSReportService : ServiceBase
    {
        private bool _cancel;
        string connectionString;
        public MFKSReportService()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        protected override void OnStart(string[] args)
        {
            _cancel = false;
            Task.Run(() => Processing());
            EventLog.WriteEntry("We did it! Started", EventLogEntryType.Information);
        }

        private void Processing()
        {
            try
            {
                while (!_cancel)
                {
                    
                    using(IDbConnection db = new SqlConnection(connectionString))
                    {
                        var query = @"INSERT INTO dbo.Report1 (JSON_Value)
Select [dbo].[udf-Str-JSON](1,1,(SELECT TOP (10) LastName, FirstName, MiddleName
  FROM [AdventureWorks2014].[Person].[Person]
FOR XML RAW))";
                        db.Execute(query);
                    }
                    Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnStop()
        {
            EventLog.WriteEntry("We did it! Stopped", EventLogEntryType.Information);
        }
    }
}
