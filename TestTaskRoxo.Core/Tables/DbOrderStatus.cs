using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTaskRoxo.BaseClasses;

namespace TestTaskRoxo.Core.Tables
{
    [Table("OrderStatus")]
    public class DbOrderStatus : BaseOrderStatus
    {
        [Key]
        public override Int32 OrderStatusId { get; set; }

        public List<DbOrder> Orders { get; set; }
    }
}
