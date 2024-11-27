using System.Net;
using Infrastructure.Custom.ResultPattern;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Presentation.WebApi;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        // Log la excepción (puedes usar un servicio de logging aquí)
        var exception = context.Exception;
        
        // Crea una respuesta personalizada según el tipo de excepción
        var result = exception switch
        {
            // Manejo de excepciones específicas
            DbUpdateConcurrencyException => Result.Failure("Error de concurrencia en bd", HttpStatusCode.InternalServerError),
            ArgumentNullException => Result.Failure("Uno de los valores requeridos es nulo", HttpStatusCode.BadRequest),
            DbUpdateException => Result.Failure("Error de base de datos", HttpStatusCode.InternalServerError),
            SqlException => Result.Failure("Error de base de datos. Verifique la conexion al estado de las consultas", HttpStatusCode.InternalServerError),
            NullReferenceException => Result.Failure("Ocurrió un error inesperado. Por favor, intente más tarde.", HttpStatusCode.InternalServerError),
            _ => Result.Failure("Ocurrió un error desconocido.", HttpStatusCode.InternalServerError) // Excepción genérica
        };

        // Configura el resultado de la respuesta HTTP
        context.Result = new JsonResult(result)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        // Marca la excepción como manejada
        context.ExceptionHandled = true;
    }
    }
