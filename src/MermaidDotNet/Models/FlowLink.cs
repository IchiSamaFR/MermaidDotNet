using MermaidDotNet.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidDotNet.Models
{
    public class FlowLink : Link
    {
        public FlowLink(string sourceNode, string destinationNode, string label = "", string linkstyle = "", bool isBidirectional = false, LinkType linkType = LinkType.Normal, ArrowType arrowType = ArrowType.Normal)
            : base(sourceNode, destinationNode)
        {
            Label = label;
            IsBidirectional = isBidirectional;
            LinkStyle = linkstyle;
            Type = linkType;
            Arrow = arrowType;
        }

        public string Label { get; set; }
        public string LinkStyle { get; set; }
        public bool IsBidirectional { get; }
        public LinkType Type { get; set; }
        public ArrowType Arrow { get; set; }

        protected override string GetLink()
        {
            StringBuilder sb = new StringBuilder();
            if (IsBidirectional)
            {
                sb.Append(GetStartArrowSymbol(Arrow));
            }
            sb.Append(GetLinkSymbol(Type));
            if (!string.IsNullOrEmpty(Label))
            {
                sb.Append(Label);
                var linkSymbol = GetLinkSymbol(Type);
                if (Type == LinkType.Dotted)
                {
                    linkSymbol = new string(linkSymbol.Reverse().ToArray());
                }
                sb.Append(linkSymbol);
            }
            sb.Append(GetEndArrowSymbol(Arrow));
            return sb.ToString();
        }
        public string GetStyleString(int index)
        {
            if (string.IsNullOrEmpty(LinkStyle))
            {
                return string.Empty;
            }
            return $"linkStyle {index} {LinkStyle}";
        }

        private string GetLinkSymbol(LinkType linkType)
        {
            switch (linkType)
            {
                case LinkType.Normal:
                    return "--";
                case LinkType.Dotted:
                    return "-.";
                case LinkType.Thick:
                    return "==";
                case LinkType.Invisible:
                    return "~~~";
                default:
                    return "--";
            }
        }

        private string GetStartArrowSymbol(ArrowType arrowType)
        {
            switch (arrowType)
            {
                case ArrowType.Normal:
                case ArrowType.Open:
                    return "<";
                case ArrowType.Circle:
                    return "o";
                case ArrowType.Cross:
                    return "x";
                default:
                    return "<";
            }
        }

        private string GetEndArrowSymbol(ArrowType arrowType)
        {
            switch (arrowType)
            {
                case ArrowType.Normal:
                case ArrowType.Open:
                    return ">";
                case ArrowType.Circle:
                    return "o";
                case ArrowType.Cross:
                    return "x";
                default:
                    return ">";
            }
        }
    }
}
