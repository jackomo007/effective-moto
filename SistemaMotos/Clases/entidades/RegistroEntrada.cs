using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using SistemaViviendas.Clases.ConectorDB;

namespace SistemaMotos.Clases.entidades
{
    internal class RegistroEntrada
    {
        #region Constructor
        public RegistroEntrada() { }
        #endregion

        #region Metodos
        public static DataTable AllRegistroEntrada(DateTime FECHA)
        {
            string cmdText = "SELECT * FROM REPORTE_ENTRADA WHERE DATE(FECHA_HORA_REG) = DATE(@FECHA)";
            DataSet dataSet = new DataSet();

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos.");

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
                throw new Exception("Error al obtener registros de entrada: " + e.Message);
            }

            return dataSet.Tables[0];
        }

        public static bool NewRegistroEntrada(DateTime FECHA_HORA_REG, string MATRICULA)
        {
            Persona persona = Persona.GetPersona(MATRICULA);
            if (persona == null)
                throw new Exception("Persona no encontrada con la matrícula proporcionada.");

            string cmdText = "INSERT INTO REG_ENTRADA(FECHA_HORA_REG, ID_PERSONA2) VALUES(@FECHA_HORA_REG, @ID_PERSONA2)";
            bool resultado = false;

            try
            {
                // Establecemos la conexión usando "using" para asegurar su liberación
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos, revise el estado de conexión de su internet o del servidor");

                    // Realizamos la inserción en la base de datos
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
                throw new Exception("Error al insertar nuevo registro de entrada: " + e.Message);
            }

            return resultado;
        }
        #endregion
    }
}
