namespace PruebaTecnicaBitnovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class username : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "NombreUsuario", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "NombreUsuario");
        }
    }
}
