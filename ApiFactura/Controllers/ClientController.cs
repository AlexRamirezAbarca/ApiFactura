using ApiFactura.Models;
using ApiFactura.ODB;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ApiFactura.Controllers
{

    [ApiController]
    [Route("client")]
    public class ClientController : ControllerBase
    {
        private readonly ObjectDataBase _dbHelper;

        public ClientController(ObjectDataBase dbHelper)
        {
            _dbHelper = dbHelper;
        }

        [HttpPost("add-client")]
        public IActionResult AddClient([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Client data is null."
                });
            }

            var parameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@Identification", Value = client.Identification },
                new SqlParameter { ParameterName = "@Name", Value = client.Name },
                new SqlParameter { ParameterName = "@Telephone", Value = client.Telephone },
                new SqlParameter { ParameterName = "@Email", Value = client.Email },
                new SqlParameter
            {
                ParameterName = "@Message",
                SqlDbType = SqlDbType.NVarChar,
                Size = 100,
                Direction = ParameterDirection.Output
            }
            };

            var result = _dbHelper.ExecuteGSP("dbo.AddClient", parameters);

            if (result == null)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = "Error executing stored procedure."
                });
            }

            // Obtener el mensaje de salida del SP
            string? mensajeSalida = parameters
                .FirstOrDefault(p => p.ParameterName == "@Message")?
                .Value.ToString();

            // Evaluar el mensaje para determinar si es una inserción exitosa o un error de existencia
            if (mensajeSalida == "El usuario ya existe")
            {
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = mensajeSalida
                });
            }

            return Ok(new Response
            {
                StatusCode = 200,
                Message = mensajeSalida ?? "El usuario se registró correctamente",
                Data = client
            });

        }

        [HttpGet("get-all-clients")]
        public IActionResult GetAllClients()
        {
            try
            {
                // Ejecuta el procedimiento almacenado para obtener todos los clientes
                var result = _dbHelper.ExecuteGSP("dbo.GetAllClients", null);

                if (result.Rows.Count == 0)
                {
                    return NotFound(new Response
                    {
                        StatusCode = 404,
                        Message = "No se encontraron clientes.",
                        Data = null
                    });
                }

                // Convierte el resultado en una lista de objetos Client
                var clients = new List<Client>();
                foreach (DataRow row in result.Rows)
                {
                    clients.Add(new Client
                    {
                        IdClient = Convert.ToInt32(row["IdClient"]),
                        Identification = row["Identification"].ToString(),
                        Name = row["Name"].ToString(),
                        Telephone = row["Telephone"].ToString(),
                        Email = row["Email"].ToString()
                    });
                }

                // Retorna la lista de clientes
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Clientes obtenidos exitosamente.",
                    Data = clients
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error interno del servidor: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("get-client-by-id/{identification}")]
        public IActionResult GetClientById(string identification)
        {
            try
            {
                // Configuración de parámetros para el SP
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@Identification", Value = identification }
                };

                // Ejecuta el SP para obtener el cliente
                var result = _dbHelper.ExecuteGSP("dbo.GetClientByIdentification", parameters);

                if (result == null || result.Rows.Count == 0)
                {
                    return NotFound(new Response
                    {
                        StatusCode = 404,
                        Message = "Cliente no encontrado.",
                        Data = null
                    });
                }

                // Crea el objeto Client a partir del resultado
                var row = result.Rows[0];
                var client = new Client
                {
                    IdClient = Convert.ToInt32(row["IdClient"]),
                    Identification = row["Identification"].ToString(),
                    Name = row["Name"].ToString(),
                    Telephone = row["Telephone"].ToString(),
                    Email = row["Email"].ToString()
                };

                // Retorna el cliente encontrado
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Cliente obtenido exitosamente.",
                    Data = client
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error interno del servidor: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPut("update-client")]
        public IActionResult UpdateClient([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Datos del cliente no válidos.",
                    Data = null
                });
            }

            // Configurar los parámetros para el SP
            var parameters = new List<SqlParameter>
    {
        new SqlParameter { ParameterName = "@Identification", Value = client.Identification },
        new SqlParameter { ParameterName = "@Name", Value = client.Name },
        new SqlParameter { ParameterName = "@Telephone", Value = client.Telephone },
        new SqlParameter { ParameterName = "@Email", Value = client.Email },
        new SqlParameter
        {
            ParameterName = "@Message",
            SqlDbType = SqlDbType.NVarChar,
            Size = 100,
            Direction = ParameterDirection.Output
        }
    };

            // Ejecuta el SP
            var result = _dbHelper.ExecuteGSP("UpdateClient", parameters);

            // Obtiene el mensaje de salida
            var mensajeSalida = parameters.Find(p => p.ParameterName == "@Message")?.Value.ToString();

            if (mensajeSalida == "El cliente no existe.")
            {
                return NotFound(new Response
                {
                    StatusCode = 404,
                    Message = mensajeSalida,
                    Data = null
                });
            }

            return Ok(new Response
            {
                StatusCode = 200,
                Message = mensajeSalida,
                Data = null
            });
        }

        [HttpDelete("delete-client/{identification}")]
        public IActionResult DeleteClient(int identification)
        {
            // Configurar los parámetros para el SP
            var parameters = new List<SqlParameter>
    {
        new SqlParameter { ParameterName = "@Identification", Value = identification },
        new SqlParameter
        {
            ParameterName = "@Message",
            SqlDbType = SqlDbType.NVarChar,
            Size = 100,
            Direction = ParameterDirection.Output
        }
    };

            // Ejecuta el SP
            var result = _dbHelper.ExecuteGSP("DeleteClient", parameters);

            // Obtiene el mensaje de salida
            var mensajeSalida = parameters.Find(p => p.ParameterName == "@Message")?.Value.ToString();

            if (mensajeSalida == "El cliente no existe.")
            {
                return NotFound(new Response
                {
                    StatusCode = 404,
                    Message = mensajeSalida,
                    Data = null
                });
            }

            return Ok(new Response
            {
                StatusCode = 200,
                Message = mensajeSalida,
                Data = null
            });
        }


    }
}
