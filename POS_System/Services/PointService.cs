using Newtonsoft.Json;
using POS_System.Entity;
using POS_System.Helper;
using POS_System.Interface;
using POS_System.Model;
using StackExchange.Redis;

namespace POS_System.Services
{
    public class PointService : IPointService
    {
        private IDatabase _db;

        public PointService()
        {
            ConfigureRedis();
        }

        private void ConfigureRedis()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }

        //public PointResponseModel PointAPIService(PointRequestModel requestModel)
        //{
        //    PointResponseModel responseModel = new PointResponseModel();

        //    var alcoholPrice = requestModel.Items.Where(x => x.Type.ToLower() == "alcohol").Sum(s => s.Price);
        //    var price = requestModel.TotalPrice - alcoholPrice;
        //    var points = Math.Round((price * 10) / 100);

        //    using (var transaction = _context.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            PurchaseHistory purchaseHistory = new PurchaseHistory();
        //            purchaseHistory.MemberCode = requestModel.MemberCode;
        //            purchaseHistory.TotalItem = requestModel.TotalItem;
        //            purchaseHistory.TotalPrice = requestModel.TotalPrice;
        //            purchaseHistory.CreatedBy = 1;
        //            purchaseHistory.CreatedDate = DateTime.Now;
        //            _context.PurchaseHistories.Add(purchaseHistory);
        //            _context.SaveChanges();

        //            foreach (var data in requestModel.Items)
        //            {
        //                Item item = new Item();
        //                item.Name = data.ItemName;
        //                item.Price = data.Price;
        //                item.Type = data.Type;
        //                item.PurchasedId = purchaseHistory.Id;
        //                item.CreatedBy = 1;
        //                item.CreatedDate = DateTime.Now;
        //                _context.Items.Add(item);
        //                _context.SaveChanges();
        //            }

        //            Point point = new Point();
        //            point.Points = Convert.ToInt32(points);
        //            point.MemberCode = requestModel.MemberCode;
        //            point.CreatedBy = 1;
        //            point.CreatedDate = DateTime.Now;
        //            _context.Points.Add(point);
        //            _context.SaveChanges();

        //            transaction.Commit();

        //            responseModel.RespCode = "000";
        //            responseModel.RespDescription = "Success";
        //            responseModel.Point = Convert.ToInt32(points);
        //        }
        //        catch (Exception e)
        //        {
        //            transaction.Rollback();
        //            responseModel.RespCode = "999";
        //            responseModel.RespDescription = e.Message;
        //        }
        //    }

        //    return responseModel;
        //}

        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }

        public object RemoveData(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }
    }
}
