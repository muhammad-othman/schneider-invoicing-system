namespace SchneiderElectric.InvoicingSystem.Domain
{
    public enum ExpenseState
    {
        AtEmployeeSaved = 1,
        AtPA = 2,
        AtPARejected = 3,
        AtPACanceled = 4,
        AtEM = 5,
        AtFinance = 6,
        Finished = 7
    }
}
