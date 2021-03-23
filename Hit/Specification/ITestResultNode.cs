using System.Collections.Generic;

namespace Hit.Specification
{
    public interface ITestResultNode
    {
        ITestResultNode Parent { get; }
        IEnumerable<ITestResultNode> Children { get; }
        bool IsRoot { get; }
        bool IsLeaf { get; }
        bool IsInternal { get; }
    }
}
