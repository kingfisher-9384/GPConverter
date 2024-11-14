using GPConverter.Models.Enums;

namespace GPConverter.Models;

public record ConversionParameters
{
    public ConversionType ConversionType { get; set; }
    
    public FileType InputFileType { get; set; }
    
    public FileType OutputFileType { get; set; }
    
    public List<string> InputFilePaths { get; set; }
}