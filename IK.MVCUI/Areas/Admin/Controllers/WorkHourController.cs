using IK.BLL.Managers.Abstracts;
using IK.ENTITIES.Enums;
using Microsoft.AspNetCore.Mvc;

namespace IK.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkHourController : Controller
    {
        private readonly IWorkHourManager _workhourmanager;


        public WorkHourController(IWorkHourManager workhourmanager)
        {
            _workhourmanager = workhourmanager;
        }


        public async Task<IActionResult> Index()
        {
            var list = await _workhourmanager.GetAllAsync();
            return View(list);
        }


        public async Task<IActionResult> Approve(int id)
        {
            var wh = await _workhourmanager.GetByIdAsync(id);
            wh.Status = DataStatus.Approved;
            await _workhourmanager.UpdateAsync(wh);
            return RedirectToAction("Index");
        }
    }
}
