using Antlr4.Runtime.Misc;
using ProyectoEvaluacionParserV2.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace ProyectoEvaluacionParserV2.Model
{
    internal class Automata
    {
        public List<ImplicitState> states = new List<ImplicitState>();
        public string name = "defaultAutomata";

        public string GenerateDoc()
        {
            // Consigue todos los estados finales
            List<string> finalStates = (from state in states where state.node.props.isAcceptance == true select state.node.name).ToList();

            // Alistate para conseguir todos los pasos
            List<string> allSteps = new List<string>();

            // Que son, con cada estado
            foreach (ImplicitState state in states)
            {
                // Con cada transicion de ese estado
                foreach (var implTransition in state.transitions)
                {
                    // Con cada salida de esa transición
                    foreach (string destination in implTransition.Value)
                    {
                        // Agrega ese paso (conformado por el nombre del estado, su destino, y con lo que se llega)
                        allSteps.Add($"{state.node.name} -> {destination} [label = \"{implTransition.Key}\"];");
                    }
                }
            }

            // Junta todos los estados finales con coma
            string doubleCircle = string.Join(',', finalStates);

            // Obten el primer estado inicial
            string initialState = (from state in states where state.node.props.isInitial == true select state.node.name).ToList()[0];

            // Junta todos los pasos con carriage return y nueva linea, mas 4 espacios.
            string stateMap = String.Join("\r\n    ", allSteps);

            // Y con este formato
            string pattern =
@"digraph {0} {{
    rankdir=LR;
    node [shape = none, height=0, width=0]; inicio [label=""""];
    node [shape = doublecircle];{1}
    node [shape = circle];
    inicio -> {2};
    {3}
}}
";
            // Junta todas las salidas en este patron formateado
            return string.Format(pattern, name, doubleCircle, initialState, stateMap);
        }
    }
}
