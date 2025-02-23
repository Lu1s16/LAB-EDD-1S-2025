using System;
using System.Runtime.InteropServices;
//Importar paquetes para graphviz
using DotNetGraph.Compilation;
using DotNetGraph.Core;
using DotNetGraph.Extensions;
//Importar paquete para ejecutar el comando (este ya viene instalado)
using System.Diagnostics;


namespace List
{
    //2. Crear la clase para la lista
    public unsafe class ListaSimple<T> where T : unmanaged
    {
        private Nodo<T>* cabeza;

        public ListaSimple()
        {
            cabeza = null;
        }

        public void Insertar(T data, string name)
        {
            //Creamos el nodo
            Nodo<T>* nuevo = (Nodo<T>*)Marshal.AllocHGlobal(sizeof(Nodo<T>));
            nuevo->Data = data;
            nuevo->Nombre = name;
            nuevo->Sig = null;

            //Caso 1, que este vacia la lista
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            //Caso 2, que la lista este llena
            else
            {
                Nodo<T>* temp = cabeza;
                while (temp->Sig != null)
                {
                    temp = temp->Sig;
                }
                temp->Sig = nuevo;
            }
        }

        public void Eliminar(T data)
        {
            //Verificar que este vacia la lista
            if (cabeza == null) return;


            //Caso 1, que el nod a eliminar sea el primero
            if (cabeza->Data.Equals(data))
            {
                //guardamos el nodo a eliminar como temp
                Nodo<T>* temp = cabeza;
                cabeza = cabeza->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
                return;
            }

            Nodo<T>* actual = cabeza;
            //Verificamos que el siguiente del actual no sea null y que tambien sea el dato que queremos eliminar
            while (actual->Sig != null && !actual->Sig->Data.Equals(data))
            {
                actual = actual->Sig;
            }

            if (actual->Sig != null)
            {
                //guartamos el nodo a eliminar como temp
                Nodo<T>* temp = actual->Sig;
                actual->Sig = actual->Sig->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }

        public void Imprimir()
        {
            Nodo<T>* temp = cabeza;
            while (temp != null)
            {
                Console.WriteLine(temp->Data);
                Console.WriteLine(temp->Nombre);

                temp = temp->Sig;

            }
        }

        public void GenerarReporte()
        {
            //Nodo 1
            Nodo<T>* temp = cabeza;
            //Inicializar DotGraph para que al conectar los nodos tengan direccion.
            var directedGraph = new DotGraph().WithIdentifier("MyDirectedGraph").Directed();
            int id = 1;
            while (temp != null)
            {
                if (temp->Sig != null)
                {
                    //Nodo 2
                    Nodo<T>* temp2 = temp->Sig;
                    //Crear el nodo
                    var Nodo1 = new DotNode()
                        //Atributo para identificar el nodo
                        .WithIdentifier(Convert.ToString(id)) //-> Para identificarlos pueden utilizar el id de cada estructura
                        //Forma del nodo
                        .WithShape(DotNodeShape.Ellipse)
                        //Texto que contiene el nodo
                        .WithLabel(temp->Nombre)
                        //Color del nodo
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);

                    id+=1;

                    var Nodo2 = new DotNode()
                        .WithIdentifier(Convert.ToString(id))
                        .WithShape(DotNodeShape.Ellipse)
                        .WithLabel(temp2->Nombre)
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);

                    //Conectar los nodos creados
                    var Conexion = new DotEdge().From(Nodo1).To(Nodo2)
                        .WithArrowHead(DotEdgeArrowType.Diamond)
                        .WithArrowTail(DotEdgeArrowType.Diamond)
                        .WithColor(DotColor.Red)
                        .WithFontColor(DotColor.Black)
                        .WithPenWidth(1.5);

                    //Agregar los nodos y la conexion a graphviz
                    directedGraph.Add(Nodo1);
                    directedGraph.Add(Nodo2);
                    directedGraph.Add(Conexion);

                    //Aqui se empieza a crear el archivo .dot
                    var writer = new StringWriter();
                    var context = new CompilationContext(writer, new CompilationOptions());
                    var grafica = directedGraph.CompileAsync(context);

                    if (grafica != null)
                    {
                        var result = writer.GetStringBuilder().ToString();
                        File.WriteAllText("graph.dot", result);

                        //Aqui se ejecuta el comando para pasar el archivo .dot a .png
                        ProcessStartInfo startInfo = new ProcessStartInfo("dot.exe");
                        //-Tpng nombre_archivo.dot -o nombre_imagen.png
                        startInfo.Arguments = "-Tpng graph.dot -o reporte.png";

                        Process.Start(startInfo);



                    }
                    else
                    {
                        Console.WriteLine("Error al graficar");
                    }


                }

                temp = temp->Sig;
                id+=1;
            }
        }


        ~ListaSimple()
        {
            while (cabeza != null)
            {
                Nodo<T>* temp = cabeza;
                cabeza = cabeza->Sig;
                Marshal.FreeHGlobal((IntPtr)temp);
            }
        }
    }
}