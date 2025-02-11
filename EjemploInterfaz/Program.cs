using Gdk;
using Gtk;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

class MyWindow : Gtk.Window
{

    //Elementos
    private Entry textInput;
    private Button btnSubmit;
    private Button btnLoadJson;
    private Label lblJsonContent;
    private Button btnMoreOptions;



    //Constructor de mywindow
    [Obsolete]
    public MyWindow() : base("Hello, World")
    {
        //Configuracion de la ventana
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);


        //Crear un contenedor
        VBox vbox = new VBox(false, 5);

        //Crear el campo de texto
        textInput = new Entry();
        textInput.PlaceholderText = "Escribe algo aqui...";

        //Crear un boton
        btnSubmit = new Button("Enviar");
        btnSubmit.Clicked += OnButtonClicked;

        btnLoadJson = new Button("Cargar Json");
        btnLoadJson.Clicked += OnLoadJsonClicked;

        lblJsonContent = new Label("Contenido del archivo json");
        lblJsonContent.Wrap = true;

        btnMoreOptions = new Button("Mas opciones");
        btnMoreOptions.Clicked += OnMoreOptionsClicked;


        //Agregar elementos al contendedor
        vbox.PackStart(textInput, false, false, 5);
        vbox.PackStart(btnSubmit, false, false, 5);
        vbox.PackStart(btnLoadJson, false, false, 5);
        vbox.PackStart(lblJsonContent, false, false, 5);
        vbox.PackStart(btnMoreOptions, false, false, 5);

        //Agregar contenedor a la ventana
        Add(vbox);

        ShowAll();

    }

    private void OnButtonClicked(object sendr, EventArgs e)
    {
        string userInput = textInput.Text;
        Console.WriteLine("Texto ingresado: " + userInput);
    }

    private void OnLoadJsonClicked(object sender, EventArgs e)
    {
        //Crear el dialogo para seleccionar el archivo
        FileChooserDialog fileChooser = new FileChooserDialog(
            "Seleccionar un archivo JSON", this, 
            FileChooserAction.Open,
            "Cancelar", ResponseType.Cancel, 
            "Abrir", ResponseType.Accept

        );

        //Filtro para elegir solo archivos json
        FileFilter filter = new FileFilter();
        filter.AddPattern("*.json"); 
        fileChooser.Filter = filter;

        if (fileChooser.Run() == (int)ResponseType.Accept)
        {

            string filePath = fileChooser.Filename; // Obtener la ruta del archivo
            fileChooser.Destroy();

            try 
            {

                string jsonContent = File.ReadAllText(filePath);
                lblJsonContent.Text = jsonContent;
                Console.WriteLine("Contenido JSON:\n"+ jsonContent); 

                List<Persona> personas = JsonConvert.DeserializeObject<List<Persona>>(jsonContent);

                string resultado = "Lista de personas:\n";
                foreach(var persona in personas)
                {
                    resultado += $"Nombre: {persona.Nombres}, Id: {persona.Id}, Email: {persona.Correo}\n";
                }

                Console.Write(resultado);




            } catch (Exception ex)
            {
                lblJsonContent.Text = "Error al leer el archivo.";
                Console.WriteLine("Error: " + ex.Message);
            }

        } else 
        {
            fileChooser.Destroy();
        };


    }

    private void OnMoreOptionsClicked(object sender, EventArgs e) 
    {
        new OptionsWindow();
    }

    protected override bool OnDeleteEvent(Event e)
    {
        Application.Quit();
        return true;
        
    }

    class Hello 
    {

        static void Main()
        {
            Application.Init();
            MyWindow w = new MyWindow();
            //Muestra la ventana
            w.ShowAll();
            //Permite a la interfaz responder a la interacion del usuariio
            Application.Run();
        }

    }


}