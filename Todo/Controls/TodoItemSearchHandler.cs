using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Todo.Controls
{
    public class TodoItemSearchHandler : SearchHandler
    {
        protected override async void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            await QueryItems(oldValue, newValue);
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            await Task.Delay(1000);

            var id = ((TodoItem) item).ID;

            // Note: strings will be URL encoded for navigation
            await Shell.Current.GoToAsync($"//todo/todoItem?itemid={id}");
        }

        private async Task QueryItems(string oldValue, string newValue)
        {
            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                var dbItems = await App.Database.GetItemsAsync();

                ItemsSource = dbItems
                        .Where(x => x.Name.ToLower()
                        .Contains(newValue.ToLower()))
                        .ToList();
            }
        }
    }
}