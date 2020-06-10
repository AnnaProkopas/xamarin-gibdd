using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Gibdd
{
    public interface IPhotographerPlatform
    {
        void TakePhoto();
        bool IsCameraAvailable();
        Action<byte[]> PhotoCallback { get; set; }
        void SaveImage(byte[] data);
    }
}
