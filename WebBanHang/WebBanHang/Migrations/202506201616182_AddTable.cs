namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Reports", newName: "tb_Report");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tb_Report", newName: "Reports");
        }
    }
}
