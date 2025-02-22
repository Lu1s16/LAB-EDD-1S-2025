using System;
using System.Runtime.InteropServices;

namespace ListaCircular

 
{
    public unsafe class ListaCircular<T> where T : unmanaged
    {
        private Nodo<int>* head;

        public ListaCircular()
        {
            head = null;
        }

        public void Insertar(int data, string name, string nombrePropietario, string direccion)
        {

           

            //Creacion del nodo
            Nodo<int>* nuevo = (Nodo<int>*)Marshal.AllocHGlobal(sizeof(Nodo<T>));


            //Asignar valores, metodo 1
            nuevo->id = data;
            nuevo->nombre = name; 
            nuevo->nombrePropietario = nombrePropietario;
            nuevo->direccion = direccion;

            if(head == null){
                head = nuevo;
                head->Sig = head;
            }
            else 
            {
                Nodo<int>* temp = head;
                while(temp->Sig != head)
                {
                    temp = temp->Sig;

                }

                temp->Sig = nuevo;
                nuevo->Sig = head;
                

            }




        }

         public void Eliminar(int id)
            {
                //Verificar que la lista esta vacia
                if(head == null) return;

                //Eliminar head cuando es el unico nodo en la lista
                if(head->id == id && head->Sig == head)
                {

                    Marshal.FreeHGlobal((IntPtr)head);
                    head = null;
                    return;

                }

                Nodo<int>* temp = head;
                Nodo<int>* prev = null;
                do
                {
                    //Ver si el temp tiene el id a eliminar
                    if(temp->id == id)
                    {
                        //Significa que el nodo a eliminar no es head
                        if(prev != null)
                        {
                            prev->Sig = temp->Sig;
                        }
                        else 
                        {
                            Nodo<int>* last = head;
                            //Mueve el last hasta que el sig. sea head
                            while(last->Sig != head)
                            {
                                last = last->Sig;
                            }
                            head = head->Sig;
                            last->Sig = head;
                        }
                        Marshal.FreeHGlobal((IntPtr)temp);
                        return;
                    }
                    prev = temp;
                    temp = temp->Sig;
                } while(temp != head);

            }

            public void Mostrar()
        {
            if (head == null)
            {
                Console.WriteLine("Lista vacía.");
                return;
            }

            Nodo<int>* temp = head;
            do
            {
                Console.WriteLine(temp->nombre);
                temp = temp->Sig;
            } while (temp != head);
        }

        ~ListaCircular()
        {
            if (head == null) return;

            Nodo<int>* temp = head;
            do
            {
                Nodo<int>* next = temp->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
                temp = next;
            } while (temp != head);
        }
    }
}