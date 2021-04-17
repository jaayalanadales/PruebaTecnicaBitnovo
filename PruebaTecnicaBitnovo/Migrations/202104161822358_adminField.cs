namespace PruebaTecnicaBitnovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adminField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Administrador", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Administrador");
        }
    }
}
