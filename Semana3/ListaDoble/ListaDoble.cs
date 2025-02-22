using System;
using System.Runtime.InteropServices;

namespace ListaDoble
{
    public unsafe class ListaDoblementeEnlazada
    {
        private Nodo<int>* head;
        private Nodo<int>* tail;

        public ListaDoblementeEnlazada()
        {
            head = null;
            tail = null;
        }

        public void Insertar(int id, string nombre, string nombrePropietario, string direccion)
        {
           
            Nodo<int>* nuevoNodo = (Nodo<int>*)Marshal.AllocHGlobal(sizeof(Nodo<int>));
            nuevoNodo->id = id;
            nuevoNodo->nombre = nombre;
            nuevoNodo->nombrePropietario = nombrePropietario;
            nuevoNodo->direccion = direccion;
            nuevoNodo->Next = null;
            nuevoNodo->Prev = null;

            if (head == null)
            {
                head = tail = nuevoNodo;
            }
            else
            {
                tail->Next = nuevoNodo;
                nuevoNodo->Prev = tail;
                tail = nuevoNodo;
            }
        }

        public void Eliminar(int id)
        {
            Nodo<int>* actual = head;
            while (actual != null)
            {
                if (actual->id == id)
                {
                    //Verificar si es el primero en el nodo
                    if (actual->Prev != null)
                        actual->Prev->Next = actual->Next;
                        
                    else
                    //Es el primero de la cabeza
                        head = actual->Next;

                    //Verificar si es el ultimo
                    if (actual->Next != null)
                        actual->Next->Prev = actual->Prev;
                    else
                        tail = actual->Prev;

                    Marshal.FreeHGlobal((IntPtr)actual);
                    return;
                }
                actual = actual->Next;
            }
        }

        public void Mostrar()
        {
            Nodo<int>* actual = head;
            while (actual != null)
            {
                Console.WriteLine(actual->nombre);
                actual = actual->Next;
            }
        }

        public void MostrarReversa()
        {
            Nodo<int>* actual = tail;
            while (actual != null)
            {
                Console.WriteLine(actual->nombre);
                actual = actual->Prev;
            }
        }

        ~ListaDoblementeEnlazada()
        {
            Nodo<int>* actual = head;
            while (actual != null)
            {
                Nodo<int>* temp = actual;
                actual = actual->Next;
                Marshal.FreeHGlobal((IntPtr)actual);
            }
        }
    }
}