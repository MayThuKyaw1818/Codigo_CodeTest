using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Entity
{
    [Table("item")]
    public class Item
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("purchase_id")]
        public int PurchasedId { get; set; }

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
