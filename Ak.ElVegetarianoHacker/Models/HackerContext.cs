﻿namespace Ak.ElVegetarianoHacker.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class HackerContext : DbContext
    {
        // Der Kontext wurde für die Verwendung einer HackerContext-Verbindungszeichenfolge aus der 
        // Konfigurationsdatei ('App.config' oder 'Web.config') der Anwendung konfiguriert. Diese Verbindungszeichenfolge hat standardmäßig die 
        // Datenbank 'Ak.ElVegetarianoHacker.Models.HackerContext' auf der LocalDb-Instanz als Ziel. 
        // 
        // Wenn Sie eine andere Datenbank und/oder einen anderen Anbieter als Ziel verwenden möchten, ändern Sie die HackerContext-Zeichenfolge 
        // in der Anwendungskonfigurationsdatei.
        public HackerContext()
            : base("name=HackerContext")
        {
        }

        // Fügen Sie ein 'DbSet' für jeden Entitätstyp hinzu, den Sie in das Modell einschließen möchten. Weitere Informationen 
        // zum Konfigurieren und Verwenden eines Code First-Modells finden Sie unter 'http://go.microsoft.com/fwlink/?LinkId=390109'.

         public virtual DbSet<User> Users { get; set; }
    }


}