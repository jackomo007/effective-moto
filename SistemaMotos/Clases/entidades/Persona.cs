using System;
using System.Data;
using System.Data.SQLite;
using SistemaViviendas.Clases.ConectorDB;

namespace SistemaMotos.Clases.entidades
{
    internal class Persona
    {
        #region Atributos
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string MATRICULA { get; set; }
        public string NSS { get; set; }
        public string CURP { get; set; }
        public string TEL { get; set; }
        public string DIR { get; set; }
        public string GRAD_GRUPO { get; set; }
        public string NO_EST { get; set; }
        public int ID_VEHICULO { get; set; }
        #endregion

        #region Constructor
        public Persona() { }
        #endregion

        #region Metodos
        public static Persona GetPersona(string matricula)
        {
            string cmdText = "SELECT * FROM REGISTRO_PERSONA WHERE MATRICULA = @matricula";
            Persona persona = new Persona();

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos.");

                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@matricula", matricula);

                        using (SQLiteDataReader reader = cmdDB.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                persona.ID = Convert.ToInt32(reader["ID_PERSONA"]);
                                persona.Nombre = reader["NOM_PERSONA"].ToString();
                                persona.MATRICULA = reader["MATRICULA"].ToString();
                                persona.NSS = reader["NSS"].ToString();
                                persona.CURP = reader["CURP"].ToString();
                                persona.TEL = reader["TEL"].ToString();
                                persona.DIR = reader["DIR"].ToString();
                                persona.GRAD_GRUPO = reader["GRAD_GRUPO"].ToString();
                                persona.NO_EST = reader["NO_EST"].ToString();
                                persona.ID_VEHICULO = Convert.ToInt32(reader["ID_VEHICULO2"]);
                            }
                            else
                            {
                                return null; // No se encontró la persona
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener persona: " + e.Message);
            }

            return persona;
        }

        public static DataTable GetDatosPersonaVehiculo(string matricula)
        {
            string cmdText = "SELECT * FROM DATOS_PERSONA_VEHICULO WHERE MATRICULA = @matricula";
            DataSet dataSet = new DataSet();

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos.");

                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@matricula", matricula);

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
                throw new Exception("Error al obtener datos de persona y vehículo: " + e.Message);
            }

            return dataSet.Tables[0];
        }

        public static bool NewPersona(string NOM_PERSONA, string MATRICULA, string NSS, string CURP, string TEL, string DIR, string GRAD_GRUPO, string NO_EST, int ID_VEHICULO)
        {
            string cmdText = "INSERT INTO REGISTRO_PERSONA(NOM_PERSONA, MATRICULA, NSS, CURP, TEL, DIR, GRAD_GRUPO, NO_EST, ID_VEHICULO2) " +
                             "VALUES(@NOM_PERSONA, @MATRICULA, @NSS, @CURP, @TEL, @DIR, @GRAD_GRUPO, @NO_EST, @ID_VEHICULO2)";
            bool resultado = false;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos.");

                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@NOM_PERSONA", NOM_PERSONA);
                        cmdDB.Parameters.AddWithValue("@MATRICULA", MATRICULA);
                        cmdDB.Parameters.AddWithValue("@NSS", NSS);
                        cmdDB.Parameters.AddWithValue("@CURP", CURP);
                        cmdDB.Parameters.AddWithValue("@TEL", TEL);
                        cmdDB.Parameters.AddWithValue("@DIR", DIR);
                        cmdDB.Parameters.AddWithValue("@GRAD_GRUPO", GRAD_GRUPO);
                        cmdDB.Parameters.AddWithValue("@NO_EST", NO_EST);
                        cmdDB.Parameters.AddWithValue("@ID_VEHICULO2", ID_VEHICULO);

                        if (cmdDB.ExecuteNonQuery() == 1)
                            resultado = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al insertar nueva persona: " + e.Message);
            }

            return resultado;
        }

        public static bool UpdatePersonaData(string MATRICULA, string NOM_PERSONA, string NSS, string CURP, string TEL, string DIR, string GRAD_GRUPO, string NO_EST, int ID_VEHICULO)
        {
            string cmdText = "UPDATE REGISTRO_PERSONA " +
                             "SET NOM_PERSONA = @NOM_PERSONA, NSS = @NSS, CURP = @CURP, TEL = @TEL, DIR = @DIR, GRAD_GRUPO = @GRAD_GRUPO, NO_EST = @NO_EST, ID_VEHICULO2 = @ID_VEHICULO2 " +
                             "WHERE MATRICULA = @CLAVE";
            bool resultado = false;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos.");

                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@NOM_PERSONA", NOM_PERSONA);
                        cmdDB.Parameters.AddWithValue("@NSS", NSS);
                        cmdDB.Parameters.AddWithValue("@CURP", CURP);
                        cmdDB.Parameters.AddWithValue("@TEL", TEL);
                        cmdDB.Parameters.AddWithValue("@DIR", DIR);
                        cmdDB.Parameters.AddWithValue("@GRAD_GRUPO", GRAD_GRUPO);
                        cmdDB.Parameters.AddWithValue("@NO_EST", NO_EST);
                        cmdDB.Parameters.AddWithValue("@ID_VEHICULO2", ID_VEHICULO);
                        cmdDB.Parameters.AddWithValue("@CLAVE", MATRICULA);

                        if (cmdDB.ExecuteNonQuery() == 1)
                            resultado = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al actualizar datos de persona: " + e.Message);
            }

            return resultado;
        }

        public static bool DeletePersona(string clave)
        {
            string cmdText = "DELETE FROM REGISTRO_PERSONA WHERE MATRICULA = @CLAVE";
            bool resultado = false;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    if (conn == null || conn.State != ConnectionState.Open)
                        throw new Exception("No se pudo establecer la conexión con la base de datos.");

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
                throw new Exception("Error al eliminar persona: " + e.Message);
            }

            return resultado;
        }
        #endregion
    }
}
