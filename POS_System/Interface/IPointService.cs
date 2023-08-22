using POS_System.Model;

namespace POS_System.Interface
{
    public interface IPointService
    {
        //PointResponseModel PointAPIService(PointRequestModel requestModel);
        T GetData<T>(string key);

        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);

        object RemoveData(string key);
    }
}
