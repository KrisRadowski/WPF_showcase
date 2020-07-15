namespace WPF_showcase
{
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Linq;

	public class DBModel : DbContext
	{
		// Your context has been configured to use a 'Model1' connection string from your application's 
		// configuration file (App.config or Web.config). By default, this connection string targets the 
		// 'WPF_showcase.Model1' database on your LocalDb instance. 
		// 
		// If you wish to target a different database and/or database provider, modify the 'Model1' 
		// connection string in the application configuration file.
		public DBModel()
			: base("name=DBModel")
		{
		}

		// Add a DbSet for each entity type that you want to include in your model. For more information 
		// on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

		public virtual DbSet<Entry> Entries{ get; set; }
	}

	public class Entry
	{
	    public int Id { get; set; }
	    public DateTime EntryDate { get; set; }
		public string EntryContent { get; set; }
		public virtual Alarm Alarm { get; set; }
	}

	public class Alarm
	{
		public int AlarmId { get; set; }
		public DateTime AlarmDate { get; set; }
		public bool Enabled { get; set; }
	}
}