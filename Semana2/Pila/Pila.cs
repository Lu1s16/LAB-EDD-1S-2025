namespace Estructuras
{
    class Pila 
    {
        private Nodo top; 

        public Pila() 
        {
            top = null; 
        }

        //Metodo para agregar nuevo elemento al tope
        public void Push(int valor)
        {
            Nodo nuevoNodo = new Nodo(valor);
            nuevoNodo.Siguiente = top; 
            top = nuevoNodo;
        }

        public int Pop()
        {
            if (top == null) return -999;
            int ret = top.Data;
            top = top.Siguiente;
            return ret;
        }

        public void Print()
        {
            Nodo temp = top; // Comienza en el tope de la pila.
            while (temp != null) // Mientras haya nodos en la pila.
            {
                Console.Write(temp.Data + " -> "); // Imprime el valor del nodo.
                temp = temp.Siguiente; // Se mueve al siguiente nodo.
            }
            Console.WriteLine("NULL"); // Indica el final de la pila.
        }


    }
}