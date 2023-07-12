using Microsoft.AspNetCore.Mvc;
using POS_System.Entity;
using POS_System.Interface;
using POS_System.Model;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace POS_System.Services
{
    public class MemberService : IMemberService
    {
        private readonly DatabaseContext _context;

        public MemberService(DatabaseContext context)
        {
            _context = context;
        }

        public string GenerateQRCode(string qRCode)
        {
            QRCodeGenerator QrGenerator = new QRCodeGenerator();
            QRCodeData QrCodeInfo = QrGenerator.CreateQrCode(qRCode, QRCodeGenerator.ECCLevel.Q);
            QRCode QrCode = new QRCode(QrCodeInfo);
            Bitmap QrBitmap = QrCode.GetGraphic(60);
            byte[] BitmapArray = QrBitmap.BitmapToByteArray();
            string QRCodeText = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray));

            return QRCodeText;
        }

        public RegisterMemberResponseModel RegisterMember(RegisterMemberRequestModel requestModel)
        {
            RegisterMemberResponseModel responseModel = new RegisterMemberResponseModel();

            var isExist = _context.Members.Any(x => x.MobileNo == requestModel.MobileNo);

            if (isExist)
            {
                responseModel.RespCode = "012";
                responseModel.RespDescription = "This mobile no is already registered.";
            }
            else
            {
                Member member = new Member();
                member.Name = requestModel.Name;
                member.MobileNo = requestModel.MobileNo;
                member.MemberCode = "M" + DateTime.Now.Ticks.ToString().Substring(0, 4);
                member.CreatedBy = 1;
                member.CreatedDate = DateTime.Now;
                _context.Members.Add(member);
                _context.SaveChanges();

                responseModel.RespCode = "000";
                responseModel.RespDescription = "Success";
            }
         
            return responseModel;
        }

        public LoginResponseModel UserLogin(LoginRequestModel requestModel)
        {
            LoginResponseModel responseModel = new LoginResponseModel();

            var isLoginUser = _context.Members.Where(x => x.MobileNo == requestModel.MobileNo).FirstOrDefault();

            if (isLoginUser != null)
            {
                Random random = new Random();
                var otp = (random.Next(100000, 999999)).ToString();

                Login login = new Login();
                login.OTP = otp;
                login.DeviceId = requestModel.DeviceId;
                login.MemberId = isLoginUser.Id;
                _context.Logins.Add(login);
                _context.SaveChanges();
            }
            else
            {
                responseModel.RespCode = "012";
                responseModel.RespDescription = "Login User is invalid.";
            }

            responseModel.RespCode = "000";
            responseModel.RespDescription = "Success";
            return responseModel;
        }

        public OTPResponseModel VerifyOTP(OTPRequestModel requestModel)
        {
            OTPResponseModel responseModel = new OTPResponseModel();

            var verifyData = _context.Members
                               .Join(_context.Logins,
                                  member => member.Id,
                                  login => login.MemberId,
                                  (member, login) => new { Member = member, Login = login })
                               .Where(x => x.Member.MobileNo == requestModel.MobileNo && x.Login.OTP == requestModel.OTP).FirstOrDefault();

            if (verifyData != null)
            {
                var loginUser = _context.Logins.Where(x => x.MemberId == verifyData.Member.Id && x.DeviceId == requestModel.DeviceId).FirstOrDefault();

                loginUser.LoginStatus = 1;
                _context.Logins.Update(loginUser);
                _context.SaveChanges();

                var loginUserList = _context.Members
                              .Join(_context.Logins,
                                 member => member.Id,
                                 login => login.MemberId,
                                 (member, login) => new { Member = member, Login = login })
                              .Where(x => x.Member.MobileNo == requestModel.MobileNo && x.Login.DeviceId != requestModel.DeviceId).ToList();

                foreach (var user in loginUserList)
                {
                    var loginData = _context.Logins.Where(x => x.Id == user.Login.Id).FirstOrDefault();

                    loginData.LoginStatus = 0;
                    _context.Logins.Update(loginData);
                    _context.SaveChanges();
                }

                responseModel.RespCode = "000";
                responseModel.RespDescription = "Success";
            }
            else
            {
                responseModel.RespCode = "012";
                responseModel.RespDescription = "OTP is invalid.";
            }
           
            return responseModel;
        }
    }

    public static class BitmapExtension
    {
        public static byte[] BitmapToByteArray(this Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
