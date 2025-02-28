using System;

namespace Matriz
{
    class Program
    {
        static unsafe void Main()
        {
            
            MatrizDispersa<int> matriz = new MatrizDispersa<int>(0);
            //(pos_x, pos_y, nombre)
            matriz.insert(1, 1, "a");
            matriz.insert(1, 2, "b");
            matriz.insert(3, 4, "c");
            matriz.insert(3, 10, "d");
            matriz.insert(4, 2, "e");
            matriz.insert(6, 1,"f");

            //matriz.mostrar();
            matriz.graficar2();
            
        }
    }
}