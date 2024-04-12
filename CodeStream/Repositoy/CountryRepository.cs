using CodeStream.Models;
using CodeStream.Models.DTO;
using CodeStream.Pages.Admin.Duration;
using CodeStream.Repositoy.IRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CodeStream.Repositoy
{
    public class CountryRepository : ICountryRepository
    {

        private readonly IDbConnection _bd;

        public CountryRepository(IConfiguration configuration)
        {
            _bd = new SqlConnection(configuration.GetConnectionString("connSQlServer"));
        }

        public async Task<bool> CreateCountry(FormCountryDTO formCountryDTO)
        {
            try
            {
                var sql = "INSERT INTO Country (Name)" +
                           "VALUES (@Name)";

                var countryCreated = await _bd.ExecuteAsync(sql, new { Name = formCountryDTO.Name });

                if (countryCreated != 0)
                {
                    return true;
                }




                return false;
            }
            catch (Exception)
            {

                return false;
            }

           
        }

        public async Task<List<AdminCountryListDTO>> GetAdminCountryListDTOs()
        {
            var sql = "SELECT Id, Name, Active FROM Country";
            var countries = await _bd.QueryAsync<AdminCountryListDTO>(sql);
            return countries.ToList();
        }

        public async Task<List<Country>> GetAll()
        {
            var sql = "SELECT * FROM Country WHERE Active = 1 ORDER BY Name ASC ";

          var countries = await _bd.QueryAsync<Country>(sql);
            

            return countries.ToList();

        }

        public async Task<FormCountryDTO> GetCountry(int countryId)
        {
            var sql = "SELECT * FROM Country WHERE Id = @Id";
            var country = await _bd.QueryAsync<FormCountryDTO>(sql, new { Id = countryId });

            return country.Single();
        }



        public async Task<bool> UpdateActiveStatus(bool active, int countryId)
        {
            var sql = "UPDATE Country SET Active = @Active  WHERE Id = @Id";



            var country = await _bd.ExecuteAsync(sql, new { Active = !active, Id = countryId });

            if (country > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> UpdateCountry(FormCountryDTO formCountryDTO)
        {

            try
            {
                var sql = "UPDATE Country SET Name = @Name " +
                 "WHERE Id = @Id;";




                var country = await _bd.ExecuteAsync(sql, new { Name = formCountryDTO.Name, Id = formCountryDTO.Id });

                if (country > 0)
                {
                    return true;
                }

                return false;

            }
            catch (Exception)
            {

                return false;
            }

        }
    }
}
