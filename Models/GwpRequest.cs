using System.Collections.Generic;

namespace Galytix.WebApi.Models
{
    public class GwpRequest
    {
        public string Country { get; set; }
        public List<string> LineOfBusinesses { get; set; }
    }
}
