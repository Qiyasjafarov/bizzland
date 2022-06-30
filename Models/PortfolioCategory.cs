namespace BizLand.Models
{
    public class PortfolioCategory : BaseEntity
    {
        public string CategoryName { get; set; }
        public string DataFilter { get; set; }
        public List<PortfolioItem> PortfolioItems { get; set; }
    }
}
