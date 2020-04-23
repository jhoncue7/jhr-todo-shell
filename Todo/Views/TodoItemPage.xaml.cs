using System;
using System.Linq;
using System.Threading.Tasks;
using Todo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.Views
{
    [QueryProperty("ItemId", "itemid")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoItemPage : ContentPage
    {
        public TodoItemPage()
        {
            InitializeComponent();
        }

        private int? _id;

        public string ItemId
        {
            set
            {
                var idString = Uri.UnescapeDataString(value);
                var isParsed = int.TryParse(idString, out int id);

                _id = isParsed ? id : (int?) null;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await SetBindingContext();
        }

        private async Task SetBindingContext()
        {
            if (_id.HasValue)
            {
                var dbItems = await App.Database.GetItemsAsync();
                BindingContext = dbItems.FirstOrDefault(x => x.ID == _id.Value);
            }
            else
            {
                BindingContext = new TodoItem();
            }
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.Database.SaveItemAsync(todoItem);
            await Shell.Current.Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.Database.DeleteItemAsync(todoItem);
            await Shell.Current.Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}