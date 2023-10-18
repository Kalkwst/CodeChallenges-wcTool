# CodeChallenges - C# wc Unix Function

## Overview

This repository contains a C# implementation of the `wc` Unix function, a command-line utility that counts lines, words, 
characters, and bytes in a given input file or standard input. The implementation includes command-line argument parsing, 
file reading, and count calculations.

## Features
- Count lines, words, characters and bytes in a text file or standard input.
- Customize which counts to display using command-line options.
- Implements the basic functionality of the Unix `wc` command.

## Usage

To use the `wc` C# implementation, follow these steps:

1. Clone or download this repository to your local machine
2. Open the solution in your preferred C# development environment (e.g., Visual Studio) and build the solution. Alternatively, you can use the `dotnet build CodingChallenge-wcTool.sln` command.
3. Navigate to the project's build output directory.
4. Run the `wc` program with the desired options and provide a file path as an argument.

For ecample, to count lines and words in a file named `example.txt`, you can use the following command:
```sh
dotnet wc.dll -l -w example.txt
```

For a full list of available options and usage, run:
```sh
dotnet wc.dll --help
```

## Command-Line Options
- `-c`, `--bytes`: Print byte counts.
- `-m`, `--chars`: Print character counts.
- `-l`, `--lines`: Print newline counts.
- `-w`, `--words`: Print word counts.
- `-h`, `--help`: Display help and usage information.

## Contribution (Please Read)
Thank you for your interest in this project, which was created as a personal side project in my spare time. While I appreciate your enthusiasm and willingness to contribute, please note that this project is not intended for production-level code or extensive collaboration.

As such, I regret to inform you that I will not be accepting any pull requests (PRs) or issues for this project. My intention in sharing this code is primarily for educational purposes and as an example of a specific coding challenge.

I encourage you to explore and learn from the code, and if you have any questions or would like to discuss any aspect of the project, feel free to reach out. Your understanding is greatly appreciated.

Enjoy coding and happy learning!

## License
This project is licensed under the Apache License. See the [LICENSE](./LICENSE.md) file for details.

## Acknowledgements
- This project was created as part of a coding challenge.
- The `CommandLineParser` library was used for handling command-line arguments.