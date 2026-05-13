using MermaidSharp.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MermaidSharp.Extensions
{
    internal static class StringExtension
    {
        public static string JoinNonEmpty(this IEnumerable<string> lst, string separator)
        {
            return string.Join(separator, lst.Where(i => !string.IsNullOrEmpty(i)));
        }

        public static string Indent(this string str, int indent = 1)
        {
            string indentString = string.Concat(Enumerable.Repeat(FormattingConstants.Indentation, indent));
            var lines = str.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
            return string.Join(Environment.NewLine, lines.Select(line => indentString + line));
        }
        public static List<string> Indent(this IEnumerable<string> lst, int indent = 1)
        {
            return lst.Select(i => i.Indent(indent)).ToList();
        }
        public static List<string> ClearNewLines(this IEnumerable<string> lst)
        {
            return lst.Where(i => !string.IsNullOrWhiteSpace(i)).ToList();
        }

        public static string FormatAngleBracket(this string str)
        {
            return str?.Replace("<", "~").Replace(">", "~") ?? string.Empty;
        }
    }
}
