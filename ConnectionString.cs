using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

public class DatabaseService
{
    private readonly string _connectionString = "Server=SERVIDOR;Database=NOME_DO_BANCO;User Id=USUARIO;Password=SENHA;TrustServerCertificate=True;";

    public async Task<bool> TestarConexaoAsync()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                Console.WriteLine("Conexão bem-sucedida!");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao conectar: {ex.Message}");
            return false;
        }
    }
}
