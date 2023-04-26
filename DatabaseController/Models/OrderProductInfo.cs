namespace DatabaseController.Models
{
    //Смежная таблица для многие ко многим между Заказом и продуктами
    public class OrderProductInfo
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }


        public int ProductQuantity { get; set; }

        public int Price { get; set; }
    }
}
