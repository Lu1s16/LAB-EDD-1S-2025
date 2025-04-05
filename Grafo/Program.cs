using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;


public class Nodo_Repuesto
{
    public int ID;
    //Aqui pueden agregar los demas atributos

    public Nodo_Repuesto(int id)
    {
        ID = id;
    }
}


public class Nodo_Vehiculo
{
    public int ID;
    //Los demas atributos
    public Nodo_Vehiculo siguiente;
    public List<Nodo_Repuesto> hijos;

    public Nodo_Vehiculo(int id)
    {
        ID = id;
        siguiente = null;
        hijos = new List<Nodo_Repuesto> {};


    }

    public void insertar_repuesto(Nodo_Repuesto nodo)
    {
        hijos.Add(nodo);
    }

    public bool buscar_repuesto(int id_repuesto)
    {
        foreach(Nodo_Repuesto hijo in hijos)
        {
            if(hijo.ID == id_repuesto){
                return true;
            }

        }

        return false;
    }

}


public class Grafo
{
    public Nodo_Vehiculo raiz;
    public Grafo()
    {
        raiz = null;
    }

    public void insertar(int id_vehiculo, int id_repuesto)
    {
        Nodo_Vehiculo newNodoVehiculo = new Nodo_Vehiculo(id_vehiculo);
        Nodo_Repuesto newNodoRepuesto = new Nodo_Repuesto(id_repuesto);

        if(raiz == null)
        {
            raiz = newNodoVehiculo;

            //Insertamos su repuesto
            newNodoVehiculo.insertar_repuesto(newNodoRepuesto);

        }

        Nodo_Vehiculo temp = raiz;
        while(temp != null)
        {
            if(id_vehiculo == temp.ID)
            {
                break;
            }

            temp = temp.siguiente;
        }

        //Primer caso: que exista un vehiculo y debamos ingresar un nuevo repuesto
        if(temp != null)
        {
            bool existe = temp.buscar_repuesto(id_repuesto);

            if(existe)
            {
                Console.WriteLine("El vehiculo: " + Convert.ToString(temp.ID) + "Ya esta relacionado con el repuesto: " + Convert.ToString(id_repuesto));
            
            }
            else {
                temp.insertar_repuesto(newNodoRepuesto);
            }
        } 
        else 
        {
            //Segundo caso: Que no exista el vehiculo
            Nodo_Vehiculo temp2 = raiz;
            while(temp2.siguiente != null){
                temp2 = temp2.siguiente;
            }

            temp2.siguiente = newNodoVehiculo;

            newNodoVehiculo.insertar_repuesto(newNodoRepuesto);

        }
    }

    public void imprimir()
    {
        Nodo_Vehiculo temp = raiz;
        while(temp != null)
        {
            Console.WriteLine("Vehiculo");
            Console.WriteLine(Convert.ToString(temp.ID));

            //Realizar un recorrido para cada repuesto del vehiculo
            Console.WriteLine("Repuestos");
            foreach(Nodo_Repuesto hijo in temp.hijos)
            {
                Console.WriteLine("R"+Convert.ToString(hijo.ID));
            }

            Console.WriteLine("---------------------");

            temp = temp.siguiente;
        }
    }

    public void graficar()
    {

        Nodo_Vehiculo temp = raiz;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("digraph Grafo {");
        while(temp != null){
           
           
           
           
           string id_v = "V" + Convert.ToString(temp.ID);
           sb.AppendLine(id_v + "[label="+ id_v + "]");

           foreach(Nodo_Repuesto hijo in temp.hijos)
           {
            string id_r = "R" + Convert.ToString(hijo.ID);
            sb.AppendLine(id_r + "[label=" + id_r + "];");
            sb.AppendLine(id_v + "->" + id_r);
           }


           temp = temp.siguiente;

        }

        sb.AppendLine("}");
        Console.WriteLine(sb.ToString());



    }

}

class Program
{

    static void Main(string[] args)
    {
        Grafo grafo = new Grafo();
        grafo.insertar(10,20);
        grafo.insertar(39,48);
        grafo.insertar(5,20);
        grafo.insertar(1,5);
        grafo.insertar(2,48);
        grafo.insertar(2, 40);
        grafo.insertar(5, 48);
        grafo.imprimir();

        grafo.graficar();
    }
}


