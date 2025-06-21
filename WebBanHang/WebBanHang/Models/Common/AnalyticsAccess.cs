using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
namespace WebBanHang.Models.Common
{
    public class AnalyticsAccess
    {
        private static string strConnect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public static ReportViewModel Report()
        {
            using (var connect = new SqlConnection(strConnect))
            {
                var item = connect.QueryFirstOrDefault<ReportViewModel>("sp_Reports",commandType: CommandType.StoredProcedure);
                return null;
            }
        }
    }
}