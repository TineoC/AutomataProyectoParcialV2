using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace ProyectoEvaluacionParserV2.Model
{
    internal class Node
    {
        public string name;
        public NodeProperties props;

        public Node(string name, NodeProperties props)
        {
            this.name = name;
            this.props = props;
        }
    }
}
