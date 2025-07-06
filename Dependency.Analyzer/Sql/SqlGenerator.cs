using Dapper;
using System.Data.SqlClient;
using System.Reflection;

namespace Dependency.Analyzer;

public static class SqlGenerator
{

    public static string GenerateInsertStatement<T>(List<T> items, string tableName)
    {
        if (string.IsNullOrEmpty(tableName)) throw new ArgumentException("Table name cannot be null or empty.", nameof(tableName));

        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        if (properties.Length == 0) return $"-- No properties found for type {type.Name}";

        List<string> columnNames = properties.Select(p => p.Name).ToList();

        string insertStatement = $"INSERT INTO {tableName} ({string.Join(", ", columnNames)}) VALUES ";
        var comma = "";
        foreach (var item in items)
        {
            var valuePlaceholders = GetPropertyValues<T>(item).Select(e => e.Value != null ? $"'{e.Value.ToString()}'" : "null").ToList();
            insertStatement += $"\r\n{comma}({string.Join(",", valuePlaceholders)})";
            comma = ",";
        }
        return insertStatement;
    }

    public static async Task<IEnumerable<T>> GetData<T>(string query)
    {
        IEnumerable<T> list = new List<T>();
        using (var sql = new SqlConnection(DependencyAnalyzerOptions.DbConnectionString))
        {
            await sql.OpenAsync();
            list = await sql.QueryAsync<T>(query);
            await sql.CloseAsync();
        }
        return list;
    }

    public static async Task InsertData<T>(List<T> items)
    {
        using (var sql = new SqlConnection(DependencyAnalyzerOptions.DbConnectionString))
        {
            await sql.OpenAsync();
            await sql.ExecuteAsync(@"
if(NOT EXISTS (SELECT 1 FROM sys.objects where name='DependencyItems'))
BEGIN
CREATE TABLE DependencyItems (
    Assembly NVARCHAR(255) NOT NULL,
    AbstractionAssembly NVARCHAR(255) NULL,
    Name NVARCHAR(255) NOT NULL,
    Namespace NVARCHAR(255) NULL,
    AbstractionName NVARCHAR(255) NULL,
    AbstractionNamespace NVARCHAR(255) NULL,
    Classification NVARCHAR(255) NOT NULL
);
END
ELSE
TRUNCATE TABLE DependencyItems

");
            var total = items.Count;
            int i = 1;
            var chunk = new List<T>();
            var chunkSize = 1000;
            var current = Console.CursorTop;
            foreach (var item in items)
            {
                chunk.Add(item);
                if (chunk.Count >= chunkSize)
                {
                    var insertSql = GenerateInsertStatement(chunk, "DependencyItems");
                    await sql.ExecuteAsync(insertSql);
                    Console.WriteLine($"");
                    Console.CursorTop = current;
                    Console.WriteLine($"{i += chunk.Count}/{total}");
                    Console.CursorTop = current;
                    chunk = new List<T>();
                }
            }
            if (chunk.Any())
            {
                var insertSql = GenerateInsertStatement(chunk, "DependencyItems");
                await sql.ExecuteAsync(insertSql);
                Console.WriteLine($"");
                Console.CursorTop = current;
                Console.WriteLine($"{i += chunk.Count}/{total}");
            }
            await sql.CloseAsync();
        }
    }

    public static Dictionary<string, object?> GetPropertyValues<T>(T obj)
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        return properties.ToDictionary(p => p.Name, p => p.GetValue(obj));
    }
}
