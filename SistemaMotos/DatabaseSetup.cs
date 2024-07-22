using System;
using System.Data.SQLite;

namespace SistemaMotos
{
    internal class DatabaseSetup
    {
        public static void InitializeDatabase()
        {
            string dbFilePath = SistemaMotos.Properties.Settings.Default.dbFilePath;
            string connectionString = $"Data Source={dbFilePath};Version=3;";

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS USUARIO (
                    ID_USUARIO INTEGER PRIMARY KEY AUTOINCREMENT,
                    NOMBRE TEXT NOT NULL,
                    PASSWORD TEXT NOT NULL
                );
                
                INSERT INTO USUARIO (NOMBRE, PASSWORD) VALUES ('CARLOS MANUEL', '1234');
                INSERT INTO USUARIO (NOMBRE, PASSWORD) VALUES ('ALEJANDRO', '123456');
                
                CREATE TABLE IF NOT EXISTS DOCUMENTO_VEHICULO (
                    ID_DOC INTEGER PRIMARY KEY AUTOINCREMENT,
                    DESCRIPCION TEXT
                );

                INSERT INTO DOCUMENTO_VEHICULO (DESCRIPCION) VALUES ('TARJETON'), ('CARTA RESPONSIVA'), ('PERMISO'), ('COMPROBANTE');

                CREATE TABLE IF NOT EXISTS VEHICULO (
                    ID_VEHICULO INTEGER PRIMARY KEY AUTOINCREMENT,
                    NOM_VEHICULO TEXT NOT NULL,
                    MODELO_VEHICULO TEXT NOT NULL,
                    MARCA TEXT NOT NULL,
                    PLACAS TEXT NOT NULL,
                    NIV TEXT NOT NULL,
                    ID_DOC2 INTEGER,
                    FOREIGN KEY(ID_DOC2) REFERENCES DOCUMENTO_VEHICULO(ID_DOC) ON DELETE CASCADE ON UPDATE CASCADE
                );

                CREATE TABLE IF NOT EXISTS REGISTRO_PERSONA (
                    ID_PERSONA INTEGER PRIMARY KEY AUTOINCREMENT,
                    NOM_PERSONA TEXT NOT NULL,
                    MATRICULA TEXT NOT NULL,
                    NSS TEXT NOT NULL,
                    CURP TEXT NOT NULL,
                    TEL TEXT,
                    DIR TEXT,
                    GRAD_GRUPO TEXT,
                    NO_EST TEXT,
                    ID_VEHICULO2 INTEGER,
                    FOREIGN KEY(ID_VEHICULO2) REFERENCES VEHICULO(ID_VEHICULO) ON DELETE CASCADE ON UPDATE CASCADE
                );

                CREATE TABLE IF NOT EXISTS REG_ENTRADA (
                    ID_REG INTEGER PRIMARY KEY AUTOINCREMENT,
                    FECHA_HORA_REG DATETIME NOT NULL,
                    ID_PERSONA2 INTEGER,
                    FOREIGN KEY(ID_PERSONA2) REFERENCES REGISTRO_PERSONA(ID_PERSONA) ON DELETE CASCADE ON UPDATE CASCADE
                );

                CREATE TABLE IF NOT EXISTS REG_SALIDA (
                    ID_REG INTEGER PRIMARY KEY AUTOINCREMENT,
                    FECHA_HORA_REG DATETIME NOT NULL,
                    ID_PERSONA2 INTEGER,
                    FOREIGN KEY(ID_PERSONA2) REFERENCES REGISTRO_PERSONA(ID_PERSONA) ON DELETE CASCADE ON UPDATE CASCADE
                );

                CREATE VIEW IF NOT EXISTS REPORTE_ENTRADA AS
                SELECT FECHA_HORA_REG, NOM_VEHICULO, PLACAS, DESCRIPCION AS DOCUMENTO, NO_EST, NOM_PERSONA, MATRICULA, GRAD_GRUPO
                FROM REGISTRO_PERSONA
                INNER JOIN REG_ENTRADA ON ID_PERSONA2 = ID_PERSONA
                INNER JOIN VEHICULO ON ID_VEHICULO2 = ID_VEHICULO
                INNER JOIN DOCUMENTO_VEHICULO ON ID_DOC2 = ID_DOC;

                CREATE VIEW IF NOT EXISTS REPORTE_SALIDA AS
                SELECT FECHA_HORA_REG, NOM_VEHICULO, PLACAS, DESCRIPCION AS DOCUMENTO, NO_EST, NOM_PERSONA, MATRICULA, GRAD_GRUPO
                FROM REGISTRO_PERSONA
                INNER JOIN REG_SALIDA ON ID_PERSONA2 = ID_PERSONA
                INNER JOIN VEHICULO ON ID_VEHICULO2 = ID_VEHICULO
                INNER JOIN DOCUMENTO_VEHICULO ON ID_DOC2 = ID_DOC;

                CREATE VIEW IF NOT EXISTS DATOS_PERSONA_VEHICULO AS
                SELECT NOM_VEHICULO, PLACAS, DESCRIPCION AS DOCUMENTO, NO_EST, NOM_PERSONA, MATRICULA, GRAD_GRUPO
                FROM REGISTRO_PERSONA
                INNER JOIN VEHICULO ON ID_VEHICULO2 = ID_VEHICULO
                INNER JOIN DOCUMENTO_VEHICULO ON ID_DOC2 = ID_DOC;
                ";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
