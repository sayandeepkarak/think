using System;
using System.Data.SqlClient;

namespace think
{
    public class FineCalculator
    {

        public static void calculateFine(bool isGlobalIssue, string userId)
        {
            string query="SELECT id,fine,returndate FROM activebooks WHERE CONVERT(date,returndate) < GETDATE()";
            if (!isGlobalIssue)
            {
                query += " AND studentid=" + userId;
            }
            InternalSqlCrud crud = new InternalSqlCrud();
            SqlDataReader data = crud.executeReader(query);
            if (data.HasRows)
            {
                while (data.Read())
                {
                    int oldFine = int.Parse(data["fine"].ToString());
                    DateTime returnDate = DateTime.Parse(data["returndate"].ToString());
                    int gap = (int)(DateTime.Today - returnDate).TotalDays;
                    int newFine = 30 * gap;
                    if (oldFine != newFine)
                    {
                        crud.executeCommand("UPDATE activebooks SET fine='" + newFine + "' WHERE id=" + data["id"].ToString());
                    }
                }
            }
        }
    }
}