using Microsoft.AspNetCore.Mvc;

namespace Finance.Main.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class FinanceMainController
    {
        
        public FinanceMainController()
        {
            
        }


        [HttpGet("Version")]
        public object Version()
        {
            return "Finance main controller working";
        }
    }
}