using System;
using System.Data;
using System.Data.SQLite;
using SistemaViviendas.Clases.ConectorDB;

namespace SistemaMotos.Clases.entidades
{
    internal class Usuario
    {
        #region Atributos
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        #endregion

        #region Constructor
        public Usuario(int iD, string nombre, string password)
        {
            ID = iD;
            Nombre = nombre;
            Password = password;
        }

        public Usuario() { }
        #endregion

        #region Metodos
        public static Usuario GetUsuario(int clave)
        {
            string cmdText = "SELECT * FROM USUARIO WHERE ID_USUARIO = @CLAVE";
            Usuario usuario = null;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@CLAVE", clave);

                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmdDB))
                        {
                            DataSet dataSet = new DataSet();
                            if (da.Fill(dataSet) > 0)
                            {
                                usuario = new Usuario
                                {
                                    ID = Convert.ToInt32(dataSet.Tables[0].Rows[0]["ID_USUARIO"]),
                                    Nombre = dataSet.Tables[0].Rows[0]["NOMBRE"].ToString(),
                                    Password = dataSet.Tables[0].Rows[0]["PASSWORD"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener usuario: " + e.Message);
            }

            return usuario;
        }

        public static Usuario GetUsuario(string nombre, string password)
        {
            string cmdText = "SELECT * FROM USUARIO WHERE NOMBRE = @NOM AND PASSWORD = @PASSWORD";
            Usuario usuario = null;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@NOM", nombre);
                        cmdDB.Parameters.AddWithValue("@PASSWORD", password);

                        using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmdDB))
                        {
                            DataSet dataSet = new DataSet();
                            if (da.Fill(dataSet) > 0)
                            {
                                usuario = new Usuario
                                {
                                    ID = Convert.ToInt32(dataSet.Tables[0].Rows[0]["ID_USUARIO"]),
                                    Nombre = dataSet.Tables[0].Rows[0]["NOMBRE"].ToString(),
                                    Password = dataSet.Tables[0].Rows[0]["PASSWORD"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener usuario: " + e.Message);
            }

            return usuario;
        }

        public static bool NewUsuario(int clave, string nombre, string password)
        {
            string cmdText = "INSERT INTO USUARIO(ID_USUARIO, NOMBRE, PASSWORD) VALUES(@ID, @NOMBRE, @PASSWORD)";
            bool resultado = false;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@ID", clave);
                        cmdDB.Parameters.AddWithValue("@NOMBRE", nombre);
                        cmdDB.Parameters.AddWithValue("@PASSWORD", password);

                        if (cmdDB.ExecuteNonQuery() == 1)
                            resultado = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al insertar nuevo usuario: " + e.Message);
            }

            return resultado;
        }

        public static bool UpdateUsuarioData(int clave, string nombre, string password)
        {
            string cmdText = "UPDATE USUARIO SET PASSWORD = @PASSWORD, NOMBRE = @NOMBRE WHERE ID_USUARIO = @CLAVE";
            bool resultado = false;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
                    using (SQLiteCommand cmdDB = new SQLiteCommand(cmdText, conn))
                    {
                        cmdDB.Parameters.AddWithValue("@PASSWORD", password);
                        cmdDB.Parameters.AddWithValue("@NOMBRE", nombre);
                        cmdDB.Parameters.AddWithValue("@CLAVE", clave);

                        if (cmdDB.ExecuteNonQuery() == 1)
                            resultado = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al actualizar datos de usuario: " + e.Message);
            }

            return resultado;
        }

        public static bool DeleteUsuario(int clave)
        {
            string cmdText = "DELETE FROM USUARIO WHERE ID_USUARIO = @CLAVE";
            bool resultado = false;

            try
            {
                using (SQLiteConnection conn = ConectorSQLite.InitConexion())
                {
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
                throw new Exception("Error al eliminar usuario: " + e.Message);
            }

            return resultado;
        }
        #endregion
    }
}
