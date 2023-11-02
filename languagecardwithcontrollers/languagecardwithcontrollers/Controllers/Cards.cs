using languagecardwithcontrollers.Models;
using Microsoft.AspNetCore.Mvc;


namespace languagecardwithcontrollers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Cards : Controller
    {
        [HttpGet(Name = "GetCards")]
        public List<Card> Get()
        {
            DatabaseThingy dt = new DatabaseThingy();


            dt.Insert(new Card { English = "Dog", Welsh = "Ci" });
            List<Card> cards = new List<Card>();
           
            cards = dt.Select();


            return cards;
        }
    }
}
