using FluentValidation; //validationException için gerekli
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusTicketSystem.WebApi.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); //isteği bir sonraki adıma gönder
            }
            catch(Exception error)
            {
                await HandleExceptionAsync(context, error); //eğer bir hata fırlatılırsa burada yakala
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            context.Response.ContentType = "application/json";

            //varsayılan hata kodu(500)
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var responseModel = new ErrorResponse { Message = error.Message };

            switch (error)
            {
                case ValidationException e:
                    //Eğer hata FluentValidation'dan geliyorsa
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Message = "Doğrulama hatası oluştu";
                    //Hataları listeye çeviriyoruz
                    responseModel.Errors = e.Errors.Select(x => x.ErrorMessage).ToList();
                    break;

                 default:
                    responseModel.Message = "Beklenmedik bir hata oluştu";
                    break;
            }
            //JSON olarak yanıt dön
            var result = JsonSerializer.Serialize(responseModel);
            await context.Response.WriteAsync(result);
        }
    }

    public class ErrorResponse  //JSON dönüş formatımız
    {
        public string Message { get; set; } = string.Empty;
        public List<string>? Errors { get; set; } //Validasyon hataları için liste
    }
}
