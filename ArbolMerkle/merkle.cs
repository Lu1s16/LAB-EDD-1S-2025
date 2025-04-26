// dotnet add package Newtonsoft.Json --version 13.0.3
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json; 


namespace merkle
{

    public class Factura
    {
        public int Id;
        public int Id_servicio;
        public double Total;

        //Colocar los demas atributos

        public Factura(int id, int id_servicio, double total) 
        {
            Id = id;
            Id_servicio = id_servicio;
            Total = total;
        }

        public string GetHash()
        {
            string data = JsonConvert.SerializeObject(this); // Serializar la factura a JSON
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); // Convertir a hexadecimal
                }
                return builder.ToString();
            }
        }



    }



    public class MerkleNode 
    {
        public string Hash;
        public MerkleNode Left;
        public MerkleNode Right;

        public Factura Factura;

        public MerkleNode(Factura factura)
        {
            Factura = factura;
            Hash = factura.GetHash();
            Left = null;
            Right = null;
        }

        public MerkleNode(MerkleNode left, MerkleNode right)
        {
            Factura = null;
            Left = left;
            Right = right;
            Hash = CalculateHash(left.Hash, right?.Hash);

        }

        private string CalculateHash(string leftHash, string rightHash)
        {

            string combined = leftHash + (rightHash ?? leftHash);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }

        }


    }

    public class MerkleTree
    {

        private List<MerkleNode> Leaves;
        private MerkleNode Root;

        public MerkleTree()
        {
            Leaves = new List<MerkleNode>();
            Root = null;
        }


        public void Insert(int id, int id_servicio, double total)
        {
            foreach(var leaf in Leaves)
            {
                if(leaf.Factura.Id == id)
                {
                    Console.WriteLine("Error: Ya existe una factura con el ID:", Convert.ToString(id));
                }
            }

            Factura factura = new Factura(id, id_servicio, total);

            MerkleNode newLeaf = new MerkleNode(factura);
            Leaves.Add(newLeaf);


            BuildTree();
        }


        public void BuildTree()
        {
            if(Leaves.Count == 0)
            {
                Root = null;
                return;
            }


            List<MerkleNode> currentLevel = new List<MerkleNode>(Leaves);

            while(currentLevel.Count > 1)
            {
                List<MerkleNode> nextLevel = new List<MerkleNode>();

                for(int i = 0; i < currentLevel.Count; i +=2)
                {

                    MerkleNode left = currentLevel[i];
                    MerkleNode right = (i + 1 < currentLevel.Count) ? currentLevel[i + 1] : null;
                    MerkleNode parent = new MerkleNode(left, right);

                    nextLevel.Add(parent);

                }

                currentLevel = nextLevel;
            }

            Root = currentLevel[0];

        }
    
        public string GenerateDot()
        {
            StringBuilder dot = new StringBuilder();
            dot.AppendLine("digraph MerkleTree {"); 
            dot.AppendLine("  node [shape=record];"); 
            dot.AppendLine("  graph [rankdir=TB];"); 
            dot.AppendLine("  subgraph cluster_0 {"); 
            dot.AppendLine("    label=\"Facturas\";");

            if(Root == null)
            {
                dot.AppendLine("    empty [label=\"Árbol vacío\"];");
            }
            else {
                Dictionary<string, int> nodeIds = new Dictionary<string, int>();
                int idCounter = 0;
                GenerarDotRecursive(Root, dot, nodeIds, ref idCounter);

            }

            dot.AppendLine("  }");
            dot.AppendLine("}"); 
            return dot.ToString();

        }

        public void GenerarDotRecursive(MerkleNode node, StringBuilder dot, Dictionary<string, int> nodeIds, ref int idCounter)
        {

            if(node == null) return;

            if(!nodeIds.ContainsKey(node.Hash))
            {
                nodeIds[node.Hash] = idCounter++;
            }

            int nodeId = nodeIds[node.Hash];

            string label;
            if(node.Factura != null)
            {
                label = $"\"Factura {node.Factura.Id}\\nTotal: {node.Factura.Total}\\nHash: {node.Hash.Substring(0, 8)}...\"";
            }
            else {
                label = $"\"Hash: {node.Hash.Substring(0, 8)}...\"";
            }
            dot.AppendLine($"  node{nodeId} [label={label}];");

            if(node.Left != null)
            {
                if(!nodeIds.ContainsKey(node.Left.Hash))
                {
                    nodeIds[node.Left.Hash] = idCounter++;
                }
                int leftId = nodeIds[node.Left.Hash];
                dot.AppendLine($"  node{nodeId} -> node{leftId};");
                GenerarDotRecursive(node.Left, dot, nodeIds, ref idCounter);

            }

            if(node.Right != null)
            {
                if(!nodeIds.ContainsKey(node.Right.Hash))
                {
                    nodeIds[node.Right.Hash] = idCounter++;
                }
                int rightId = nodeIds[node.Right.Hash];
                dot.AppendLine($"  node{nodeId} -> node{rightId};");
                GenerarDotRecursive(node.Right, dot, nodeIds, ref idCounter);

            }

        }
    
    
    
    }
    
}
