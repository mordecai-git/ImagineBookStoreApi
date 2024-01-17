using System.ComponentModel.DataAnnotations;

namespace ImagineBookStore.Core.Models.App
{
    public class Book : BaseAppModel
    {
        [Required]
        [StringLength(255)]
        public  string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string Author { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int TotalStock { get; set; }

        public bool IsDeleted { get; set; } = false;

        public int? DeletedById { get; set; }

        public User DeletedBy { get; set; }
    }
}
