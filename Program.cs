using FilemanagerDemo;

var currentFolder = Directory.GetCurrentDirectory();
var workFolderName = "input";
var workFolderPath = Path.Combine(currentFolder, workFolderName);

Directory.CreateDirectory(workFolderPath); 

var fileProcessor = new FileProcessor(currentFolder);
fileProcessor.RegisterHandler(".txt", new TextFileHandler());

using var fileWatcher = new FileSystemWatcher(workFolderPath);



FileSystemEventHandler fileCreatedHandler = (object source, FileSystemEventArgs args) =>
{
    Console.WriteLine($"File created: {args.Name}");
    fileProcessor.ProcessFile(args.FullPath);
};

fileWatcher.Created += fileCreatedHandler;
//fileWatcher.Changed += (s, e) => fileProcessor.ProcessFile(e.FullPath);

fileWatcher.EnableRaisingEvents = true;

Console.WriteLine("Waiting for file changes");
Console.ReadLine();
