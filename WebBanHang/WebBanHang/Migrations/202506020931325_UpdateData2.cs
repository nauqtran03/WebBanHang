namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateData2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Advertisement", "Alias", c => c.String());
            AlterColumn("dbo.tb_Advertisement", "Title", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.tb_New", "Alias", c => c.String());
            AlterColumn("dbo.tb_New", "Title", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_New", "Title", c => c.String());
            AlterColumn("dbo.tb_New", "Alias", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.tb_Advertisement", "Title", c => c.String());
            AlterColumn("dbo.tb_Advertisement", "Alias", c => c.String(nullable: false, maxLength: 150));
        }
    }
}
