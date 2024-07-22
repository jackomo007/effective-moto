using System;
using System.Data;
using System.Data.SQLite;
using SistemaViviendas.Clases.ConectorDB;

namespace SistemaMotos.Clases.entidades
{
    internal class Vehiculo
    {
        #region Atributos
        public int ID { get; set; }
        public string NOM_VEHICULO { get; set; }
        public string MODELO_VEHICULO { get; set; }
        public string MARCA { get; set; }
        public string PLACAS { get; set; }
        public string NIV { get; set; }
        public int ID_DOC { get; set; }
        #endregion

        #region Constructor
        public Vehiculo() { }
        #endregion

        #region Metodos
        public static DataTable GetAllVehiculo()
        {
            string cmdText = "SELECT * FROM VEHICULO";
            DataSet dataSet = new DataSet();

            try
            {
                // Establecemos la conexión usando "using" para asegurar su liberación
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos, revise el estado de conexión de su internet o del servidor");

                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
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
                throw new Exception("Error al obtener todos los vehículos: " + e.Message);
            }

            return dataSet.Tables[0];
        }

        public static Vehiculo GetVehiculo(string PLACAS)
        {
            string cmdText = "SELECT * FROM VEHICULO WHERE PLACAS = @PLACAS";
            Vehiculo vehiculo = null;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos, revise el estado de conexión de su internet o del servidor");

                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@PLACAS", PLACAS);

                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmdDB))
                        {
                            DataSet dataSet = new DataSet();
                            if (da.Fill(dataSet) > 0)
                            {
                                vehiculo = new Vehiculo
                                {
                                    ID = Convert.ToInt32(dataSet.Tables[0].Rows[0]["ID_VEHICULO"]),
                                    NOM_VEHICULO = dataSet.Tables[0].Rows[0]["NOM_VEHICULO"].ToString(),
                                    MODELO_VEHICULO = dataSet.Tables[0].Rows[0]["MODELO_VEHICULO"].ToString(),
                                    MARCA = dataSet.Tables[0].Rows[0]["MARCA"].ToString(),
                                    PLACAS = dataSet.Tables[0].Rows[0]["PLACAS"].ToString(),
                                    NIV = dataSet.Tables[0].Rows[0]["NIV"].ToString(),
                                    ID_DOC = Convert.ToInt32(dataSet.Tables[0].Rows[0]["ID_DOC2"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener vehículo: " + e.Message);
            }

            return vehiculo;
        }

        public static bool NewVehiculo(string NOM_VEHICULO, string MODELO_VEHICULO, string MARCA, string PLACAS, string NIV, int ID_DOC)
        {
            string cmdText = "INSERT INTO VEHICULO(NOM_VEHICULO, MODELO_VEHICULO, MARCA, PLACAS, NIV, ID_DOC2) VALUES(@NOM_VEHICULO, @MODELO_VEHICULO, @MARCA, @PLACAS, @NIV, @ID_DOC2)";
            bool resultado = false;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos, revise el estado de conexión de su internet o del servidor");

                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@NOM_VEHICULO", NOM_VEHICULO);
                        cmdDB.Parameters.AddWithValue("@MODELO_VEHICULO", MODELO_VEHICULO);
                        cmdDB.Parameters.AddWithValue("@MARCA", MARCA);
                        cmdDB.Parameters.AddWithValue("@PLACAS", PLACAS);
                        cmdDB.Parameters.AddWithValue("@NIV", NIV);
                        cmdDB.Parameters.AddWithValue("@ID_DOC2", ID_DOC);

                        if (cmdDB.ExecuteNonQuery() == 1)
                            resultado = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al insertar nuevo vehículo: " + e.Message);
            }

            return resultado;
        }

        public static bool UpdateVehiculoData(string PLACAS, string NOM_VEHICULO, string MODELO_VEHICULO, string MARCA, string NIV, int ID_DOC)
        {
            string cmdText = "UPDATE VEHICULO SET NOM_VEHICULO = @NOM_VEHICULO, MODELO_VEHICULO = @MODELO_VEHICULO, MARCA = @MARCA, NIV = @NIV, ID_DOC2 = @ID_DOC2 WHERE PLACAS = @PLACAS";
            bool resultado = false;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos, revise el estado de conexión de su internet o del servidor");

                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@NOM_VEHICULO", NOM_VEHICULO);
                        cmdDB.Parameters.AddWithValue("@MODELO_VEHICULO", MODELO_VEHICULO);
                        cmdDB.Parameters.AddWithValue("@MARCA", MARCA);
                        cmdDB.Parameters.AddWithValue("@NIV", NIV);
                        cmdDB.Parameters.AddWithValue("@ID_DOC2", ID_DOC);
                        cmdDB.Parameters.AddWithValue("@PLACAS", PLACAS);

                        if (cmdDB.ExecuteNonQuery() == 1)
                            resultado = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al actualizar datos de vehículo: " + e.Message);
            }

            return resultado;
        }

        public static bool DeleteVehiculo(string clave)
        {
            string cmdText = "DELETE FROM VEHICULO WHERE PLACAS = @CLAVE";
            bool resultado = false;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos, revise el estado de conexión de su internet o del servidor");

                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@CLAVE", clave);

                        if (cmdDB.ExecuteNonQuery() == 1)
                            resultado = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al eliminar vehículo: " + e.Message);
            }

            return resultado;
        }
        #endregion
    }
}
