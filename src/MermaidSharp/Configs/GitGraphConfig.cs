using MermaidSharp.Attributes;
using MermaidSharp.Configs.Themes;
using MermaidSharp.Enums;

namespace MermaidSharp.Configs
{
	/// <summary>
	/// Represents the configuration settings for rendering a git graph diagram, including display options and main
	/// branch customization.
	/// </summary>
	public class GitGraphConfig : AConfig<GitGraphThemeVariables>
    {
        /// <summary>
        /// Gets the name of the configuration section represented by the derived class.
        /// </summary>
		protected override string SectionName => "gitGraph";

		/// <summary>
		/// Gets or sets a value indicating whether the commit label is displayed.
		/// </summary>
		[ConfigVariable("showCommitLabel")]
		public bool? ShowCommitLabel { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether branches are displayed in the diagram.
		/// </summary>
		[ConfigVariable("showBranches")]
		public bool? ShowBranches { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether the commit label should be rotated in the diagram.
		/// </summary>
		[ConfigVariable("rotateCommitLabel")]
		public bool? RotateCommitLabel { get; set; }
		/// <summary>
		/// Gets or sets the name of the main branch in the repository.
		/// </summary>
		[ConfigVariable("mainBranchName")]
		public string MainBranchName { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="GitGraphConfig"/> class with default settings.
		/// </summary>
		public GitGraphConfig() : base()
		{

		}

		/// <summary>
		/// Initializes a new instance of the GitGraphConfig class with the specified theme and display options for the
		/// git graph.
		/// </summary>
		/// <param name="theme">The visual theme to apply to the git graph. The default is ConfigTheme.None.</param>
		/// <param name="showCommitLabel">A value indicating whether commit labels are displayed. If null, the default behavior is used.</param>
		/// <param name="showBranches">A value indicating whether branch lines are shown. If null, the default behavior is used.</param>
		/// <param name="rotateCommitLabel">A value indicating whether commit labels are rotated. If null, the default behavior is used.</param>
		/// <param name="mainBranchName">The name of the main branch to display. If null, the default branch name is used.</param>
		/// <param name="themeVariables">The theme variables to apply to the git graph. If null, default theme variables are used.</param>
		public GitGraphConfig(ConfigTheme theme = ConfigTheme.None,
			bool? showCommitLabel = null,
			bool? showBranches = null,
			bool? rotateCommitLabel = null,
			string mainBranchName = null,
			GitGraphThemeVariables themeVariables = default) : base(theme, themeVariables)
		{
			ShowCommitLabel = showCommitLabel;
			ShowBranches = showBranches;
			RotateCommitLabel = rotateCommitLabel;
			MainBranchName = mainBranchName;
		}
	}
}
