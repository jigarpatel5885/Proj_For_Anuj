namespace Proj_Anuj.Models.ViewModels
{
    public class ProductVM 
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Company { get; set; }

        public List<Colors> Colors { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public bool Feature { get; set; }

        public bool Shipping { get; set; }

        public int Stock { get; set; }

        public int Review { get; set; }

        public float Stars { get; set; }

        public List<Image>Image { get; set; }

    }
}
