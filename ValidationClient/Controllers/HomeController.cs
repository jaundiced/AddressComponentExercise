using System.Threading.Tasks;
using System.Web.Mvc;
using AddressManager.Interfaces;
using Models.Address;
using ValidationClient.Models;

namespace ValidationClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAddressManager _addressManager;  

        public HomeController(IAddressManager addressManager)
        {
            _addressManager = addressManager;
        }


        public ActionResult Index()
        {
            var address = new Address{State = new State()};
            var model = new HomeModel
            {
                SelectedAddress = address
            };
            return View(model);
        }

        public async Task<ActionResult> Address(int? id)
        {
            var address = new Address {State = new State()};

            if (id.HasValue)
            {
                var task = await _addressManager.GetAddress(id.Value);
                address = task.Item;
            }
            var model = new HomeModel
            {
                SelectedAddress = address
            };
            return View("~/Views/Home/SaveAddress.cshtml",model);
            
        }

        [HttpPost]
        public async Task<ActionResult> IndexResult()
        {
            int id;
            int.TryParse(Request.Form["addressIdSelect"], out id);

            var task = await _addressManager.GetAddress(id);
            var model = new HomeModel
            {
                SelectedAddress = (id > 0) ? task.Item : new Address()
            };

            return View("~/Views/Home/Index.cshtml", model);
        }

        [HttpPost]
        public async Task<ActionResult> AddressResult()
        {
            int addressId, stateId;
            int.TryParse(Request.Form["addressId"], out addressId);
            int.TryParse(Request.Form["stateId"], out stateId);
            var address = new Address
            {
                AddressId = addressId,
                State = new State { Id = stateId },
                Name = Request.Form["Name"],
                Company = Request.Form["Company"],
                City = Request.Form["City"],
                Zip = Request.Form["Zip"],
                AddressLine1 = Request.Form["Add1"],
                AddressLine2 = Request.Form["Add2"]

            };
            var task = await _addressManager.SaveAddress(address);

            var model = new HomeModel
            {
                Message = (task.IsSuccess()) ? "Address saved" : "Error saving  address",
                SelectedAddress = address
            };

            return View("~/Views/Home/SaveAddress.cshtml", model);
        }
    }
}
