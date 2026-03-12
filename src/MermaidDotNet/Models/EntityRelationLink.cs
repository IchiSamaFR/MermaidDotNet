using MermaidDotNet.Enums;

namespace MermaidDotNet.Models
{
    public class EntityRelationLink : Link
    {
        public string Label { get; }
        public RelationType SourceRelation { get; }
        public RelationType DestinationRelation { get; }

        /// <summary>
        /// Crée un lien de relation entre deux entités avec types de relation Mermaid ER.
        /// </summary>
        public EntityRelationLink(
            string sourceNode,
            string destinationNode,
            string label,
            RelationType sourceRelation,
            RelationType destinationRelation)
            : base(
                sourceNode,
                destinationNode)
        {
            Label = label;
            SourceRelation = sourceRelation;
            DestinationRelation = destinationRelation;
        }

        /// <summary>
        /// Crée un lien simple entre deux entités (sans syntaxe Mermaid ER personnalisée).
        /// </summary>
        public EntityRelationLink(
            string sourceNode,
            string destinationNode,
            RelationType sourceRelation,
            RelationType destinationRelation)
            : this(
                sourceNode,
                destinationNode,
                null,
                sourceRelation,
                destinationRelation)
        {
        }

        public override string GetLinkString()
        {
            // Utilise la syntaxe Mermaid ER pour les relations
            string relationSyntax = GetMermaidRelationSyntax(SourceRelation, DestinationRelation);
            string label = !string.IsNullOrEmpty(Label) ? $" : \"{Label}\"" : string.Empty;
            return $"{SourceNode} {relationSyntax} {DestinationNode}{label}";
        }

        /// <summary>
        /// Génère la syntaxe Mermaid ER pour le lien selon les types de relation.
        /// </summary>
        private static string GetMermaidRelationSyntax(RelationType source, RelationType destination)
        {
            // Symboles Mermaid ER
            string left;
            switch (source)
            {
                case RelationType.ZeroOrOne:
                    left = "|o";
                    break;
                case RelationType.ExactlyOne:
                    left = "||";
                    break;
                case RelationType.ZeroOrMore:
                    left = "}o";
                    break;
                case RelationType.OneOrMore:
                    left = "}|";
                    break;
                default:
                    left = "||";
                    break;
            }

            string right;
            switch (destination)
            {
                case RelationType.ZeroOrOne:
                    right = "o|";
                    break;
                case RelationType.ExactlyOne:
                    right = "||";
                    break;
                case RelationType.ZeroOrMore:
                    right = "o{";
                    break;
                case RelationType.OneOrMore:
                    right = "|{";
                    break;
                default:
                    right = "||";
                    break;
            }
            return left + "--" + right;
        }
    }
}