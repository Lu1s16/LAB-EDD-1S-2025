using System;

namespace Matriz
{

    public unsafe struct NodoInterno<T> where T : unmanaged
    {
        public T id;
        public string nombre;
        public int coordenadaX; //Fila
        public int coordenadaY; //Columna
        public NodoInterno<T>* arriba;
        
        public NodoInterno<T>* abajo;
        public NodoInterno<T>* derecha; //siguiente
        public NodoInterno<T>* izquierda; //anterior

       
    }
}