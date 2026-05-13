using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Represents the configuration settings for rendering a class diagram.
    /// </summary>
    public class ClassDiagramConfig : AConfig<ClassDiagramThemeVariables>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassDiagramConfig"/> class with default settings.
        /// </summary>
        public ClassDiagramConfig() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ClassDiagramConfig class with the specified theme and text position.
        /// </summary>
        /// <param name="theme">The visual theme to apply to the diagram. Defaults to <see cref="ConfigTheme.None"/>.</param>
        /// <param name="themeVariables">
        /// The theme variables to apply to the diagram. If null, a new empty <see cref="ClassDiagramThemeVariables"/> instance is used.
        /// </param>
        public ClassDiagramConfig(ConfigTheme theme = ConfigTheme.None, ClassDiagramThemeVariables themeVariables = default) : base(theme, themeVariables)
        {
        }
    }
}
