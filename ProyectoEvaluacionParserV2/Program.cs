// See https://aka.ms/new-console-template for more information
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using ProyectoEvaluacionParcialParser;
using ProyectoEvaluacionParserV2;
using ProyectoEvaluacionParserV2.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            ImmediateErrorListener errListener = ImmediateErrorListener.Instance;
            ICharStream stream = CharStreams.fromPath(@"..\..\..\Automata.txt");
            AUTOMATALexer lexer = new AUTOMATALexer(stream);
            CommonTokenStream token = new CommonTokenStream(lexer);
            AUTOMATAParser parser = new AUTOMATAParser(token);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(errListener);
            AUTOMATAParser.AutomataContext tree = parser.automata();
            AutomataHomeworkVisitor automata = new AutomataHomeworkVisitor();
            Automata result = (Automata)automata.Visit(tree);
            Console.WriteLine(result.GenerateDoc());
        }
        catch (ParseCanceledException e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine("Something was wrong in your Automata.txt file...");
            Console.WriteLine("Please try again...");
        }

    }
}

//var circle = automata.circle;
//var doublecircle = automata.doublecircle;
//GraphvizFormat graphviz = new GraphvizFormat(doublecircle, circle);

//Console.WriteLine(graphviz.result);

