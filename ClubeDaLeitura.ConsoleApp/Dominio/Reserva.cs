using System.Security.Cryptography;

namespace ClubeDaLeitura.ConsoleApp.Dominio;

public enum StatusReserva
{
    Indefinida,
    Ativa,
    Concluída
}

public class Reserva
{
    public string Id { get; set; } = string.Empty;

    public Revista Revista { get; set;}

    public Amigo Amigo { get; set; }

    //public Emprestimo Emprestimo { get; set; }

    public DateTime Abertura { get; set; }

    public StatusReserva Status { get; set; } = StatusReserva.Indefinida;

    public Reserva(Revista revista, Amigo amigo)
    {
        Id = Convert.ToHexString(RandomNumberGenerator.GetBytes(20)).ToLower().Substring(0, 7);

        Revista = revista;
        Amigo = amigo;
        
    }

    public string[] Validar()
    {
        string erros = string.Empty;

        if (Revista == null)
            erros = "O campo \"Revista\" deve ser preenchido.";

        if (Amigo == null)
            erros = "O campo \"Amigo\" deve ser preenchido.";

        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries);
    }

    public void Abrir()
    {
        Abertura = DateTime.Now;

        Status = StatusReserva.Ativa;
        Revista.Reservar();
        Amigo.AdicionarReserva(this);
              
    }

    public void Concluir()
    {
        Status = StatusReserva.Concluída;
        //Revista.Emprestar();
        //Amigo.AdicionarEmprestimo();
    }
}
