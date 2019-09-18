﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Item
{
    public class ItemEdit
    {
        public int ItemId { get; set; }
        public int StoreId { get; set; }
        public virtual Data.Store Store { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
    }
}
