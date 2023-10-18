using CommandLine;
using System;

class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(opts => RunOptions(opts))
            .WithNotParsed<Options>((errs) => HandleParseError(errs));
    }

    static void RunOptions(Options opts)
    {
        if (opts.countBytes)
        {
            Console.WriteLine($"{new FileInfo(opts.FilePath).Length} {Path.GetFileName(opts.FilePath)}");
        }
        else if (opts.countChars)
        {
        }
        else if (opts.countLines)
        {
            Console.WriteLine($"{File.ReadLines(opts.FilePath).Count()} {Path.GetFileName(opts.FilePath)}");
        }
        else if (opts.countWords)
        {
            var wordCount = 0;
            using (var reader = new StreamReader(opts.FilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    wordCount += line.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
                }
            }
            
            Console.WriteLine($"{wordCount} {Path.GetFileName(opts.FilePath)}");
        }
    }

    static void HandleParseError(IEnumerable<Error> errs)
    {
        // Your code here
    }
}

class Options
{
    [Option('c', "bytes", Required = false, HelpText = "Print the byte counts")]
    public bool countBytes { get; set; }

    [Option('m', "chars", Required = false, HelpText = "Print the character counts")]
    public bool countChars { get; set; }

    [Option('l', "lines", Required = false, HelpText = "Print the newline counts")]
    public bool countLines { get; set; }
    
    [Option('w', "words", Required = false, HelpText = "Print the word counts")]
    public bool countWords { get; set; }

    [Value(0, Required = true, HelpText = "The file to process")]
    public string FilePath { get; set; }
}