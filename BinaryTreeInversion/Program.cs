using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTreeInversion
{
    class Program
    {
        // TODO: Implement functionality for InvertBinaryTree to accept an IEnumerable<Node> so that the inverted tree can be reinverted back to the original.
        static void Main(string[] args)
        {
            var tree = BuildBinaryTree("1", 2);

            Console.WriteLine("BINARY TREE:\n");
            Console.WriteLine(Display(new Node[] { tree })); 

            var invertedTree = InvertBinaryTree(tree);

            Console.WriteLine("INVERTED TREE: \n");
            Console.WriteLine(Display(invertedTree));
        }

        private static Node BuildBinaryTree(string value, int levels)
        {
            var children = levels == 0
                ? new Node[0]
                : new Node[]
                    {
                        BuildBinaryTree(value + "_1", levels - 1),
                        BuildBinaryTree(value + "_2", levels - 1)
                    };

            return new Node(value, children);
        }

        private static IEnumerable<Node> InvertBinaryTree(Node node, Node parent = null, Node grandparent = null)
        {
            if (parent != null) parent.Children = grandparent != null 
                ? new List<Node>() { grandparent } 
                : new List<Node>();

            return node.Children.Any()
                ? node.Children.SelectMany(child => InvertBinaryTree(child, node, parent))
                : new Node[] { new Node(node.Value, parent) };
        }

        private static string Display(IEnumerable<Node> nodes, int level = 0)
        {
            var indentation = new string('\t', level);

            return string.Join('\n', 
                nodes.Select(n => 
                    indentation + n.Value + "\n" + (n.Children.Any() 
                        ? Display(n.Children, level + 1) 
                        : "")));
        }
    }

    class Node
    {
        public Node()
        {
        }

        public Node(string value, params Node[] children)
        {
            Value = value;
            Children = children.ToList();
        }

        public string Value { get; set; }
        public List<Node> Children { get; set; }
    }
}
