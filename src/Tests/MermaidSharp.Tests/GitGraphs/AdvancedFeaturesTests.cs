using MermaidSharp.Configs;
using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.GitGraphs
{
	[TestClass]
	public class AdvancedFeaturesTests
	{
		#region Basic GitGraph

		[TestMethod]
		public void GitGraph_NoConfig()
		{
			//Arrange
			var graph = new GitGraph();
			graph.Commit("c1");

			string expected = @"gitGraph
    commit id: ""c1""";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_EmptyNoConfig()
		{
			//Arrange
			var graph = new GitGraph();

			string expected = @"gitGraph";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_EmptyConfig()
		{
			//Arrange
			var config = new GitGraphConfig(); // All properties null/None
			var graph = new GitGraph(config: config);
			graph.Commit("c1");

			string expected = @"gitGraph
    commit id: ""c1""";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_EmptyConfigNoActions()
		{
			//Arrange
			var config = new GitGraphConfig(ConfigTheme.Neutral);
			var graph = new GitGraph(config: config);

			string expected = @"---
config:
    theme: neutral
---
gitGraph";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region Title

		[TestMethod]
		public void GitGraph_WithTitle()
		{
			//Arrange
			var graph = new GitGraph(title: "My Git Repository");
			graph.Commit("c1");

			string expected = @"---
title: My Git Repository
---
gitGraph
    commit id: ""c1""";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_WithTitleAndConfig()
		{
			//Arrange
			var config = new GitGraphConfig(ConfigTheme.Dark)
			{
				ShowCommitLabel = true
			};
			var graph = new GitGraph(title: "Development Workflow", config: config);
			graph.Commit("c1");
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2");

			string expected = @"---
title: Development Workflow
config:
    theme: dark
    gitGraph:
        showCommitLabel: true
---
gitGraph
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_WithTitleAndDirection()
		{
			//Arrange
			var graph = new GitGraph(title: "Feature Development", direction: GitDirection.LeftRight);
			graph.Commit("c1");
			graph.Branch("feature");
			graph.Checkout("feature");
			graph.Commit("c2");

			string expected = @"---
title: Feature Development
---
gitGraph LR:
    commit id: ""c1""
    branch feature
    checkout feature
    commit id: ""c2""";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_EmptyTitle()
		{
			//Arrange
			var graph = new GitGraph(title: "");
			graph.Commit("c1");

			string expected = @"gitGraph
    commit id: ""c1""";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region Complex Workflows

		[TestMethod]
		public void GitGraph_Complete()
		{
			//Arrange
			var graph = new GitGraph();
			graph.Commit("c1");
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2");
			graph.Checkout("main");
			graph.Merge("develop");

			string expected = @"gitGraph
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    checkout main
    merge develop";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_CompleteWithTheme()
		{
			//Arrange
			var config = new GitGraphConfig(ConfigTheme.Dark)
			{
				ShowCommitLabel = true,
				MainBranchName = "master"
			};
			var graph = new GitGraph(config: config);
			graph.Commit("c1");
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2");
			graph.Commit("c3");
			graph.Checkout("master");
			graph.Merge("develop");

			string expected = @"---
config:
    theme: dark
    gitGraph:
        showCommitLabel: true
        mainBranchName: ""master""
---
gitGraph
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    commit id: ""c3""
    checkout master
    merge develop";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_CompleteWithConfig()
		{
			//Arrange
			var config = new GitGraphConfig
			{
				ShowBranches = false,
				MainBranchName = "master"
			};
			var graph = new GitGraph(config: config);
			graph.Commit("c1");
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2");
			graph.Commit("c3");
			graph.Checkout("master");
			graph.Merge("develop");

			string expected = @"---
config:
    gitGraph:
        showBranches: false
        mainBranchName: ""master""
---
gitGraph
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    commit id: ""c3""
    checkout master
    merge develop";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region Commit Types

		[TestMethod]
		public void GitGraph_WithCommitType()
		{
			//Arrange
			var graph = new GitGraph();
			graph.Commit("c1", commitType: GitCommitType.Reverse);

			string expected = @"gitGraph
    commit id: ""c1"" type: REVERSE";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_WithCommitTypeOnMerge()
		{
			//Arrange
			var graph = new GitGraph();
			graph.Commit("c1");
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2");
			graph.Checkout("main");
			graph.Merge("develop", commitType: GitCommitType.Highlight);

			string expected = @"gitGraph
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    checkout main
    merge develop type: HIGHLIGHT";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region Tags

		[TestMethod]
		public void GitGraph_WithCommitTag()
		{
			//Arrange
			var graph = new GitGraph();
			graph.Commit("c1", tag: "v1.0");
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2", tag: "feature-1");
			graph.Checkout("main");
			graph.Merge("develop");

			string expected = @"gitGraph
    commit id: ""c1"" tag: ""v1.0""
    branch develop
    checkout develop
    commit id: ""c2"" tag: ""feature-1""
    checkout main
    merge develop";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_WithMergeTag()
		{
			//Arrange
			var graph = new GitGraph();
			graph.Commit("c1");
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2");
			graph.Checkout("main");
			graph.Merge("develop", tag: "v1.1");

			string expected = @"gitGraph
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    checkout main
    merge develop tag: ""v1.1""";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region Cherry Pick

		[TestMethod]
		public void GitGraph_WithCherryPick()
		{
			//Arrange
			var graph = new GitGraph();
			graph.Commit("c1");
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2");
			graph.Checkout("main");
			graph.CherryPick("c2");

			string expected = @"gitGraph
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    checkout main
    cherry-pick id:""c2""";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_WithCherryPickFromMerge()
		{
			// A cherry-pick on a merge commit automatically resolves the parent
			// to the last commit with an id on the merged branch before the merge

			//Arrange
			var graph = new GitGraph();
			graph.Commit("c1");
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2");
			graph.Checkout("main");
			graph.Merge("develop", "m1");
			graph.Branch("hotfix");
			graph.Checkout("hotfix");
			graph.CherryPick("m1"); // parent resolved automatically: last id on develop = "c2"

			string expected = @"gitGraph
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    checkout main
    merge develop id: ""m1""
    branch hotfix
    checkout hotfix
    cherry-pick id:""m1"" parent: ""c2""";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region Direction

		[TestMethod]
		public void GitGraph_WithDirection_LeftRight()
		{
			//Arrange
			var graph = new GitGraph(direction: GitDirection.LeftRight);
			graph.Commit("c1");
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2");
			graph.Checkout("main");
			graph.Merge("develop");

			string expected = @"gitGraph LR:
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    checkout main
    merge develop";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_WithDirection_TopBottom()
		{
			//Arrange
			var graph = new GitGraph(direction: GitDirection.TopBottom);
			graph.Commit("c1");
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2");
			graph.Checkout("main");
			graph.Merge("develop");

			string expected = @"gitGraph TB:
    commit id: ""c1""
    branch develop
    checkout develop
    commit id: ""c2""
    checkout main
    merge develop";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion

		#region Full Workflow

		[TestMethod]
		public void GitGraph_FullWorkflow()
		{
			// Combines all features: config, direction, commit types, tags, merges with id/tag/type, cherry picks

			//Arrange
			var config = new GitGraphConfig(ConfigTheme.Forest)
			{
				ShowCommitLabel = true,
				ShowBranches = true,
				RotateCommitLabel = false,
				MainBranchName = "master"
			};
			var graph = new GitGraph(direction: GitDirection.LeftRight, config: config);
			graph.Commit("c1", tag: "v1.0", commitType: GitCommitType.Normal);
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2", commitType: GitCommitType.Reverse);
			graph.Commit("c3", tag: "feature");
			graph.Checkout("master");
			graph.Merge("develop", "m1", "v1.1", GitCommitType.Highlight);
			graph.Branch("hotfix");
			graph.Checkout("hotfix");
			graph.CherryPick("c3");                 // regular commit cherry-pick, no parent
			graph.Commit("c4");
			graph.Checkout("develop");
			graph.Commit("c5");
			graph.Merge("hotfix", "m2");           // merge without id/tag/type, id auto-generated
			graph.Checkout("master");
			graph.CherryPick("m2");                 // merge cherry-pick, parent resolved to "c3" (last id on develop)

			string expected = @"---
config:
    theme: forest
    gitGraph:
        showCommitLabel: true
        showBranches: true
        rotateCommitLabel: false
        mainBranchName: ""master""
---
gitGraph LR:
    commit id: ""c1"" tag: ""v1.0"" type: NORMAL
    branch develop
    checkout develop
    commit id: ""c2"" type: REVERSE
    commit id: ""c3"" tag: ""feature""
    checkout master
    merge develop id: ""m1"" tag: ""v1.1"" type: HIGHLIGHT
    branch hotfix
    checkout hotfix
    cherry-pick id:""c3""
    commit id: ""c4""
    checkout develop
    commit id: ""c5""
    merge hotfix id: ""m2""
    checkout master
    cherry-pick id:""m2"" parent: ""c4""";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void GitGraph_FullWorkflowWithTitle()
		{
			// Combines all features including title: config, direction, commit types, tags, merges with id/tag/type, cherry picks

			//Arrange
			var config = new GitGraphConfig(ConfigTheme.Forest)
			{
				ShowCommitLabel = true,
				ShowBranches = true,
				RotateCommitLabel = false,
				MainBranchName = "master"
			};
			var graph = new GitGraph(title: "Complete Git Workflow", direction: GitDirection.LeftRight, config: config);
			graph.Commit("c1", tag: "v1.0", commitType: GitCommitType.Normal);
			graph.Branch("develop");
			graph.Checkout("develop");
			graph.Commit("c2", commitType: GitCommitType.Reverse);
			graph.Commit("c3", tag: "feature");
			graph.Checkout("master");
			graph.Merge("develop", "m1", "v1.1", GitCommitType.Highlight);
			graph.Branch("hotfix");
			graph.Checkout("hotfix");
			graph.CherryPick("c3");
			graph.Commit("c4");

			string expected = @"---
title: Complete Git Workflow
config:
    theme: forest
    gitGraph:
        showCommitLabel: true
        showBranches: true
        rotateCommitLabel: false
        mainBranchName: ""master""
---
gitGraph LR:
    commit id: ""c1"" tag: ""v1.0"" type: NORMAL
    branch develop
    checkout develop
    commit id: ""c2"" type: REVERSE
    commit id: ""c3"" tag: ""feature""
    checkout master
    merge develop id: ""m1"" tag: ""v1.1"" type: HIGHLIGHT
    branch hotfix
    checkout hotfix
    cherry-pick id:""c3""
    commit id: ""c4""";

			//Act
			string result = graph.CalculateDiagram();

			//Assert
			Assert.IsNotNull(graph);
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		#endregion
	}
}