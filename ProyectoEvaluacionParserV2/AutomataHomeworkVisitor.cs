using Antlr4.Runtime.Misc;
using ProyectoEvaluacionParserV2.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ProyectoEvaluacionParserV2
{
    internal class AutomataHomeworkVisitor : AUTOMATABaseVisitor<object>
    {

        public override Automata VisitAutomata([NotNull] AUTOMATAParser.AutomataContext context)
        {
            Automata automata = new Automata();
            List<ImplicitState> sts = context.state().Select((ctx) =>  { return VisitState(ctx); }).ToList();

            // remove duplicates from state
            foreach (ImplicitState implState in sts)
            {
                Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

                foreach (var item in implState.transitions)
                {
                    dict.Add(item.Key, item.Value.Distinct().ToList());
                }
                automata.states.Add(new ImplicitState(implState.node, dict));
            }

            return automata;
        }

        public override Node VisitId([NotNull] AUTOMATAParser.IdContext context)
        {
            NodeProperties props = VisitProp(context.prop());
            string name = VisitState_name(context.state_name());
            return new Node(name, props);
        }

        public override string VisitInput([NotNull] AUTOMATAParser.InputContext context)
        {
            return context.GetText();
        }

        public override NodeProperties VisitProp([NotNull] AUTOMATAParser.PropContext context)
        {
            return new NodeProperties(context.initial() != null, context.acceptance() != null);
        }

        public override ImplicitState VisitState([NotNull] AUTOMATAParser.StateContext context)
        {
            Node node = VisitId(context.id());
            Dictionary<string, List<string>> transitions = VisitTransitions(context.transitions());
            return new ImplicitState(node, transitions);
        }

        public override string VisitState_name([NotNull] AUTOMATAParser.State_nameContext context)
        {
            return context.GetText();
        }

        public override Pair<string, List<string>> VisitTransition([NotNull] AUTOMATAParser.TransitionContext context)
        {
            string input = VisitInput(context.input());
            List<string> outputs = context.state_name().Select((ctx) => { return VisitState_name(ctx); }).ToList();

            return new Pair<string, List<string>>(input, outputs);
        }

        public override Dictionary<string, List<string>> VisitTransitions([NotNull] AUTOMATAParser.TransitionsContext context)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            if (context == null)
                return result;

            List<Pair<string, List<string>>> nodeConnections = context.transition().Select((ctx) => {return VisitTransition(ctx); }).ToList();
            foreach (Pair<string, List<string>> item in nodeConnections)
            {
                if (result.ContainsKey(item.a))
                {
                    result[item.a].AddRange(item.b);
                } else
                {
                    result.Add(item.a, item.b);
                }
            }
            return result;
        }
    }
}
