using System;
using System.Collections.Generic;
using System.Linq;

// Classe que representa uma palavra na escritura
class Palavra
{
    public string Texto { get; set; }
    public bool Oculta { get; set; }

    public Palavra(string texto)
    {
        Texto = texto;
        Oculta = false;
    }
}

// Classe que representa uma referência de escritura
class Referencia
{
    public string Livro { get; set; }
    public int Capitulo { get; set; }
    public int VersiculoInicio { get; set; }
    public int VersiculoFim { get; set; }

    public Referencia(string livro, int capitulo, int versiculoInicio, int versiculoFim)
    {
        Livro = livro;
        Capitulo = capitulo;
        VersiculoInicio = versiculoInicio;
        VersiculoFim = versiculoFim;
    }

    // Método para obter a representação da referência em string
    public override string ToString()
    {
        if (VersiculoInicio == VersiculoFim)
            return $"{Livro} {Capitulo}:{VersiculoInicio}";
        else
            return $"{Livro} {Capitulo}:{VersiculoInicio}-{VersiculoFim}";
    }
}

// Classe que representa uma escritura
class Escritura
{
    private List<Palavra> palavras;

    public Referencia Referencia { get; set; }

    public Escritura(Referencia referencia, string texto)
    {
        Referencia = referencia;
        InicializarPalavras(texto);
    }

    private void InicializarPalavras(string texto)
    {
        string[] palavrasArray = texto.Split(' ');
        palavras = palavrasArray.Select(palavra => new Palavra(palavra)).ToList();
    }

    // Método para ocultar palavras aleatórias
    public void OcultarPalavrasAleatorias()
    {
        Random random = new Random();

        foreach (Palavra palavra in palavras)
        {
            // Se a palavra não estiver oculta, há uma chance aleatória de ocultá-la
            if (!palavra.Oculta && random.Next(2) == 0)
            {
                palavra.Oculta = true;
            }
        }
    }

    // Método para exibir a escritura
    public void ExibirEscritura()
    {

        Console.Clear();
        Console.WriteLine($"{Referencia.ToString()} - Escritura:");

        foreach (Palavra palavra in palavras)
        {
            // Se a palavra estiver oculta, exibe sublinhado, caso contrário, exibe a palavra
            Console.Write(palavra.Oculta ? " _ " : $"{palavra.Texto} ");
        }

        Console.WriteLine("\nPressione Enter para continuar...");
    }

    // Método para verificar se todas as palavras estão ocultas
    public bool TodasPalavrasOcultas()
    {
        return palavras.All(palavra => palavra.Oculta);
    }
}

class Program
{
    static void Main()
    {
        // Exibe a mensagem de boas-vindas
        ExibirMensagemDeBoasVindas();

        // Aguarda a entrada do usuário antes de criar a escritura de exemplo
        Console.ReadLine();

        // Exibe o menu de escolha de escrituras
        int escolhaEscritura = ExibirMenuDeEscrituras();

        // Criar uma escritura com base na escolha do usuário
        Escritura escritura;

        switch (escolhaEscritura)
        {
            case 1:
                escritura = CriarEscrituraExemplo("João", 3, 16, 16, "Porque Deus amou o mundo...");
                break;
            case 2:
                escritura = CriarEscrituraExemplo("Mateus", 5, 1, 12, "Vendo a multidão, subiu ao monte...");
                break;
            case 3:
                escritura = CriarEscrituraExemplo("Salmo", 23, 1, 6, "O Senhor é o meu pastor, nada me faltará...");
                break;
            default:
                Console.WriteLine("Escolha inválida. Saindo do programa.");
                return;
        }

        do
        {
            // Exibe a escritura
            escritura.ExibirEscritura();

            // Aguarda a entrada do usuário
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
            {
                break;
            }

            // Oculta palavras aleatórias e exibe a escritura novamente
            escritura.OcultarPalavrasAleatorias();

        } while (!escritura.TodasPalavrasOcultas());
    }

    static void ExibirMensagemDeBoasVindas()
    {
        Console.WriteLine("Bem-vindo ao Memorizador de Escrituras!");
        Console.WriteLine("Este programa ajudará você a memorizar uma escritura ocultando palavras a cada pressionar de Enter.");
        Console.WriteLine("Digite 'quit' a qualquer momento para sair.");
        Console.WriteLine();
    }

    static int ExibirMenuDeEscrituras()
    {
        Console.WriteLine("Escolha uma escritura para praticar:");
        Console.WriteLine("1. João 3:16");
        Console.WriteLine("2. Mateus 5:1-12");
        Console.WriteLine("3. Salmo 23:1-6");

        int escolha;
        while (!int.TryParse(Console.ReadLine(), out escolha) || escolha < 1 || escolha > 3)
        {
            Console.WriteLine("Escolha inválida. Tente novamente:");
        }

        return escolha;
    }

    static Escritura CriarEscrituraExemplo(string livro, int capitulo, int versiculoInicio, int versiculoFim, string texto)
    {
        Referencia referencia = new Referencia(livro, capitulo, versiculoInicio, versiculoFim);
        return new Escritura(referencia, texto);
    }
}
