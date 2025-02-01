namespace RestAPI_RepositoryPattern.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public double Salario { get; set; }
        public string CPF { get; set; }
        public string Senha { get; set; }
        public bool Status { get; set; }
    }
}
