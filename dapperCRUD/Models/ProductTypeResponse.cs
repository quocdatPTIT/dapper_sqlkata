using System;
using System.Collections.Generic;

namespace dapperCRUD.Models
{
    public class ProductTypeResponse
    {
        public Guid ProductTypeID { get; set; }
        public string ProductTypeKey { get; set; }
        public string ProductTypeName { get; set; }
        public short RecordStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedUser { get; set; }
        public ICollection<ProductModel> Products { get; set; }
    }
}