using System;
using List;

class Program
    {
        static void Main()
        {
            ListaSimple<int> lista = new ListaSimple<int>();
            lista.Insertar(1, "Luis");
            lista.Insertar(2, "Jose");
            lista.Insertar(3, "Ericka");

            Console.WriteLine("Lista antes de eliminar:");
            lista.Imprimir();

            lista.Eliminar(3);
            Console.WriteLine("\nLista después de eliminar:");
            lista.Imprimir();

            //lista.Graficar();
            lista.GenerarReporte();
        }
    }