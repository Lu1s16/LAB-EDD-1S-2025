using System;
using System.Runtime.InteropServices;

//1. Crear el nodo
namespace List
{
    public unsafe struct Nodo<T> where T : unmanaged
    {
        //Agregar los atributos, datos y el puntero siguiente
        public T Data;
        public string Nombre;
        public Nodo<T>* Sig;
        
    }
}