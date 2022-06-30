using BizLand.Models;

namespace BizLand.ViewModels
{
    public class HomeViewModel
    {
        public List<PortfolioCategory> Categories { get; set; }
        public List<PortfolioItem> PortfoliDetails { get; set; }
    }
}
