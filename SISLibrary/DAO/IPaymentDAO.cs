using SISLibrary.Entity;
using System.Collections.Generic;

namespace SISLibrary.DAO
{
    public interface IPaymentDAO
    {
        
        List<Payments> GetPaymentsForStudent(int studentId);

        
        List<Payments> GetAllPayments();

        
        void AddPayment(Payments payment);

        
        void UpdatePayment(Payments payment);
    }
}
