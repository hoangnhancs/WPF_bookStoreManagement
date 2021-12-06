using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace bookStoreManagetment.Model
{
    static class bitmap2bitmapImage
    {
        public static ImageSource Convert(Bitmap bmp)
        {
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(bmp.GetHbitmap(),IntPtr.Zero,Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }
            
    }
}
