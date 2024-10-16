﻿namespace PedroWernekNetto;

public class Funcionario
{
    public int FuncionarioId { get; set; }
    public string? Nome { get; set; }
    public string Cpf { get; set; } = null!;
    public ICollection<Folha> Folhas { get; set; } = [];

}
