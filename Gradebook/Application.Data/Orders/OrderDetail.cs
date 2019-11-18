namespace AppCore.Data
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }

        public int PieId { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public virtual Pie Pie { get; set; }

        public virtual Order Order { get; set; }

    }
}