using System;

namespace TestTaskRoxo.BaseClasses
{
    public abstract class BaseOrderDetail
    {
        public virtual Int32 OrderDetailId { get; set; }
        public Int32 OrderId { get; set; }
        public Int32 ProductId { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal Price { get; set; }
        //public virtual Decimal Amount { get; private set; }

    }
}
