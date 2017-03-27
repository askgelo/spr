using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace src.Components
{
    public class SampleViewComponent: ViewComponent
    {
        public SampleViewComponent()
        {
        }

        public IViewComponentResult Invoke(string data)
        {
            return Content($"Sample Component: {data}");
        }
    }
}
