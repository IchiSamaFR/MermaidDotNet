using MermaidDotNet.EntityFramework.Tests.Mock;
using MermaidDotNet.EntityFramework.Tests.Mock.Entities;
using MermaidDotNet.EntityFrameworkCore;
using MermaidDotNet.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SQLite;

namespace MermaidDotNet.EntityFramework.Tests
{
    [TestClass]
    public class DiagramTests
    {
        private static string GetInMemoryConnectionString()
        {
            return "Data Source=:memory:;Version=3;New=True;";
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SQLiteConnection(GetInMemoryConnectionString());
            connection.Open();
            return connection;
        }

        [TestMethod]
        public void GenerateDiagram()
        {
            try
            {
                using (var connection = CreateInMemoryDatabase())
                using (var context = new DatabaseContextMock(connection, true))
                {
                    var diagram = context.ToMermaidEntityDiagram();
                    var result = diagram.CalculateDiagram();
                    var expected = @"erDiagram
    Assignment {
        Int32 Id PK ""The unique identifier for the assignment.""
        Int32 CourseId FK
        String Description
        DateTime DueDate
        String Title
    }
    Course {
        Int32 Id PK
        String Description
        String Name
        Int32 TeacherId FK
    }
    Enrollment {
        Int32 Id PK
        Int32 CourseId FK
        DateTime EnrolledAt
        Decimal Grade
        Int32 StudentId FK
    }
    SchoolClass {
        Int32 Id PK
        Int32 Level
        String Name
        Int32 TeacherId FK
    }
    Student {
        Int32 Id PK
        DateTime DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId FK
    }
    Submission {
        Int32 Id PK
        Int32 AssignmentId FK
        String Comment
        Decimal Grade ""The grade awarded for the submission, if graded.""
        Int32 StudentId FK
        DateTime SubmittedAt
    }
    Teacher {
        Int32 Id PK ""The unique identifier for the teacher.""
        String Email
        String FirstName ""The first name of the teacher.""
        String LastName ""The last name of the teacher.""
    }
    Assignment }|--|| Course : ""CourseId (Cascade)""
    Course }|--|| Teacher : ""TeacherId (Cascade)""
    Enrollment }|--|| Course : ""CourseId (Cascade)""
    Enrollment }|--|| Student : ""StudentId (Cascade)""
    SchoolClass }|--|| Teacher : ""TeacherId (Cascade)""
    Student }|--|| SchoolClass : ""SchoolClassId (Cascade)""
    Submission }|--|| Assignment : ""AssignmentId (Cascade)""
    Submission }|--|| Student : ""StudentId (Cascade)""";
                    Assert.IsNotNull(result);
                    Assert.AreEqual(expected, result);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception levée : " + ex.ToString());
            }
        }

        [TestMethod]
        public void GenerateDiagram_NoColumnTypes()
        {
            // Arrange
            using (var connection = CreateInMemoryDatabase())
            {
                using (var context = new DatabaseContextMock(connection, true))
                {
                    context.Database.CreateIfNotExists();

                    var diagramOptions = new EntityRelationshipDiagramOptions
                    {
                        IncludeColumns = false
                    };

                    var expected = @"erDiagram
    Assignment
    Course
    Enrollment
    SchoolClass
    Student
    Submission
    Teacher
    Assignment }|--|| Course : ""CourseId (Cascade)""
    Course }|--|| Teacher : ""TeacherId (Cascade)""
    Enrollment }|--|| Course : ""CourseId (Cascade)""
    Enrollment }|--|| Student : ""StudentId (Cascade)""
    SchoolClass }|--|| Teacher : ""TeacherId (Cascade)""
    Student }|--|| SchoolClass : ""SchoolClassId (Cascade)""
    Submission }|--|| Assignment : ""AssignmentId (Cascade)""
    Submission }|--|| Student : ""StudentId (Cascade)""";

                    // Act
                    var diagram = context.ToMermaidEntityDiagram(diagramOptions);
                    var result = diagram.CalculateDiagram();

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(expected, result);
                }
            }
        }

        [TestMethod]
        public void GenerateDiagram_NoColumnKeyTypes()
        {
            // Arrange
            using (var connection = CreateInMemoryDatabase())
            {
                using (var context = new DatabaseContextMock(connection, true))
                {
                    context.Database.CreateIfNotExists();

                    var diagramOptions = new EntityRelationshipDiagramOptions
                    {
                        IncludeColumnKeyTypes = false
                    };

                    var expected = @"erDiagram
    Assignment {
        Int32 Id ""The unique identifier for the assignment.""
        Int32 CourseId
        String Description
        DateTime DueDate
        String Title
    }
    Course {
        Int32 Id
        String Description
        String Name
        Int32 TeacherId
    }
    Enrollment {
        Int32 Id
        Int32 CourseId
        DateTime EnrolledAt
        Decimal Grade
        Int32 StudentId
    }
    SchoolClass {
        Int32 Id
        Int32 Level
        String Name
        Int32 TeacherId
    }
    Student {
        Int32 Id
        DateTime DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId
    }
    Submission {
        Int32 Id
        Int32 AssignmentId
        String Comment
        Decimal Grade ""The grade awarded for the submission, if graded.""
        Int32 StudentId
        DateTime SubmittedAt
    }
    Teacher {
        Int32 Id ""The unique identifier for the teacher.""
        String Email
        String FirstName ""The first name of the teacher.""
        String LastName ""The last name of the teacher.""
    }
    Assignment }|--|| Course : ""CourseId (Cascade)""
    Course }|--|| Teacher : ""TeacherId (Cascade)""
    Enrollment }|--|| Course : ""CourseId (Cascade)""
    Enrollment }|--|| Student : ""StudentId (Cascade)""
    SchoolClass }|--|| Teacher : ""TeacherId (Cascade)""
    Student }|--|| SchoolClass : ""SchoolClassId (Cascade)""
    Submission }|--|| Assignment : ""AssignmentId (Cascade)""
    Submission }|--|| Student : ""StudentId (Cascade)""";

                    // Act
                    var diagram = context.ToMermaidEntityDiagram(diagramOptions);
                    var result = diagram.CalculateDiagram();

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(expected, result);
                }
            }
        }

        [TestMethod]
        public void GenerateDiagram_FilteredColumnKeyTypes()
        {
            // Arrange
            using (var connection = CreateInMemoryDatabase())
            {
                using (var context = new DatabaseContextMock(connection, true))
                {
                    context.Database.CreateIfNotExists();

                    var diagramOptions = new EntityRelationshipDiagramOptions
                    {
                        FilterColumnByKeyTypes = ColumnKeyType.PrimaryKey | ColumnKeyType.ForeignKey
                    };

                    var expected = @"erDiagram
    Assignment {
        Int32 Id PK ""The unique identifier for the assignment.""
        Int32 CourseId FK
    }
    Course {
        Int32 Id PK
        Int32 TeacherId FK
    }
    Enrollment {
        Int32 Id PK
        Int32 CourseId FK
        Int32 StudentId FK
    }
    SchoolClass {
        Int32 Id PK
        Int32 TeacherId FK
    }
    Student {
        Int32 Id PK
        Int32 SchoolClassId FK
    }
    Submission {
        Int32 Id PK
        Int32 AssignmentId FK
        Int32 StudentId FK
    }
    Teacher {
        Int32 Id PK ""The unique identifier for the teacher.""
    }
    Assignment }|--|| Course : ""CourseId (Cascade)""
    Course }|--|| Teacher : ""TeacherId (Cascade)""
    Enrollment }|--|| Course : ""CourseId (Cascade)""
    Enrollment }|--|| Student : ""StudentId (Cascade)""
    SchoolClass }|--|| Teacher : ""TeacherId (Cascade)""
    Student }|--|| SchoolClass : ""SchoolClassId (Cascade)""
    Submission }|--|| Assignment : ""AssignmentId (Cascade)""
    Submission }|--|| Student : ""StudentId (Cascade)""";

                    // Act
                    var diagram = context.ToMermaidEntityDiagram(diagramOptions);
                    var result = diagram.CalculateDiagram();

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(expected, result);
                }
            }
        }

        [TestMethod]
        public void GenerateDiagram_NoColumnWithoutKeyTypes()
        {
            // Arrange
            using (var connection = CreateInMemoryDatabase())
            {
                using (var context = new DatabaseContextMock(connection, true))
                {
                    context.Database.CreateIfNotExists();

                    var diagramOptions = new EntityRelationshipDiagramOptions
                    {
                        IncludeColumnComments = false
                    };

                    var expected = @"erDiagram
    Assignment {
        Int32 Id PK
        Int32 CourseId FK
        String Description
        DateTime DueDate
        String Title
    }
    Course {
        Int32 Id PK
        String Description
        String Name
        Int32 TeacherId FK
    }
    Enrollment {
        Int32 Id PK
        Int32 CourseId FK
        DateTime EnrolledAt
        Decimal Grade
        Int32 StudentId FK
    }
    SchoolClass {
        Int32 Id PK
        Int32 Level
        String Name
        Int32 TeacherId FK
    }
    Student {
        Int32 Id PK
        DateTime DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId FK
    }
    Submission {
        Int32 Id PK
        Int32 AssignmentId FK
        String Comment
        Decimal Grade
        Int32 StudentId FK
        DateTime SubmittedAt
    }
    Teacher {
        Int32 Id PK
        String Email
        String FirstName
        String LastName
    }
    Assignment }|--|| Course : ""CourseId (Cascade)""
    Course }|--|| Teacher : ""TeacherId (Cascade)""
    Enrollment }|--|| Course : ""CourseId (Cascade)""
    Enrollment }|--|| Student : ""StudentId (Cascade)""
    SchoolClass }|--|| Teacher : ""TeacherId (Cascade)""
    Student }|--|| SchoolClass : ""SchoolClassId (Cascade)""
    Submission }|--|| Assignment : ""AssignmentId (Cascade)""
    Submission }|--|| Student : ""StudentId (Cascade)""";

                    // Act
                    var diagram = context.ToMermaidEntityDiagram(diagramOptions);
                    var result = diagram.CalculateDiagram();

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(expected, result);
                }
            }
        }

        [TestMethod]
        public void GenerateDiagram_NoLinks()
        {
            // Arrange
            using (var connection = CreateInMemoryDatabase())
            {
                using (var context = new DatabaseContextMock(connection, true))
                {
                    context.Database.CreateIfNotExists();

                    var diagramOptions = new EntityRelationshipDiagramOptions
                    {
                        IncludeLinks = false
                    };

                    var expected = @"erDiagram
    Assignment {
        Int32 Id PK ""The unique identifier for the assignment.""
        Int32 CourseId FK
        String Description
        DateTime DueDate
        String Title
    }
    Course {
        Int32 Id PK
        String Description
        String Name
        Int32 TeacherId FK
    }
    Enrollment {
        Int32 Id PK
        Int32 CourseId FK
        DateTime EnrolledAt
        Decimal Grade
        Int32 StudentId FK
    }
    SchoolClass {
        Int32 Id PK
        Int32 Level
        String Name
        Int32 TeacherId FK
    }
    Student {
        Int32 Id PK
        DateTime DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId FK
    }
    Submission {
        Int32 Id PK
        Int32 AssignmentId FK
        String Comment
        Decimal Grade ""The grade awarded for the submission, if graded.""
        Int32 StudentId FK
        DateTime SubmittedAt
    }
    Teacher {
        Int32 Id PK ""The unique identifier for the teacher.""
        String Email
        String FirstName ""The first name of the teacher.""
        String LastName ""The last name of the teacher.""
    }";

                    // Act
                    var diagram = context.ToMermaidEntityDiagram(diagramOptions);
                    var result = diagram.CalculateDiagram();

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(expected, result);
                }
            }
        }

        [TestMethod]
        public void GenerateDiagram_NoLinkLabels()
        {
            // Arrange
            using (var connection = CreateInMemoryDatabase())
            {
                using (var context = new DatabaseContextMock(connection, true))
                {
                    context.Database.CreateIfNotExists();

                    var diagramOptions = new EntityRelationshipDiagramOptions
                    {
                        IncludeLinkLabels = false
                    };

                    var expected = @"erDiagram
    Assignment {
        Int32 Id PK ""The unique identifier for the assignment.""
        Int32 CourseId FK
        String Description
        DateTime DueDate
        String Title
    }
    Course {
        Int32 Id PK
        String Description
        String Name
        Int32 TeacherId FK
    }
    Enrollment {
        Int32 Id PK
        Int32 CourseId FK
        DateTime EnrolledAt
        Decimal Grade
        Int32 StudentId FK
    }
    SchoolClass {
        Int32 Id PK
        Int32 Level
        String Name
        Int32 TeacherId FK
    }
    Student {
        Int32 Id PK
        DateTime DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId FK
    }
    Submission {
        Int32 Id PK
        Int32 AssignmentId FK
        String Comment
        Decimal Grade ""The grade awarded for the submission, if graded.""
        Int32 StudentId FK
        DateTime SubmittedAt
    }
    Teacher {
        Int32 Id PK ""The unique identifier for the teacher.""
        String Email
        String FirstName ""The first name of the teacher.""
        String LastName ""The last name of the teacher.""
    }
    Assignment }|--|| Course : "" (Cascade)""
    Course }|--|| Teacher : "" (Cascade)""
    Enrollment }|--|| Course : "" (Cascade)""
    Enrollment }|--|| Student : "" (Cascade)""
    SchoolClass }|--|| Teacher : "" (Cascade)""
    Student }|--|| SchoolClass : "" (Cascade)""
    Submission }|--|| Assignment : "" (Cascade)""
    Submission }|--|| Student : "" (Cascade)""";

                    // Act
                    var diagram = context.ToMermaidEntityDiagram(diagramOptions);
                    var result = diagram.CalculateDiagram();

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(expected, result);
                }
            }
        }

        [TestMethod]
        public void GenerateDiagram_NoLinkDeleteBehaviors()
        {
            // Arrange
            using (var connection = CreateInMemoryDatabase())
            {
                using (var context = new DatabaseContextMock(connection, true))
                {
                    context.Database.CreateIfNotExists();

                    var diagramOptions = new EntityRelationshipDiagramOptions
                    {
                        IncludeLinkDeleteBehaviors = false
                    };

                    var expected = @"erDiagram
    Assignment {
        Int32 Id PK ""The unique identifier for the assignment.""
        Int32 CourseId FK
        String Description
        DateTime DueDate
        String Title
    }
    Course {
        Int32 Id PK
        String Description
        String Name
        Int32 TeacherId FK
    }
    Enrollment {
        Int32 Id PK
        Int32 CourseId FK
        DateTime EnrolledAt
        Decimal Grade
        Int32 StudentId FK
    }
    SchoolClass {
        Int32 Id PK
        Int32 Level
        String Name
        Int32 TeacherId FK
    }
    Student {
        Int32 Id PK
        DateTime DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId FK
    }
    Submission {
        Int32 Id PK
        Int32 AssignmentId FK
        String Comment
        Decimal Grade ""The grade awarded for the submission, if graded.""
        Int32 StudentId FK
        DateTime SubmittedAt
    }
    Teacher {
        Int32 Id PK ""The unique identifier for the teacher.""
        String Email
        String FirstName ""The first name of the teacher.""
        String LastName ""The last name of the teacher.""
    }
    Assignment }|--|| Course : ""CourseId""
    Course }|--|| Teacher : ""TeacherId""
    Enrollment }|--|| Course : ""CourseId""
    Enrollment }|--|| Student : ""StudentId""
    SchoolClass }|--|| Teacher : ""TeacherId""
    Student }|--|| SchoolClass : ""SchoolClassId""
    Submission }|--|| Assignment : ""AssignmentId""
    Submission }|--|| Student : ""StudentId""";

                    // Act
                    var diagram = context.ToMermaidEntityDiagram(diagramOptions);
                    var result = diagram.CalculateDiagram();

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.AreEqual(expected, result);
                }
            }
        }
    }
}