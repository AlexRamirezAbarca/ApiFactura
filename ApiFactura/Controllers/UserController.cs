using ApiFactura.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using ApiFactura.ODB;

namespace ApiFactura.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly ObjectDataBase _dbHelper;

        public UserController(ObjectDataBase dbHelper)
        {
            _dbHelper = dbHelper;
        }

        [HttpPost("add-user")]
        public IActionResult AddUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User data is null.");
            }

            var parameters = new List<SqlParameter>
            {
                 new SqlParameter { ParameterName = "@Identification", Value = user.Identification },
                 new SqlParameter { ParameterName = "@Username", Value = user.Username },
                 new SqlParameter { ParameterName = "@Password", Value = user.Password },
                 new SqlParameter { ParameterName = "@DateOrigin", Value = user.DateOrigin },
                 new SqlParameter { ParameterName = "@isAdmin", Value = user.IsAdmin },
                 new SqlParameter { ParameterName = "@NameUser", Value = user.NameUser },
                 new SqlParameter { ParameterName = "@Email", Value = user.Email },
                 new SqlParameter
            {
            ParameterName = "@Message",
            SqlDbType = SqlDbType.NVarChar,
            Size = 100,
            Direction = ParameterDirection.Output
            }
            };

            var result = _dbHelper.ExecuteGSP("dbo.AddUser", parameters);
            string mensajeSalida = parameters.FirstOrDefault(p => p.ParameterName == "@Message")?.Value.ToString();

            return Ok(new Response
            {
                StatusCode = 200,
                Message = mensajeSalida,
                Data = null
            });
        }

        [HttpGet("get-all-users")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var result = _dbHelper.ExecuteGSP("dbo.GetAllUsers", null);

                // Verificar si se obtuvieron resultados
                if (result == null || result.Rows.Count == 0)
                {
                    return NotFound(new Response
                    {
                        StatusCode = 404,
                        Message = "No se encontraron usuarios.",
                        Data = null
                    });
                }

                // Convertir los resultados en una lista de objetos User (asume que tienes un modelo User)
                var users = result.AsEnumerable().Select(row => new User
                {
                    IdUser = Convert.ToInt32(row["IdUser"]),
                    Identification = row["Identification"].ToString(),
                    Username = row["Username"].ToString(),
                    DateOrigin = Convert.ToDateTime(row["DateOrigin"]),
                    IsAdmin = Convert.ToBoolean(row["isAdmin"]),
                    NameUser = row["NameUser"].ToString(),
                    Email = row["Email"].ToString()
                }).ToList();

                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Usuarios obtenidos correctamente.",
                    Data = users
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error al obtener usuarios: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}
