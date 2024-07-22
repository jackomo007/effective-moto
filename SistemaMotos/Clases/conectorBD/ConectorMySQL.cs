using System;
using System.Data.SQLite;
using SistemaMotos.Properties;

namespace SistemaViviendas.Clases.ConectorDB
{
    internal class ConectorSQLite
    {
        #region Atributos
        private static string DbFilePath { get; set; } = Settings.Default.dbFilePath;
        private static readonly string Cad_conexion = $"Data Source={DbFilePath};Version=3;";
        #endregion

        #region Metodos
        public static SQLiteConnection InitConexion()
        {
            SQLiteConnection conn = new SQLiteConnection(Cad_conexion);
            try
            {
                conn.Open();
                return conn;
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Error al abrir la conexión con la base de datos: " + ex.Message);
            }
        }

        public static void TermConexion(SQLiteConnection conn)
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (SQLiteException ex)
            {
                throw new Exception("Error al cerrar la conexión con la base de datos: " + ex.Message);
            }
        }
        #endregion
    }
}
