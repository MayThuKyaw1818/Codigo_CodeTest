using POS_System.Model;

namespace POS_System.Interface
{
    public interface IPointService
    {
        PointResponseModel PointAPIService(PointRequestModel requestModel);
    }
}
