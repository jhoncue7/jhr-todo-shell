using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoListPage : ContentPage
    {
        public TodoListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetItemsAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("todo/todoItem");
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var selectedItem = e.SelectedItem as TodoItem;
                await Shell.Current.GoToAsync($"//todo/todoItem?itemid={selectedItem?.ID}");
            }
        }
    }
}