using System.IO.Compression;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaEmprestimo
{
    private RepositorioEmprestimo repositorioEmprestimo;
    private RepositorioRevista repositorioRevista;
    private RepositorioAmigo repositorioAmigo;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioRevista repositorioRevista, RepositorioAmigo repositorioAmigo)
    {
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioRevista = repositorioRevista;
        this.repositorioAmigo = repositorioAmigo;
    }

    public void VisualizarTodos(bool deveExibirCabecalho)
    {
        if(deveExibirCabecalho)
            ExibirCabecalho("Visualisação de Empréstimos");

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2,-10} | {3, -10} | {4, -15} | {5, -10}",
            "id", "Revista", "Amigo", "Abertura", "Conclusão Prev.", "Status"
        );

        Emprestimo?[] emprestimos = repositorioEmprestimo.SelecionarTodos();

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo? e = emprestimos[i];

            if (e == null)
                continue;

            Console.Write("{0, -7} | ", e.Id);
            Console.Write("{0, -15} | ", e.Revista.Titulo);
            Console.Write("{0, -10} | ", e.Amigo.Nome);
            Console.Write("{0, -10} | ", e.Abertura.ToShortDateString());
            Console.Write("{0, -15} | ", e.ConclusaoPrevista.ToShortDateString());

            string status = e.Status.ToString();

            if (e.EstaAtrasado)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                status = "Atrasado";
            }

            else if (e.Status == StatusEmprestimo.Indefinido)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;                    
            }

            else if (e.Status == StatusEmprestimo.Aberto)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;                
            }
                

            else if (e.Status == StatusEmprestimo.Concluído)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                status = "Concluído";
            }               

            Console.Write("{0, -10}", status);

            //Console.WriteLine(
            //"{0, -7} |  {1, -15} | {2,-10} | {3, -10} | {4, -15} | {5, -10}",
            //e.Id, e.Revista.Titulo, e.Amigo.Nome, e.Abertura.ToShortDateString(), e.ConclusaoPrevista.//ToShortDateString(), e.Status);

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

    
    public string? ObterOpcaoMenu()
    {
                
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Empréstimos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Abrir empréstimo");
        Console.WriteLine("2 - Concluir empréstimo");
        Console.WriteLine("3 - Visualizar empréstimos");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public void Abrir()
    {
        Emprestimo emprestimo = ObterDadosCadastrais();

        string[] erros = emprestimo.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine("---------------------------------");

            Console.ForegroundColor = ConsoleColor.Red;

            for (int i =0; i < erros.Length; i++)
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

        emprestimo.Abrir();

        repositorioEmprestimo.Cadastrar(emprestimo);

        ExibirMensagem($"O empréstimo \"{emprestimo.Id}\" foi aberto e cadastrado com sucesso!");       
    }

    private Emprestimo ObterDadosCadastrais()
    {
        VisualizarRevistas();

        Revista? revista = null;

        do
        {
            Console.Write("Digite o id da revista que deseja emprestar: ");
            string? idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                revista = (Revista?)repositorioRevista.SelecionarPorId(idSelecionado);
            
        } while (revista == null);
        
        VisualizarAmigos();

        Amigo? amigo = null;

        do
        {
            Console.Write("Digite o id do amigo que deseja emprestar a revista: ");
            string? idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                amigo = (Amigo?)repositorioAmigo.SelecionarPorId(idSelecionado);
            
        } while (amigo == null);
        

        return new Emprestimo(revista, amigo);
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

    protected void ExibirMensagem(string mensagem)
    {        
        Console.WriteLine("---------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");        
        Console.ReadLine();
    }

    protected void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Emprétimos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(titulo);        
        Console.WriteLine("---------------------------------");
    }
    
}
