using Microsoft.AspNetCore.Hosting;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using MVCPROJE2_BLOGPROJE.SERVICES.FileService.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.SERVICES.FileService.Concrete
{
    public class ImageService: IImageService
    {
        private readonly IWebHostEnvironment _environment;
        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public async Task ImageRecordAsync(Uye uye)
        {
            await Task.Run(() =>
            {
                if (uye.KullaniciResimYolu != null)
                {
                    string resimler = Path.Combine(_environment.WebRootPath, "resimler");
                    if (uye.KullaniciResimYolu.Length > 0)
                    {
                        using (FileStream file = new FileStream(Path.Combine(resimler, uye.KullaniciResimYolu.FileName), FileMode.Create))
                        {
                            uye.KullaniciResimYolu.CopyTo(file);
                        }
                    }
                    uye.KullaniciResim = uye.KullaniciResimYolu.FileName;
                }
            });

        }
    }
}
