using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.Tests.EntityRelationships
{
    [TestClass]
    public class EntityRelationshipDiagramTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeNodesAndLinks_WithComplexDatabase()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("User", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("Id", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("Name", "string"),
                    new EntityRelationColumn("Email", "string")
                }),
                new EntityRelationNode("Order", columns:  new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("OrderId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("UserId", "int", RelationConstraintType.ForeignKey),
                    new EntityRelationColumn("OrderDate", "datetime")
                }),
                new EntityRelationNode("Product", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("ProductId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("Title", "string"),
                    new EntityRelationColumn("Price", "decimal")
                }),
                new EntityRelationNode("OrderItem", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("OrderItemId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("OrderId", "int", RelationConstraintType.ForeignKey),
                    new EntityRelationColumn("ProductId", "int", RelationConstraintType.ForeignKey),
                    new EntityRelationColumn("Quantity", "int")
                }),
                new EntityRelationNode("Category", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("CategoryId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("Name", "string")
                }),
                new EntityRelationNode("ProductCategory", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("ProductId", "int", RelationConstraintType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("CategoryId", "int", RelationConstraintType.PrimaryKeyForeignKey)
                })
            };

            var links = new List<EntityRelationLink>
            {
                new EntityRelationLink("User", "Order", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "UserId (Cascade)"),
                new EntityRelationLink("Order", "OrderItem", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "OrderId"),
                new EntityRelationLink("Product", "OrderItem", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "ProductId"),
                new EntityRelationLink("Product", "ProductCategory", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "ProductId"),
                new EntityRelationLink("Category", "ProductCategory", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "CategoryId")
            };
            var diagram = new EntityRelationshipDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);

            var expected = @"erDiagram
    User {
        int Id PK
        string Name
        string Email
    }
    Order {
        int OrderId PK
        int UserId FK
        datetime OrderDate
    }
    Product {
        int ProductId PK
        string Title
        decimal Price
    }
    OrderItem {
        int OrderItemId PK
        int OrderId FK
        int ProductId FK
        int Quantity
    }
    Category {
        int CategoryId PK
        string Name
    }
    ProductCategory {
        int ProductId PK, FK
        int CategoryId PK, FK
    }
    User }|--o{ Order : ""UserId (Cascade)""
    Order }|--o{ OrderItem : ""OrderId""
    Product }|--o{ OrderItem : ""ProductId""
    Product }|--o{ ProductCategory : ""ProductId""
    Category }|--o{ ProductCategory : ""CategoryId""";

            // Act;
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldReturnEmptyDiagram_WhenNoNodesOrLinks()
        {
            // Arrange
            var diagram = new EntityRelationshipDiagram();
            var expected = "erDiagram";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldHandleSingleNodeWithoutLinks()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("Customer", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("CustomerId", "int"),
                    new EntityRelationColumn("FullName", "string")
                })
            };
            var diagram = new EntityRelationshipDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"erDiagram
    Customer {
        int CustomerId
        string FullName
    }";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldHandleCircularRelations()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("Person", columns : new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("PersonId", "int", RelationConstraintType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("ManagerId", "int", RelationConstraintType.PrimaryKeyForeignKey)
                })
            };
            var links = new List<EntityRelationLink>
            {
                new EntityRelationLink("Person", "Person", RelationLinkType.ZeroOrOne, RelationLinkType.ZeroOrMore, "ManagerId")
            };
            var diagram = new EntityRelationshipDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);

            var expected = @"erDiagram
    Person {
        int PersonId PK, FK
        int ManagerId PK, FK
    }
    Person |o--o{ Person : ""ManagerId""";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldHandleMultipleLinksBetweenSameEntities()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("Author", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("AuthorId", "int"),
                    new EntityRelationColumn("Name", "string")
                }),
                new EntityRelationNode("Book", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("BookId", "int"),
                    new EntityRelationColumn("Title", "string"),
                    new EntityRelationColumn("AuthorId", "int"),
                    new EntityRelationColumn("EditorId", "int")
                })
            };
            var links = new List<EntityRelationLink>
            {
                new EntityRelationLink("Author", "Book", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "AuthorId"),
                new EntityRelationLink("Author", "Book", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "EditorId")
            };
            var diagram = new EntityRelationshipDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);

            var expected = @"erDiagram
    Author {
        int AuthorId
        string Name
    }
    Book {
        int BookId
        string Title
        int AuthorId
        int EditorId
    }
    Author }|--o{ Book : ""AuthorId""
    Author }|--o{ Book : ""EditorId""";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldHandleVariousRelationTypesAndEdgeCases()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("Person", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("PersonId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("SpouseId", "int", RelationConstraintType.ForeignKey)
                }),
                new EntityRelationNode("Company", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("CompanyId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("Name", "string")
                }),
                new EntityRelationNode("Employee", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("EmployeeId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("PersonId", "int", RelationConstraintType.ForeignKey),
                    new EntityRelationColumn("CompanyId", "int", RelationConstraintType.ForeignKey)
                }),
                new EntityRelationNode("Project", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("ProjectId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("Title", "string")
                }),
                new EntityRelationNode("EmployeeProject", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("EmployeeId", "int", RelationConstraintType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("ProjectId", "int", RelationConstraintType.PrimaryKeyForeignKey)
                }),
                new EntityRelationNode("Department", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("DepartmentId", "int", RelationConstraintType.PrimaryKey),
                    new EntityRelationColumn("Name", "string")
                }),
                new EntityRelationNode("DepartmentManager", columns: new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("DepartmentId", "int", RelationConstraintType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("PersonId", "int", RelationConstraintType.PrimaryKeyForeignKey)
                })
            };

            var links = new List<EntityRelationLink>
            {
		        // One-to-one (circular)
		        new EntityRelationLink("Person", "Person", RelationLinkType.ZeroOrOne, RelationLinkType.ZeroOrOne, "SpouseId"),
		        // One-to-many
		        new EntityRelationLink("Company", "Employee", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "CompanyId"),
		        // Many-to-many
		        new EntityRelationLink("Employee", "EmployeeProject", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "EmployeeId"),
                new EntityRelationLink("Project", "EmployeeProject", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "ProjectId"),
		        // Multiple links between same entities
		        new EntityRelationLink("Department", "DepartmentManager", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "DepartmentId"),
                new EntityRelationLink("Person", "DepartmentManager", RelationLinkType.OneOrMore, RelationLinkType.ZeroOrMore, "PersonId"),
		        // No foreign key relation
		        new EntityRelationLink("Employee", "Department", RelationLinkType.ZeroOrMore, RelationLinkType.ZeroOrMore, "DepartmentId")
            };

            var expected = @"erDiagram
    Person {
        int PersonId PK
        int SpouseId FK
    }
    Company {
        int CompanyId PK
        string Name
    }
    Employee {
        int EmployeeId PK
        int PersonId FK
        int CompanyId FK
    }
    Project {
        int ProjectId PK
        string Title
    }
    EmployeeProject {
        int EmployeeId PK, FK
        int ProjectId PK, FK
    }
    Department {
        int DepartmentId PK
        string Name
    }
    DepartmentManager {
        int DepartmentId PK, FK
        int PersonId PK, FK
    }
    Person |o--o| Person : ""SpouseId""
    Company }|--o{ Employee : ""CompanyId""
    Employee }|--o{ EmployeeProject : ""EmployeeId""
    Project }|--o{ EmployeeProject : ""ProjectId""
    Department }|--o{ DepartmentManager : ""DepartmentId""
    Person }|--o{ DepartmentManager : ""PersonId""
    Employee }o--o{ Department : ""DepartmentId""";

            // Act
            var diagram = new EntityRelationshipDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);

            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
