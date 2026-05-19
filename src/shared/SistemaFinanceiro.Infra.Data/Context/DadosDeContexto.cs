using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaFinanceiro.Infra.Data.Context;

public class DadosDeContexto
{
    public static DadosDeContexto Instance { get; private set; }
    public string ConnectionString { get; private set; }

    public static void AssignNewInstance(string connectionString)
    {
        Instance = new DadosDeContexto
        {
            ConnectionString = connectionString
        };
    }
}
