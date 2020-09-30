using System;

namespace TestTaskRoxo.BaseClasses
{
    public abstract class BaseOrderStatus
    {
        public virtual Int32 OrderStatusId { get; set; }
        public String Name { get; set; }
    }
}
