namespace AdDeposit.Domain.Entities
{
    public record Localization(
        string Street,
        string PostalCode,
        string City,
        string Country
        )
    {
    }
}