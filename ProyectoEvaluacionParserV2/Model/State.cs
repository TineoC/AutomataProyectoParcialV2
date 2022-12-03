using ProyectoEvaluacionParserV2.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace ProyectoEvaluacionParserV2.Model
{
    internal class State
    {
        public Node node;

        public Dictionary<string, List<Node>> transitions;

        public State(ImplicitState impState, Dictionary<string, Node> nodeNames)
        {
            this.node = impState.node;
            this.transitions = new Dictionary<string, List<Node>>();
            foreach (var item in impState.transitions)
            {
                if (transitions.ContainsKey(item.Key))
                {
                    transitions.Add(item.Key, item.Value.Select(strs => nodeNames[strs]).ToList());
                }
            }
        }
    }
}
