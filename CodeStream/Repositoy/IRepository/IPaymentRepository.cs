using CodeStream.Models;
using CodeStream.Models.DTO;

namespace CodeStream.Repositoy.IRepository
{
    public interface IPaymentRepository
    {

        public Task<int> CreatePayment(CreatePaymentDTO createPaymentDto, List<int> courses);
        public Task<List<Invoice>> GetInvoice(int paymentId);
        public Task<List<AdminPaymentListDTO>> GetAdminPaymentListDTOs();
    }
}
