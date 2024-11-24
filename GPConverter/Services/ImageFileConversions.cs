using System.Drawing.Imaging;
using GPConverter.Interfaces;
using GPConverter.Utilities;

namespace GPConverter.Services;

public class ImageFileConversions : IImageFileConversions
{
    public Image ConvertToImage(byte[] imageBytes)
    {
        var stream = new MemoryStream(imageBytes);

        Image img = new Bitmap(stream);
        return img;
    }

    public byte[] ConvertToByteArray(Image img)
    {
        using var stream = new MemoryStream();

        img.Save(stream, img.RawFormat);
        return stream.ToArray();
    }
    
    public void SaveToPathWithFormat(Image img, string path)
    {
        var extension = Path.GetExtension(path);
        
        path.EnsurePathExists();

        if (extension is ".jpg" or ".jpeg" or ".jif" or ".jfif" or ".jpe" or ".jfi")
        {
            SaveToPathJpeg(img, path);
            return;
        }
        
        img.Save(path, extension.ParseImageFormat());
    }

    private void SaveToPathJpeg(Image img, string path)
    {
        using var encoderParameters = new EncoderParameters(1);
        using var encoderParameter = new EncoderParameter(Encoder.Quality, 100L);
        
        var codecInfo = ImageCodecInfo.GetImageEncoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid);
        encoderParameters.Param[0] = encoderParameter;
        img.Save(path, codecInfo, encoderParameters);
    }
}
