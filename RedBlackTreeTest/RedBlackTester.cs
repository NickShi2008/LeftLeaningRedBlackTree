using RedBlackTreeProject;


namespace RedBlackTreeTest
{
    public class RedBlackTester
    {
        [Fact]
        public void AddTest()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            int[] arr = [50, 40, 60, 30, 70, 80, 20, 10, 90, 100];
               //  5, 110, 25, 35, 45, 55, 65, 75, 85, 95,
                // 105, 115, 120, 15, 1, 36, 46, 56, 66, 76];
            for (int i = 0; i < arr.Length; i++)
            {
                tree.Add(arr[i]);
            }



            ;
        }

        [Fact]
        public void RemoveTest()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            int[] arr = [50, 40, 60, 30, 70, 80, 20, 10, 90, 100];
            //  5, 110, 25, 35, 45, 55, 65, 75, 85, 95,
            // 105, 115, 120, 15, 1, 36, 46, 56, 66, 76];
            for (int i = 0; i < arr.Length; i++)
            {
                tree.Add(arr[i]);
            }

            Assert.True(tree.Remove(30));
            ;
        }
    }
}