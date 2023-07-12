using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Entity
{
    [Table("member")]
    public class Member
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("member_code")]
        public string MemberCode { get; set; }

        [Column("mobile_no")]
        public string MobileNo { get; set; }

        [Column("created_date")]
        public DateTime? CreatedDate { get; set; }

        [Column("created_by")]
        public int? CreatedBy { get; set; }

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [Column("updated_by")]
        public int? UpdatedBy { get; set; }
    }
}
