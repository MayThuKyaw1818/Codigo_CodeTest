using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Entity
{
    [Table("purchase_history")]
    public class PurchaseHistory
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("member_code")]
        public string MemberCode { get; set; }

        [Column("total_item")]
        public int TotalItem { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

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
