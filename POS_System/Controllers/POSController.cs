using Microsoft.AspNetCore.Mvc;
using POS_System.Interface;
using POS_System.Model;

namespace POS_System.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class POSController : Controller
    {
        private readonly IPointService _pointService;
        private readonly IPurchaseHistory _purchaseHistory;
        private readonly IMemberService _memberService;

        public POSController(IPointService pointService, IPurchaseHistory purchaseHistory, IMemberService memberService)
        {
            _pointService = pointService;
            _purchaseHistory = purchaseHistory;
            _memberService = memberService;
        }

        [HttpPost]
        [Route("PointAPI")]
        public PointResponseModel PointAPI(PointRequestModel requestModel)
        {
            var pointResponseModel = _pointService.PointAPIService(requestModel);

            return pointResponseModel;
        }

        [HttpGet]
        [Route("PurchaseHistoryAPI")]
        public PurchaseHistoryResponseModel PurchaseHistoryAPI()
        {
            var purchaseHistory = _purchaseHistory.GetPurchaseHistory();

            return purchaseHistory;
        }

        [HttpPost]
        [Route("GenerateQRCodeAPI")]
        public string GenerateQRCode(QRCodeModel qRCode)
        {
            var qrCode = _memberService.GenerateQRCode(qRCode.QRCodeText);

            return qrCode;
        }

        [HttpPost]
        [Route("RegisterMemberAPI")]
        public RegisterMemberResponseModel RegisterMember(RegisterMemberRequestModel requestModel)
        {
            var responseModel = _memberService.RegisterMember(requestModel);

            return responseModel;
        }

        [HttpPost]
        [Route("LoginAPI")]
        public LoginResponseModel Login(LoginRequestModel requestModel)
        {
            var responseModel = _memberService.UserLogin(requestModel);

            return responseModel;
        }

        [HttpPost]
        [Route("VerifyOTPAPI")]
        public OTPResponseModel VerifyOTPAPI(OTPRequestModel requestModel)
        {
            var responseModel = _memberService.VerifyOTP(requestModel);

            return responseModel;
        }
    }
}
