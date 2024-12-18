using RosuLucianLab7.Models;

namespace RosuLucianLab7;

public partial class ProductPage : ContentPage
{
    ShopList sl;
    public ProductPage(ShopList slist)
    {
        InitializeComponent();
        sl = slist;
    }
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var product = (Product)BindingContext;
        await App.Database.SaveProductAsync(product);
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var product = listView.SelectedItem as Product;
        if (product == null)
        {
            // Handle the case when no product is selected
            await DisplayAlert("Error", "No product selected", "OK");
            return;
        }
        await App.Database.DeleteProductAsync(product);
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        System.Diagnostics.Debug.WriteLine("OnAppearing called");
        listView.ItemsSource = await App.Database.GetProductsAsync();
    }
    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        if (listView.SelectedItem is Product p)
        {
            var lp = new ListProduct()
            {
                ShopListID = sl.ID,
                ProductID = p.ID
            };
            await App.Database.SaveListProductAsync(lp);
            p.ListProducts = [lp];

            // Reîmprospătează lista de produse
            listView.ItemsSource = await App.Database.GetProductsAsync();

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "No product selected", "OK");
        }
    }
    void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem is Product selectedProduct)
        {
            // Debugging statement to verify selection
            System.Diagnostics.Debug.WriteLine($"Selected product: {selectedProduct.Description}");
        }
    }
}