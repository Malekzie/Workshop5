using Microsoft.AspNetCore.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TravelExperts.Utils
{
    public static class SvgHelper
    {
        public static HtmlString InlineSvg(string path)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path);
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException("SVG file not found.", fullPath);
            }
            var svgContent = File.ReadAllText(fullPath);
            return new HtmlString(svgContent);
        }
    }
}
