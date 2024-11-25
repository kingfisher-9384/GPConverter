using System.Drawing.Imaging;
using GPConverter.Interfaces;
using GPConverter.Utilities;
using ImageMagick;

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
    
    public void SaveToPathWithFormat(MagickImage img, string path)
    {
        var extension = Path.GetExtension(path);
        
        path.EnsurePathExists();
        
        img.Format = extension.ParseMagickFormat();

        if (extension is ".jpg" or ".jpeg" or ".jif" or ".jfif" or ".jpe" or ".jfi")
        {
            if (img.Format == MagickFormat.Png)
            {
                img.Format = MagickFormat.Jpeg;
            }

            img.Quality = 100;
        }
        
        img.Write(path);
    }
}
