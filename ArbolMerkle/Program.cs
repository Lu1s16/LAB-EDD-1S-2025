using merkle;

class Program
{
    static void Main(string[] args)
    {
        MerkleTree tree = new MerkleTree();

        tree.Insert(1, 101, 150.00);
        tree.Insert(2, 101, 160.5);
        tree.Insert(3, 102, 160.00);
        tree.Insert(4, 103, 170.5);
        tree.Insert(5, 104, 1850.10);

        Console.WriteLine(tree.GenerateDot());
        

    }
}