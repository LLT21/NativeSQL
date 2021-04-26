using System;
using System.Data;
using System.Data.Odbc;
using Microsoft.Data.SqlClient;

namespace nativesql
{
   class Program
   {
      static void Main(string[] args)
      {

         //Odbc is working in Native compilation mode
         //Using: dotnet publish -r osx.11.0-x64 -c release
         //Then execute: /bin/release/net5.0/osx.11.0-x64/native/nativesql

         using (IDbConnection dbConnection = new OdbcConnection(ConnectionStrings.OdbcConnection))
         {
            string names = string.Empty;

            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
               dbCommand.CommandText = "SELECT * FROM Contact";
               dbCommand.CommandType = CommandType.Text;

               dbConnection.Open();

               using (IDataReader dataReader = dbCommand.ExecuteReader())
               {
                  while (dataReader.Read())
                  {
                     names += ", " + Convert.ToString(dataReader["Name1"]);
                  }
               }

               Console.WriteLine(names);
            }
         }

         //SqlClient is not working in Native compilation mode: stops at the using step

         using (IDbConnection dbConnection = new SqlConnection(ConnectionStrings.SqlConnection))
         {
            string names = string.Empty;

            using (IDbCommand dbCommand = dbConnection.CreateCommand())
            {
               dbCommand.CommandText = "SELECT * FROM Contact";
               dbCommand.CommandType = CommandType.Text;

               dbConnection.Open();

               using (IDataReader dataReader = dbCommand.ExecuteReader())
               {
                  while (dataReader.Read())
                  {
                     names += ", " + Convert.ToString(dataReader["Name1"]);
                  }
               }
            }

            Console.WriteLine(names);
         }
      }
   }
}
