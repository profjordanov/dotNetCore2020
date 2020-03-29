using DataWrapper.Samples.ViewModelLayer;
using System.Web.Mvc;

namespace DataWrapper.Samples.Controllers
{
  public class DynamicSQLController : Controller
  {
    public ActionResult ProductGet()
    {
      ProductViewModel vm = new ProductViewModel();

      return View(vm);
    }

    [HttpPost]
    public ActionResult ProductGet(ProductViewModel vm)
    {
      vm.HandleRequest();

      ModelState.Clear();

      return View(vm);
    }

    public ActionResult ProductGetDataSet()
    {
      ProductDataSetViewModel vm = new ProductDataSetViewModel();

      return View(vm);
    }

    [HttpPost]
    public ActionResult ProductGetDataSet(ProductDataSetViewModel vm)
    {
      vm.HandleRequest();

      ModelState.Clear();

      return View(vm);
    }

    public ActionResult ProductModify()
    {
      ProductViewModel vm = new ProductViewModel();
      vm.CreateNewEntity();

      return View(vm);
    }

    [HttpPost]
    public ActionResult ProductModify(ProductViewModel vm)
    {
      vm.HandleRequest();

      ModelState.Clear();

      return View(vm);
    }
  }
}