namespace PruebaTecnicaBitnovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuarioId : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Usuario");
            AddColumn("dbo.Usuario", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Usuario", "NombreUsuario", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.Usuario", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Usuario");
            AlterColumn("dbo.Usuario", "NombreUsuario", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Usuario", "ID");
            AddPrimaryKey("dbo.Usuario", "NombreUsuario");
        }
    }
}
