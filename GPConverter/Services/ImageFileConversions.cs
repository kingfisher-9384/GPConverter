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
        
        
        img.Save(path, extension.ParseImageFormat());
    }
}