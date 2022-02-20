using ExcelToSql.Models;
using ExcelToSql.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ExcelToSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public List<Student> Students()
        {
            var file = Request.Form.Files[0];
            var exfile = file.OpenReadStream();

            using (ExcelPackage package = new ExcelPackage(exfile))
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                var sheet = package.Workbook.Worksheets["data"];
                var persons = ExcelServices.GetList<Student>(sheet);
                return persons;
            }
        }
    }
}
