namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateProduct1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Product", "Detail", c => c.String());
            AddColumn("dbo.tb_ProductCategory", "SeoTitle", c => c.String(maxLength: 250));
            AddColumn("dbo.tb_ProductCategory", "SeoDescription", c => c.String(maxLength: 500));
            AddColumn("dbo.tb_ProductCategory", "SeoKeywords", c => c.String(maxLength: 250));
            DropColumn("dbo.tb_Product", "Deatail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Product", "Deatail", c => c.String());
            DropColumn("dbo.tb_ProductCategory", "SeoKeywords");
            DropColumn("dbo.tb_ProductCategory", "SeoDescription");
            DropColumn("dbo.tb_ProductCategory", "SeoTitle");
            DropColumn("dbo.tb_Product", "Detail");
        }
    }
}
