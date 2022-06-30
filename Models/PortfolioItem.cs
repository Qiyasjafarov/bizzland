using System.ComponentModel.DataAnnotations.Schema;

namespace BizLand.Models
{
    public class PortfolioItem : BaseEntity
    {
        public int PortfolioCategoryId { get; set; }
        public string Name { get; set; }
        public string ClientName { get; set; }
        public DateTime Date { get; set; }
        public string ProjectUrl { get; set; }
        public string Image { get; set; }
        public string DetailedInformation { get; set; }
        public PortfolioCategory Category { get; set; }

        [NotMapped]
        public IFormFile Img { get; set; }
    }
}
