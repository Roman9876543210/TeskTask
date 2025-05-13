using System.Globalization;
using System.Text.RegularExpressions;

namespace TestTask;

public class Task3
{
    static readonly string inputPath = "input.txt";
    static readonly string outputPath = "output.txt";
    static readonly string problemsPath = "problems.txt";

    static readonly Dictionary<string, string> levelMap = new()
    {
        {"INFORMATION", "INFO"},
        {"INFO", "INFO"},
        {"WARNING", "WARN"},
        {"WARN", "WARN"},
        {"ERROR", "ERROR"},
        {"DEBUG", "DEBUG"}
    };

    public static void Execute()
    {
        Execute(inputPath, outputPath, problemsPath);
    }
    
    public static void Execute(string inputPath, string outputPath, string problemsPath)
    {
        var lines = File.ReadAllLines(inputPath);
        using var outputWriter = new StreamWriter(outputPath);
        using var problemsWriter = new StreamWriter(problemsPath);

        foreach (var line in lines)
        {
            if (TryParseFormat1(line, out var formatted1) )
            {
                outputWriter.WriteLine(formatted1);
            }
            else if (TryParseFormat2(line, out var formatted2))
            {
                
                outputWriter.WriteLine(formatted2);
            }
            else
            {
                problemsWriter.WriteLine(line);
            }
        }

        Console.WriteLine("Обработка завершена.");
    }

    static bool TryParseFormat1(string line, out string? formatted)
    {
        formatted = null;
        var match = Regex.Match(line, 
            @"^(\d{2}\.\d{2}\.\d{4}) (\d{2}:\d{2}:\d{2}\.\d+) (INFORMATION|WARNING|ERROR|DEBUG) (.+)$");

        if (!match.Success)
            return false;

        string dateStr = match.Groups[1].Value;
        string time = match.Groups[2].Value;
        string level = match.Groups[3].Value;
        string message = match.Groups[4].Value;

        if (!DateTime.TryParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            return false;

        string formattedDate = date.ToString("dd-MM-yyyy");
        string levelMapped = levelMap.GetValueOrDefault(level, level);
        formatted = $"{formattedDate}\t{time}\t{levelMapped}\tDEFAULT\t{message}";
        return true;
    }

    static bool TryParseFormat2(string line, out string? formatted)
    {
        formatted = null;
        var match = Regex.Match(line,
            @"^(\d{4}-\d{2}-\d{2}) (\d{2}:\d{2}:\d{2}\.\d+)\| (INFO|WARN|ERROR|DEBUG)\|\d+\|([^|]+)\| (.+)$");

        if (!match.Success)
            return false;

        string dateStr = match.Groups[1].Value;
        string time = match.Groups[2].Value;
        string level = match.Groups[3].Value;
        string method = match.Groups[4].Value;
        string message = match.Groups[5].Value;

        if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            return false;

        string formattedDate = date.ToString("dd-MM-yyyy");
        string levelMapped = levelMap.GetValueOrDefault(level, level);
        formatted = $"{formattedDate}\t{time}\t{levelMapped}\t{method}\t{message}";
        return true;
    }
}