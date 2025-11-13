using System.Text;
using DesafioProjetoHospedagem.Models;
using Newtonsoft.Json;

Console.OutputEncoding = Encoding.UTF8;

// Cria os modelos de hóspedes e cadastra na lista de hóspedes
List<Pessoa> hospedes = new List<Pessoa>();

Pessoa p1 = new Pessoa(nome: "Hóspede 1");
Pessoa p2 = new Pessoa(nome: "Hóspede 2");

hospedes.Add(p1);
hospedes.Add(p2);

// Cria a suíte
Suite suite = new Suite(tipoSuite: "Premium", capacidade: 2, valorDiaria: 30);

// Cria uma nova reserva, passando a suíte e os hóspedes
Reserva reserva = new Reserva(diasReservados: 10);
reserva.CadastrarSuite(suite);
reserva.CadastrarHospedes(hospedes);

// Exibe a quantidade de hóspedes e o valor da diária
Console.WriteLine("NOVA RESERVA: ");

Console.WriteLine($"Hóspedes: {reserva.ObterQuantidadeHospedes()}");
Console.WriteLine($"Valor diária: {reserva.CalcularValorDiaria():C} \n");

string caminho = "Arquivos/reservas.json";

// Cria o reservas.json caso não exista
if (!File.Exists(caminho))
{
    File.WriteAllText(caminho, null);
}

// Exibe as reservas registradas no arquivo
string conteudoJson = File.ReadAllText(caminho);
List<Reserva> reservas = JsonConvert.DeserializeObject<List<Reserva>>(conteudoJson) ?? new List<Reserva>();

Console.WriteLine("HISTORICO DE RESERVAS: ");

foreach (Reserva reservaAtual in reservas)
{   
    Console.WriteLine($"Hóspedes: {reservaAtual.ObterQuantidadeHospedes()}");
    Console.WriteLine($"Valor diária: {reservaAtual.CalcularValorDiaria():C} \n");
}

// Adiciona a nova reserva as já existentes no .json
reservas.Add(reserva);

// Persiste as reservas no .json
string reservaJson = JsonConvert.SerializeObject(reservas, Formatting.Indented);
File.WriteAllText(caminho, reservaJson);