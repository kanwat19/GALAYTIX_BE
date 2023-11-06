using System;
using CsvHelper.Configuration.Attributes;

namespace Galytix.WebApi.Models
{
    public class GwpData
    {
        [Name("country")]
        public string Country { get; set; }

        [Name("lineOfBusiness")]
        public string LineOfBusiness { get; set; }

        [Name("Y2000")]
        public decimal Y2000 { get; set; }

        [Name("Y2001")]
        public decimal Y2001 { get; set; }

        [Name("Y2002")]
        public decimal Y2002 { get; set; }

        [Name("Y2003")]
        public decimal Y2003 { get; set; }

        [Name("Y2004")]
        public decimal Y2004 { get; set; }

        [Name("Y2005")]
        public decimal Y2005 { get; set; }

        [Name("Y2006")]
        public decimal Y2006 { get; set; }

        [Name("Y2007")]
        public decimal Y2007 { get; set; }

        [Name("Y2008")]
        public decimal Y2008 { get; set; }

        [Name("Y2009")]
        public decimal Y2009 { get; set; }

        [Name("Y2010")]
        public decimal Y2010 { get; set; }

        [Name("Y2011")]
        public decimal Y2011 { get; set; }

        [Name("Y2012")]
        public decimal Y2012 { get; set; }

        [Name("Y2013")]
        public decimal Y2013 { get; set; }

        [Name("Y2014")]
        public decimal Y2014 { get; set; }

        [Name("Y2015")]
        public decimal Y2015 { get; set; }


        public decimal GetAverageGwpValue()
        {
            decimal total = 0;
            int yearCount = 0;

            for (int year = 2000; year <= 2015; year++)
            {
                total += GetYearValue(year);
                yearCount++;
            }

            return yearCount > 0 ? total / yearCount : 0m;
        }

        private decimal GetYearValue(int year)
        {
            string yearValue = (string)GetType().GetProperty($"Y{year}").GetValue(this);

            if (string.IsNullOrEmpty(yearValue) || !decimal.TryParse(yearValue, out decimal result))
            {
                // Handle the case when the data is empty or not convertible to decimal.
                // Here, we set a default value of 0.
                return 0m; // Default value
            }

            return result;
        }

    }
}
