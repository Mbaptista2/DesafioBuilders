using System;
using System.Collections.Generic;

namespace BST
{
    public class BinaryTree
    {
        public Node Root { get; set; }

        public BinaryTree()
        {
        }

        public BinaryTree(List<int> Datas)
        {
            foreach (var item in Datas)
            {
                this.Add(item);
            }
        }

        public bool Add(int value)
        {
            Node before = null, after = this.Root;

            while (after != null)
            {
                before = after;
                if (value < after.Data)
                    after = after.LeftNode;
                else if (value > after.Data)
                    after = after.RightNode;
                else
                {
                    return false;
                }
            }

            Node newNode = new Node();
            newNode.Data = value;

            if (this.Root == null)
                this.Root = newNode;
            else
            {
                if (value < before.Data)
                    before.LeftNode = newNode;
                else
                    before.RightNode = newNode;
            }

            return true;
        }

        public Node Find(int value)
        {
            return this.Find(value, this.Root);
        }

        private Node Find(int value, Node parent)
        {
            if (parent != null)
            {
                if (value == parent.Data) return parent;
                if (value < parent.Data)
                    return Find(value, parent.LeftNode);
                else
                    return Find(value, parent.RightNode);
            }

            return null;
        }

        public int GetTreeDepth()
        {
            return this.GetTreeDepth(this.Root);
        }

        private int GetTreeDepth(Node parent)
        {
            return parent == null ? 0 : Math.Max(GetTreeDepth(parent.LeftNode), GetTreeDepth(parent.RightNode)) + 1;
        }

        private void CreateList(Node parent, List<int> nodes)
        {
            if (parent != null)
            {
                CreateList(parent.LeftNode, nodes);
                nodes.Add(parent.Data);
                CreateList(parent.RightNode, nodes);
            }
        }

        public List<int> GetAllNodes()
        {
            List<int> ret = new List<int>();
            CreateList(Root, ret);
            return ret;
        }
    }
}