using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Configuration;

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriarTabelaController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public CriarTabelaController(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //[HttpGet]
        //public IActionResult GetAllTables()
        //{
        //    try
        //    {
        //        var tableNames = _context.Model.GetEntityTypes()
        //            .Select(t => t.GetTableName())
        //            .ToList();
        //        return Ok(tableNames);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}


        [HttpGet]
        public IActionResult GetAllTables()
        {
            try
            {
                var tableNames = new List<string>();
                var connection = _context.Database.GetDbConnection();
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tableNames.Add(reader.GetString(0));
                    }
                }
                return Ok(tableNames);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpGet("{tableName}")]
        public async Task<IActionResult> GetTable(string tableName)
        {
            if (!await TableExists(tableName))
                return NotFound();

            var columns = await GetColumns(tableName);

            return Ok(columns);
        }





        //[HttpGet("tabela/{tableName}")]

        //public async Task<IEnumerable<dynamic>> GetTabela(string tableName)
        //{
        //    string connectionString = _configuration.GetConnectionString("DefaultConnection");
        //    using IDbConnection connection = new MySqlConnection(connectionString);
        //    string query = $"SELECT * FROM {tableName}";
        //    IEnumerable<dynamic> result = await connection.QueryAsync(query);
        //    return result;
        //}


       
        //pesquisa sem ligação
        [HttpGet("tabela/{tableName}")]
        public async Task<IActionResult> GetTabelaPaginada(string tableName, int pagina, int totalPaginas, string? fieldName = null, string? fieldValue = null, string? campoExpressao = null, string? sqlExpressao = null)
        {
            int skip = (pagina - 1) * totalPaginas;
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using IDbConnection connection = new MySqlConnection(connectionString);

            // Query para obter a quantidade total de registros
            string countQuery = $"SELECT COUNT(*) FROM {tableName}";

            if (fieldName != null && fieldValue != null)
            {
                // Adiciona a cláusula WHERE na query com o campo e valor a serem buscados
                countQuery += $" WHERE {fieldName} LIKE '%{fieldValue}%'";
            }

            int totalRegistros = await connection.QueryFirstOrDefaultAsync<int>(countQuery);

            // Query para obter os registros paginados
            string paginatedQuery = $"SELECT *";

            if (!string.IsNullOrEmpty(campoExpressao) && !string.IsNullOrEmpty(sqlExpressao))
            {
                paginatedQuery += $", ({sqlExpressao}) as {campoExpressao}";
            }

            paginatedQuery += $" FROM {tableName}";

            if (fieldName != null && fieldValue != null)
            {
                // Adiciona a cláusula WHERE na query com o campo e valor a serem buscados
                paginatedQuery += $" WHERE {fieldName} LIKE '%{fieldValue}%'";
            }

            paginatedQuery += $" LIMIT {skip},{totalPaginas}";
            IEnumerable<dynamic> result = await connection.QueryAsync(paginatedQuery);

            return Ok(new
            {
                totalRegistros,
                data = result
            });
        }






        [HttpGet("tabela/{tableName}/{tabelaLigada}/{campoLigacao}/{campoExibido}")]
        public async Task<IActionResult> GetTabelaPaginada(
     string tableName, string tabelaLigada, string campoLigacao, string campoExibido,
     [FromQuery] int pagina, [FromQuery] int totalpagina,
     string? fieldName = null, string? fieldValue = null,
     string? campoExpressao = null, string? sqlExpressao = null)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var connection = new MySqlConnection(connectionString);

                var countQuery = $"SELECT COUNT(*) FROM {tableName} INNER JOIN {tabelaLigada} ON {tableName}.{campoLigacao} = {tabelaLigada}.id";
                if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(fieldValue))
                {
                    countQuery += $" WHERE {tableName}.{fieldName} = '{fieldValue}'";
                }

                var countResult = await connection.QueryFirstOrDefaultAsync<int>(countQuery);

                var skip = (pagina - 1) * totalpagina;
                var take = totalpagina;

                var query = $"SELECT {tableName}.*";
                if (!string.IsNullOrEmpty(campoExpressao) && !string.IsNullOrEmpty(sqlExpressao))
                {
                    query += $", ({sqlExpressao}) as {campoExpressao}";
                }
                query += $", CONCAT({tabelaLigada}.id, '| ', {tabelaLigada}.{campoExibido}) as {campoLigacao} FROM {tableName} INNER JOIN {tabelaLigada} ON {tableName}.{campoLigacao} = {tabelaLigada}.id";
                if (!string.IsNullOrEmpty(fieldName) && !string.IsNullOrEmpty(fieldValue))
                {
                    query += $" WHERE {tableName}.{fieldName} LIKE '%{fieldValue}%'";

                }

                query += $" ORDER BY {tableName}.id ASC LIMIT {skip},{take}";

                var result = await connection.QueryAsync<dynamic>(query);

                return Ok(new
                {
                    total = countResult,
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar os dados da tabela {tableName}: {ex.Message}");
            }
        }












        //get da tabela ligação

        [HttpGet("tabela/{tableName}/{campoExibidor}")]
        public async Task<IEnumerable<object>> GetTabela(string tableName, string campoExibidor)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using IDbConnection connection = new MySqlConnection(connectionString);
            string query = $"SELECT id as value, {campoExibidor} as label FROM {tableName}";

            IEnumerable<dynamic> result = await connection.QueryAsync(query);
            IEnumerable<object> list = result.Select(r => new { value = r.value, label = r.label });

            return list;
        }

        //popular as tabelas

        [HttpPost("criarTabela{tableName}")]
        public async Task<IActionResult> Post(string tableName, [FromBody] IEnumerable<Dictionary<string, object>> items)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using IDbConnection connection = new MySqlConnection(connectionString);

                foreach (var item in items)
                {
                    var columns = string.Join(", ", item.Keys);
                    var values = string.Join(", ", item.Values.Select(v => $"'{v.ToString()}'"));
                    string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
                    await connection.ExecuteAsync(query);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }



       





        [HttpPost("{tableName}")]
        public async Task<IActionResult> CreateTable(string tableName, [FromBody] List<Dictionary<string, object>> columns)
        {
            if (await TableExists(tableName))
                return Conflict();

            string query = $"CREATE TABLE {tableName} (";

            foreach (var column in columns)
            {
                var name = column["name"].ToString();
                var type = column["type"].ToString();

                
                if ((column.TryGetValue("primaryKey", out var primaryKeyObj) && primaryKeyObj.ToString().Equals("true", StringComparison.OrdinalIgnoreCase)))
                {
                    query += $"{name} INT AUTO_INCREMENT PRIMARY KEY, ";
                }
                else
                {
                    query += $"{name} {type}, ";
                }
            }

            query = query.Remove(query.Length - 2); // Remove a última vírgula e espaço

            query += ")";

            await _context.Database.ExecuteSqlRawAsync(query);

            return Created($"api/tabela/{tableName}", null);
        }


        [HttpPost("{tableName}/update")]
        public async Task<IActionResult> UpdateTable(string tableName, [FromBody] List<Dictionary<string, object>> columns)
        {
            if (!await TableExists(tableName))
                return NotFound();

            var existingColumns = await GetTableColumns(tableName);

            var addColumns = columns.Where(c => !existingColumns.Any(ec => ec.Equals(c["name"].ToString(), StringComparison.OrdinalIgnoreCase)));
            var removeColumns = existingColumns.Where(ec => !columns.Any(c => c["name"].ToString().Equals(ec, StringComparison.OrdinalIgnoreCase)));

            foreach (var column in addColumns)
            {
                var name = column["name"].ToString();
                var type = column["type"].ToString();

                if (column.TryGetValue("primaryKey", out var primaryKeyObj) && primaryKeyObj.ToString().Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    await _context.Database.ExecuteSqlRawAsync($"ALTER TABLE {tableName} ADD COLUMN {name} INT AUTO_INCREMENT PRIMARY KEY");
                }
                else
                {
                    await _context.Database.ExecuteSqlRawAsync($"ALTER TABLE {tableName} ADD COLUMN {name} {type}");
                }
            }

            foreach (var column in removeColumns)
            {
                await _context.Database.ExecuteSqlRawAsync($"ALTER TABLE {tableName} DROP COLUMN {column}");
            }

            return Ok();
        }

        //private async Task<List<string>> GetTableColumns(string tableName)
        //{
        //    var columns = new List<string>();
        //    var connection = _context.Database.GetDbConnection();
        //    await connection.OpenAsync();

        //    using (var command = connection.CreateCommand())
        //    {
        //        command.CommandText = $"SELECT column_name FROM information_schema.columns WHERE table_name = '{tableName}'";

        //        using (var reader = await command.ExecuteReaderAsync())
        //        {
        //            while (await reader.ReadAsync())
        //            {
        //                columns.Add(reader.GetString(0));
        //            }
        //        }
        //    }

        //    await connection.CloseAsync();

        //    return columns;
        //}

        private async Task<List<string>> GetTableColumns(string tableName)
{
    var columns = new List<string>();
    var connection = _context.Database.GetDbConnection();
    if (connection.State != ConnectionState.Open)
    {
        await connection.OpenAsync();
    }

    using (var command = connection.CreateCommand())
    {
        command.CommandText = $"SELECT column_name FROM information_schema.columns WHERE table_name = '{tableName}'";

        using (var reader = await command.ExecuteReaderAsync())
        {
            while (await reader.ReadAsync())
            {
                columns.Add(reader.GetString(0));
            }
        }
    }

    return columns;
}




        //deletar dados pela tabela e campo

        [HttpDelete("deletarRegistros/{tableName}/{fieldName}/{fieldValue}")]
        public async Task<IActionResult> Delete(string tableName, string fieldName, string fieldValue)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");
                using IDbConnection connection = new MySqlConnection(connectionString);

                string query = $"DELETE FROM {tableName} WHERE {fieldName} = '{fieldValue}'";
                await connection.ExecuteAsync(query);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

       

        

        private async Task<bool> TableExists(string tableName)
        {
            var tables = await _context.Database.GetDbConnection().GetSchemaAsync("Tables", new[] { null, null, tableName });

            return tables.Rows.Count > 0;
        }

        private async Task<List<Column>> GetColumns(string tableName)
        {
            var columns = new List<Column>();

            var schema = await _context.Database.GetDbConnection().GetSchemaAsync("Columns", new[] { null, null, tableName });

            foreach (System.Data.DataRow row in schema.Rows)
            {
                var name = row["COLUMN_NAME"].ToString();
                var type = row["DATA_TYPE"].ToString();

                columns.Add(new Column(name, type));
            }

            return columns;
        }
    }

    public class Column
    {
        public Column(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public string Name { get; set; }
        public string Type { get; set; }
    }
}

