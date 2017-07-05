namespace SchneiderElectric.InvoicingSystem.Domain
{
    public class Currency
    {
        public int CurrencyID { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencySymbol { get; set; }

        public double? CountryId { get; set; }

        public decimal RateToUS { get; set; }

        public virtual CountryList CountryList { get; set; }

    }
}
