using GPConverter.Models.Enums;

namespace GPConverter;

public class Application
{
    public async Task Main(string[] args)
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
            return;
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
            return;
        }

        var folderPath = "";

        if (args.Length == 3)
        {
            folderPath = args[2];
        }

        else
        {
            var dialogResult = BrowseFolders();
            if (dialogResult == "Cancelled")
            {
                Console.WriteLine("Folder selection interrupted!");
                return;
            }
            folderPath = dialogResult;
        }
        
        // TODO: implement logic here
        Console.Write("Press any key to continue...");
        Console.ReadKey();
    }
    
    private string BrowseFolders()
    {
        var dialog = new FolderBrowserDialog();
        var result = dialog.ShowDialog();
        return DialogResult.OK == result ? dialog.SelectedPath : "Cancelled";
    }
}
