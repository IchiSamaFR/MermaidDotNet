using MermaidSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.AutoDiagram.Models.ClassDiagrams
{
    public class ClassAssemblyContext
    {
        public string Name => Assembly.GetName().Name ?? "UnknownAssembly";
        public Assembly Assembly { get; }
        public List<ClassNodeContext> ClassDiagrams { get; } = new List<ClassNodeContext>();

        public ClassAssemblyContext(Assembly assembly)
        {
            Assembly = assembly;
        }
    }
}
