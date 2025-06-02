using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHang.Models.EF
{
    [Table("tb_Product")]
    public class Product : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }
        public string ProductCode { get; set; }
        public int ProductCategoryId {  get; set; }
        public string Description { get; set; }
        public string Deatail { get; set; }
        public string Image {  get; set; }
        public decimal Price { get; set; }
        public decimal PricePrice { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale {  get; set; }
        public int Quantity { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        public virtual ProductCategory ProductCategorys {  get; set; }
    }
}