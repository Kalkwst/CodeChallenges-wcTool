using System.Text;
using CommandLine;

class Program
{
    private static uint _totalLines;
    private static uint _totalWords;
    private static uint _totalChars;
    private static uint _totalBytes;

    private static string _data = string.Empty;

    private static bool _printLines;
    private static bool _printWords;
    private static bool _printChars;
    private static bool _printBytes;

    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(opts => RunOptions(opts))
            .WithNotParsed<Options>((errs) => HandleParseError(errs));
    }

    private static void RunOptions(Options opts)
    {
        if (opts.showHelp)
        {
            DisplayHelp();
            return;
        }
        
        if (opts.countBytes)
        {
            _printBytes = true;
        }

        if (opts.countChars)
        {
            _printChars = true;
        }

        if (opts.countLines)
        {
            _printLines = true;
        }

        if (opts.countWords)
        {
            _printWords = true;
        }

        if (!string.IsNullOrEmpty(opts.FilePath))
        {
            _data = File.ReadAllText(opts.FilePath);
        }
        else
        {
            while (Console.ReadLine()! is { } line)
            {
                _data += line + "\r\n";
            }
        }

        if (!(_printBytes || _printChars || _printLines || _printWords))
        {
            _printBytes = true;
            _printLines = true;
            _printWords = true;
        }

        if (_printBytes)
        {
            GetByteCount(_data);
        }

        if (_printChars)
        {
            GetCharacterCount(_data);
        }

        if (_printLines)
        {
            GetLineCount(_data);
        }

        if (_printWords)
        {
            GetWordCount(_data);
        }

        WriteCounts(_totalLines, _totalWords, _totalChars, _totalBytes, opts.FilePath);
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
    }

    private static void GetByteCount(string data)
    {
        _totalBytes += (uint)Encoding.Default.GetByteCount(data);
    }

    private static void GetCharacterCount(string data)
    {
        _totalChars += (uint)data.Length;
    }

    private static void GetLineCount(string data)
    {
        _totalLines += (uint)data.Split(new string[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries)
            .Length;
    }

    private static void GetWordCount(string data)
    {
        _totalWords += (uint)data.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
            .Length -1;
    }

    private static void DisplayHelp()
    {
        var programName = "wc";
        Console.WriteLine("Usage: [OPTION] [FILE]");
        Console.WriteLine("Print newline, word, and byte counts for the FILE. A word is a nonempty sequence of non white");
        Console.WriteLine("space delimited by white space characters or by start or end of input.");
        Console.WriteLine("With no FILE, read standard input.");
        Console.WriteLine("The options below may be used to select witch counts are printed, always in");
        Console.WriteLine("the following order: newline, word, character, byte.");
        Console.WriteLine("  -c, --bytes            print the byte counts");
        Console.WriteLine("  -m, --chars            print the character counts");
        Console.WriteLine("  -l, --lines            print the newline counts");
        Console.WriteLine("  -w, --words            print the word counts");
        Console.WriteLine("  -h, --help             display this help and exit");
    }

    private static void WriteCounts(uint lines, uint words, uint chars, uint bytes, string file)
    {
        if (_printBytes)
        {
            Console.Write($"{bytes} ");
        }

        if (_printChars)
        {
            Console.Write($"{chars}");
        }
        
        if (_printLines)
        {
            Console.Write($"{lines} ");
        }

        if (_printWords)
        {
            Console.Write($"{words} ");
        }

        if (_printChars)
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
    
    [Option('h', "help", Required = false, HelpText = "Display the manual")]
    public bool showHelp { get; set; }
    
    [Value(0, Required = false, HelpText = "The file to process")]
    public string FilePath { get; set; }
}