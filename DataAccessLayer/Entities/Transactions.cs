
namespace DataAccessLayer.Entities;

public class Transactions
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
    public DateTime Date { get; set; }

}
