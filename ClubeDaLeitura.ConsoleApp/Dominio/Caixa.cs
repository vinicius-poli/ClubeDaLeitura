namespace ClubeDaLeitura.ConsoleApp.Dominio;

/*
● Campos obrigatórios:
    ○ Etiqueta (texto único, máximo 50 caracteres)
    ○ Cor (seleção de paleta ou hexadecimal)
    ○ Dias de empréstimo (número, padrão 7)
● Não pode haver etiquetas duplicadas
● Não permitir excluir uma caixa caso tenha revistas vinculadas
● Cada caixa define o prazo máximo para empréstimo de suas revistas
*/
//Encapsulamento

public class Caixa
{
    public string Etiqueta { get; set; } = string.Empty; 
    public string Cor { get; set; } = string.Empty;
    public int DiasDeEmprestimo { get; set; } = 7;
}
