using System;
using System.Data;
using System.Data.SQLite;
using SistemaViviendas.Clases.ConectorDB;

namespace SistemaMotos.Clases.entidades
{
    internal class Documento
    {
        public Documento() { }

        public static DataTable getAllDocumentos()
        {
            string cmdText = "SELECT * FROM DOCUMENTO_VEHICULO";
            DataSet dataSet = new DataSet();

            try
            {
                // Establecemos la conexión usando "using" para asegurar su liberación
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexion con la base de datos, revise el estado de conexion de su internet o del servidor");

                    // Realizamos la consulta en la base de datos
                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmdDB))
                        {
                            // Si no hay ningun registro se retorna un valor nulo
                            if (da.Fill(dataSet) == 0)
                                return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener documentos: " + e.Message);
            }

            // Devolvemos la tabla obtenida
            return dataSet.Tables[0];
        }
    }
}
