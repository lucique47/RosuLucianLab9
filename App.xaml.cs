using RosuLucianLab7.Data;

namespace RosuLucianLab7
{
    public partial class App : Application
    {
        static ShoppingListDatabase? database;
        public static ShoppingListDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ShoppingListDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyDatabase.db3")); // Change 'Database' to 'ShoppingListDatabase'
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
