using CodeStream.Models.DTO;

namespace CodeStream.Services
{
    public class SearchState
    {
        public string search { get; set; } = string.Empty;

        public event EventHandler? StateChangeHandler;
        private string _currentSearch = string.Empty;
        private void StateHasChanged()
        {
            StateChangeHandler?.Invoke(this, EventArgs.Empty);

        }

        public string GetSearch()
        {
            return _currentSearch;
        }

        public void SetSearch()
        {
             _currentSearch = search;

            StateHasChanged();
        }
    }
}
