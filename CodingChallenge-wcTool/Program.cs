using CommandLine;

class Program
{
    private static uint totalLines;
    private static uint totalWords;
    private static uint totalChars;
    private static uint totalBytes;

    private static bool printLines;
    private static bool printWords;
    private static bool printChars;
    private static bool printBytes;

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
            printBytes = true;
        }

        if (opts.countChars)
        {
            printChars = true;
        }

        if (opts.countLines)
        {
            printLines = true;
        }

        if (opts.countWords)
        {
            printWords = true;
        }

        if (!(printBytes || printChars || printLines || printWords))
        {
            printBytes = true;
            printLines = true;
            printWords = true;
        }

        if (printBytes)
        {
            GetByteCount(opts.FilePath);
        }

        if (printLines)
        {
            GetLineCount(opts.FilePath);
        }

        if (printWords)
        {
            GetWordCount(opts.FilePath);
        }

        WriteCounts(totalLines, totalWords, totalChars, totalBytes, opts.FilePath);
    }

    static void HandleParseError(IEnumerable<Error> errs)
    {
    }

    static void GetByteCount(string filePath)
    {
        totalBytes += (uint)new FileInfo(filePath).Length;
    }

    static void GetLineCount(string filePath)
    {
        totalLines += (uint)File.ReadLines(filePath).Count();
    }

    static void GetWordCount(string filePath)
    {
        uint wordCount = 0;
        using var reader = new StreamReader(filePath);
        while (reader.ReadLine() is { } line)
        {
            wordCount += (uint)line.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Length;
        }

        totalWords += wordCount;
    }

    private static void WriteCounts(uint lines, uint words, uint chars, uint bytes, string file)
    {
        if (printBytes)
        {
            Console.Write($"{bytes} ");
        }
        
        if (printLines)
        {
            Console.Write($"{lines} ");
        }

        if (printWords)
        {
            Console.Write($"{words} ");
        }

        if (printChars)
        {
            Console.Write($"{chars} ");
        }

        if (!string.IsNullOrWhiteSpace(file))
        {
            Console.Write($"{file} ");
        }
        
        Console.WriteLine();
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
    
    [Value(0, Required = false, HelpText = "The file to process")]
    public string FilePath { get; set; }
}