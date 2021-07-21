using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using MVCPROJE2_BLOGPROJE.SERVICES.Repositories.Abstract;
using MVCPROJE2_BLOGPROJE.WEBUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.WEBUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMakaleRepository _makaleRepository;
        private readonly IKonuRepository _konuRepository;

        public HomeController(ILogger<HomeController> logger,IMakaleRepository makaleRepository,IKonuRepository konuRepository)
        {
            _logger = logger;
            _makaleRepository = makaleRepository;
            _konuRepository = konuRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(Tuple.Create<IEnumerable<Makale>,IEnumerable<Konu>>(_makaleRepository.Makaleler.ToList(),_konuRepository.Konular.ToList()));
        }
        [HttpPost]
        public IActionResult Index([Bind(Prefix = "item1")] Makale item1, [Bind(Prefix = "item2")] Konu item2) => View();
        public IActionResult Konu() => View();
        public IActionResult Hakkimizda() => View();
       
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
