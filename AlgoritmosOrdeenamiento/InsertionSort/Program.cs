using System;

//Crear el nodo
class Nodo
{
    public int Id;
    public string Nombres;
    public string Apellidos;
    public string Correo;
    public int Edad;
    public string Contrasenia;
    public Nodo? Siguiente;

    //Crear el constructor
    public Nodo(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
    {
        Id = id;
        Nombres = nombres;
        Apellidos = apellidos;
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
        Nodo nuevo = new Nodo(id, nombres, apellidos, correo, edad, contrasenia);
        if (cabeza == null)
        {
            cabeza = nuevo;
        }
        else
        {
            Nodo actual = cabeza;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevo;
        }
    }

    public void InsertionSort()
    {
        // Si la lista está vacía o solo tiene un elemento, ya está ordenada
        if (cabeza == null || cabeza.Siguiente == null)
            return;


        //Cabeza para la lista ordenada
        Nodo? listaOrdenada = null;

        //Nodo actual que se ira insertando en la lista ordenada
        Nodo actual = cabeza;

        while(actual != null)
        {
            //Guardamos el siguiente nodo antes de modificar el actual
            Nodo siguiente = actual.Siguiente;

            //Insertamos el actual en la lista ordenada
            //Funcionamiento del string.Compare()
            //Compara la cadena actual.Nombres con listaOrdenada.Nombres
            //utiliza el valor  de los caracteres unicode.
            //Si el resultado es menor a cero significa que actual.Nombres es menor que listaOrdenada.Nombres
            if(listaOrdenada == null || string.Compare(actual.Nombres, listaOrdenada.Nombres, StringComparison.OrdinalIgnoreCase) < 0)
            {
                //Si la lista ordenada esta vacia o el actual va antes alfabeticamente que la cabeza de la cadena
                actual.Siguiente = listaOrdenada;
                listaOrdenada = actual;
            }
            else {

                //Buscar la posicion correcta en la lista ordenada
                Nodo temp = listaOrdenada;
                while(temp.Siguiente != null && string.Compare(temp.Siguiente.Nombres, actual.Nombres, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    temp = temp.Siguiente;
                }

                //Insertamos el nodo en la posicion encontrada
                actual.Siguiente = temp.Siguiente;
                temp.Siguiente = actual;
                
            }

            //Movemos el actual al siguiente nodo sin modificar
            actual = siguiente;

        }

        //Actualizamos la cabeza con la lista ordenada
        cabeza = listaOrdenada;



        
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

        lista.Agregar(1, "Luiz", "Gómez", "luis@mail.com", 25, "pass123");
        lista.Agregar(2, "Guatemala", "Pérez", "guatemala@mail.com", 20, "pass456");
        lista.Agregar(3, "Estiven", "Martínez", "Estiven@mail.com", 99, "pass789");
        lista.Agregar(4, "Berta", "García", "Berta@gmail.com", 22, "passabc");
        lista.Agregar(5, "Alberto", "Pernandez", "Alberto@gmail.com", 10, "passdef");

        Console.WriteLine("Lista antes de ordenar:");
        lista.Imprimir();

        lista.InsertionSort();

        Console.WriteLine("\nLista después de ordenar:");
        lista.Imprimir();
    }
}