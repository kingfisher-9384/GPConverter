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
}