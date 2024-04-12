using CodeStream.Models;
using CodeStream.Models.DTO;
using CodeStream.Repositoy.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace CodeStream.Repositoy
{
    public class PaymentRepository : IPaymentRepository
    {

        private readonly IDbConnection _bd;

        public PaymentRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("connSQlServer"));
        }
        public async Task<int> CreatePayment(CreatePaymentDTO createPaymentDto, List<int> courses)
        {
            var sql = "INSERT INTO Payment (UserId, CountryId, TotalPrice) " +
            "OUTPUT INSERTED.Id " +  // Esto devuelve el PaymentId insertado
            "VALUES (@UserId, @CountryId, @TotalPrice)";

            var paymentId = await _bd.QueryFirstOrDefaultAsync<int>(sql, createPaymentDto);

            if(paymentId> 0)
            {
                var sql2 = "INSERT INTO PaymentCourse(PaymentId, CourseId) VALUES (@PaymentId, @CourseId)";
             
                foreach (var x in courses)
                {
                    await _bd.ExecuteAsync(sql2, new { PaymentId = paymentId, CourseId = x });
                }
            }

            return paymentId;

           
        }

        public async Task<List<AdminPaymentListDTO>> GetAdminPaymentListDTOs()
        {
            var sql = "SELECT p.Id, p.UserId, s.Id as StudentId, s.Name, s.Phone, u.Email, TotalPrice, Date, c.Name as Country FROM Payment p " +
                "INNER JOIN UserAccount u ON u.Id = p.UserId " +
                "INNER JOIN Country c ON c.Id = p.CountryId " +
                "INNER JOIN Student s ON s.UserId = p.UserId ";


            var payments = await _bd.QueryAsync<AdminPaymentListDTO>(sql);
            return payments.ToList();
        }

        public async Task<List<Invoice>> GetInvoice(int paymentId)
        {
            var sql = "SELECT p.Id, p.TotalPrice, p.Date, ce.Name, ce.Price, cy.Name as Country, s.Name as UserName  FROM Payment p " +
                "INNER JOIN PaymentCourse pc ON  pc.PaymentId = p.Id " +
                "INNER JOIN Country cy ON cy.Id = p.CountryId " +
                "INNER JOIN Course ce ON ce.Id = pc.CourseId " +
                "INNER JOIN Student s ON s.UserId = p.UserId " +
                "WHERE p.Id = @PaymentId";


            var invoices =await _bd.QueryAsync<Invoice>(sql, new { PaymentId = paymentId });


            return invoices.ToList();
        }
    }
}
