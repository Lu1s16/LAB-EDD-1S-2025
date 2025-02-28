using System;
using System.Runtime.InteropServices;
using System.Text;
using DotNetGraph.Compilation;
using DotNetGraph.Core;
using DotNetGraph.Extensions;
using System.Diagnostics;


namespace Matriz


{
    public unsafe class MatrizDispersa<T> where T : unmanaged
    {


        public int capa; //Aparece en graficacion
        public ListaEncabezado<int> filas;
        public ListaEncabezado<int> columnas;

        public MatrizDispersa(int capa)
        {
            this.capa = capa;
            filas = new ListaEncabezado<int>("Fila");
            columnas = new ListaEncabezado<int>("Columna");
        }

        public void insert(int pos_x, int pos_y, string nombre)
        {

            //Creacion del nodo interno
            NodoInterno<int>* nuevo = (NodoInterno<int>*)Marshal.AllocHGlobal(sizeof(NodoInterno<int>));


            nuevo->id = 1;
            nuevo->nombre = nombre;
            nuevo->coordenadaX = pos_x;
            nuevo->coordenadaY = pos_y;
            nuevo->arriba = null;
            nuevo->abajo = null;
            nuevo->derecha = null;
            nuevo->izquierda = null;

            //Verificar si ya existen los encabezados en la matriz

            NodoEncabezado<int>* nodo_X = filas.getEncabezado(pos_x);

            NodoEncabezado<int>* nodo_Y = columnas.getEncabezado(pos_y);

            if (nodo_X == null) //Verificar que el encabezado fila pox_x exista
            {

                //Si nodo_X es nulo, significa que no existe el encabezado por lo que se crea
                filas.insertar_nodoEncabezado(pos_x);
                nodo_X = filas.getEncabezado(pos_x);


            }

            if (nodo_Y == null) //Verificamos que el encabezado columna pos_y exista
            {

                //Si nodo_Y es nulo, significa que aun no exist el encabezado por lo que se crea
                columnas.insertar_nodoEncabezado(pos_y);
                nodo_Y = columnas.getEncabezado(pos_y);


            }

            if (nodo_X == null || nodo_Y == null)
            {
                throw new InvalidOperationException("Error al crear los encabezados.");
            }

            //-----------INSERTAR NUEVO EN LA FILA
            if (nodo_X->acceso == null)
            {
                //Validamos que el nodo_X no este apuntando a ningun nodo interno
                nodo_X->acceso = nuevo;
            }
            else
            {
                //Si esta apuntando, validamos si la posicion de la columna del nuevo nodo interno es menor a la posicion de la columna del acceso
                if (nuevo->coordenadaY < nodo_X->acceso->coordenadaY) //F1 --> NI 1,1    NI 1,3   |NI = nodo intrno
                {
                    nuevo->derecha = nodo_X->acceso;
                    nodo_X->acceso->izquierda = nuevo;
                    nodo_X->acceso = nuevo;
                }
                else
                {
                    //De no cumplirse debemos movernos de izquirda a derecha buscando donde posicionar el nuevo nodo interno
                    NodoInterno<int>* tmp = nodo_X->acceso; //nodo_X:Fila1 ----->  NI 1,2; NI 1,3; NI 1,5;
                    while (tmp != null)
                    {
                        if (nuevo->coordenadaY < tmp->coordenadaY)
                        {
                            nuevo->derecha = tmp;
                            nuevo->izquierda = tmp->izquierda;
                            tmp->izquierda->derecha = nuevo;
                            tmp->izquierda = nuevo;
                            break;
                        }
                        else if (nuevo->coordenadaX == tmp->coordenadaX && nuevo->coordenadaY == tmp->coordenadaY)
                        {
                            //Valida que no haya repetidos
                            break;
                        }
                        else
                        {
                            if (tmp->derecha == null)
                            {
                                tmp->derecha = nuevo;
                                nuevo->izquierda = tmp;
                                break;
                            }
                            else
                            {
                                tmp = tmp->derecha;
                                //         nodo_Y:        C1    C3      C5      C6      
                                // nodo_X:F1 --->      NI 1,2; NI 1,3; NI 1,5; NI 1,6;
                                // nodo_X:F2 --->      NI 2,2; NI 2,3; NI 2,5; NI 2,6;
                            }
                        }
                    }

                }


            }

            // ----------------INSERTAR NUEVO EN COLUMNA
            if (nodo_Y->acceso == null) //-- comprobamos que el nodo_y no esta apuntando hacia ningun nodoCelda
            {
                nodo_Y->acceso = nuevo;
            }
            else //-- si esta apuntando, validamos si la posicion de la fila del NUEVO nodoCelda es menor a la posicion de la fila del acceso 
            {
                if (nuevo->coordenadaX < nodo_Y->acceso->coordenadaX)
                {
                    nuevo->abajo = nodo_Y->acceso;
                    nodo_Y->acceso->arriba = nuevo;
                    nodo_Y->acceso = nuevo;
                }
                else //de no cumplirse, debemos movernos de arriba hacia abajo buscando donde posicionar el NUEVO
                {
                    NodoInterno<int>* tmp2 = nodo_Y->acceso;
                    while (tmp2 != null)
                    {
                        if (nuevo->coordenadaX < tmp2->coordenadaX)
                        {
                            nuevo->abajo = tmp2;
                            nuevo->arriba = tmp2->arriba;
                            tmp2->arriba->abajo = nuevo;
                            tmp2->arriba = nuevo;
                            break;
                        }
                        else if (nuevo->coordenadaX == tmp2->coordenadaX && nuevo->coordenadaY == tmp2->coordenadaY)
                        //validamos que no haya repetidas
                        {
                            break;
                        }
                        else
                        {
                            if (tmp2->abajo == null)
                            {
                                tmp2->abajo = nuevo;
                                nuevo->arriba = tmp2;
                                break;
                            }
                            else
                            {
                                tmp2 = tmp2->abajo;
                            }

                        }


                    }



                }
            }



        }
        public void mostrar()
        {
            //nodos encabezados, columnas
            NodoEncabezado<int>* y_columna = columnas.primero;
            Console.Write("0->");

            while (y_columna != null)
            {
                Console.Write(y_columna->id + "->");
                y_columna = y_columna->siguiente;
            }
            Console.Write("\n");

            //nodos encabezados, empezamos con filas
            NodoEncabezado<int>* x_fila = filas.primero;
            while (x_fila != null)
            {
                NodoInterno<int>* interno = x_fila->acceso;
                Console.Write(x_fila->id + "->");

                while (interno != null)
                {
                    Console.Write(interno->nombre + "->");
                    interno = interno->derecha;

                }
                Console.Write("\n");


                x_fila = x_fila->siguiente;
            }

        }


