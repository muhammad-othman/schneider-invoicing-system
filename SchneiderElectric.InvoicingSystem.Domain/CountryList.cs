using System.Collections.Generic;

namespace SchneiderElectric.InvoicingSystem.Domain
{
    public class CountryList
    {
        public CountryList()
        {
            Currencies = new HashSet<Currency>();
        }
        public double ID { get; set; }

        public string City { get; set; }

        public string Code { get; set; }

        public string Country { get; set; }

        public string Region { get; set; }
        public virtual ICollection<Currency> Currencies { get; set; }
    }
}
