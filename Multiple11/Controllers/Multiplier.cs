using Microsoft.AspNetCore.Mvc;
using Multiple11.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Multiple11.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Multiplier : ControllerBase
    {
        [HttpPost]
        public Task<string> GetNumbers([FromBody] Numb items)
        {
            List<M11Response> m11Response = new List<M11Response>();
            M11Response m11 = new M11Response();

            string result = "";
            if (items != null)
            {
                foreach (var number in items.Numbers)
                {
                    bool isDivisible11 = CheckDivisible11(number.ToString());

                    m11.Number = number;
                    m11.IsMultiple = isDivisible11;

                    m11Response.Add(m11);
                    m11 = new M11Response();
                }

                result = JsonSerializer.Serialize(m11Response);

            }

            return Task.FromResult(result);
        }

        private bool CheckDivisible11(string numb)
        {
            int n = numb.Length;

            int oddDigSum = 0, evenDigSum = 0;

            for (int i = 0; i < n; i++)
            {
                if (i % 2 == 0)
                    oddDigSum += (numb[i] - '0');
                else
                    evenDigSum += (numb[i] - '0');
            }

            return ((oddDigSum - evenDigSum) % 11 == 0);
        }
    }
}
