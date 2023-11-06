using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CsvHelper;
using Galytix.WebApi.Models;

namespace Galytix.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryGwpController : ControllerBase
    {
        [HttpPost]
        [Route("avg")]
        public async Task<IActionResult> CalculateAverageGwp([FromBody] GwpRequest request)
        {
            var result = await GetAverageGwpAsync(request.Country, request.LineOfBusinesses);
            return Ok(result);
        }

        private async Task<Dictionary<string, decimal>> GetAverageGwpAsync(string country, List<string> lineOfBusinesses)
        {
            var dataFilePath = "Data/gwpByCountry.csv";
            using (var reader = new StreamReader(dataFilePath))
            using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<GwpData>();
                var filteredRecords = records.Where(r => r.Country.ToLower() == country.ToLower() && lineOfBusinesses.Contains(r.LineOfBusiness));

                var result = new Dictionary<string, List<decimal>>();
                foreach (var record in filteredRecords)
                {
                    if (!result.ContainsKey(record.LineOfBusiness))
                    {
                        result[record.LineOfBusiness] = new List<decimal>();
                    }
                    result[record.LineOfBusiness].Add(record.GetAverageGwpValue());
                }

                var averageResult = result.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Any() ? kvp.Value.Average() : 0m);
                return averageResult;
            }
        }
    }
}
