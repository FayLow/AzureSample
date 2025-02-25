using AzureSampleDataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzureSampleWebServices.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(25)]
        public string ProductNumber { get; set; } = null!;

        [StringLength(15)]
        public string? Color { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal StandardCost { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal ListPrice { get; set; }

        [StringLength(5)]
        public string? Size { get; set; }

        [Range(0.00, 999999.99)]
        public decimal? Weight { get; set; }

        public int? ProductCategoryId { get; set; }

        public int? ProductModelId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime SellStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? SellEndDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DiscontinuedDate { get; set; }

        [DataType(DataType.Upload)]
        public byte[]? ThumbNailPhoto { get; set; }

        [StringLength(50)]
        public string? ThumbnailPhotoFileName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Rowguid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ModifiedDate { get; set; }

        public virtual ProductCategory? ProductCategory { get; set; }

        public virtual ProductModel? ProductModel { get; set; }

        public virtual ICollection<SalesOrderDetail> SalesOrderDetails { get; set; } = new List<SalesOrderDetail>();

    }
}
