namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatecategory : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Category", "Description", c => c.String(maxLength: 4000));
            AlterColumn("dbo.tb_Category", "SeoTitle", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Category", "SeoDescription", c => c.String(maxLength: 250));
            AlterColumn("dbo.tb_Category", "SeoKeywords", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Category", "SeoKeywords", c => c.String());
            AlterColumn("dbo.tb_Category", "SeoDescription", c => c.String());
            AlterColumn("dbo.tb_Category", "SeoTitle", c => c.String());
            AlterColumn("dbo.tb_Category", "Description", c => c.String());
        }
    }
}
