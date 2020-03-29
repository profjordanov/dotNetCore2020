using DataWrapper.Samples.ViewModelLayer;
using System.Web.Mvc;

namespace DataWrapper.Samples.Controllers
{
  public class AttributesController : Controller
  {
    public ActionResult ProductCategoryGet()
    {
      ProductCategoryViewModel vm = new ProductCategoryViewModel();

      return View(vm);
    }

    [HttpPost]
    public ActionResult ProductCategoryGet(ProductCategoryViewModel vm)
    {
      vm.HandleRequest();

      ModelState.Clear();

      return View(vm);
    }
  }
}