﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Domain.Entities
{
    public class ProductImageFile : File
    {
        public ICollection<Product> Products { get; set; }
    }
}
