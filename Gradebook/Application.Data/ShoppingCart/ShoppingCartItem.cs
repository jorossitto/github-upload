namespace AppCore.Data
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public virtual Pie Pie { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }

    }
}
