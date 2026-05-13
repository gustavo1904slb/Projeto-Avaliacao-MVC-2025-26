namespace _14480_employes_managment.Models
{
    public class UserActivity
    {
        public string? CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? ModifiedById { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
// o Employee herda: Employee : UserActivity
// evita repetir código
// adiciona o controlo de criação e edição