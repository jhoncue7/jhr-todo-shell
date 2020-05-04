using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
            BindingContext = this;
        }

        // Application navigation URIs
        void RegisterRoutes()
        {
            Routing.RegisterRoute("todo/todoItem", typeof(TodoItemPage));
            Routing.RegisterRoute("todo/todoList", typeof(TodoListPage));
            Routing.RegisterRoute("info/about", typeof(AboutPage));
        }
    }
}