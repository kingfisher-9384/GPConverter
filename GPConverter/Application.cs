using GPConverter.Interfaces;
using GPConverter.Models;
using GPConverter.Models.Enums;
using GPConverter.Utilities;

namespace GPConverter;

public class Application(IConversionManager _conversionManager)
{
    public async Task Main(string[] args)
    {
        var parameters = ParseUserInput(args);
        if (parameters != null) _conversionManager.Convert(parameters);
        AnyKeyPrompt();
    }

    private ConversionParameters? ParseUserInput(string[] args)
    {
        var inputType = "";
        
        if (args.Length < 2)
        {
            Console.WriteLine("Input-file type: ");
            inputType = Console.ReadLine()?.ToUpper();
        }
        else
        {
            inputType = args[0];
        }
        
        if (!Enum.TryParse(inputType, out FileType inputFileType))
        {
            Console.WriteLine("Invalid input-file type!");
            return null;
        }
        
        var outputType = "";
        
        if (args.Length < 2)
        {
            Console.WriteLine("Output-file type: ");
            outputType = Console.ReadLine()?.ToUpper();
        }
        else
        {
            outputType = args[1];
        }

        if (!Enum.TryParse(outputType, out FileType outputFileType))
        {
            Console.WriteLine("Invalid output-file type!");
            return null;
        }

        if (outputFileType.GetConversionType() != inputFileType.GetConversionType())
        {
            Console.WriteLine("Cannot convert different format categories!");
            return null;
        }
        
        if (outputFileType.Equals(inputFileType))
        {
            Console.WriteLine("Cannot convert identical file types!");
            
            return null;
        }

        var fileNames = new List<string>();

        if (args.Length == 3)
        {
            fileNames = [args[2]];  // TODO kunnon käsittely tälle tää on ihan kauhee paskakasa
        }

        else
        {
            var dialogResult = BrowseFiles(inputFileType);
            if (dialogResult[0] == "Cancelled")
            {
                Console.WriteLine("File selection interrupted!");
                return null;
            }
            fileNames = dialogResult.ToList();
        }

        return new ConversionParameters
        {
            ConversionType = inputFileType.GetConversionType(),
            InputFileType = inputFileType,
            OutputFileType = outputFileType,
            InputFilePaths = fileNames
        };
    }
    
    private string[] BrowseFiles(FileType type)
    {
        var dialog = new OpenFileDialog();

        dialog.Multiselect = true;
        dialog.Filter = "Selected type|*." + type.ToString().ToLower();
        
        var result = dialog.ShowDialog();
        return DialogResult.OK == result ? dialog.FileNames : ["Cancelled"];
    }
    
    private void AnyKeyPrompt()
    {
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
}
