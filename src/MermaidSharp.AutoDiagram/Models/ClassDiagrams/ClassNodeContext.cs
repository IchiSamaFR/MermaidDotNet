using MermaidSharp.AutoDiagram.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.AutoDiagram.Models.ClassDiagrams
{
    public class ClassNodeContext
    {
        public ClassTypeContext Type { get; }

        public List<ClassMethodContext> Methods { get; } = new List<ClassMethodContext>();
        public List<ClassPropertyContext> Properties { get; } = new List<ClassPropertyContext>();

        public ClassNodeContext(Type type)
        {
            Type = new ClassTypeContext(type);
        }
    }
}
