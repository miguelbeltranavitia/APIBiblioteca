using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace apiWebUJED
{
    public class image
    {
        internal static string resize(byte[] imageBytes)
        {
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {

                using (Image img = Image.FromStream(ms))
                {
                    int w = 150;
                    int h = 204;

                    using (Bitmap b = new Bitmap(img, new Size(w, h)))
                    {
                        using (MemoryStream ms2 = new MemoryStream())
                        {
                            b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                            byte[] imgbytes = ms2.ToArray();

                            string base64String = Convert.ToBase64String(imgbytes);
                            string url = "data:image/png;base64," + base64String;
                            return url;
                        }
                    }
                }
            }
        }
        internal static string empty()
        {
            string vacio = "data:image/jpeg;base64,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";
            return vacio;
        }
    }
}