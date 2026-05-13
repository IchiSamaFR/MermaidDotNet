using MermaidSharp.Attributes;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;
using System;

namespace MermaidSharp.Configs
{
    /// <summary>
    /// Represents the configuration settings for rendering a Mermaid pie chart diagram.
    /// </summary>
    public class PieChartConfig : AConfig<PieChartThemeVariables>
    {
        /// <summary>
        /// Gets the name of the configuration section represented by the derived class.
        /// </summary>
        protected override string SectionName => "pie";

        private double? _textPosition;

        /// <summary>
        /// Gets or sets the axial position of the pie slice labels, from 0.0 at the center to 1.0 at the outside edge
        /// of the circle. The default value used by Mermaid is 0.75.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when value is not finite or is not between 0.0 and 1.0 inclusive.</exception>
        [ConfigVariable("textPosition")]
        public double? TextPosition
        {
            get => _textPosition;
            set
            {
                if (value.HasValue && (double.IsNaN(value.Value) || double.IsInfinity(value.Value) || value.Value < 0.0 || value.Value > 1.0))
                    throw new ArgumentOutOfRangeException(nameof(value), "TextPosition must be a finite number between 0.0 and 1.0 inclusive.");
                _textPosition = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PieChartConfig"/> class with default settings.
        /// </summary>
        public PieChartConfig() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the PieChartConfig class with the specified theme and text position.
        /// </summary>
        /// <param name="theme">The visual theme to apply to the diagram. Defaults to <see cref="ConfigTheme.None"/>.</param>
        /// <param name="textPosition">
        /// The axial position of the pie slice labels (0.0 to 1.0 inclusive). If null, Mermaid's default value (0.75) is used.
        /// </param>
        /// <param name="themeVariables">
        /// The theme variables to apply to the diagram. If null, a new empty <see cref="PieChartThemeVariables"/> instance is used.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when textPosition is not finite or is not between 0.0 and 1.0 inclusive.</exception>
        public PieChartConfig(ConfigTheme theme = ConfigTheme.None, double? textPosition = null, PieChartThemeVariables themeVariables = default) : base(theme, themeVariables)
        {
            TextPosition = textPosition;
        }
    }
}
