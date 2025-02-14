using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System;

namespace Estructuras
{
    public unsafe class Pila<T> where T : unmanaged 
    {
        //private Nodo top; 
        private Nodo<T>* top;

        public Pila() 
        {
            top = null; 
        }

        //Metodo para agregar nuevo elemento al tope
        public void Push(T valor)
        {
            //Nodo nuevoNodo = new Nodo(valor);
            Nodo<T>* nuevoNodo = (Nodo<T>*)Marshal.AllocHGlobal(sizeof(Nodo<T>));
            nuevoNodo->Data = valor;
            nuevoNodo->Sig = top; 
            top = nuevoNodo;
        }

        public Nodo<T>* Pop()
        {
            if (top == null) return null;
            Nodo<T>* ret = top;
            top = top->Sig;
            Marshal.FreeHGlobal((IntPtr)ret);
            return ret;
        }

        public void Print()
        {
            Nodo<T>* temp = top; // Comienza en el tope de la pila.
            while (temp != null) // Mientras haya nodos en la pila.
            {
                Console.Write(temp->Data + " -> "); // Imprime el valor del nodo.
                temp = temp->Sig; // Se mueve al siguiente nodo.
            }
            Console.WriteLine("NULL"); // Indica el final de la pila.
        }


    }
}