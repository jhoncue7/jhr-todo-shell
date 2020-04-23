using System.Linq;
using System.Threading.Tasks;
using Todo.Models;
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

            // Note: strings will be URL encoded for navigation
            await (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync($"//todo/todoItem?itemid={((TodoItem)item).ID}");
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
                        .ToList<TodoItem>();
            }
        }
    }
}