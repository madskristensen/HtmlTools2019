using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;

namespace HtmlTools
{
    public static class ImageHelper
    {
        private static readonly IVsImageService2 _imageService;

        static ImageHelper()
        {
            _imageService = Package.GetGlobalService(typeof(SVsImageService)) as IVsImageService2;
        }

        public static BitmapSource GetImage(ImageMoniker moniker)
        {
            var imageAttributes = new ImageAttributes()
            {
                Flags = (uint)_ImageAttributesFlags.IAF_RequiredFlags,
                ImageType = (uint)_UIImageType.IT_Bitmap,
                Format = (uint)_UIDataFormat.DF_WPF,
                LogicalHeight = 16,
                LogicalWidth = 16,
                StructSize = Marshal.SizeOf(typeof(ImageAttributes))
            };

            IVsUIObject result = _imageService.GetImage(moniker, imageAttributes);
            result.get_Data(out object data);

            if (data == null)
            {
                return null;
            }

            return data as BitmapSource;
        }
    }
}