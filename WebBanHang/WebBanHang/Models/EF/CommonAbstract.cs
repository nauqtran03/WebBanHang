﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHang.Models.EF
{
    public abstract class CommonAbstract
    {
        public string CreatedBy { get; set;}
        public  DateTime CreatedDate { get; set;}
        public DateTime ModifiedDate { get; set;}
        public string ModifierBy { get; set;}
    }
}