using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.IO;

using System.Net;
using System.Net.Http.Headers;
using System.Threading;

namespace GoogleLocationsReader
{


    public static class AnalyticsHelperMethods
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static System.Data.DataTable ExportToDatatableGeneric<InputType>(List<InputType> entries, string name)
        {
            log.Info("Creating datatable");

            var row_index = 1;
            System.Data.DataTable table = new System.Data.DataTable();
            DataRow row;
            DataColumn column;
            var columns = entries.First().GetType().GetProperties();

            //create columns
            foreach (var property in columns)
            {
                column = new DataColumn();

                column.ColumnName = property.Name.ToString();
                log.Info($"Adding Column {column.ColumnName}");
                // Add the Column to the DataColumnCollection.
                table.Columns.Add(column);

            }
            //populate rows
            foreach (var entry in entries)
            {
                row = table.NewRow();
                foreach (var property in entry.GetType().GetProperties())
                {

                    object value = property.GetValue(entry, null);
                    if (value != null)
                    {

                        row[property.Name.ToString()] = value;

                    }

                }
                table.Rows.Add(row);
                row_index++;
            }

            return table;

        }



        public static void ExportToCSV(System.Data.DataTable table, string name)
        {
            log.Info("exporting to CSV");
            StringBuilder sb = new StringBuilder();

            IEnumerable<string> columnNames = table.Columns.Cast<DataColumn>().
                                              Select(col => col.ColumnName);
            sb.AppendLine(string.Join(",", columnNames));

            foreach (DataRow r in table.Rows)
            {
                IEnumerable<string> fields = r.ItemArray.Select(field => string.Concat("\"", field.ToString().Replace("\"", "\"\""), "\""));
                List<string> formattedfields = new List<string>();
                foreach (var field in fields)
                {
                    byte[] bytes = Encoding.Default.GetBytes(field);
                    string formatted = Encoding.UTF8.GetString(bytes);
                    formattedfields.Add(formatted);
                }

                sb.AppendLine(string.Join(",", formattedfields));
            }

            File.AppendAllText(name, sb.ToString());
        }




    }
}

