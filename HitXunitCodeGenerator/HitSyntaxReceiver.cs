using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace HitXunitCodeGenerator
{
    public class HitSyntaxReceiver : ISyntaxReceiver
    {
        private readonly List<ClassDeclarationSyntax> _useAsAttributes = new();
        private readonly List<ClassDeclarationSyntax> _sysConAttributes = new();

        public IEnumerable<ClassDeclarationSyntax> GetUseAsAttributeSyntax => _useAsAttributes.AsReadOnly();

        public IEnumerable<ClassDeclarationSyntax> GetSysConAttributeSyntax => _sysConAttributes.AsReadOnly();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not AttributeSyntax attribute)
                return;

            
            var name = ExtractName(attribute.Name);
            if (name.Equals("UseAs") || name.Equals("UseAsAttribute"))
            {
                if (attribute.Parent?.Parent is ClassDeclarationSyntax syntax)
                {
                    _useAsAttributes.Add(syntax);
                }
                
            }
            else if (name.Equals("SysCon") || name.Equals("SysConAttribute"))
            {
                if (attribute.Parent?.Parent is ClassDeclarationSyntax syntax)
                {
                    _sysConAttributes.Add(syntax);
                }
            }
        }

        public IEnumerable<AttributeDataFromSyntax> GetUseAsAttributeDataFromSyntax(GeneratorExecutionContext context) => GetAttributeDataFromSyntax(_useAsAttributes, context);

        public IEnumerable<AttributeDataFromSyntax> GetSysConAttributeDataFromSyntax(GeneratorExecutionContext context) => GetAttributeDataFromSyntax(_sysConAttributes, context);

        private IEnumerable<AttributeDataFromSyntax> GetAttributeDataFromSyntax(IEnumerable<ClassDeclarationSyntax> classInfos, GeneratorExecutionContext context)
        {
            var l = new List<AttributeDataFromSyntax>();

            foreach (var classInfo in classInfos)
            {
                var model = context.Compilation.GetSemanticModel(classInfo.SyntaxTree, true);
                var type = model.GetDeclaredSymbol(classInfo) as ITypeSymbol;
                if (!type.IsAbstract)
                {
                    var attributes = type.GetAttributes();
                    foreach (var a in attributes)
                    {
                        var adfs = new AttributeDataFromSyntax(a.AttributeClass.Name);
                        l.Add(adfs);
                        foreach (var ca in a.ConstructorArguments)
                        {
                            adfs.AddConstructorArgument(ca.Value);
                        }
                        foreach (var na in a.NamedArguments)
                        {
                            var key = na.Key;
                            var value = na.Value.Value;
                            adfs.AddNamedArgument(key, value);
                        }
                    }
                }
            }

            return l.ToArray();
        }

        private static string ExtractName(TypeSyntax type)
        {
            while (type != null)
            {
                switch (type)
                {
                    case IdentifierNameSyntax ins:
                        return ins.Identifier.Text;
                    case QualifiedNameSyntax qns:
                        type = qns.Right;
                        break;
                    default:
                        return null;
                }
            }

            return null;
        }

    }

}
