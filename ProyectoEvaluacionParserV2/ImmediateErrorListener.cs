using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoEvaluacionParserV2
{
    internal class ImmediateErrorListener : BaseErrorListener
    {
        static Lazy<ImmediateErrorListener> instance = new Lazy<ImmediateErrorListener>(() => new ImmediateErrorListener());

        public static ImmediateErrorListener Instance
        {
            get { return instance.Value; }
        }

        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new Antlr4.Runtime.Misc.ParseCanceledException($"Error in line" +
                $" {line}:{charPositionInLine}: {msg}");
        }
    }
}
