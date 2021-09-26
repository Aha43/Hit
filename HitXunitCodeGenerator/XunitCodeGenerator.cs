using Microsoft.CodeAnalysis;
using System.Text;

namespace HitXunitCodeGenerator
{
    [Generator]
    public class XunitCodeGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var receiver = (HitSyntaxReceiver)context.SyntaxReceiver;

            var (name, source) = GetClassSource(receiver, context);
            context.AddSource($"{name}.g.cs", source);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new HitSyntaxReceiver());
        }

        private (string name, string source) GetClassSource(HitSyntaxReceiver syntaxReceiver, GeneratorExecutionContext context)
        {
            

            var sb = new StringBuilder();

            //var coordinates = GetCoordinates();

            sb.AppendLine("public class HitTestGeneratedClass")
              .AppendLine("{");

            var useAsInfo = syntaxReceiver.GetUseAsAttributeDataFromSyntax(context);
            var sysConInfo = syntaxReceiver.GetSysConAttributeDataFromSyntax(context);

            //foreach (var attr in syntaxReceiver.GetUseAsAttributeSyntax)
            //{
            //    var model = context.Compilation.GetSemanticModel(attr.SyntaxTree, true);
            //    var type = model.GetDeclaredSymbol(attr) as ITypeSymbol;
            //    var attributes = type.GetAttributes();
            //    foreach (var a in attributes)
            //    {
                    
            //        foreach (var ca in a.ConstructorArguments)
            //        {
            //            var n = ca.Value;
            //        }
            //        foreach (var na in a.NamedArguments)
            //        {
            //            var key = na.Key;
            //            var n = na.Value.Value;
            //        }
                    
            //    }
                

            //    //var name = attr.Name;
            //    //foreach (var arg in attr.ArgumentList.Arguments)
            //    //{
            //    //    var t = arg.Expression;
            //    //    //var t = arg[0];
            //    //}
            //    //sb.Append($"private string attr_{name} = null;");
            //}

            sb.AppendLine("}");

            return ("Test3", sb.ToString());
        }

        //private IHitSpaceCoordinates GetCoordinates()
        //{
        //    var type = typeof(IHitSpaceCoordinates);
        //    var types = AppDomain.CurrentDomain.GetAssemblies()
        //        .SelectMany(s => s.GetTypes())
        //        .Where(p => type.IsAssignableFrom(p));

        //    var count = types.Count();
        //    if (count == 0)
        //    {
        //        throw new Exception("No IHitSpaceCoordinates type found");
        //    }
        //    else if (count == 1)
        //    {
        //        var found = types.First();
        //        return Activator.CreateInstance(found) as IHitSpaceCoordinates;
        //    }

        //    throw new Exception("More than one IHitSpaceCoordinate types found");
        //}

    }

}
