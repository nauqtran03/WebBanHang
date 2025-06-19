namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatTableReport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        VisitsPage = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tb_New", "ViewCount", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Post", "ViewCount", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Product", "ViewCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tb_Product", "ViewCount");
            DropColumn("dbo.tb_Post", "ViewCount");
            DropColumn("dbo.tb_New", "ViewCount");
            DropTable("dbo.Reports");
        }
    }
}
