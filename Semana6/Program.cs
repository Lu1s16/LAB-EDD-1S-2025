using System;


class Tnode {
    public int ID;
    public Tnode? right;
    public Tnode? left;


    public Tnode(int id)
    {
        ID = id;
        right = null;
        left = null;

    } 
}

class Tree{
    private Tnode root;
    string connections = "";
    string nodes = "";


    public void Agregar(int Id)
    {
        Tnode nodo = new Tnode(Id);

        if(root == null)
        {
            root = nodo;
        } else {
            recursividad(root, nodo);

        }


    }

    public void recursividad(Tnode curr, Tnode newNode)
    {

        if(newNode.ID < curr.ID)
        {
            if(curr.left != null)
            {
                recursividad(curr.left, newNode);

            } else {
                curr.left = newNode;
            }

        }
        else if(newNode.ID > curr.ID)
        {
            if(curr.right != null)
            {
                recursividad(curr.right, newNode);
                
            } else {
                curr.right = newNode;
            }

        }
        else
        {
            Console.WriteLine("El nodo ya fue insertado");
        }

    }

    public void treeGraph()
    {
        connections = "";
        nodes = "";
        graphRecursivo(root);
        Console.WriteLine(nodes);
        Console.WriteLine(connections);

        
    }

    public void graphRecursivo(Tnode current)
    {

        if(current.left != null)
        {
            graphRecursivo(current.left);
            connections += "S"+Convert.ToString(current.ID) + " -> " + "S" + Convert.ToString(current.left.ID) + "\n";
        }

        nodes += "S"+Convert.ToString(current.ID)+"[label="+Convert.ToString(current.ID)+"];";

        if(current.right != null)
        {
            graphRecursivo(current.right);
            connections += "S"+Convert.ToString(current.ID) + " -> " + "S" + Convert.ToString(current.right.ID) + "\n";
        }

    }


    public void inOrden()
    {
        connections = "";
        nodes = "";
        inOrdenRecursivo(root);
        Console.WriteLine("----");
        Console.WriteLine(nodes);
        Console.WriteLine(connections);

    }

    public void inOrdenRecursivo(Tnode current)
    {
        if(current.left != null)
        {
            inOrdenRecursivo(current.left);
            connections += " -> ";
        }
        //Obtenemos el valor de cada nodo
        nodes += "S" + Convert.ToString(current.ID) + "[label="+ Convert.ToString(current.ID) + "];";
        connections += "S"+Convert.ToString(current.ID);
        //Console.WriteLine(Convert.ToString(current.ID));

        if(current.right != null)
        {
            connections += " -> ";
            inOrdenRecursivo(current.right);
        }



    }


    public void preOrden()
    {
        connections = "";
        nodes = "";
        preOrdenRecursivo(root);
        Console.WriteLine("------");
        Console.WriteLine(nodes);
        Console.WriteLine(connections);
    }

    public void preOrdenRecursivo(Tnode current)
    {
        //Obtenemos el valor de cada nodo
        nodes += "S" + Convert.ToString(current.ID) + "[label="+ Convert.ToString(current.ID) + "];";
        connections += "S"+Convert.ToString(current.ID);
        Console.WriteLine(current.ID);

        if(current.left != null)
        {
            connections += " -> ";
            preOrdenRecursivo(current.left);
        }
        if(current.right != null)
        {
            connections += " -> ";
            preOrdenRecursivo(current.right);
        }


    }

    public void postOrden()
    {
        connections = "";
        nodes = "";
        postOrdenRecursivo(root);
        Console.WriteLine("------");
        Console.WriteLine(nodes);
        Console.WriteLine(connections);

    }

    public void postOrdenRecursivo(Tnode current)
    {
        if(current.left != null)
        {
            postOrdenRecursivo(current.left);
            connections += " -> ";

        }

        if(current.right != null)
        {
            postOrdenRecursivo(current.right);
            connections += " -> ";

        }
        //Obtenemos el valor del nodo
        nodes += "S" + Convert.ToString(current.ID) + "[label="+ Convert.ToString(current.ID) + "];";
        connections += "S"+Convert.ToString(current.ID);
        
    }


}


class Prgram
{
    static void Main()
    {

        Tree arbol = new Tree();
        //arbol.Agregar(5);
        //arbol.Agregar(2);
        //arbol.Agregar(6);
        //arbol.Agregar(4);
        //arbol.Agregar(1);
        //arbol.Agregar(10);
        //arbol.Agregar(9);
        //arbol.Agregar(12);

        arbol.Agregar(40);
        arbol.Agregar(30);
        arbol.Agregar(25);
        arbol.Agregar(35);
        arbol.Agregar(20);
        arbol.Agregar(23);
        arbol.Agregar(30);

       

        arbol.treeGraph();
        //arbol.inOrden();
        //arbol.preOrden();
        //arbol.postOrden();
    }
}