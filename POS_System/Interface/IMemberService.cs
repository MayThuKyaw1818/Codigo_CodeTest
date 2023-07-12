using POS_System.Model;

namespace POS_System.Interface
{
    public interface IMemberService
    {
        string GenerateQRCode(string qRCode);
        RegisterMemberResponseModel RegisterMember(RegisterMemberRequestModel requestModel);
        LoginResponseModel UserLogin(LoginRequestModel requestModel);
        OTPResponseModel VerifyOTP(OTPRequestModel requestModel);
    }
}
