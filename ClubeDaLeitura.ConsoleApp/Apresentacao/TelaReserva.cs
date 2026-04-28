using System;
using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaReserva : ITela
{
    private RepositorioReserva repositorioReserva;
    private RepositorioEmprestimo repositorioEmprestimo;
    private RepositorioRevista repositorioRevista;
    private RepositorioAmigo repositorioAmigo;

    public TelaReserva(RepositorioReserva repositorioReserva, RepositorioEmprestimo repositorioEmprestimo, RepositorioRevista repositorioRevista, RepositorioAmigo repositorioAmigo)
    {
        this.repositorioReserva = repositorioReserva;
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioRevista = repositorioRevista;
        this.repositorioAmigo = repositorioAmigo;
    }

    public string? ObterOpcaoMenu()
    {                
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Empréstimos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Criar reserva");
        Console.WriteLine("2 - Concluir reserva");
        Console.WriteLine("3 - Visualizar reservas");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public void Abrir()
    {
        Reserva reserva = ObterDadosCadastrais();

        string[] erros = reserva.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine("---------------------------------");

            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < erros.Length; i++)
            {
                string erro = erros[i];

                Console.WriteLine(erro);
            }

            Console.ResetColor();
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");        
            Console.ReadLine();

            Abrir();
            return;
        } 

        reserva.Abrir();

        repositorioReserva.Cadastrar(reserva);

        ExibirMensagem($"O reserva \"{reserva.Id}\" foi aberta e cadastrada com sucesso!");       
    }

    public void Concluir()
    {
        ExibirCabecalho("Conclusão de Reserva");

        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("---------------------------------");

        Reserva? reservaSelecionada = null;

        do
        {
            Console.Write("Digite o id da reserva que deseja concluir: ");
            string? idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                reservaSelecionada = repositorioReserva.SelecionarPorId(idSelecionado);
            
        } while (reservaSelecionada == null);

        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2,-10} | {3, -15}",
            "id", "Revista", "Amigo", "Abertura"
        );

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2,-10} | {3, -15}",
            reservaSelecionada.Id, reservaSelecionada.Revista.Titulo, reservaSelecionada.Amigo.Nome, reservaSelecionada.Abertura.ToShortDateString()
        );

        Console.WriteLine("---------------------------------");
        Console.WriteLine("Deseja realmente concluir a reserva selecionado? (s/N)");
        string? opcaoContinuar = Console.ReadLine()?.ToUpper();

        if (opcaoContinuar != "S")
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");         
            Console.ReadLine(); 
            return;
        }

        reservaSelecionada.Concluir();

        ExibirMensagem($"A reserva \"{reservaSelecionada.Id}\" foi concluída com sucesso!");
    }

    public void VisualizarTodos(bool deveExibirCabecalho)
    {
        if(deveExibirCabecalho)
            ExibirCabecalho("Visualisação de Reservas");

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2,-10} | {3, -10} | {4, -15}",
            "id", "Revista", "Amigo", "Abertura", "Status"
        );

        Reserva?[] reservas = repositorioReserva.SelecionarTodos();

        for (int i = 0; i < reservas.Length; i++)
        {
            Reserva? r = reservas[i];

            if (r == null)
                continue;

            Console.Write("{0, -7} | ", r.Id);
            Console.Write("{0, -15} | ", r.Revista.Titulo);
            Console.Write("{0, -10} | ", r.Amigo.Nome);
            Console.Write("{0, -10} | ", r.Abertura.ToShortDateString());            

            string status = r.Status.ToString();

            
            if (r.Status == StatusReserva.Indefinida)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;                    
            }

            else if (r.Status == StatusReserva.Ativa)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;                
            }
                

            else if (r.Status == StatusReserva.Concluída)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                status = "Concluído";
            }               

            Console.Write("{0, -10}", status);

            Console.ResetColor();
            Console.WriteLine();
            
        } 


        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");         
            Console.ReadLine();            
        }
    }

    protected void ExibirMensagem(string mensagem)
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");        
        Console.ReadLine();
    }

    private Reserva ObterDadosCadastrais()
    {
        VisualizarRevistas();

        Revista? revista = null;

        do
        {
            Console.Write("Digite o id da revista que deseja reservar: ");
            string? idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                revista = (Revista?)repositorioRevista.SelecionarPorId(idSelecionado);
            
        } while (revista == null);
        
        VisualizarAmigos();

        Amigo? amigo = null;

        do
        {
            Console.Write("Digite o id do amigo que deseja reservar a revista: ");
            string? idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                amigo = (Amigo?)repositorioAmigo.SelecionarPorId(idSelecionado);
            
        } while (amigo == null);
        
        return new Reserva(revista, amigo);    
    }

    private void VisualizarRevistas()
    {        
        Console.WriteLine(
            "{0, -7} | {1, -25} | {2,-6} | {3, -17} | {4, -15}",
            "id", "Título", "Edição", "Ano de Publicação", "Caixa"
        );

        EntidadeBase?[] revistas = repositorioRevista.SelecionarTodas();

        for (int i = 0; i < revistas.Length; i++)
        {
            Revista? r = (Revista?)revistas[i];

            if (r == null)
                continue;
            
            Console.Write("{0, -7} | ", r.Id);
            Console.Write("{0, -25} | ", r.Titulo);
            Console.Write("{0, -6} | ", r.NumeroEdicao);
            Console.Write("{0, -17} | ", r.AnoPublicacao);

            string corSelecionada = r.Caixa.Cor;

            if (corSelecionada == "Vermelho")
                Console.ForegroundColor = ConsoleColor.Red;

            else if (corSelecionada == "Verde")
                Console.ForegroundColor = ConsoleColor.Green;

            else if (corSelecionada == "Azul")
                Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write("{0, -15}", r.Caixa.Etiqueta);

            Console.ResetColor();
            Console.WriteLine();
        }   

        Console.WriteLine("---------------------------------");            
    }

    private void VisualizarAmigos()
    {
        Console.WriteLine(
            "{0, -7} |  {1, -15} | {2,-15} | {3, -13}",
            "id", "Nome", "Responsável", "Telefone"
        );

        EntidadeBase?[] amigos = repositorioAmigo.SelecionarTodas();

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo? a = (Amigo?)amigos[i];

            if (a == null)
                continue;

            Console.WriteLine(
            "{0, -7} |  {1, -15} | {2,-15} | {3, -13}",
            a.Id, a.Nome, a.NomeResponsavel, a.Telefone
        );
            
        } 

        Console.WriteLine("---------------------------------");       
    }

    protected void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Reservas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(titulo);        
        Console.WriteLine("---------------------------------");
    }
}
