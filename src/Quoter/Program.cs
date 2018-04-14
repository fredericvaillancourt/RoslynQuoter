using System;
using System.IO;
using Microsoft.CodeAnalysis.CSharp;
using RoslynQuoter;

namespace QuoterHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceText = File.ReadAllText(args[0]);
            var sourceNode = CSharpSyntaxTree.ParseText(sourceText).GetRoot() as CSharpSyntaxNode;
            var quoter = new Quoter
            {
                OpenParenthesisOnNewLine = false,
                ClosingParenthesisOnNewLine = false,
                UseDefaultFormatting = true,
                ShortenCodeWithUsingStatic = true
            };

            var generatedNode = quoter.Quote(sourceNode);
            var generatedCode = quoter.Print(generatedNode);
            var code = quoter.Evaluate(generatedNode);

            Console.WriteLine(generatedCode);
        }
    }
}
