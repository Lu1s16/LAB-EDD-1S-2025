using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System;

//Usar el mismo namespace para todos los archivos
namespace Estructuras
{
    //Crear clase
    public unsafe class Cola<T> where T : unmanaged 
    {
        private Nodo<T>* primero;
        private Nodo<T>* ultimo;
        //private Nodo primero;
        //private Nodo ultimo;

        public Cola()
        {
            primero = null;
            ultimo = null;
        }

        //Agregar elementos a la cola
        public void encolar(T valor)
        {
            Nodo<T>* nuevo = (Nodo<T>*)Marshal.AllocHGlobal(sizeof(Nodo<T>));
            nuevo->Data = valor;
            nuevo->Sig = null;
         
            if (ultimo == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            } else 
            {
                ultimo->Sig = nuevo;
                ultimo = nuevo;
            }
        }

        public Nodo<T>* desencolar()
        {
            if (primero == null) return null;
            //int ret = primero->Data;
            Nodo<T>* ret = primero;
            primero = primero->Sig;
            if(primero == null) ultimo = null;
            Marshal.FreeHGlobal((IntPtr)ret);
            return ret;

        }

        public void Print()
        {
            Nodo<T>* temp = primero;
            while(temp != null)
            {
                Console.Write(temp->Data + " <- ");
                temp = temp->Sig;
            }
        }

    }
}