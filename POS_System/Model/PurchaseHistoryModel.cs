namespace POS_System.Model
{
    public class PurchaseHistoryRequestModel
    {
        public DateTime PurchasedDate { get; set; }
    }

    public class PurchaseHistoryResponseModel
    {
        public string RespCode { get; set; }
        public string RespDescription { get; set; }
        public List<PurchasedItemModel> PurchasedItems { get; set; }
    }
}
