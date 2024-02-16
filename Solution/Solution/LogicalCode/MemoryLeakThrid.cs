namespace Solution.LogicalCode
{
    public class MemoryLeakThrid
    {
        public void Main()
        {
            var rootNode = new TreeNode();
            while (true)
            {
                // create a new subtree of 10000 nodes
                var newNode = new TreeNode();
                for (int i = 0; i < 10000; i++)
                {
                    var childNode = new TreeNode();
                    newNode.AddChild(childNode);
                }
                rootNode.AddChild(newNode);
            }   
        }

        class TreeNode
        {
            private readonly List<TreeNode> _children = new List<TreeNode>();
            public void AddChild(TreeNode child)
            {
                _children.Add(child);
            }
        }
    }
}
