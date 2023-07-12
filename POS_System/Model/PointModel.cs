namespace POS_System.Model
{
    public class PointRequestModel
    {
        public string MemberCode { get; set; }
        public int TotalItem { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime PurchasedDate { get; set; }
        public List<ItemModel> Items { get; set; }      
    }

    public class ItemModel
    {
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }       
    }

    public class PointResponseModel
    {
        public string RespCode { get; set; }
        public string RespDescription { get; set; }
        public int Point { get; set; }
    }

    public class PurchasedItemModel
    {
        public int TotalItem { get; set; }
        public decimal TotalPrice { get; set; }
        public string PurchasedDate { get; set; }
        public List<ItemModel> Items { get; set; }
    }
}
