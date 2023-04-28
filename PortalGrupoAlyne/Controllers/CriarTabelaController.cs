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

        [HttpGet]
        public IActionResult GetAllTables()
        {
            try
            {
                var tableNames = _context.Model.GetEntityTypes()
                    .Select(t => t.GetTableName())
                    .ToList();
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





        [HttpGet("tabela/{tableName}")]
 
        public async Task<IEnumerable<dynamic>> GetTabela(string tableName)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using IDbConnection connection = new MySqlConnection(connectionString);
            string query = $"SELECT * FROM {tableName}";
            IEnumerable<dynamic> result = await connection.QueryAsync(query);
            return result;
        }






        [HttpGet("tabela/{tableName}/{tabelaLigada}/{campoLigacao}/{campoExibido}")]
        public async Task<IEnumerable<dynamic>> GetTabela(string tableName, string tabelaLigada, string campoLigacao, string campoExibido)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using IDbConnection connection = new MySqlConnection(connectionString);
            string campoVirtual = $"{campoLigacao}";
            string query = $"SELECT {tableName}.*, CONCAT({tabelaLigada}.id, '-', {tabelaLigada}.{campoExibido}) as {campoVirtual} FROM {tableName} INNER JOIN {tabelaLigada} ON {tableName}.{campoLigacao} = {tabelaLigada}.id";


            IEnumerable<dynamic> result = await connection.QueryAsync(query);
            return result;
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

        private async Task<List<string>> GetTableColumns(string tableName)
        {
            var columns = new List<string>();
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

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

            await connection.CloseAsync();

            return columns;
        }



        //[HttpPut("{tableName}")]
        //public async Task<IActionResult> UpdateTable(string tableName, [FromBody] List<Dictionary<string, object>> columns)
        //{
        //    if (!await TableExists(tableName))
        //        return NotFound();

        //    var existingColumns = await GetColumns(tableName);

        //    var existingColumnNames = existingColumns.Select(c => c.Name);

        //    var addedColumns = columns.Where(c => !existingColumnNames.Contains(c["name"].ToString())).ToList();

        //    var modifiedColumns = columns.Where(c => existingColumnNames.Contains(c["name"].ToString())).ToList();

        //    if (addedColumns.Any())
        //    {
        //        var query = $"ALTER TABLE {tableName} ";

        //        foreach (var column in addedColumns)
        //        {
        //            var name = column["name"].ToString();
        //            var type = column["type"].ToString();

        //            query += $"ADD {name} {type}, ";
        //        }

        //        query = query.Remove(query.Length - 2); // Remove a última vírgula e espaço

        //        await _context.Database.ExecuteSqlRawAsync(query);
        //    }

        //    if (modifiedColumns.Any())
        //    {
        //        foreach (var column in modifiedColumns)
        //        {
        //            var name = column["name"].ToString();
        //            var type = column["type"].ToString();

        //            var query = $"ALTER TABLE {tableName} MODIFY {name} {type}";

        //            await _context.Database.ExecuteSqlRawAsync(query);
        //        }
        //    }

        //    return Ok();
        //}

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

