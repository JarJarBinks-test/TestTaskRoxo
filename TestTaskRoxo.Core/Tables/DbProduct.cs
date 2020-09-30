using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTaskRoxo.BaseClasses;

namespace TestTaskRoxo.Core.Tables
{
    [Table("Product")]
    public class DbProduct : BaseProduct
    {
        [Key]
        public override Int32 ProductId { get; set; }


        public List<DbOrderDetail> OrderDetails { get; set; }
    }
}
