namespace Infrastructure.Repositories;

public partial class ChampionRepository(ApplicationDbContext dbContext)
{
    public static class ErrorMessages
    {
        public static string GetAll => "An error occurred while retrieving Champions";
        public static string Get => "An error occurred while retrieving the Champion";
        public static string Add => "An error occurred while adding the Champion";
        public static string Update => "An error occurred while updating the Champion";
        public static string Delete => "An error occurred while deleting the Champion";
        public static string NotFound => "Champion not found";
    }
}
