class Repuestos
{
    public int ID;
    public string Repuesto;
    public string Detalle;
    public double Costo;

    public Repuestos(int id, string repuesto, string detalle, double costo)
    {
        ID = id;
        Repuesto = repuesto;
        Detalle = detalle;
        Costo = costo;
    }

}


class AvlNode
{
    public Repuestos item;
    public AvlNode? right;
    public AvlNode? left;
    public int height;

    public AvlNode(Repuestos Item)
    {
        item = Item;
        right = null;
        left = null;
        height = 0;
    }
}

class AvlTree
{
    private AvlNode root;
    string connections = "";
    string nodes = "";

    public void insert(Repuestos item)
    {
        root = insertRecursive(item, root);
    }

    public AvlNode insertRecursive(Repuestos item, AvlNode node)
    {

        if(node == null)
        {
            node = new AvlNode(item);

        } else if(item.ID < node.item.ID)
        {
            //lado izquierdo
            node.left = insertRecursive(item, node.left);
            if(GetHeight(node.left) - GetHeight(node.right) == 2) //Si se cumple hay desequilibrio
            {
                //Rotacion derecha
                if(item.ID < node.left.item.ID)
                {
                    node = rotateRight(node);

                } else {
                    node = doubleRight(node);

                }

            }


        } else if(item.ID > node.item.ID)
        {
            //lado derecho
            node.right = insertRecursive(item, node.right);
            if(GetHeight(node.right) - GetHeight(node.left) == 2)
            {
                //Rotacion izquirda
                if(item.ID > node.right.item.ID)
                {
                    node = rotateleft(node);
                } else {
                    node = doubleLeft(node);
                }

            }


        } else {
            Console.WriteLine("Elemento ya insertado en el arbol.");
        }

        node.height = getMaxheight(GetHeight(node.left), GetHeight(node.right)) + 1;
        return node;


    }
    public AvlNode rotateRight(AvlNode node2)
    {
        AvlNode node1 = node2.left;
        node2.left = node1.right;
        node1.right = node2;
        node2.height = getMaxheight(GetHeight(node2.left), GetHeight(node2.right)) + 1;
        node1.height = getMaxheight(GetHeight(node1.left), node2.height) + 1;
        return node1;
    }

    public AvlNode doubleRight(AvlNode node)
    {
        node.right = rotateleft(node.right);
        return rotateRight(node);
    }

    public AvlNode rotateleft(AvlNode node1)
    {
        AvlNode node2 = node1.right;
        node1.right = node2.left;
        node2.left = node1;
        node1.height = getMaxheight(GetHeight(node1.left), GetHeight(node1.right)) + 1;
        node2.height = getMaxheight(GetHeight(node2.right), node1.height) + 1;
        return node2;
    }

    public AvlNode doubleLeft(AvlNode node)
    {
        node.left = rotateRight(node.left);
        return rotateleft(node);
    }

    public int GetHeight(AvlNode node)
    {
        return node == null ? -1 : node.height;
    }

    public int getMaxheight(int leftNode, int rightNode)
    {
        return leftNode > rightNode ? leftNode : rightNode;
    }


    //Graficacion
    public void treeGraph()
    {
        nodes = "";
        connections = "";
        treeGraphRecursive(root);
        Console.WriteLine(nodes);
        Console.WriteLine(connections);

    }

    public void treeGraphRecursive(AvlNode current)
    {
        if(current.left != null)
        {
            treeGraphRecursive(current.left);
            connections += "S"+ Convert.ToString(current.item.ID) + "->" + "S" + Convert.ToString(current.left.item.ID) + "\n";

        }

        


        nodes += "S"+Convert.ToString(current.item.ID) + "[label=" + '"' +Convert.ToString(current.item.ID)+"|"+ current.item.Repuesto + '"' + "shape ="+ '"'+"record" +'"'+"];";
       // nodes += 'S'+Convert.ToString(current.item.ID)+  '[label='+Convert.ToString(current.item.ID)+"];";
        if(current.right != null)
        {
            treeGraphRecursive(current.right);
            connections += "S"+ Convert.ToString(current.item.ID) + "->" + "S" + Convert.ToString(current.right.item.ID) + "\n";
        }

    }

}

class Program
{
    static void Main()
    {
        Repuestos repuesto1 = new Repuestos(1, "motor", "nuevo", 50.2);
        Repuestos repuesto2 = new Repuestos(2, "motor", "nuevo", 50.2);
        Repuestos repuesto3 = new Repuestos(3, "motor", "nuevo", 50.2);
        Repuestos repuesto4 = new Repuestos(4, "motor", "nuevo", 50.2);
        Repuestos repuesto5 = new Repuestos(5, "motor", "nuevo", 50.2);
        Repuestos repuesto6 = new Repuestos(6, "motor", "nuevo", 50.2);

        AvlTree arbol = new AvlTree();

        arbol.insert(repuesto1);
        arbol.insert(repuesto2);
        arbol.insert(repuesto3);
        arbol.insert(repuesto4);
        arbol.insert(repuesto5);
        arbol.insert(repuesto6);

        arbol.treeGraph();
    }
}

