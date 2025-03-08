using System;


//Crear nodo
class Nodo
{
    public int Id;
    public string Nombres;
    public string Apellidos;
    public string Correo;
    public int Edad;

    public string Contrasenia;
    public Nodo? Siguiente; 

    //Constructor
    public Nodo(int id, string nombre, string apellido, string correo, int edad, string contrasenia)
    {
        Id = id;
        Nombres = nombre;
        Apellidos = apellido;
        Correo = correo;
        Edad = edad;
        Contrasenia = contrasenia;
        Siguiente = null;

    }

}

class ListaEnlazada
{
    private Nodo cabeza;

    public void Agregar(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
    {
        //Creamos el nodo
        Nodo nuevo = new Nodo(id, nombres, apellidos, correo, edad, contrasenia);
        //Verificamos si es null
        if (cabeza == null)
        {
            cabeza = nuevo;
        }
        else
        {
            //Creamos un temp
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }
    }

    public void BubbleSort()
    {
        if (cabeza == null || cabeza.Siguiente == null)
            return;

        bool intercambio;

        do
        {
            intercambio = false;
            Nodo actual = cabeza;
            Nodo anterior = null;

            //Recorremos la lista, validar que no sea null y que si exista otro nodo despues.
            while(actual != null && actual.Siguiente != null)
            {
                //Comparamos los datos
                if(actual.Edad > actual.Siguiente.Edad)
                {
                    //Guardamos el siguiente nodo temporalmente
                    Nodo temp = actual.Siguiente;

                    //Intercambiamos los nodos
                    actual.Siguiente = temp.Siguiente;
                    temp.Siguiente = actual;

                    //Verificar si actual esta al inicio para actualizar la cabeza
                    if(anterior == null)
                    {
                        cabeza = temp;
                    } 
                    else 
                    {
                        anterior.Siguiente = temp; //Hace que el nodo conecte con el temp
                    }

                    //Marcamos que hubo un intercambio
                    intercambio = true;
                }

                //Pasar al siguiente nodo
                anterior = actual;
                actual = actual.Siguiente;

            }

        }while(intercambio);

    }



    public void Imprimir()
    {
        Nodo actual = cabeza;
        while (actual != null)
        {
            Console.WriteLine($"ID: {actual.Id}, Nombre: {actual.Nombres} {actual.Apellidos}, Edad: {actual.Edad}");
            actual = actual.Siguiente;
        }
    }




}

class Program
{
    static void Main()
    {
        ListaEnlazada lista = new ListaEnlazada();

        lista.Agregar(1, "Carlos", "Gómez", "carlos@mail.com", 25, "pass123");
        lista.Agregar(2, "Ana", "Pérez", "ana@mail.com", 20, "pass456");
        lista.Agregar(3, "Luis", "Martínez", "luis@mail.com", 99, "pass789");
        lista.Agregar(4, "María", "García", "maria@gmail.com", 22, "passabc");
        lista.Agregar(5, "Pedrito", "Pernandez", "pedrito@gmail.com", 10, "passdef");

        Console.WriteLine("Lista antes de ordenar:");
        lista.Imprimir();

        lista.BubbleSort();

        Console.WriteLine("\nLista después de ordenar:");
        lista.Imprimir();
    }
}