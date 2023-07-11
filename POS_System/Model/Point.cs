using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Model
{
    [Table("point")]
    public class Point
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("point")]
        public int Points { get; set; }
    }
}
