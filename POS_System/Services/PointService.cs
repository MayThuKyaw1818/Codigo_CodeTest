using POS_System.Entity;
using POS_System.Interface;
using POS_System.Model;

namespace POS_System.Services
{
    public class PointService : IPointService
    {
        private readonly DatabaseContext _context;

        public PointService(DatabaseContext context)
        {
            _context = context;
        }

        public PointResponseModel PointAPIService(PointRequestModel requestModel)
        {
            PointResponseModel responseModel = new PointResponseModel();

            var alcoholPrice = requestModel.Items.Where(x => x.Type.ToLower() == "alcohol").Sum(s => s.Price);
            var price = requestModel.TotalPrice - alcoholPrice;
            var points = Math.Round((price * 10) / 100);

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    PurchaseHistory purchaseHistory = new PurchaseHistory();
                    purchaseHistory.MemberCode = requestModel.MemberCode;
                    purchaseHistory.TotalItem = requestModel.TotalItem;
                    purchaseHistory.TotalPrice = requestModel.TotalPrice;
                    purchaseHistory.CreatedBy = 1;
                    purchaseHistory.CreatedDate = DateTime.Now;
                    _context.PurchaseHistories.Add(purchaseHistory);
                    _context.SaveChanges();

                    foreach (var data in requestModel.Items)
                    {
                        Item item = new Item();
                        item.Name = data.ItemName;
                        item.Price = data.Price;
                        item.Type = data.Type;
                        item.PurchasedId = purchaseHistory.Id;
                        item.CreatedBy = 1;
                        item.CreatedDate = DateTime.Now;
                        _context.Items.Add(item);
                        _context.SaveChanges();
                    }

                    Point point = new Point();
                    point.Points = Convert.ToInt32(points);
                    point.MemberCode = requestModel.MemberCode;
                    point.CreatedBy = 1;
                    point.CreatedDate = DateTime.Now;
                    _context.Points.Add(point);
                    _context.SaveChanges();

                    transaction.Commit();

                    responseModel.RespCode = "000";
                    responseModel.RespDescription = "Success";
                    responseModel.Point = Convert.ToInt32(points);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    responseModel.RespCode = "999";
                    responseModel.RespDescription = e.Message;
                }
            }

            return responseModel;
        }
    }
}
