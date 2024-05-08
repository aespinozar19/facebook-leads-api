using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AutoPlan.Facebook.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacebookLeadsController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public FacebookLeadsController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                string requestBody = await reader.ReadToEndAsync();

                // Loggear el cuerpo de la solicitud en un archivo de texto
                LogRequestBody(requestBody);
            }

            // Responder OK a Facebook
            return Ok();
        }

        private void LogRequestBody(string requestBody)
        {
            // Ruta donde se guardará el archivo de registro
            string logFilePath = "logs/facebook_webhook.log";

            try
            {
                // Escribir el cuerpo de la solicitud en el archivo de registro
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine(requestBody);
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error al escribir en el archivo de registro
                _logger.LogError($"Error al escribir en el archivo de registro: {ex.Message}");
            }
        }


    }
}
