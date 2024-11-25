using GPConverter.Interfaces;
using GPConverter.Models;
using GPConverter.Models.Enums;
using ImageMagick;
using Microsoft.Extensions.Configuration;

namespace GPConverter.Services;

public class ConversionManager(IImageFileConversions imageFileConversions, IConfiguration configuration) : IConversionManager
{
    public bool Convert(ConversionParameters parameters)
    {
        foreach (var filePath in parameters.InputFilePaths)
        {
            var inputFilePath = new FileInfo(filePath);
            
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            
            switch (parameters.ConversionType)
            {
                case ConversionType.Image:
                    using (var img = new MagickImage(filePath))
                    {
                        var outputFilePath = configuration["Converter:Path:ImageOutput"];

                        imageFileConversions.SaveToPathWithFormat(img,
                            outputFilePath + fileName + "." + parameters.OutputFileType.ToString().ToLower());
                        continue;
                    } // TODO overwrite file
                    
                case ConversionType.Video:
                    break;
                case ConversionType.Audio:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(parameters.ConversionType), message: "Invalid conversion type");
            }
        }

        return true;
    }
}
