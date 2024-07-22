using System;
using System.Drawing;
using System.IO;
using iTextSharpImage = iTextSharp.text.Image;

namespace SistemaMotos.Clases.entidades
{
    internal class Imagenes
    {
        /// <summary>
        /// Convierte una imagen en un arreglo de bytes.
        /// </summary>
        /// <param name="img">Imagen que convertiremos en un arreglo de bytes.</param>
        /// <returns>Un arreglo de bytes de la imagen ingresada.</returns>
        public static byte[] ImagenToByteArray(System.Drawing.Image img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Convierte un arreglo de bytes en una imagen.
        /// </summary>
        /// <param name="byteImage">Arreglo de bytes.</param>
        /// <returns>Una imagen construida de un arreglo de bytes.</returns>
        public static System.Drawing.Image ByteArrayToImagen(byte[] byteImage)
        {
            using (var ms = new MemoryStream(byteImage))
            {
                return System.Drawing.Image.FromStream(ms);
            }
        }

        /// <summary>
        /// Genera una imagen para colocar en un archivo PDF.
        /// </summary>
        /// <param name="img">Imagen que convertiremos a una imagen para PDF.</param>
        /// <returns>Una imagen de iTextSharp para PDF.</returns>
        public static iTextSharpImage ImgToPDF(Bitmap img)
        {
            using (var ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return iTextSharpImage.GetInstance(ms.ToArray());
            }
        }
    }
}
