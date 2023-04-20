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

namespace PortalGrupoAlyne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriarTabelaController : ControllerBase
    {
        private readonly DataContext _context;

        public CriarTabelaController(DataContext context)
        {
            _context = context;
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


        //[HttpGet("{tableName}")]
        //public async Task<IActionResult> GetTable(string tableName)
        //{
        //    if (!await TableExists(tableName))
        //        return NotFound();

        //    var columnNames = await GetColumnNames(tableName);

        //    return Ok(columnNames);
        //}

        //private async Task<List<string>> GetColumnNames(string tableName)
        //{
        //    var columnNames = new List<string>();

        //    using (var connection = _context.Database.GetDbConnection())
        //    {
        //        await connection.OpenAsync();

        //        var command = connection.CreateCommand();
        //        command.CommandText = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";

        //        using (var reader = await command.ExecuteReaderAsync())
        //        {
        //            while (await reader.ReadAsync())
        //            {
        //                var columnName = reader.GetString(0);
        //                columnNames.Add(columnName);
        //            }
        //        }
        //    }

        //    return columnNames;
        //}








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

                query += $"{name} {type}, ";
            }

            query = query.Remove(query.Length - 2); // Remove a última vírgula e espaço

            query += ")";

            await _context.Database.ExecuteSqlRawAsync(query);

            return Created($"api/tabela/{tableName}", null);
        }

        [HttpPut("{tableName}")]
        public async Task<IActionResult> UpdateTable(string tableName, [FromBody] List<Dictionary<string, object>> columns)
        {
            if (!await TableExists(tableName))
                return NotFound();

            var existingColumns = await GetColumns(tableName);

            var existingColumnNames = existingColumns.Select(c => c.Name);

            var addedColumns = columns.Where(c => !existingColumnNames.Contains(c["name"].ToString())).ToList();

            var modifiedColumns = columns.Where(c => existingColumnNames.Contains(c["name"].ToString())).ToList();

            if (addedColumns.Any())
            {
                var query = $"ALTER TABLE {tableName} ";

                foreach (var column in addedColumns)
                {
                    var name = column["name"].ToString();
                    var type = column["type"].ToString();

                    query += $"ADD {name} {type}, ";
                }

                query = query.Remove(query.Length - 2); // Remove a última vírgula e espaço

                await _context.Database.ExecuteSqlRawAsync(query);
            }

            if (modifiedColumns.Any())
            {
                foreach (var column in modifiedColumns)
                {
                    var name = column["name"].ToString();
                    var type = column["type"].ToString();

                    var query = $"ALTER TABLE {tableName} MODIFY {name} {type}";

                    await _context.Database.ExecuteSqlRawAsync(query);
                }
            }

            return Ok();
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

