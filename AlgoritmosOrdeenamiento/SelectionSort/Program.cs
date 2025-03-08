using System;
//Creacion de Nodo
class Nodo
{
    public int Id;
    public string Nombres;
    public string Apellidos;
    public string Correo;
    public int Edad;
    public string Contrasenia;
    public Nodo? Siguiente;

    //Crear constructor
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
    private Nodo? cabeza = null;  // Inicializado como null

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

    public void SelectionSort()
    {
        // Si la lista está vacía o solo tiene un elemento, no es necesario hacer nada
        if (cabeza == null || cabeza.Siguiente == null)
            return;

        // Empezamos desde el primer nodo de la lista
        Nodo? actual = cabeza;

        //Recorremos la lista
        //Buscando el nodo con el apellido mas pequeño para cada iteracion
        while (actual != null)
        {
            Nodo? minimo = actual;

            //Recorremos el resto de la lista a partir del actual.siguiente buscando el nodo 
            //Con el apellido mas pequeño
            Nodo? siguiente = actual.Siguiente;

            //Comparamos los apellidos de los nodos restantes con el nodo actual
            while (siguiente != null)
            {
                //Si encontramos un apellido mas pequeño
                //Actualizamos el minimmo
                if (string.Compare(siguiente.Apellidos, minimo.Apellidos, StringComparison.OrdinalIgnoreCase) < 0)
                {
                    minimo = siguiente;

                }

                siguiente = siguiente.Siguiente;

            }

            //Si encontramos un nodo con un apellido mas pequeño que el nodo actual  lo sintercambiamos
            if (minimo != actual)
            {
                //Intercambiamos los datos entre el nodo actual y el nodo minimo
                int tempId = actual.Id;
                string tempNombres = actual.Nombres;
                string tempApellidos = actual.Apellidos;
                string tempCorreo = actual.Correo;
                int tempEdad = actual.Edad;
                string tempContrasenia = actual.Contrasenia;

                //Asignamos los nodos minimo al nodo actual
                actual.Id = minimo.Id;
                actual.Nombres = minimo.Nombres;
                actual.Apellidos = minimo.Apellidos;
                actual.Correo = minimo.Correo;
                actual.Edad = minimo.Edad;
                actual.Contrasenia = minimo.Contrasenia;

                //Asignamos los datos del nodo actual al nodo minimo
                minimo.Id = tempId;
                minimo.Nombres = tempNombres;
                minimo.Apellidos = tempApellidos;
                minimo.Correo = tempCorreo;
                minimo.Edad = tempEdad;
                minimo.Contrasenia = tempContrasenia;



            }

            //Avanzamos al siguiente en la lista
            actual = actual.Siguiente;
        }
    }


    public void Imprimir()
    {
        Nodo? actual = cabeza;
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
        lista.Agregar(5, "Pedrito", "Fernandez", "pedrito@gmail.com", 10, "passdef");

        Console.WriteLine("Lista antes de ordenar:");
        lista.Imprimir();

        lista.SelectionSort();  // Usamos SelectionSort para ordenar por apellidos

        Console.WriteLine("\nLista después de ordenar por apellidos:");
        lista.Imprimir();
    }
}

