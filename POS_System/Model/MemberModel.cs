namespace POS_System.Model
{
    public class RegisterMemberRequestModel
    {
        public string Name { get; set; }
        public string MobileNo { get; set; }
    }

    public class RegisterMemberResponseModel
    {
        public string RespCode { get; set; }
        public string RespDescription { get; set; }
    }

    public class LoginRequestModel
    {
        public string MobileNo { get; set; }
        public string DeviceId { get; set; }
    }

    public class OTPRequestModel
    {
        public string MobileNo { get; set; }
        public string OTP { get; set; }

        public string DeviceId { get; set; }
    }
    public class OTPResponseModel
    {
        public string RespCode { get; set; }
        public string RespDescription { get; set; }
    }


}
