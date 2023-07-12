using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Entity
{
    [Table("login")]
    public class Login
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("login_status")]
        public int LoginStatus { get; set; }

        [Column("token")]
        public string? Token { get; set; }

        [Column("otp")]
        public string? OTP { get; set; }

        [Column("device_id")]
        public string? DeviceId { get; set; }

        [Column("member_id")]
        public int MemberId { get; set; }
    }
}
