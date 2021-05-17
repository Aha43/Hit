using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace CodeGenTest
{
    [Generator]
    public class MySourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var sb = new StringBuilder();
            sb.AppendLine("using Xunit;")
                .AppendLine("")
                .AppendLine("public class UnitTest")
                .AppendLine("{")
                    .AppendLine("  [Fact]")
                    .AppendLine("  public void GeneratedTest()")
                    .AppendLine("  {")
                    .AppendLine("  }")
                .AppendLine("}");


            context.AddSource("hitUnitTestGenerator", sb.ToString());
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            
        }

    }
   
}
