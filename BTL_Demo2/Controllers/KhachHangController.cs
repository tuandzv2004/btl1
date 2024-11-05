using BTL_Demo2.Data;
using BTL_Demo2.Helpers;
using BTL_Demo2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class KhachHangController : Controller
{
    private readonly QuanLyCafeContext _dbContext;

    public KhachHangController(QuanLyCafeContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult DangKy()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DangKy(RegisterVM model)
    {
        if (ModelState.IsValid)
        {
			// Check if the customer already exists
			var existingCustomer = await _dbContext.KhachHang.Where(k => k.DienThoai == model.DienThoai).FirstOrDefaultAsync();
			if (existingCustomer != null)
            {
                TempData["Message"] = "Chào mừng bạn đã trở lại !";
                return RedirectToAction("Index", "Home");
            }

            // Create new customer
            var khachHang = new KhachHang
            {
                MaKH = await MyUtil.GenerateCustomerKeyAsync(_dbContext), 
                TenKH = model.TenKH,
                DienThoai = model.DienThoai,
                Email = model.Email
            };

            // Add new customer to the database
            _dbContext.Add(khachHang);
            await _dbContext.SaveChangesAsync();

            TempData["Message"] = "Chào mừng bạn đến với HiHi Coffee!";
            return RedirectToAction("Index", "Home");
        }

        return View(model);
    }
}