        public void graficar2()
        {

            //Es graficar las cabeceras de columnas
            NodoEncabezado<int>* y_columna = columnas.primero;
            Console.Write("0->");

            //Inicializar dotgraph
            var graph = new DotGraph().WithIdentifier("MyDirectedGraph").Directed();
            // Crear subgrafo para columnas
            var mySubGraph = new DotSubgraph()
            {
                RankDir = "same"
            }.WithIdentifier("MySDirectedSubGraph");

            while (y_columna != null)
            {

                var Nodo1 = new DotNode()
                        //Atributo para identificar el nodo
                        .WithIdentifier("Columna" + Convert.ToString(y_columna->id)) //-> Para identificarlos pueden utilizar el id de cada estructura                                               //Forma del nodo
                        .WithShape(DotNodeShape.Ellipse)
                        //Texto que contiene el nodo
                        .WithLabel("R" + Convert.ToString(y_columna->id))
                        //Color del nodo
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);
                mySubGraph.Add(Nodo1);

                if (y_columna->siguiente != null)
                {
                    var Nodo2 = new DotNode()
                        .WithIdentifier("Columna" + Convert.ToString(y_columna->siguiente->id))
                        .WithShape(DotNodeShape.Ellipse)
                        .WithLabel("R" + Convert.ToString(y_columna->siguiente->id))
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

                    mySubGraph.Add(Nodo2);
                    mySubGraph.Add(Conexion);

                }

                y_columna = y_columna->siguiente;



            }

            graph.Add(mySubGraph);

            NodoEncabezado<int>* x_fila = filas.primero;
            int capa = 1;
            while (x_fila != null)
            {
                var mySubGraphFilas = new DotSubgraph()
                {
                    RankDir = "same"
                }.WithIdentifier("Capa" + Convert.ToString(capa));


                //Primer nodo de la fila
                var Nodo1 = new DotNode()
                        //Atributo para identificar el nodo
                        .WithIdentifier("Fila" + Convert.ToString(x_fila->id)) //-> Para identificarlos pueden utilizar el id de cada estructura
                        .WithShape(DotNodeShape.Ellipse)
                        //Texto que contiene el nodo
                        .WithLabel("V" + Convert.ToString(x_fila->id))
                        //Color del nodo
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);
                mySubGraphFilas.Add(Nodo1);

