namespace Domain.Invoices
{
    public enum InvoiceStatus
    {
        Created,
        WaitingForPayment,
        Paid,
        Cancelled
    }
}
