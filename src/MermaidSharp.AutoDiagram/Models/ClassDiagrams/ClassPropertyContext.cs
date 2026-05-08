using MermaidSharp.AutoDiagram.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.AutoDiagram.Models.ClassDiagrams
{
    public class ClassPropertyContext
    {
        public string Name => Property.Name;
        public PropertyInfo Property { get; }
        public ClassTypeContext Type { get; }

        public ClassPropertyContext(PropertyInfo property)
        {
            Property = property;
            Type = new ClassTypeContext(property);
        }
    }
}
