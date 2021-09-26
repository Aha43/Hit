using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HitXunitCodeGenerator
{
    public class AttributeDataFromSyntax
    {
        private readonly Dictionary<string, string> _attributes = new();

        private readonly List<string> _consAttribute = new();

        private readonly string _name;

        public AttributeDataFromSyntax(string name) => _name = name;

        public string Name => _name;

        public void AddConstructorArgument(object value)
        {
            if (value != null)
            {
                _consAttribute.Add(value.ToString());
            }
        }

        public int ConstructorArgumentsCount => _consAttribute.Count;

        public string GetConstructorArgument(int i) => _consAttribute[i];

        public void AddNamedArgument(string name, object value)
        {
            if (name != null && value != null)
            {
                _attributes.Add(name, value.ToString());
            }
        }

        public string GetNamedArgumentValue(string name)
        {
            if (_attributes.TryGetValue(name, out string retVal))
            {
                return retVal;
            }

            return null;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("Name: ").Append(Name).Append(", ConstructorArgs: (")
              .Append(string.Join(",", _consAttribute)).Append(", NamedArguments: (")
              .Append(string.Join(",", _attributes.Select(e => e.Key + "=" + e.Value))).AppendLine(")");
           
            return sb.ToString();
        }

    }

}
