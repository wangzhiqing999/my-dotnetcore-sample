namespace A0050_BlazorApp.Components
{
    public class CascadingModel<T>
    {
        public CascadingModel()
        {

        }

        public CascadingModel(T defaultValue)
        {
            _value = defaultValue;
        }
        public Action StateHasChanged;

        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                StateHasChanged?.Invoke();
            }
        }


    }
}
