using System;
using System.IO;

namespace Quoter.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceText = File.ReadAllText(args[0]);
            var quoter = new RoslynQuoter.Quoter
            {
                OpenParenthesisOnNewLine = false,
                ClosingParenthesisOnNewLine = false,
                UseDefaultFormatting = true,
                ShortenCodeWithUsingStatic = true
            };

            var generatedNode = quoter.Quote(sourceText);
            var generatedCode = quoter.Print(generatedNode);
            var code = quoter.Evaluate(generatedNode);

            Console.WriteLine(generatedCode);
        }
    }
}
