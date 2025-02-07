// See https://aka.ms/new-console-template for more information
using System;



public class Juego
{
    public static void Main(string[] args)
    {
        Console.Write("Bienvenido, ingrese su nombre: ");
        bool seguir = true;

        var player = Console.ReadLine();

        do{
            
            Console.Write("\nElija piedra papel o tijera ");
            Console.Write("\n1. Piedra");
            Console.Write("\n2. Papel");
            Console.Write("\n3. Tijera");
            Console.Write("\n4. Salir");

            int op = Convert.ToInt32(Console.ReadLine());

            Random rnd = new Random();
            int opcioncpu = rnd.Next(1,4);

            //Opciones maquina
            // 1. Piedra
            // 2. Papel
            // 3. Tijera
            

            switch(op)
            {
                case 1:
                    //Console.Write("Piedra");
                    

                    if(opcioncpu == 1) {
                        Console.Write("Empate\n");
                    } else if(opcioncpu == 2) {
                        Console.Write("Gana CPU\n");
                    } else if(opcioncpu == 3) {
                        Console.Write("Gana " + player + "\n");
                    }

                    break;

                case 2:
                    //Console.Write("Papel");
                    if(opcioncpu == 1) {
                        Console.Write("Gana " + player + "\n");
                    } else if(opcioncpu == 2) {
                        Console.Write("Empate\n");
                    } else if(opcioncpu == 3) {
                        Console.Write("Gana CPU\n");
                    }
                    break;

                case 3:
                    //Console.Write("Tijera");
                    if(opcioncpu == 1) {
                        Console.Write("Gana CPU\n");
                    } else if(opcioncpu == 2) {
                        Console.Write("Gana " + player + "\n");
                    } else if(opcioncpu == 3) {
                        Console.Write("Empate\n");
                    }
                    
                    break;

                case 4:
                    seguir = false;
                    break;
                
                default:
                    break;

            }
            

        } while(seguir);

    }
}


