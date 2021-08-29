using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using MVCPROJE2_BLOGPROJE.SERVICES.FileService.Abstract;
using MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.WEBUI.Controllers
{
    public class EssayController : Controller
    {
        private readonly IMakaleRepository _repository;
        private readonly IImageService _image;
        private readonly IKonuRepository _konuService;
        public EssayController(IMakaleRepository repository, IImageService image, IKonuRepository konuService)
        {
            _image = image;
            _repository = repository;
            _konuService = konuService;
        }
        public IActionResult Index() => View(_repository.Makaleler);

        [HttpGet]
        public IActionResult Create() => View(Tuple.Create<IEnumerable<Konu>, Makale>(_konuService.Konular, new Makale()));
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix ="item1")] Konu konu,[Bind(Prefix ="item2")] Makale makale)
        {
            if (ModelState.IsValid)
            {
                await _image.ImageRecordAsync(makale);
                makale.UyeID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
                await _repository.MakaleEkleAsync(makale);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> ReadEssay(int id)
        {
            Makale makale = await _repository.GetByIdAsync(id);
            return View(makale);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEssay(int id) => View(await _repository.GetByIdAsync(id));
        [HttpPost]
        public async Task<IActionResult> UpdateEssay(Makale makale)
        {
            if (ModelState.IsValid)
            {           
            await _image.ImageRecordAsync(makale);
            makale.UyeID = Convert.ToInt32(HttpContext.Session.GetInt32("id"));
            await _repository.MakaleGuncelleAsync(makale);
            return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public async Task<IActionResult> Delete(int id) => View(await _repository.MakaleSilAsync(id)); 
       
    }
}