                NodoInterno<int>* interno = x_fila->acceso;
                if (interno != null)
                {

                    var Nodo2 = new DotNode()
                        //Atributo para identificar el nodo
                        .WithIdentifier(interno->nombre) //-> Para identificarlos pueden utilizar el id de cada estructura
                        .WithShape(DotNodeShape.Ellipse)
                        //Texto que contiene el nodo
                        .WithLabel(interno->nombre)
                        //Color del nodo
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);
                    mySubGraphFilas.Add(Nodo2);

                    //Conectar los nodos creados
                    var Conexion = new DotEdge().From(Nodo1).To(Nodo2)
                        .WithArrowHead(DotEdgeArrowType.Diamond)
                        .WithArrowTail(DotEdgeArrowType.Diamond)
                        .WithColor(DotColor.Red)
                        .WithFontColor(DotColor.Black)
                        .WithPenWidth(1.5);

                    mySubGraphFilas.Add(Nodo2);
                    mySubGraphFilas.Add(Conexion);

                }

                while (interno != null)
                {

                    var Nodo2 = new DotNode()
                        //Atributo para identificar el nodo
                        .WithIdentifier(interno->nombre) //-> Para identificarlos pueden utilizar el id de cada estructura
                        .WithShape(DotNodeShape.Ellipse)
                        //Texto que contiene el nodo
                        .WithLabel(interno->nombre)
                        //Color del nodo
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);
                    mySubGraphFilas.Add(Nodo2);

                    if (interno->derecha != null)
                    {
                        var Nodo3 = new DotNode()
                        .WithIdentifier(interno->derecha->nombre)
                        .WithShape(DotNodeShape.Ellipse)
                        .WithLabel(interno->derecha->nombre)
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);

                        //Conectar los nodos creados
                        var Conexion = new DotEdge().From(Nodo2).To(Nodo3)
                            .WithArrowHead(DotEdgeArrowType.Diamond)
                            .WithArrowTail(DotEdgeArrowType.Diamond)
                            .WithColor(DotColor.Red)
                            .WithFontColor(DotColor.Black)
                            .WithPenWidth(1.5);

