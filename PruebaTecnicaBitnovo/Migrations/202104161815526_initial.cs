namespace PruebaTecnicaBitnovo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        DNI = c.String(nullable: false, maxLength: 9),
                        Nombre = c.String(maxLength: 50),
                        Apellidos = c.String(maxLength: 50),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.DNI);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Usuario");
        }
    }
}
