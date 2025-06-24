using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Customer name is required")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Email { get; set; }
        public string Address { get; set; }
        public int TypeMethod {  get; set; }
    }
}