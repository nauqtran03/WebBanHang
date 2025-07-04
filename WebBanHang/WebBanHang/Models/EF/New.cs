﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Models.EF
{
    [Table("tb_New")]
    public class New : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "The news title is required.")]
        [StringLength(150)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public string Image {  get; set; }
        public int CategoryId { get; set; }
        public int ViewCount { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }
        public bool IsActive    { get; set; }
        public virtual Category Category { get; set; }  
    }
}