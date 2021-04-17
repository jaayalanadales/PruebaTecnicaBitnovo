namespace PruebaTecnicaBitnovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuarioPassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Password", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Password");
        }
    }
}
