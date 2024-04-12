using CodeStream.Models.DTO;

namespace CodeStream.Services
{
    public class CourseCartDtoState
    {
        public List<CourseCartDTO> cart { get; set; } = new List<CourseCartDTO>();

        public event EventHandler? StateChangeHandler;
        private List<CourseCartDTO> _currentCart = new List<CourseCartDTO>();
        private void StateHasChanged()
        {
            StateChangeHandler?.Invoke(this, EventArgs.Empty);

        }

        public List<CourseCartDTO> GetCart()
        {
            return _currentCart;
        }

        public void SetCart()
        {
            _currentCart = cart;

            StateHasChanged();
        }
    }
}
