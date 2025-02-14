using System.Runtime.ConstrainedExecution;

namespace Estructuras
{
    //Crear clase
    class Cola 
    {
        private Nodo primero;
        private Nodo ultimo;

        public Cola()
        {
            primero = null;
            ultimo = null;
        }

        //Agregar elementos a la cola
        public void encolar(int valor)
        {
            Nodo nuevoNodo = new Nodo(valor);
            if (ultimo == null)
            {
                primero = nuevoNodo;
                ultimo = nuevoNodo;
            } else 
            {
                ultimo.Siguiente = nuevoNodo;
                ultimo = nuevoNodo;
            }
        }

        public int desencolar()
        {
            if (primero == null) return -999;
            int ret = primero.Data;
            primero = primero.Siguiente;
            if(primero == null) ultimo = null;
            return ret;

        }

        public void Print()
        {
            Nodo temp = primero;
            while(temp != null)
            {
                Console.Write(temp.Data + " <- ");
                temp = temp.Siguiente;
            }
        }

    }
}