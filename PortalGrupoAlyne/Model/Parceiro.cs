using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalGrupoAlyne.Model
{
    public class Parceiro
    {
        [Key]
        public int id { get; set; }
        public int? codParceiro { get; set; }

        [StringLength(100, ErrorMessage = "inserir no máximo 100 caracteres")]
        public string? Nome { get; set; }

        [StringLength(10, ErrorMessage = "inserir no máximo 10 caracteres")]
        public string? TipoPessoa { get; set; }

        [StringLength(100, ErrorMessage = "inserir no máximo 100 caracteres")]
        public string? NomeFantasia { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Cnpj_Cpf { get; set; }

        [StringLength(80, ErrorMessage = "inserir no máximo 80 caracteres")]
        public string? Email { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Fone { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Canal { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? Classificacao { get; set; }

        [StringLength(60, ErrorMessage = "inserir no máximo 60 caracteres")]
        public string? TamanhoLoja { get; set; }

        [StringLength(100, ErrorMessage = "inserir no máximo 100 caracteres")]
        public string? Endereco { get; set; }

        [StringLength(50, ErrorMessage = "inserir no máximo 50 caracteres")]
        public string? Bairro { get; set; }

        [StringLength(50, ErrorMessage = "inserir no máximo 50 caracteres")]
        public string? Municipio { get; set; }

        [StringLength(4, ErrorMessage = "inserir no máximo 4 caracteres")]
        public string? UF { get; set; }

        [StringLength(10, ErrorMessage = "inserir no máximo 10 caracteres")]
        public string? Lat { get; set; }

        [StringLength(10, ErrorMessage = "inserir no máximo 10 caracteres")]
        public string? Long { get; set; }

        [StringLength(10, ErrorMessage = "inserir no máximo 10 caracteres")]
        public string? Status { get; set; }
        public bool? SemVisita { get; set; }
        public bool? PrimeiraSem { get; set; }
        public bool? SegundaSem { get; set; }
        public bool? TerceiraSem { get; set; }
        public bool? QuartaSem { get; set; }
        public bool? QuintaSem { get; set; }
        public bool? Segunda { get; set; }
        public bool? Terca { get; set; }
        public bool? Quarta { get; set; }
        public bool? Quinta { get; set;}
        public bool? Sexta { get; set; }
        public bool? Sabado { get; set; }

        [StringLength(30, ErrorMessage = "inserir no máximo 30 caracteres")]
        public string? TipoNegociacao { get; set; }

        [StringLength(30, ErrorMessage = "inserir no máximo 30 caracteres")]
        public string? Empresa { get; set; }

        [ForeignKey("Vendedor")]
        public int VendedorId { get; set; }
        public Vendedor? Vendedor { get; set; }

        public IEnumerable<TabelaPrecoParceiro>? TabelaPrecoParceiro { get; set; }
        public DateTime? AtualizadoEm { get; set; }



    }
}
