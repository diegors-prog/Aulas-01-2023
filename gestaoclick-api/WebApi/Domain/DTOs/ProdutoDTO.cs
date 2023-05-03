namespace Domain.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public int FornecedorId { get; set; }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Imagem { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations */
        public FornecedorDTO Fornecedor { get; set; }
    }
}