using System;

namespace TestTaskRoxo.BaseClasses
{
    public abstract class BaseOrder
    {
        public virtual Int32 OrderId { get; set; }
        public DateTime DateCreated { get; set; }
        public Int32 OrderStatusId { get; set; }
    }
}
