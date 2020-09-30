using System;

namespace TestTaskRoxo.BaseClasses
{
    public class OrderDetail : BaseOrderDetail
    {
        public Decimal Amount 
        { 
            get 
            {
                return Quantity * Price;
            } 
        }
    }
}
