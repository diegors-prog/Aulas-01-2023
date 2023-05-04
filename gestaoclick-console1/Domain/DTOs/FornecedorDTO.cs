namespace Domain.DTOs
{
    public class FornecedorDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public int TipoFornecedor { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public bool Ativo { get; set; }

        /* EF Relations */
        public IList<ProdutoDTO> Produtos { get; set; }
    }
}