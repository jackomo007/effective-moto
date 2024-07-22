using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using SistemaViviendas.Clases.ConectorDB;

namespace SistemaMotos.Clases.entidades
{
    internal class RegistroSalida
    {
        #region Constructor
        public RegistroSalida() { }
        #endregion

        #region Metodos
        public static DataTable AllRegistroSalida(DateTime FECHA)
        {
            string cmdText = "SELECT * FROM REPORTE_SALIDA WHERE DATE(FECHA_HORA_REG) = DATE(@FECHA)";
            DataSet dataSet = new DataSet();

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos");

                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@FECHA", FECHA.ToString("yyyy-MM-dd"));

                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmdDB))
                        {
                            if (da.Fill(dataSet) == 0)
                                return null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener registros de salida: " + e.Message);
            }

            return dataSet.Tables[0];
        }

        public static bool NewRegistroSalida(DateTime FECHA_HORA_REG, string MATRICULA)
        {
            Persona persona = Persona.GetPersona(MATRICULA);
            if (persona == null)
                throw new Exception("Persona no encontrada con la matrícula proporcionada.");

            string cmdText = "INSERT INTO REG_SALIDA(FECHA_HORA_REG, ID_PERSONA2) VALUES(@FECHA_HORA_REG, @ID_PERSONA2)";
            bool resultado = false;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos");

                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@FECHA_HORA_REG", FECHA_HORA_REG);
                        cmdDB.Parameters.AddWithValue("@ID_PERSONA2", persona.ID);

                        int rowsAffected = cmdDB.ExecuteNonQuery();
                        resultado = rowsAffected == 1;

                        if (resultado)
                        {
                            MessageBox.Show("Inserción exitosa en la base de datos");
                        }
                        else
                        {
                            MessageBox.Show("Fallo en la inserción en la base de datos");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al insertar nuevo registro de salida: " + e.Message);
            }

            return resultado;
        }
        #endregion
    }
}
