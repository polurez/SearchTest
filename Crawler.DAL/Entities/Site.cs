using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Crawler.DAL.Entities
{
    public class Site
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Link { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [Column(TypeName = "image")]
        public byte[] Content { get; set; }

        [MaxLength(3), Column(TypeName = "char"), DefaultValue("htm")]
        public string ContentType { get; set; }
    }
}
