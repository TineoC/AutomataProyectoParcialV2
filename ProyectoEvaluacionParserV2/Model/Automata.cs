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
            List<string> finalStates = (from state in states where state.node.props.isAcceptance == true select state.node.name).ToList();
            string doubleCircle = string.Join(',', finalStates);
            string initialState = (from state in states where state.node.props.isInitial == true select state.node.name).ToList()[0];
            List<string> allSteps = new List<string>();

            foreach (ImplicitState state in states)
            {
                foreach (var implTransition in state.transitions)
                {
                    foreach (string destination in implTransition.Value)
                    {
                        allSteps.Add($"{state.node.name} -> {destination} [label = \"{implTransition.Key}\"];");
                    }
                }
            }
            string stateMap = String.Join('\n', allSteps);

            return @"digraph " + $"{name}" + @" {
                rankdir=LR;
                node [shape = none, height=0, width=0]; inicio [label=""""];
                node [shape = doublecircle];" + $"{doubleCircle}" +
                @"
                node [shape = circle]; 
                " + $"inicio -> {initialState}" + @"
                " + stateMap + @"
            }";
        }
    }
}
