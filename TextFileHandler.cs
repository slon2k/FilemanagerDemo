namespace FilemanagerDemo;
public class TextFileHandler : IFileHandler
{
    public void HandleFile(string source, string destination)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(source));
        ArgumentException.ThrowIfNullOrEmpty(nameof(destination));

        if (!File.Exists(source))
        {
            throw new FileNotFoundException();
        }

        using var inputFileStream = new FileStream(source, FileMode.Open);
        using var inputStreamReader = new StreamReader(inputFileStream);

        using var outputFileStream = new FileStream(destination, FileMode.CreateNew);
        using var outputStreamWriter = new StreamWriter(outputFileStream);

        while (!inputStreamReader.EndOfStream)
        {
            string inputLine = inputStreamReader.ReadLine() ?? "";
            string outputLine = inputLine.ToUpperInvariant();
            outputStreamWriter.WriteLine(outputLine);
        }
    }
}
