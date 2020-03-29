using DataWrapper.Samples.ViewModelLayer;
using System.Web.Mvc;

namespace DataWrapper.Samples.Controllers
{
  public class StoredProceduresController : Controller
  {
    public ActionResult ProductGet()
    {
      ProductSPViewModel vm = new ProductSPViewModel();

      return View(vm);
    }

    [HttpPost]
    public ActionResult ProductGet(ProductSPViewModel vm)
    {
      vm.HandleRequest();

      ModelState.Clear();

      return View(vm);
    }

    public ActionResult ProductGetDataSet()
    {
      ProductSPDataSetViewModel vm = new ProductSPDataSetViewModel();

      return View(vm);
    }

    [HttpPost]
    public ActionResult ProductGetDataSet(ProductSPDataSetViewModel vm)
    {
      vm.HandleRequest();

      ModelState.Clear();

      return View(vm);
    }

    public ActionResult ProductModify()
    {
      ProductSPViewModel vm = new ProductSPViewModel();
      vm.CreateNewEntity();

      return View(vm);
    }

    [HttpPost]
    public ActionResult ProductModify(ProductSPViewModel vm)
    {
      vm.HandleRequest();

      ModelState.Clear();

      return View(vm);
    }
  }
}