using System;
namespace AroundTheWorld.Prism.Models
{
    public class Response 
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public Object Result { get; set; }
    }
}
