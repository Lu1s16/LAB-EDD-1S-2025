using System;
using System.Collections.Generic;
using System.Text;

class HuffmanNode : IComparable<HuffmanNode>
{

    public char Character;
    public int Frequency;
    public HuffmanNode Left;
    public HuffmanNode Right;

    public int CompareTo(HuffmanNode other)
    {
        return this.Frequency.CompareTo(other.Frequency);
    }


}


class HuffmanCompression
{

    public static (string comprssed, HuffmanNode root) CompressWithTree(string input)
    {
        Dictionary<char, int> frecuencies = new Dictionary<char, int>();

        foreach(char c in input)
        {
            if(frecuencies.ContainsKey(c))
            {
                frecuencies[c]++;
            } else {
                frecuencies[c] = 1;
            }
        }

        PriorityQueue<HuffmanNode> priorityQueue = new PriorityQueue<HuffmanNode>();
        foreach(var kp in frecuencies)
        {
            priorityQueue.Enqueue(new HuffmanNode{Character = kp.Key, Frequency = kp.Value});
        }

        while(priorityQueue.Count > 1)
        {
            HuffmanNode left = priorityQueue.Dequeue();
            HuffmanNode right = priorityQueue.Dequeue();

            HuffmanNode parent = new HuffmanNode
            {
                Frequency = left.Frequency + right.Frequency,
                Left = left,
                Right = right

            };

            priorityQueue.Enqueue(parent);
        }
        HuffmanNode root = priorityQueue.Dequeue();
        Dictionary<char, string> codes = GenerateHuffmanCodes(root);

        StringBuilder compressed = new StringBuilder();
        
        foreach (char c in input)
        {
            compressed.Append(codes[c]);
        }

        return (compressed.ToString(), root);



    }

    public static string Decompress(string compressed, HuffmanNode root)
    {
        StringBuilder decompressed = new StringBuilder();
        HuffmanNode current = root;
        //Recorre los bits comprimidos por el arbol 
        //Al llegar al nodo hoja se añade el caracter al resultado y se reinicia desde la raiz
        foreach (char bit in compressed)
        {
            if (bit == '0')
                current = current.Left;
            else if (bit == '1')
                current = current.Right;

            if (current.Character != '\0')
            {
                decompressed.Append(current.Character);
                current = root;
            }
        }

        return decompressed.ToString();
    }


    private static Dictionary<char, string> GenerateHuffmanCodes(HuffmanNode root)
    {
        var codes = new Dictionary<char, string>(); //Crea diccionario para almacenar el codigo a cada caracter
        GenerateHuffmanCodes(root, "", codes);
        return codes;
    }

    private static void GenerateHuffmanCodes(HuffmanNode node, string code, Dictionary<char, string> codes)
    {
        //Realiza un recorrido del arbol
        
        if (node == null)
            return;


        if (node.Character != '\0')
            codes[node.Character] = code;

        //A los nodos izquierdos les asigna un 0
        GenerateHuffmanCodes(node.Left, code + "0", codes);
        //A los nodos derecha les asigna un 1
        GenerateHuffmanCodes(node.Right, code + "1", codes);
    }
    
}

class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> list = new List<T>();

    public int Count => list.Count;

    public void Enqueue(T item)
    {
        //Añade al final
        list.Add(item);
        int i = list.Count - 1;

        //Sube el elemento 
        while (i > 0)
        {
            //Compara el elemento con su padre
            //Si es menor intercambia las posiciones
            int parent = (i - 1) / 2;
            //si nodo riaz >= nodo no intercambia
            if (list[i].CompareTo(list[parent]) >= 0)
                break;
            //Intercambio
            Swap(i, parent);
            i = parent;
        }
    }

    public T Dequeue()
    {
        if (list.Count == 0)
            throw new InvalidOperationException("Queue is empty");
        //Obtiene el nodo minoma frecuencia
        T front = list[0];
        list[0] = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        int current = 0;

        //Reordena
        while (true)
        {
            int left = 2 * current + 1;
            int right = 2 * current + 2;
            int smallest = current;
            //Encuentra el menor entre el actual y sus hijos
            if (left < list.Count && list[left].CompareTo(list[smallest]) < 0)
                smallest = left;
            if (right < list.Count && list[right].CompareTo(list[smallest]) < 0)
                smallest = right;
            if (smallest == current)
                break;

            //Intercambia con el hijo menor
            Swap(current, smallest);
            current = smallest;
        }
        return front;
    }

    private void Swap(int i, int j)
    {
        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }



}


class Program
{
    static void Main(string[] args)
    {
        string input = "[ {\"ID\": 1, \"ID_Usuario\": 1,\"Marca\": \"Honda Civic\",\"Modelo\": 2005,\"Placa\": \"XZJ7H9KQ\"}]";

        

        
        var (compressed, root) = HuffmanCompression.CompressWithTree(input);
         Console.WriteLine("Texto compactado:\n" + compressed);
        string decompressed = HuffmanCompression.Decompress(compressed, root);
        Console.WriteLine("\nTexto descompactado:\n" + decompressed);
    }
}


