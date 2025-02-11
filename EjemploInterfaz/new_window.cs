using Gdk;
using Gtk;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

class OptionsWindow : Gtk.Window
{
    public OptionsWindow() : base("Opciones")
    {
        SetDefaultSize(300, 200);
        SetPosition(WindowPosition.Center);

        VBox vbox = new VBox(false, 5);

        Button btnView = new Button("Ver");
        btnView.Clicked += (sender, e) => Console.WriteLine("Ver opción seleccionada.");

        Button btnEdit = new Button("Editar");
        btnEdit.Clicked += (sender, e) => Console.WriteLine("Editar opción seleccionada.");

        Button btnDelete = new Button("Eliminar");
        btnDelete.Clicked += (sender, e) => Console.WriteLine("Eliminar opción seleccionada.");

        vbox.PackStart(btnView, false, false, 5);
        vbox.PackStart(btnEdit, false, false, 5);
        vbox.PackStart(btnDelete, false, false, 5);

        Add(vbox);
        ShowAll();
    }
}