using System;

namespace Matriz
{
    class Program
    {
        static unsafe void Main()
        {
            
            MatrizDispersa<int> matriz = new MatrizDispersa<int>(0);
            //(pos_x, pos_y, nombre)
            matriz.insert(1, 1, "*");
            matriz.insert(1, 2, "*");
            matriz.insert(3, 4, "*");
            matriz.insert(6, 1, "*");
            matriz.insert(4, 2, "*");

            matriz.mostrar();
            
        }
    }
}