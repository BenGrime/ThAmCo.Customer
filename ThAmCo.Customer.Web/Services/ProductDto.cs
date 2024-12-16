using System;

namespace ThAmCo.Customer.Web.Services
{
    public class ProductDto
    {
        public required int Id {get; set;}

        public required String Name {get; set;}

        public required String Description  {get; set;}

        public required double Cost {get; set;}
    }
}