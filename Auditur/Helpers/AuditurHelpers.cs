using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Helpers
{
    public static class AuditurHelpers
    {
        public static string QueryGetByID(long ID, string TableName)
        {
            return "SELECT * FROM " + TableName + " WHERE ID = " + ID;
        }

        public static string QueryGetByParameters(string[] Columns, string TableName)
        {
            return "SELECT * FROM " + TableName + " WHERE " + string.Join (" = ? AND ", Columns) + " = ?";
        }

        public static string QueryGetAll(string TableName)
        {
            return "SELECT * FROM " + TableName;
        }

        public static string QueryInsert(string[] Columns, string TableName)
        {
            string[] Values = new string[Columns.Length];
            for (int i = 0; i < Columns.Length; i++)
                Values[i] = "?";
            return "INSERT INTO " + TableName + " (" + string.Join(", ", Columns) + ") VALUES (" + string.Join(", ", Values) + ")";
        }

        public static string QueryUpdate(long ID, string[] Columns, string TableName)
        {
            return "UPDATE " + TableName + " SET " + string.Join("= ?, ", Columns) + " = ? WHERE ID = " + ID;
        }

        public static string QueryGetLastID(string TableName)
        {
            return "SELECT MAX(ID) AS ID FROM " + TableName;
        }

        public static string GetDateTimeString(DateTime? dateTime)
        {
            return dateTime != null ? dateTime.Value.ToShortDateString() : "";
        }

        public static string QueryDeleteByParameters(string[] Columns, string TableName)
        {
            return "DELETE FROM " + TableName + " WHERE " + string.Join(" = ? AND ", Columns) + " = ?";
        }
    }
}
