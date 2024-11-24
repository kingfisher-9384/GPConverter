using System.Drawing.Imaging;
using System.Reflection;
using GPConverter.Models.Enums;

namespace GPConverter.Utilities;

public static class HelperFunctions
{
    public static ConversionType GetConversionType(this FileType input)
    {
        switch ((int)input)
        {
            case < 100:
                break;
            case < 200:
                return ConversionType.Image;
            case < 300:
                return ConversionType.Video;
            case < 400:
                return ConversionType.Audio;
        }

        return ConversionType.Image;
    }

    public static ImageFormat ParseImageFormat(this string input)
    {
        try
        {
            input = input.Replace(".", "");
            
            var converter = new ImageFormatConverter();
            
            return (ImageFormat)converter.ConvertFromString(input)! ?? throw new InvalidOperationException();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return ImageFormat.Png;
        }
    }

    public static void EnsurePathExists(this string path)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path) ?? throw new InvalidOperationException());
    }
}
