//crear el namespace
namespace Estructuras
{
    //Crar la clase nodo
    class Nodo 
    {
        //Datos del nodo
        public int Data { get; set;}
        public Nodo Siguiente {get; set;}

        //Constructor
        public Nodo(int data)
        {
            Data = data;
            Siguiente = null;
        }
        
    }
}