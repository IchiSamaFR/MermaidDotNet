using MermaidSharp.Diagrams;
using MermaidSharp.Enums;
using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidSharp.Tests.ClassDiagrams
{
    [TestClass]
    public class ClassesDiagramTests
    {
        [TestMethod]
        public void EmptyDiagram_ReturnsHeader()
        {
            // Arrange
            var diagram = new ClassDiagram("Animal example");
            diagram.Nodes.Add(new ClassNode("Animal"));

            var expected = @"---
title: Animal example
---
classDiagram
    class Animal";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }


        #region Integration tests

        [TestMethod]
        public void FullDiagram_AnimalHierarchy_RendersCorrectly()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("Animal", properties: new List<ClassProperty>
                {
                    new ClassProperty("Name", "string", ClassPropertyVisibility.Public),
                    new ClassProperty("Age", "int", ClassPropertyVisibility.Protected),
                }, methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Speak", visibility: ClassPropertyVisibility.Public),
                    new ClassMethod(name: "Sleep", visibility: ClassPropertyVisibility.Protected),
                }),
                new ClassNode("Dog", properties: new List<ClassProperty>
                {
                    new ClassProperty("Breed", "string", ClassPropertyVisibility.Public),
                }, methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "Fetch", visibility: ClassPropertyVisibility.Public),
                }),
            };
            var links = new List<ClassLink>
            {
                new ClassLink("Animal", "Dog", ClassLinkType.Inheritance, "extends")
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    class Animal {
        +string Name
        #int Age
        +Speak()
        #Sleep()
    }
    class Dog {
        +string Breed
        +Fetch()
    }
    Animal<|--Dog : extends";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FullDiagram_RepositoryPattern_RendersCorrectly()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("IRepository", methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "GetById", returnType: "string", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("id", "int")
                    }),
                    new ClassMethod(name: "Save", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("entity", "string")
                    }),
                }),
                new ClassNode("UserRepository", properties: new List<ClassProperty>
                {
                    new ClassProperty("connectionString", "string", ClassPropertyVisibility.Private),
                }, methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "GetById", returnType: "string", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("id", "int")
                    }),
                }),
            };
            var links = new List<ClassLink>
            {
                new ClassLink("UserRepository", "IRepository", ClassLinkType.Realization)
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    class IRepository {
        +GetById(int id) string
        +Save(string entity)
    }
    class UserRepository {
        -string connectionString
        +GetById(int id) string
    }
    UserRepository..|>IRepository";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FullDiagram_AllLinkTypes_RendersAllCorrectly()
        {
            // Arrange
            var links = new List<ClassLink>
            {
                new ClassLink("A", "B", ClassLinkType.Inheritance),
                new ClassLink("B", "C", ClassLinkType.Composition),
                new ClassLink("C", "D", ClassLinkType.Aggregation),
                new ClassLink("D", "E", ClassLinkType.Association),
                new ClassLink("E", "F", ClassLinkType.SolidLink),
                new ClassLink("F", "G", ClassLinkType.Dependency),
                new ClassLink("G", "H", ClassLinkType.Realization),
                new ClassLink("H", "A", ClassLinkType.DashedLink),
            };
            var diagram = new ClassDiagram();
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    A<|--B
    B*--C
    Co--D
    D-->E
    E--F
    F..>G
    G..|>H
    H..A";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        [TestMethod]
        public void ComprehensiveClassDiagram_RendersAllCases()
        {
            // Arrange
            var nodes = new List<ClassNode>
            {
                new ClassNode("User", properties: new List<ClassProperty>
                {
                    new ClassProperty("Id", "int", ClassPropertyVisibility.Public),
                    new ClassProperty("Name", "string", ClassPropertyVisibility.Public),
                    new ClassProperty("Email", "string", ClassPropertyVisibility.Private),
                    new ClassProperty("Roles", "List<string>", ClassPropertyVisibility.Internal),
                    new ClassProperty("IsActive", "bool", ClassPropertyVisibility.Protected),
                    new ClassProperty("CreatedAt", "DateTime", ClassPropertyVisibility.None)
                }, methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "GetFullName", returnType: "string", visibility: ClassPropertyVisibility.Public),
                    new ClassMethod(name: "SetEmail", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("email", "string")
                    }),
                    new ClassMethod(name: "Activate", visibility: ClassPropertyVisibility.Internal),
                    new ClassMethod(name: "Deactivate", visibility: ClassPropertyVisibility.Protected),
                    new ClassMethod(name: "GetRoles", returnType: "List<string>", visibility: ClassPropertyVisibility.Public),
                    new ClassMethod(name: "IsAdmin", returnType: "bool", visibility: ClassPropertyVisibility.Private)
                }),
                new ClassNode("Admin", properties: new List<ClassProperty>
                {
                    new ClassProperty("Level", "int", ClassPropertyVisibility.Public)
                }, methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "GrantPermission", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("permission", "string")
                    }),
                    new ClassMethod(name: "RevokePermission", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("permission", "string")
                    })
                }),
                new ClassNode("Group", properties: new List<ClassProperty>
                {
                    new ClassProperty("Name", "string", ClassPropertyVisibility.Public),
                    new ClassProperty("Members", "List<User>", ClassPropertyVisibility.Public)
                }, methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "AddMember", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("user", "User")
                    }),
                    new ClassMethod(name: "RemoveMember", visibility: ClassPropertyVisibility.Public, parameters: new List<ClassMethodParam>
                    {
                        new ClassMethodParam("user", "User")
                    })
                }),
                new ClassNode("Permission", properties: new List<ClassProperty>
                {
                    new ClassProperty("Name", "string", ClassPropertyVisibility.Public)
                }, methods: new List<ClassMethod>
                {
                    new ClassMethod(name: "IsValid", returnType: "bool", visibility: ClassPropertyVisibility.Public)
                })
            };

            var links = new List<ClassLink>
            {
                new ClassLink("User", "Admin", ClassLinkType.Inheritance, "inherits"),
                new ClassLink("Admin", "Permission", ClassLinkType.Association, "has"),
                new ClassLink("Group", "User", ClassLinkType.Aggregation, "contains"),
                new ClassLink("User", "Permission", ClassLinkType.Dependency, "uses"),
                new ClassLink("Group", "Permission", ClassLinkType.Realization, "implements")
            };
            var diagram = new ClassDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);

            var expected = @"classDiagram
    class User {
        +int Id
        +string Name
        -string Email
        ~List~string~ Roles
        #bool IsActive
        DateTime CreatedAt
        +GetFullName() string
        +SetEmail(string email)
        ~Activate()
        #Deactivate()
        +GetRoles() List~string~
        -IsAdmin() bool
    }
    class Admin {
        +int Level
        +GrantPermission(string permission)
        +RevokePermission(string permission)
    }
    class Group {
        +string Name
        +List~User~ Members
        +AddMember(User user)
        +RemoveMember(User user)
    }
    class Permission {
        +string Name
        +IsValid() bool
    }
    User<|--Admin : inherits
    Admin-->Permission : has
    Groupo--User : contains
    User..>Permission : uses
    Group..|>Permission : implements";

            // Act
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }

}
