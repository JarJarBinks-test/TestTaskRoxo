using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestTaskRoxo.BaseClasses;

namespace TestTaskRoxo.Core.Tables
{
    [Table("OrderDetail")]
    public class DbOrderDetail : BaseOrderDetail
    {
        [Key]
        public override Int32 OrderDetailId { get; set; }

        /* TODO: 'virtual' issue.
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public override Decimal Amount
        {
            get => Quantity * Price;
            private set { needed for EF  }
        }
        */

        public DbOrder Order { get; set; }

        public DbProduct Product { get; set; }

    }
}
