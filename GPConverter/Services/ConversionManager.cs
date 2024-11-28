using GPConverter.Interfaces;
using GPConverter.Models;
using GPConverter.Models.Enums;
using ImageMagick;
using Microsoft.Extensions.Configuration;

namespace GPConverter.Services;

public class ConversionManager(IImageFileConversions imageFileConversions, IConfiguration configuration) : IConversionManager
{
    public string? Convert(ConversionParameters parameters)
    {
        string? outputPath = null;
        
        foreach (var filePath in parameters.InputFilePaths)
        {
            var inputFilePath = new FileInfo(filePath);
            
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            
            switch (parameters.ConversionType)
            {
                case ConversionType.Image:
                    using (var img = new MagickImage(filePath))
                    {
                        var outputImagePath = configuration["Converter:Path:ImageOutput"];
                        outputPath = outputImagePath;

                        imageFileConversions.SaveToPathWithFormat(img, GetAvailablePath(
                            outputImagePath + fileName + "." + parameters.OutputFileType.ToString().ToLower()));
                        continue;
                    } 
                    
                case ConversionType.Video:
                    // TODO implement support for video
                    
                    var outputVideoPath = configuration["Converter:Path:ImageOutput"];
                    outputPath = outputVideoPath;
                    break;
                
                case ConversionType.Audio:
                    // TODO implement support for audio
                    
                    var outputAudioPath = configuration["Converter:Path:ImageOutput"];
                    outputPath = outputAudioPath;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(paramName: nameof(parameters.ConversionType), message: "Invalid conversion type");
            }
        }
        return outputPath;
    }

    private static string GetAvailablePath(string path)
    {
        if (!File.Exists(path))
        {
            return path;
        }
        
        var extension = Path.GetExtension(path);
        path = path.Replace(extension, "");
        
        for (var i = 1; i < 100; i++)
        {
            var newPath = path + " (" + i + ")" + extension;
            if (!File.Exists(newPath))
            {
                return newPath;
            }
        }

        return "Error!"; 
    }
}
