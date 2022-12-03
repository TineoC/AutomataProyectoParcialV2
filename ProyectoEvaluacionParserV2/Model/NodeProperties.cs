using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;


namespace ProyectoEvaluacionParserV2.Model
{
    internal class NodeProperties
    {
        public bool isInitial;
        public bool isAcceptance;

        public NodeProperties(bool isInitial, bool isAcceptance)
        {
            this.isInitial = isInitial;
            this.isAcceptance = isAcceptance;
        }
    }
}
