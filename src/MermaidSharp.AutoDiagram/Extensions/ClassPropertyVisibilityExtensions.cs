using MermaidSharp.Enums;

namespace MermaidSharp.AutoDiagram.Extensions
{
    /// <summary>
    /// Provides extension members for <see cref="ClassPropertyVisibility"/>.
    /// </summary>
    public static class ClassPropertyVisibilityExtensions
    {
        public static ClassPropertyVisibility PublicAndProtected =>
            ClassPropertyVisibility.Public
            | ClassPropertyVisibility.Protected;

        public static ClassPropertyVisibility PublicAndInternal =>
            ClassPropertyVisibility.Public
            | ClassPropertyVisibility.Internal;

        public static ClassPropertyVisibility PublicAndPrivate =>
            ClassPropertyVisibility.Public
            | ClassPropertyVisibility.Private;

        public static ClassPropertyVisibility ProtectedAndInternal =>
            ClassPropertyVisibility.Protected
            | ClassPropertyVisibility.Internal;

        public static ClassPropertyVisibility ProtectedAndPrivate =>
            ClassPropertyVisibility.Protected
            | ClassPropertyVisibility.Private;

        public static ClassPropertyVisibility InternalAndPrivate =>
            ClassPropertyVisibility.Internal
            | ClassPropertyVisibility.Private;

        public static ClassPropertyVisibility All =>
            ClassPropertyVisibility.Public
            | ClassPropertyVisibility.Protected
            | ClassPropertyVisibility.Internal
            | ClassPropertyVisibility.Private;
    }
}