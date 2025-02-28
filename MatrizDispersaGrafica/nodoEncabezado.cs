using System;

namespace Matriz
{

    public unsafe struct NodoEncabezado<T> where T : unmanaged
    {
        public T id;
        
        public NodoEncabezado<T>* siguiente;
        public NodoEncabezado<T>* anterior;
        public NodoInterno<T>* acceso;

       
    }
}