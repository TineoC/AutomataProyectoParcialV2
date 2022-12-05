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
            // Crea el objeto vacio
            Automata automata = new Automata();

            List<ImplicitState> sts = context.state().Select( 
                // Para todos contextos de estados que encuentre...
                (ctx) =>  {
                    // Conviertelos en estados
                    return VisitState(ctx);
                })        
                .ToList();


            // Para todos los estados...
            foreach (ImplicitState implState in sts)
            {
                // Crea un diccionario intermedio
                Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

                // Para cada transicion
                foreach (var item in implState.transitions)
                {
                    // Solo agrega al diccionario intermedio las transiciones, quitando la repetición de salidas
                    dict.Add(item.Key, item.Value.Distinct().ToList());
                }

                // Agrega el diccionario intermedio a los estados finales del automata
                automata.states.Add(new ImplicitState(implState.node, dict));
            }

            return automata;
        }

        public override Node VisitId([NotNull] AUTOMATAParser.IdContext context)
        {
            // Obten el nombre
            string name = VisitState_name(context.state_name());

            // Obten si es inicial y/o de aceptación
            NodeProperties props = VisitProp(context.prop());
            return new Node(name, props);
        }

        public override string VisitInput([NotNull] AUTOMATAParser.InputContext context)
        {
            // Obten el simbolo necesario para entrar al los estados
            return context.GetText();
        }

        public override NodeProperties VisitProp([NotNull] AUTOMATAParser.PropContext context)
        {
            // Si los contextos son nulos, las propiedades se clasifican como falsas
            return new NodeProperties(context.initial() != null, context.acceptance() != null);
        }

        public override ImplicitState VisitState([NotNull] AUTOMATAParser.StateContext context)
        {
            // Obten los datos del nodo
            Node node = VisitId(context.id());
            // Obten las transiciones
            Dictionary<string, List<string>> transitions = VisitTransitions(context.transitions());
            return new ImplicitState(node, transitions);
        }

        public override string VisitState_name([NotNull] AUTOMATAParser.State_nameContext context)
        {
            // Obten el nombre del estado
            return context.GetText();
        }

        public override Pair<string, List<string>> VisitTransition([NotNull] AUTOMATAParser.TransitionContext context)
        {
            // Obten la entrada que toma esta transicion
            string input = VisitInput(context.input());

            // Obten las salidas que toma esta transicion
            List<string> outputs = context.state_name().Select( // Para cada contexto de nombre de estado que encuentre...
                (ctx) => { return VisitState_name(ctx); })      // Obten el nombre del estado al que irá
                .ToList();

            // Junta la entrada con las salidas y retorna
            return new Pair<string, List<string>>(input, outputs); 
        }

        public override Dictionary<string, List<string>> VisitTransitions([NotNull] AUTOMATAParser.TransitionsContext context)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            // Si solo está declarado, sin transición
            if (context == null)
                return result; // No hay transición que documentar.

            // Obtén todas las conexiones (entrada -> salidas)
            List<Pair<string, List<string>>> nodeConnections = context.transition().Select((ctx) => {return VisitTransition(ctx); }).ToList();

            // Llevalas a un diccionario, eliminando repetición de entradas.
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
