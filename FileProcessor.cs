namespace FilemanagerDemo;

public class FileProcessor
{
    private readonly string backupFolderPath;
    private readonly string processingFolderPath;
    private readonly string outputFolderPath;

    private readonly Dictionary<string, IFileHandler> handlers = new(); 

    public FileProcessor(string rootFolder, string backupFolder = "backup", string processingFolder = "processing", string outputFolder = "output")
    {
        if (!Directory.Exists(rootFolder))
        {
            throw new DirectoryNotFoundException();
        }

        backupFolderPath = Path.Combine(rootFolder, backupFolder);
        processingFolderPath = Path.Combine(rootFolder, processingFolder);
        outputFolderPath = Path.Combine(rootFolder, outputFolder);

        EnsureCreatedFolder();
    }

    public void RegisterHandler(string extension, IFileHandler handler)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(extension));
        ArgumentNullException.ThrowIfNull(handler);

        handlers[extension] = handler;
    }

    private void EnsureCreatedFolder()
    {
        Directory.CreateDirectory(backupFolderPath);
        Directory.CreateDirectory(processingFolderPath);
        Directory.CreateDirectory(outputFolderPath);
    }

    public void ProcessFile(string filePath)
    {
        Console.WriteLine($"Processing file {filePath}");
        EnsureCreatedFolder();

        var fileName = Path.GetFileName(filePath);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
        var fileExtension = Path.GetExtension(filePath);
        var backupFilePath = Path.Combine(backupFolderPath, fileName);
        var processingFilePath = Path.Combine(processingFolderPath, fileName);
        var outputFilePath = Path.Combine(outputFolderPath, $"{fileNameWithoutExtension}.{Guid.NewGuid()}{fileExtension}");

        //TODO: Copy the file to the backup directory
        Console.WriteLine("Creating backup");
        File.Copy(filePath, backupFilePath, true);

        //TODO: Move the file to the processing folder
        Console.WriteLine("Moving file");
        File.Move(filePath, processingFilePath);

        //TODO: Change the file and save the updated file to the complete folder
        Console.WriteLine("Handling file");
        if (!handlers.ContainsKey(fileExtension))
        {
            Console.WriteLine($"Type {fileExtension} is not supported");
        } else
        {
            handlers[fileExtension].HandleFile(processingFilePath, outputFilePath);
        }

        //TODO: Delete the file from the processing folder
        Console.WriteLine("Deleting file");
        File.Delete(processingFilePath);
    }
}
