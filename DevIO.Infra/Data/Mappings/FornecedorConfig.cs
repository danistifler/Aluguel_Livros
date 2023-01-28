using DevIO.Business.Models.Fornecedores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Infra.Data.Mappings
{
    public class FornecedorConfig :  EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorConfig()
        {
            HasKey(c => c.Id);

            Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200);

            Property(c => c.Documento)
                .IsRequired()
                .HasMaxLength(14)
                .HasColumnAnnotation("IX_Documento",
                new IndexAnnotation(new IndexAttribute { IsUnique = true }));                

            HasRequired(c => c.Endereco)
                .WithRequiredPrincipal(e => e.Fornecedor);

            ToTable("Fornecedores");

        }
    }
}
