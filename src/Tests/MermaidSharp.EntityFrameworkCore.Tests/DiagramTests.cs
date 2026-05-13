using MermaidSharp.EntityFrameworkCore.Tests.Mock;
using MermaidSharp.Enums;
using Microsoft.EntityFrameworkCore;

namespace MermaidSharp.EntityFrameworkCore.Tests
{
    [TestClass]
    public class DiagramTests
    {
        [TestMethod]
        public void GenerateIdentityDiagram()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseIdentityContextMock>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DatabaseIdentityContextMock(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var expected = @"erDiagram
    IdentityRole {
        Int32 Id PK
        String ConcurrencyStamp
        String Name
        String NormalizedName UK
    }
    IdentityRoleClaim {
        Int32 Id PK
        String ClaimType
        String ClaimValue
        Int32 RoleId FK
    }
    IdentityUserClaim {
        Int32 Id PK
        String ClaimType
        String ClaimValue
        Int32 UserId FK
    }
    IdentityUserLogin {
        String LoginProvider PK
        String ProviderKey PK
        String ProviderDisplayName
        Int32 UserId FK
    }
    IdentityUserRole {
        Int32 RoleId PK, FK
        Int32 UserId PK, FK
    }
    IdentityUserToken {
        String LoginProvider PK
        String Name PK
        Int32 UserId PK, FK
        String Value
    }
    User {
        Int32 Id PK
        Int32 AccessFailedCount
        Int32 AuditId FK
        String ConcurrencyStamp
        String Email
        Boolean EmailConfirmed
        Boolean LockoutEnabled
        DateTimeOffset LockoutEnd
        String NormalizedEmail
        String NormalizedUserName UK
        String PasswordHash
        String PhoneNumber
        Boolean PhoneNumberConfirmed
        String SecurityStamp
        Boolean TwoFactorEnabled
        String UserName
    }
    UserAuditInfo {
        Int32 Id PK
        DateTime CreatedAt
        Int32 CreatedBy
        DateTime ModifiedAt
        Int32 ModifiedBy
    }
    IdentityRoleClaim }|--|| IdentityRole : ""RoleId (Cascade)""
    IdentityUserClaim }|--|| User : ""UserId (Cascade)""
    IdentityUserLogin }|--|| User : ""UserId (Cascade)""
    IdentityUserRole }|--|| IdentityRole : ""RoleId (Cascade)""
    IdentityUserRole }|--|| User : ""UserId (Cascade)""
    IdentityUserToken }|--|| User : ""UserId (Cascade)""
    User }|--|| UserAuditInfo : ""AuditId (Cascade)""";

            // Act
            var diagram = context.ToMermaidEntityDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GenerateDiagram()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContextMock>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DatabaseContextMock(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var expected = @"erDiagram
    Assignment {
        Int32 Id PK ""The unique identifier for the assignment.""
        Int32 CourseId FK
        String Description
        DateOnly DueDate
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
        DateOnly DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId FK
        String Address_City
        String Address_PostalCode ""37000, 63400, ...""
        String Address_Street
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
    Course }|--|| Teacher : ""TeacherId (Restrict)""
    Enrollment }|--|| Course : ""CourseId (Cascade)""
    Enrollment }|--|| Student : ""StudentId (Cascade)""
    SchoolClass }|--|| Teacher : ""TeacherId (Restrict)""
    Student }|--|| SchoolClass : ""SchoolClassId (Restrict)""
    Submission }|--|| Assignment : ""AssignmentId (Cascade)""
    Submission }|--|| Student : ""StudentId (Restrict)""";

            // Act
            var diagram = context.ToMermaidEntityDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GenerateDiagram_NoColumnTypes()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContextMock>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DatabaseContextMock(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
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
    Course }|--|| Teacher : ""TeacherId (Restrict)""
    Enrollment }|--|| Course : ""CourseId (Cascade)""
    Enrollment }|--|| Student : ""StudentId (Cascade)""
    SchoolClass }|--|| Teacher : ""TeacherId (Restrict)""
    Student }|--|| SchoolClass : ""SchoolClassId (Restrict)""
    Submission }|--|| Assignment : ""AssignmentId (Cascade)""
    Submission }|--|| Student : ""StudentId (Restrict)""";

            // Act
            var diagram = context.ToMermaidEntityDiagram(diagramOptions);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GenerateDiagram_NoColumnKeyTypes()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContextMock>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DatabaseContextMock(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var diagramOptions = new EntityRelationshipDiagramOptions
            {
                IncludeColumnKeyTypes = false
            };
            var expected = @"erDiagram
    Assignment {
        Int32 Id ""The unique identifier for the assignment.""
        Int32 CourseId
        String Description
        DateOnly DueDate
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
        DateOnly DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId
        String Address_City
        String Address_PostalCode ""37000, 63400, ...""
        String Address_Street
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
    Course }|--|| Teacher : ""TeacherId (Restrict)""
    Enrollment }|--|| Course : ""CourseId (Cascade)""
    Enrollment }|--|| Student : ""StudentId (Cascade)""
    SchoolClass }|--|| Teacher : ""TeacherId (Restrict)""
    Student }|--|| SchoolClass : ""SchoolClassId (Restrict)""
    Submission }|--|| Assignment : ""AssignmentId (Cascade)""
    Submission }|--|| Student : ""StudentId (Restrict)""";

            // Act
            var diagram = context.ToMermaidEntityDiagram(diagramOptions);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GenerateDiagram_FilteredColumnKeyTypes()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContextMock>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DatabaseContextMock(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var diagramOptions = new EntityRelationshipDiagramOptions
            {
                FilterColumnByKeyTypes = RelationConstraintType.PrimaryKey | RelationConstraintType.ForeignKey
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
    Course }|--|| Teacher : ""TeacherId (Restrict)""
    Enrollment }|--|| Course : ""CourseId (Cascade)""
    Enrollment }|--|| Student : ""StudentId (Cascade)""
    SchoolClass }|--|| Teacher : ""TeacherId (Restrict)""
    Student }|--|| SchoolClass : ""SchoolClassId (Restrict)""
    Submission }|--|| Assignment : ""AssignmentId (Cascade)""
    Submission }|--|| Student : ""StudentId (Restrict)""";

            // Act
            var diagram = context.ToMermaidEntityDiagram(diagramOptions);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void GenerateDiagram_NoColumnWithoutKeyTypes()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContextMock>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DatabaseContextMock(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var diagramOptions = new EntityRelationshipDiagramOptions
            {
                IncludeColumnComments = false
            };
            var expected = @"erDiagram
    Assignment {
        Int32 Id PK
        Int32 CourseId FK
        String Description
        DateOnly DueDate
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
        DateOnly DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId FK
        String Address_City
        String Address_PostalCode
        String Address_Street
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
    Course }|--|| Teacher : ""TeacherId (Restrict)""
    Enrollment }|--|| Course : ""CourseId (Cascade)""
    Enrollment }|--|| Student : ""StudentId (Cascade)""
    SchoolClass }|--|| Teacher : ""TeacherId (Restrict)""
    Student }|--|| SchoolClass : ""SchoolClassId (Restrict)""
    Submission }|--|| Assignment : ""AssignmentId (Cascade)""
    Submission }|--|| Student : ""StudentId (Restrict)""";

            // Act
            var diagram = context.ToMermaidEntityDiagram(diagramOptions);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GenerateDiagram_NoLinks()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContextMock>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DatabaseContextMock(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var diagramOptions = new EntityRelationshipDiagramOptions
            {
                IncludeLinks = false
            };
            var expected = @"erDiagram
    Assignment {
        Int32 Id PK ""The unique identifier for the assignment.""
        Int32 CourseId FK
        String Description
        DateOnly DueDate
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
        DateOnly DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId FK
        String Address_City
        String Address_PostalCode ""37000, 63400, ...""
        String Address_Street
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

        [TestMethod]
        public void GenerateDiagram_NoLinkLabels()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContextMock>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DatabaseContextMock(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var diagramOptions = new EntityRelationshipDiagramOptions
            {
                IncludeLinkLabels = false
            };
            var expected = @"erDiagram
    Assignment {
        Int32 Id PK ""The unique identifier for the assignment.""
        Int32 CourseId FK
        String Description
        DateOnly DueDate
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
        DateOnly DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId FK
        String Address_City
        String Address_PostalCode ""37000, 63400, ...""
        String Address_Street
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
    Course }|--|| Teacher : "" (Restrict)""
    Enrollment }|--|| Course : "" (Cascade)""
    Enrollment }|--|| Student : "" (Cascade)""
    SchoolClass }|--|| Teacher : "" (Restrict)""
    Student }|--|| SchoolClass : "" (Restrict)""
    Submission }|--|| Assignment : "" (Cascade)""
    Submission }|--|| Student : "" (Restrict)""";

            // Act
            var diagram = context.ToMermaidEntityDiagram(diagramOptions);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GenerateDiagram_NoLinkDeleteBehaviors()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContextMock>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DatabaseContextMock(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            var diagramOptions = new EntityRelationshipDiagramOptions
            {
                IncludeLinkDeleteBehaviors = false
            };
            var expected = @"erDiagram
    Assignment {
        Int32 Id PK ""The unique identifier for the assignment.""
        Int32 CourseId FK
        String Description
        DateOnly DueDate
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
        DateOnly DateOfBirth
        String FirstName
        String LastName
        Int32 SchoolClassId FK
        String Address_City
        String Address_PostalCode ""37000, 63400, ...""
        String Address_Street
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
