using ProyectoEvaluacionParserV2.Model;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace ProyectoEvaluacionParserV2.Model
{
    internal class ImplicitState
    {
        public Node node;

        public Dictionary<string, List<string>> transitions;

        public ImplicitState(Node node, Dictionary<string, List<string>> transitions)
        {
            this.node = node;
            this.transitions = transitions;
        }
    }
}
