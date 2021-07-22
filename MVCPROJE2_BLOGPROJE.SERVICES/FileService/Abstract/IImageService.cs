using MVCPROJE2_BLOGPROJE.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.SERVICES.FileService.Abstract
{
   public interface IImageService
    {
        Task ImageRecordAsync(Uye uye);
    }
}
