using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication4.Data;
using WebApplication4.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        
        //private readonly ILogger<HomeController> _logger;

        public ApplicationDbContext _Context { get; set; }
        public HomeController(ApplicationDbContext context)
        {
            _Context = context;
        }
        public IActionResult Create(Animal animal)
        {
            _Context.Animals.Add(animal);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }

        //public IActionResult Index()
        //{
        //    var model = _Context.animals.ToList();
        //    return View(model);
        //}
        public IActionResult GetData()
        {
            // ดึงข้อมูลใหม่จากฐานข้อมูล
            var animals = _Context.Animals.ToList();

            // สร้างข้อมูลที่จะส่งกลับในรูปแบบ JSON
            var data = animals.Select(a => new
            {
                a.AnimalId,
                a.Name,
                a.Type,
                a.Leg,
                a.Arm,
                a.Head,
                a.Eyes,
                a.Tail
            });

            // ส่งค่าข้อมูลเป็น JSON กลับไปยังแอปพลิเคชัน
            return Json(data);
        }

        [HttpPost]
        public IActionResult UpdateLeg(int animalId)
        {
            try
            {
                // ดึงข้อมูลสัตว์เลี้ยงจากฐานข้อมูลตาม animalId
                var animal = _Context.Animals.FirstOrDefault(a => a.AnimalId == animalId);
                if (animal != null)
                {
                    // อัปเดตจำนวนขาของสัตว์เลี้ยง
                    animal.Leg += 1;

                    // บันทึกการเปลี่ยนแปลงในฐานข้อมูล
                    _Context.SaveChanges();

                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, error = "Animal not found." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddLeg(int animalId)
        {
            try
            {
                // ดึงข้อมูล Animal จากฐานข้อมูลด้วย AnimalId
                var animal = _Context.Animals.FirstOrDefault(a => a.AnimalId == animalId);

                if (animal != null)
                {
                    // ลดค่าของ Leg ทีละ 1
                    if (animal.Leg >= 0)
                    {
                        animal.Leg++;

                        // บันทึกการเปลี่ยนแปลงลงในฐานข้อมูล
                        _Context.SaveChanges();

                        // ส่งคำตอบกลับให้เป็น JSON ถ้าต้องการ
                        return Json(new { success = true });
                    }
                }

                return Json(new { success = false, error = "Animal not found or Leg cannot be reduced further." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        [HttpPost]
        public IActionResult DeleteLeg(int animalId)
        {
            try
            {
                // ดึงข้อมูล Animal จากฐานข้อมูลด้วย AnimalId
                var animal = _Context.Animals.FirstOrDefault(a => a.AnimalId == animalId);

                if (animal != null)
                {
                    // ลดค่าของ Leg ทีละ 1
                    if (animal.Leg > 0)
                    {
                        animal.Leg--;

                        // บันทึกการเปลี่ยนแปลงลงในฐานข้อมูล
                        _Context.SaveChanges();

                        // ส่งคำตอบกลับให้เป็น JSON ถ้าต้องการ
                        return Json(new { success = true });
                    }
                }

                return Json(new { success = false, error = "Animal not found or Leg cannot be reduced further." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public IActionResult Index()
        {
            var model = new Naja
            {
                Animals = _Context.Animals.ToList()
            }; // ดึงข้อมูลสัตว์จากฐานข้อมูลและแปลงเป็น List
            return View(model); // ส่ง List ไปยังหน้า Razor view
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}