using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Represents configuration settings for entity relationship diagrams.
    /// </summary>
    public class EntityRelationshipConfig : AConfig<EntityRelationshipThemeVariables>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRelationshipConfig"/> class with default settings.
        /// </summary>
        public EntityRelationshipConfig() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the EntityRelationshipConfig class with the specified theme.
        /// </summary>
        /// <param name="theme">The visual theme to apply to the diagram. Defaults to <see cref="ConfigTheme.None"/>.</param>
        /// <param name="themeVariables"> The theme variables to apply to the diagram. If null, a new empty <see cref="EntityRelationshipThemeVariables"/> instance is used.</param>
        public EntityRelationshipConfig(ConfigTheme theme = ConfigTheme.None, EntityRelationshipThemeVariables themeVariables = default) : base(theme, themeVariables)
        {
        }
    }
}
