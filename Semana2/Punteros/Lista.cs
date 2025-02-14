using System;
using System.Runtime.InteropServices;

namespace List
{
    //2. Crear la clase para la lista
    public unsafe class ListaSimple<T> where T : unmanaged
    {
        private Nodo<T>* cabeza;

        public ListaSimple()
        {
            cabeza = null;
        }

        public void Insertar(T data, string name)
        {
            //Creamos el nodo
            Nodo<T>* nuevo = (Nodo<T>*)Marshal.AllocHGlobal(sizeof(Nodo<T>));
            nuevo->Data = data;
            nuevo->Nombre = name;
            nuevo->Sig = null;

            //Caso 1, que este vacia la lista
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            //Caso 2, que la lista este llena
            else
            {
                Nodo<T>* temp = cabeza;
                while (temp->Sig != null)
                {
                    temp = temp->Sig;
                }
                temp->Sig = nuevo;
            }
        }

        public void Eliminar(T data)
        {
            //Verificar que este vacia la lista
            if (cabeza == null) return;


            //Caso 1, que el nod a eliminar sea el primero
            if (cabeza->Data.Equals(data))
            {
                //guardamos el nodo a eliminar como temp
                Nodo<T>* temp = cabeza;
                cabeza = cabeza->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
                return;
            }

            Nodo<T>* actual = cabeza;
            //Verificamos que el siguiente del actual no sea null y que tambien sea el dato que queremos eliminar
            while (actual->Sig != null && !actual->Sig->Data.Equals(data))
            {
                actual = actual->Sig;
            }

            if (actual->Sig != null)
            {
                //guartamos el nodo a eliminar como temp
                Nodo<T>* temp = actual->Sig;
                actual->Sig = actual->Sig->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }

        public void Imprimir()
        {
            Nodo<T>* temp = cabeza;
            while (temp != null)
            {
                Console.WriteLine(temp->Data);
                Console.WriteLine(temp->Nombre);
                
                temp = temp->Sig;
                
            }
        }

        ~ListaSimple()
        {
            while (cabeza != null)
            {
                Nodo<T>* temp = cabeza;
                cabeza = cabeza->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }
    }
}