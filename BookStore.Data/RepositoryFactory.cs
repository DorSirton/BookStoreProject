using BookStore.Models;


namespace BookStore.Data
{
    public class RepositoryFactory
    {
        public static IRepositery<Prodact> GetProductRespository()
        {
            return FileSystemData.Instance;
        }
    }
}
