using ApiFactura.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using ApiFactura.ODB;

namespace ApiFactura.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductsController : ControllerBase
    {
        private readonly ObjectDataBase _dbHelper;

        public ProductsController(ObjectDataBase dbHelper)
        {
            _dbHelper = dbHelper;
        }

        [HttpPost("add-product")]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Datos del producto son nulos.",
                    Data = null
                });
            }

            var parameters = new List<SqlParameter>
    {
        new SqlParameter { ParameterName = "@CodeProduct", Value = product.CodeProduct },
        new SqlParameter { ParameterName = "@NameProduct", Value = product.NameProduct },
        new SqlParameter { ParameterName = "@UnitPrice", Value = product.UnitPrice },
        new SqlParameter { ParameterName = "@StatusProduct", Value = product.StatusProduct },
        new SqlParameter
        {
            ParameterName = "@Message",
            SqlDbType = SqlDbType.NVarChar,
            Size = 100,
            Direction = ParameterDirection.Output
        }
    };

            // Ejecuta el procedimiento almacenado
            var result = _dbHelper.ExecuteGSP("dbo.AddProduct", parameters);

            // Obtiene el mensaje de salida
            var mensajeSalida = parameters.Find(p => p.ParameterName == "@Message")?.Value.ToString();

            if (mensajeSalida == "El producto ya existe.")
            {
                return Conflict(new Response
                {
                    StatusCode = 409,
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

        [HttpGet("get-all-products")]
        public IActionResult GetAllProducts()
        {
            var result = _dbHelper.ExecuteGSP("dbo.GetAllProducts", null);

            if (result == null || result.Rows.Count == 0)
            {
                return NotFound(new Response
                {
                    StatusCode = 404,
                    Message = "No se encontraron productos.",
                    Data = null
                });
            }

            var products = new List<Product>();

            foreach (DataRow row in result.Rows)
            {
                products.Add(new Product
                {
                    CodeProduct = Convert.ToInt32(row["CodeProduct"]),
                    NameProduct = row["NameProduct"].ToString(),
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                    StatusProduct = Convert.ToBoolean(row["StatusProduct"])
                });
            }

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Productos obtenidos correctamente.",
                Data = products
            });
        }
        [HttpGet("get-product-by-id/{codeProduct}")]
        public IActionResult GetProductById(int codeProduct)
        {
            try
            {
                // Configuración de parámetros para el SP
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@CodeProduct", Value = codeProduct }
                };

                // Ejecuta el SP para obtener el cliente
                var result = _dbHelper.ExecuteGSP("dbo.GetProductById", parameters);

                if (result == null || result.Rows.Count == 0)
                {
                    return NotFound(new Response
                    {
                        StatusCode = 404,
                        Message = "Producto no encontrado.",
                        Data = null
                    });
                }

                // Crea el objeto Product a partir del resultado
                var row = result.Rows[0];
                var product = new Product
                {
                    CodeProduct = Convert.ToInt32(row["CodeProduct"]),
                    NameProduct = row["NameProduct"].ToString(),
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                    StatusProduct = Convert.ToBoolean(row["StatusProduct"])
                };

                // Retorna el cliente encontrado
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Producto obtenido exitosamente.",
                    Data = product
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

        [HttpDelete("delete-product/{codeProduct}")]
        public IActionResult DeleteProduct(int codeProduct)
        {
            // Configurar los parámetros para el SP
            var parameters = new List<SqlParameter>
    {
        new SqlParameter { ParameterName = "@CodeProduct", Value = codeProduct },
        new SqlParameter
        {
            ParameterName = "@Message",
            SqlDbType = SqlDbType.NVarChar,
            Size = 100,
            Direction = ParameterDirection.Output
        }
    };

            // Ejecuta el SP
            var result = _dbHelper.ExecuteGSP("dbo.DeleteProducts", parameters);

            // Obtiene el mensaje de salida
            var mensajeSalida = parameters.Find(p => p.ParameterName == "@Message")?.Value.ToString();

            if (mensajeSalida == "El producto no existe.")
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

        [HttpPut("update-product")]
        public IActionResult UpdateClient([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest(new Response
                {
                    StatusCode = 400,
                    Message = "Datos del Producto no válidos.",
                    Data = null
                });
            }

            // Configurar los parámetros para el SP
            var parameters = new List<SqlParameter>
    {
        new SqlParameter { ParameterName = "@CodeProduct", Value = product.CodeProduct },
        new SqlParameter { ParameterName = "@NameProduct", Value = product.NameProduct },
        new SqlParameter { ParameterName = "@UnitPrice", Value = product.UnitPrice },
        new SqlParameter { ParameterName = "@StatusProduct", Value = product.StatusProduct },
        new SqlParameter
        {
            ParameterName = "@Message",
            SqlDbType = SqlDbType.NVarChar,
            Size = 100,
            Direction = ParameterDirection.Output
        }
    };

            // Ejecuta el SP
            var result = _dbHelper.ExecuteGSP("dbo.UpdateProduct", parameters);

            // Obtiene el mensaje de salida
            var mensajeSalida = parameters.Find(p => p.ParameterName == "@Message")?.Value.ToString();

            if (mensajeSalida == "El producto no existe.")
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
