using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.AutoDiagram.Models.ClassDiagrams
{
    public class ClassMethodContext
    {
        public string Name => Method.Name;
        public MethodInfo Method { get; }
        public ClassTypeContext ReturnType { get; }

        public ClassMethodContext(MethodInfo method)
        {
            Method = method;
            ReturnType = new ClassTypeContext(method);
        }
    }
}
