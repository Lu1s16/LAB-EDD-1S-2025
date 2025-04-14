using System;
using System.Security.Cryptography;
using System.Text;


public class Encrypt{
  public static string GetSHA256(string str)
  {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
  }
 
}

class Block
{
    public int Index;
    public string Nombre;
    public string Contraseña;
    //El resto de informacion

    public string PreviusHash;
    public string Hash;

    public Block next;
    public Block prev;

    public Block(int index, string nombre, string contraseña)
    {
        Index = index;
        Nombre = nombre;
        Contraseña = contraseña;
        PreviusHash = "";
        Hash = "";

    }
}


class Blockchain()
{
    public Block head; //primero
    public Block tail; //ultimo
    public int Size = 0;

    public Block insertar(string nombre, string contraseña){

        //Encriptar la contraseña
        string contraseñaEncriptada = Encrypt.GetSHA256(contraseña);


        Block newBlock = new Block(Size, nombre, contraseñaEncriptada);

        if(head == null)
        {
            newBlock.PreviusHash = "0000";
        }
        else 
        {
            newBlock.PreviusHash = tail.Hash;
        }

        string valor = Convert.ToString(newBlock.Index) + nombre + contraseña; //resto de informacion
        newBlock.Hash = Encrypt.GetSHA256(valor);

        if(head == null)
        {
            tail = newBlock;
            head = newBlock;
        }
        else 
        {
            newBlock.prev = tail;
            tail.next = newBlock;
            tail = newBlock;
        }

        Size+=1;

        return newBlock;


    }

    public void imprimir()
    {
        Block temp = head;

        while(temp != null)
        {
            Console.WriteLine("Index: " + temp.Index);
            Console.WriteLine("Nombre: " + temp.Nombre);
            Console.WriteLine("Contraseña: " + temp.Contraseña);
            Console.WriteLine("PreviusHash: " + temp.PreviusHash);
            Console.WriteLine("Hash: " + temp.Hash);
            Console.WriteLine("---------------------");
            


            temp = temp.next;
        }
    }

    public Block Buscar(string nombre)
    {
        Block temp = head;
        while(temp != null)
        {

            if(temp.Nombre == nombre)
            {
                return temp;
            }


            temp = temp.next;
        }

        return null;

        



    }

}


class Program
{
    static void Main()
    {
        Blockchain blockchain = new Blockchain();

        blockchain.insertar("Luis", "1234");
        blockchain.insertar("Ericka", "micontraseña");
        blockchain.insertar("Jorge", "clavesecreta");

        blockchain.imprimir();

        bool inicioSesion = false;

        do{

            string nombre = Console.ReadLine();
            string contraseña = Console.ReadLine();

            Block encontrado = blockchain.Buscar(nombre);

            if(encontrado != null)
            {
                //Encriptar la contraseña
                string contraseñaEncriptada = Encrypt.GetSHA256(contraseña);

                if(contraseñaEncriptada == encontrado.Contraseña)
                {
                    inicioSesion = true;
                }

            }

        }while(!inicioSesion);

    }
}