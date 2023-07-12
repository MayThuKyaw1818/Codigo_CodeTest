using POS_System.Entity;
using POS_System.Interface;
using POS_System.Model;

namespace POS_System.Services
{
    public class PurchaseHistoryService : IPurchaseHistory
    {
        private readonly DatabaseContext _context;

        public PurchaseHistoryService(DatabaseContext context)
        {
            _context = context;
        }

        public PurchaseHistoryResponseModel GetPurchaseHistory()
        {
            PurchaseHistoryResponseModel responseModel = new PurchaseHistoryResponseModel();
            List<PurchasedItemModel> purchaseItemList = new List<PurchasedItemModel>();

            var purchaseHistory = _context.PurchaseHistories.ToList();

            foreach(var purchaseHistoryItem in purchaseHistory)
            {
                PurchasedItemModel purchaseItemModel = new PurchasedItemModel();

                var items = _context.Items.Where(x => x.PurchasedId == purchaseHistoryItem.Id)
                     .Select(s => new ItemModel
                     {
                         ItemName = s.Name,
                         Price = s.Price,
                         Type = s.Type
                     }).ToList();

                purchaseItemModel.TotalPrice = purchaseHistoryItem.TotalPrice;
                purchaseItemModel.TotalItem = purchaseHistoryItem.TotalItem;
                purchaseItemModel.PurchasedDate = purchaseHistoryItem.CreatedDate.Value.ToString("dd-MM-yyyy");
                purchaseItemModel.Items = items;

                purchaseItemList.Add(purchaseItemModel);
            }

            responseModel.RespCode = "000";
            responseModel.RespDescription = "Success";
            responseModel.PurchasedItems = purchaseItemList;

            return responseModel;
        }
    }
}
