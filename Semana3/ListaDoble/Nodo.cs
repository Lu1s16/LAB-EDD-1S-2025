using System;

namespace ListaDoble
{

    public unsafe struct Nodo<T> where T : unmanaged
    {
        public T id;
        public string nombre;
        public string nombrePropietario;
        public string direccion;
        public Nodo<T>* Next;
        public Nodo<T>* Prev;

       
    }
}