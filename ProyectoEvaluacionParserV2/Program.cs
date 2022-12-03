// See https://aka.ms/new-console-template for more information
using Antlr4.Runtime;
using ProyectoEvaluacionParcialParser;
using ProyectoEvaluacionParserV2;
using ProyectoEvaluacionParserV2.Model;

internal class Program
{
    private static void Main(string[] args)
    {
        ICharStream stream = CharStreams.fromPath(@"..\..\..\Automata.txt");
        AUTOMATALexer lexer = new AUTOMATALexer(stream);
        CommonTokenStream token = new CommonTokenStream(lexer);
        AUTOMATAParser parser = new AUTOMATAParser(token);
        AUTOMATAParser.AutomataContext tree = parser.automata();
        AutomataHomeworkVisitor automata = new AutomataHomeworkVisitor();
        Automata result = (Automata)automata.Visit(tree);
        Console.WriteLine(result.GenerateDoc());
    }
}

//var circle = automata.circle;
//var doublecircle = automata.doublecircle;
//GraphvizFormat graphviz = new GraphvizFormat(doublecircle, circle);

//Console.WriteLine(graphviz.result);