                        mySubGraph.Add(Nodo3);
                        mySubGraph.Add(Conexion);
                    }

                    interno = interno->derecha;
                }

                //pasar a la siguiente fila
                x_fila = x_fila->siguiente;
                capa += 1;
                graph.Add(mySubGraphFilas);


            }


            //Ultimas conexiones
            x_fila = filas.primero;
            Console.Write("Ultimas conexiones\n");
            while (x_fila != null)
            {
                Console.Write("capa cabecera filas");
                var Nodo1 = new DotNode()
                        //Atributo para identificar el nodo

                        .WithIdentifier("Fila" + Convert.ToString(x_fila->id)) //-> Para identificarlos pueden utilizar el id de cada estructura                                               //Forma del nodo
                        .WithShape(DotNodeShape.Ellipse)
                        //Texto que contiene el nodo
                        .WithLabel("V" + Convert.ToString(x_fila->id))
                        //Color del nodo
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);
                graph.Add(Nodo1);

                if (x_fila->siguiente != null)
                {
                    var Nodo2 = new DotNode()
                        .WithIdentifier("Fila" + Convert.ToString(x_fila->siguiente->id))
                        .WithShape(DotNodeShape.Ellipse)
                        .WithLabel(Convert.ToString(x_fila->siguiente->id))
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);

                    var Conexion = new DotEdge().From(Nodo1).To(Nodo2)
                        .WithArrowHead(DotEdgeArrowType.Diamond)
                        .WithArrowTail(DotEdgeArrowType.Diamond)
                        .WithColor(DotColor.Red)
                        .WithFontColor(DotColor.Black)
                        .WithPenWidth(1.5);

                    graph.Add(Nodo2);
                    graph.Add(Conexion);


                }
                x_fila = x_fila->siguiente;


            }

            //Conexion de nodos internos por cada columna
            y_columna = columnas.primero;
            while (y_columna != null)
            {
                var Nodo1 = new DotNode()
                        //Atributo para identificar el nodo
                        .WithIdentifier("Columna" + Convert.ToString(y_columna->id)) //-> Para identificarlos pueden utilizar el id de cada estructura
                        .WithShape(DotNodeShape.Ellipse)
                        //Texto que contiene el nodo
                        .WithLabel("R" + Convert.ToString(y_columna->id))
                        //Color del nodo
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);
                graph.Add(Nodo1);

                NodoInterno<int>* interno = y_columna->acceso;

                if (interno != null)
                {
                    var Nodo2 = new DotNode()
                        //Atributo para identificar el nodo
                        .WithIdentifier(interno->nombre) //-> Para identificarlos pueden utilizar el id de cada estructura
                        .WithShape(DotNodeShape.Ellipse)
                        //Texto que contiene el nodo
                        .WithLabel(interno->nombre)
                        //Color del nodo
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);
                    graph.Add(Nodo2);

                    var Conexion = new DotEdge().From(Nodo1).To(Nodo2)
                        .WithArrowHead(DotEdgeArrowType.Diamond)
                        .WithArrowTail(DotEdgeArrowType.Diamond)
                        .WithColor(DotColor.Red)
                        .WithFontColor(DotColor.Black)
                        .WithPenWidth(1.5);

                    graph.Add(Nodo2);
                    graph.Add(Conexion);
                }

                while (interno != null)
                {
                    var Nodo2 = new DotNode()
                        //Atributo para identificar el nodo
                        .WithIdentifier(interno->nombre) //-> Para identificarlos pueden utilizar el id de cada estructura
                        .WithShape(DotNodeShape.Ellipse)
                        //Texto que contiene el nodo
                        .WithLabel(interno->nombre)
                        //Color del nodo
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);
                    graph.Add(Nodo2);

                    if (interno->abajo != null)
                    {
                        var Nodo3 = new DotNode()
                        .WithIdentifier(interno->abajo->nombre)
                        .WithShape(DotNodeShape.Ellipse)
                        .WithLabel(interno->abajo->nombre)
                        .WithFillColor(DotColor.Coral)
                        .WithFontColor(DotColor.Black)
                        .WithWidth(0.5)
                        .WithHeight(0.5)
                        .WithPenWidth(1.5);

                        //Conectar los nodos creados
                        var Conexion = new DotEdge().From(Nodo2).To(Nodo3)
                            .WithArrowHead(DotEdgeArrowType.Diamond)
                            .WithArrowTail(DotEdgeArrowType.Diamond)
                            .WithColor(DotColor.Red)
                            .WithFontColor(DotColor.Black)
                            .WithPenWidth(1.5);

                        graph.Add(Nodo3);
                        graph.Add(Conexion);
                    }

                    interno = interno->abajo;
                }

                y_columna = y_columna->siguiente;


            }




            //Es generar el .dot y crear la imagen
            var writer = new StringWriter();
            var context = new CompilationContext(writer, new CompilationOptions());
            var grafica = graph.CompileAsync(context);

            if (grafica != null)
            {
                var result = writer.GetStringBuilder().ToString();
                File.WriteAllText("graph.dot", result);

                //Aqui se ejecuta el comando para pasar el archivo .dot a .png
                ProcessStartInfo startInfo = new ProcessStartInfo("dot.exe");
                //-Tpng nombre_archivo.dot -o nombre_imagen.png
                startInfo.Arguments = "-Tpng graph.dot -o reporte2.png";

                Process.Start(startInfo);



            }
            else
            {
                Console.WriteLine("Error al graficar");
            }

        }




        ~MatrizDispersa()
        {
            // Liberar memoria de los nodos internos y encabezados de filas
            NodoEncabezado<int>* x_fila = filas.primero;
            while (x_fila != null)
            {
                // Liberar los nodos internos de la fila
                NodoInterno<int>* interno = x_fila->acceso;
                while (interno != null)
                {
                    NodoInterno<int>* tmp = interno;
                    interno = interno->derecha;
                    if (tmp != null)
                    {
                        Marshal.FreeHGlobal((IntPtr)tmp);
                    }

                }

                // Liberar el encabezado de fila
                NodoEncabezado<int>* tmp_fila = x_fila;
                x_fila = x_fila->siguiente;
                if (tmp_fila != null)
                {
                    Marshal.FreeHGlobal((IntPtr)tmp_fila);
                }

            }

            // Liberar memoria de los nodos internos y encabezados de columnas
            NodoEncabezado<int>* x_columna = columnas.primero;
            while (x_columna != null)
            {
                // Liberar los nodos internos de la columna
                NodoInterno<int>* interno = x_columna->acceso;
                while (interno != null)
                {
                    NodoInterno<int>* tmp = interno;
                    interno = interno->abajo;
                    if (tmp != null)
                    {
                        Marshal.FreeHGlobal((IntPtr)tmp);
                    }

                }

                // Liberar el encabezado de columna
                NodoEncabezado<int>* tmp_columna = x_columna;
                x_columna = x_columna->siguiente;
                if (tmp_columna != null)
                {
                    Marshal.FreeHGlobal((IntPtr)tmp_columna);
                }

            }
        }

    }
}