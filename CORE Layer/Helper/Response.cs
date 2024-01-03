using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE_Layer.Helper
{
    public class Response<T> where T : class
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public Response(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message;
        }
        public Response(T data)
        {
            Data = data;
        }
        private string GetDefultStatusCodeMessage(int statusCode)
          => statusCode switch
          {
              400 => "a bad Requset You have Made",
              401 => "Authorized , You are not ",
              404 => "Resource  was not found",
              405 => "This Email Already Exists !",
              500 => "Errore are the path to the dark side. Errors lead to Anger. Anger leads to hate. Hate leads to career change.",
              _ => null

          };

    }
}
