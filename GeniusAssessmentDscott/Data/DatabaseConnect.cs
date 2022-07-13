using System.Data.SqlClient;

namespace GeniusAssessmentDscott.Data
{
    class DatabaseConnect
    {
        readonly string ConnectionString;
        public SqlConnection connection
        {
            get;
        }
        public DatabaseConnect()
        {
            //Connection string that will be used for all connections to the database, please change this to match the Microsoft SQL database that the data from the files will be printed to.
            ConnectionString = @"Server=localhost\SQLEXPRESS;Database=MiniAdeptDB;Trusted_Connection=True;MultipleActiveResultSets=true;";

            connection = new SqlConnection(ConnectionString);
        }

        //Will return whether a connection to the database is possible or not
        public bool CheckConnection()
        {
            bool canConnect = false;
            try
            {
                connection.Open();
                canConnect = true;
                connection.Close();
            }
            catch (System.Exception)
            {
                connection.Close();
                canConnect = false;
            }
            return canConnect;
        }
    }
}
