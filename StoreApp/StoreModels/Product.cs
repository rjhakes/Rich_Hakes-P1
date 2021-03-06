namespace StoreModels
{
    //This class should contain all necessary fields to define a product.
    public class Product
    {
        private string productName;
        private double productPrice;
        private Category categoryType;
        private string brandName;

        public string ProdName { get; set; }

        public double ProdPrice { get; set; }
        public Category ProdCategory { get; set; }
        public string ProdBrandName { get; set; }
        public string Description { get; set; }

        public int Id { get; set; }

        public override string ToString() => $"\n\tProduct Name:\t\t{this.ProdName}\n\tPrice:\t\t\t{this.ProdPrice}\n\tCategory:\t\t{this.ProdCategory}\n\tBrand Name:\t\t{this.ProdBrandName}\n";
        //todo: add more properties to define a product (maybe a category?)
    }
}