namespace SISLibrary.Entity
{
    public class Payments
    {
        public int PaymentID { get; set; }
        public Students Student { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Payments(int paymentID, Students student, decimal amount, DateTime paymentDate)
        {
            PaymentID = paymentID;
            Student = student;
            Amount = amount;
            PaymentDate = paymentDate;
        }
    }
}
