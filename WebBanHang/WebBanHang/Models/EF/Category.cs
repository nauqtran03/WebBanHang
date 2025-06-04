using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHang.Models.EF
{
    [Table("tb_Category")]
    public class Category : CommonAbstract
    {
        public Category() 
        {
            this.News = new HashSet<New>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(150)]
        public string Title { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        public string Alias { get; set; }
        [StringLength(250)]
        public string SeoTitle { get; set; }
        [StringLength(250)]
        public string SeoDescription { get; set; }
        [StringLength(250)]
        public string SeoKeywords { get; set; }
        public int Position { get; set; }
        public bool IsActive { get; set; }

        public ICollection<New> News { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}