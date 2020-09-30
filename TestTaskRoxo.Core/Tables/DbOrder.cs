using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTaskRoxo.BaseClasses;

namespace TestTaskRoxo.Core.Tables
{
    [Table("Order")]
    public class DbOrder : BaseOrder
    {
        [Key]
        public override Int32 OrderId { get; set; }

        public DbOrderStatus OrderStatus { get; set; }
        public List<DbOrderDetail> OrderDetails { get; set; }
    }
}
