﻿namespace ERP.Api.Models
{
    public class RaincheckDto
    {
        public string name { get; set; }
        public string Count { get; set; }
        public int SalePrice { get; set; }
        public string Store { get; set; }
        public string Product { get; set; }

    }
    public class ProductDto
    {
        public string name { get; set; }
        public CategoryDto Category { get; set; }
    }

    public class StoreDto
    {
        public string name { get; set; }
    }

    public class CategoryDto
    {
        public string name   { get; set; }
    }

   
}