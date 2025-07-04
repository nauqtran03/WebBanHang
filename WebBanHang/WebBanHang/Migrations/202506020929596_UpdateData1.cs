﻿namespace WebBanHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateData1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tb_Category", "Alias", c => c.String());
            AlterColumn("dbo.tb_Category", "Title", c => c.String(nullable: false, maxLength: 150));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tb_Category", "Title", c => c.String());
            AlterColumn("dbo.tb_Category", "Alias", c => c.String(nullable: false, maxLength: 150));
        }
    }
}
