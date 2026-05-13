using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.GitGraphs
{
    [TestClass]
    public class GitMergeTests
    {
        [TestMethod]
        public void GitMerge()
        {
            //Arrange
            var merge = new GitMerge("myBranch");
            string expected = @"merge myBranch";

            //Act
            string result = merge.ToString();

            //Assert
            Assert.IsNotNull(merge);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitMerge_Id()
        {
            //Arrange
            var merge = new GitMerge("myBranch", id: "my id");
            string expected = @"merge myBranch id: ""my id""";

            //Act
            string result = merge.ToString();

            //Assert
            Assert.IsNotNull(merge);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitMerge_Tag()
        {
            //Arrange
            var merge = new GitMerge("myBranch", tag: "my tag");
            string expected = @"merge myBranch tag: ""my tag""";

            //Act
            string result = merge.ToString();

            //Assert
            Assert.IsNotNull(merge);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitMerge_CommitType_Normal()
        {
            //Arrange
            var merge = new GitMerge("myBranch", commitType: GitCommitType.Normal);
            string expected = @"merge myBranch type: NORMAL";

            //Act
            string result = merge.ToString();

            //Assert
            Assert.IsNotNull(merge);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitMerge_CommitType_Reverse()
        {
            //Arrange
            var merge = new GitMerge("myBranch", commitType: GitCommitType.Reverse);
            string expected = @"merge myBranch type: REVERSE";

            //Act
            string result = merge.ToString();

            //Assert
            Assert.IsNotNull(merge);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitMerge_CommitType_Highlight()
        {
            //Arrange
            var merge = new GitMerge("myBranch", commitType: GitCommitType.Highlight);
            string expected = @"merge myBranch type: HIGHLIGHT";

            //Act
            string result = merge.ToString();

            //Assert
            Assert.IsNotNull(merge);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitMerge_IdTagAndCommitType()
        {
            //Arrange
            var merge = new GitMerge("myBranch", "my id", "my tag", GitCommitType.Normal);
            string expected = @"merge myBranch id: ""my id"" tag: ""my tag"" type: NORMAL";

            //Act
            string result = merge.ToString();

            //Assert
            Assert.IsNotNull(merge);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}