

namespace Api.DTOS
{
    public class ResevationDto
    {
        public int propertyId { get; set; }
        public PaymentDto paymentDto { get; set; }
       
        public BookingDTO bookingDTO { get; set; }
        public TransactionDto transactionDto { get; set; }

    }
}
