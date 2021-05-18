using Hit.Specification.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hit.Infrastructure.UnitTestsSourceGenerators
{
    public class XunitTestsSourceGenerator<World> : IUnitTestSourceGenerator<World>
    {
        public string GenerateCode(IUnitTestsSpace<World> space)
        {
            throw new NotImplementedException();
        }

        private void WriteInlineData(StringBuilder sb, IUnitTestsSpace<World> space)
        {
            WriteInlineData(sb, space.UnitTestCoordinates);
        }

        private void WriteInlineData(StringBuilder sb, IEnumerable<(string system, string layer, string unitTest)> coordinates)
        {
            foreach (var c in coordinates)
            {
                //string system = string.IsNullOrWhiteSpace(c.system) ? "null" : 
                //sb.Append("[InlineData(\"").Append(c.system).Append("\", \"").Append(c.layer).Append("\", ")
            }
        }

    }

}
