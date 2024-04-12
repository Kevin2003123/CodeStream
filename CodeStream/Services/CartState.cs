using CodeStream.Models;

namespace CodeStream.Services
{
    public class CartState
    {
        public List<int> cart { get; set; } = new List<int>();

        public event EventHandler? StateChangeHandler;
        private List<int> _currentCart = new List<int>();
        private void StateHasChanged()
        {
            StateChangeHandler?.Invoke(this, EventArgs.Empty);

        }

        public List<int> GetCart()
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
