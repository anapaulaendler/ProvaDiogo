﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PedroWernekNetto;

#nullable disable

namespace PedroWernekNetto.Migrations
{
    [DbContext(typeof(AppDataContext))]
    partial class AppDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("PedroWernekNetto.Folha", b =>
                {
                    b.Property<int>("FolhaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Ano")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FuncionarioId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ImpostoFgts")
                        .HasColumnType("REAL");

                    b.Property<double>("ImpostoInss")
                        .HasColumnType("REAL");

                    b.Property<double>("ImpostoIrrf")
                        .HasColumnType("REAL");

                    b.Property<int>("Mes")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantidade")
                        .HasColumnType("INTEGER");

                    b.Property<double>("SalarioBruto")
                        .HasColumnType("REAL");

                    b.Property<double>("SalarioLiquido")
                        .HasColumnType("REAL");

                    b.Property<double>("Valor")
                        .HasColumnType("REAL");

                    b.HasKey("FolhaId");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("TabelaFolhas");
                });

            modelBuilder.Entity("PedroWernekNetto.Funcionario", b =>
                {
                    b.Property<int>("FuncionarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.HasKey("FuncionarioId");

                    b.ToTable("TabelaFuncionarios");
                });

            modelBuilder.Entity("PedroWernekNetto.Folha", b =>
                {
                    b.HasOne("PedroWernekNetto.Funcionario", "Funcionario")
                        .WithMany("Folhas")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("PedroWernekNetto.Funcionario", b =>
                {
                    b.Navigation("Folhas");
                });
#pragma warning restore 612, 618
        }
    }
}
