using CodeStream.Models;
using CodeStream.Models.DTO;

namespace CodeStream.Repositoy.IRepository
{
    public interface ICountryRepository
    {
        public Task<List<Country>> GetAll();
        public Task<List<AdminCountryListDTO>> GetAdminCountryListDTOs();
        public Task<bool> UpdateActiveStatus(bool active, int countryId);
        public Task<bool> CreateCountry(FormCountryDTO formCountryDTO);
        public Task<FormCountryDTO> GetCountry(int countryId);
        public Task<bool> UpdateCountry(FormCountryDTO formCountryDTO);
    }
}
