namespace PruebaTecnicaBitnovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nombreUsuarioKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Usuario");
            AlterColumn("dbo.Usuario", "NombreUsuario", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.Usuario", "NombreUsuario");
            DropColumn("dbo.Usuario", "DNI");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuario", "DNI", c => c.String(nullable: false, maxLength: 9));
            DropPrimaryKey("dbo.Usuario");
            AlterColumn("dbo.Usuario", "NombreUsuario", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.Usuario", "DNI");
        }
    }
}
