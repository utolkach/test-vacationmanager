using System;
using System.Collections.Generic;
using System.Linq;

namespace VacationManager.ApplicationServices.Models
{
    public class ServiceResult<T>
    {
        public ServiceResult()
        {
            this.Messages = new List<string>();
        }

        public ServiceResult(T result)
        {
            this.BadRequest = false;
            this.Messages = new List<string>();
            this.Result = result;
        }

        public ServiceResult(string errorMessages)
        {
            this.BadRequest = true;
            this.Messages = new List<string>();
            this.Messages.Add(errorMessages);
        }

        public ServiceResult(Exception exception)
        {
            this.BadRequest = true;
            this.Exception = exception;
            this.Messages.Add(exception.Message);
        }

        public bool BadRequest { get; set; }
        public bool NotFound { get; set; }

        public T Result { get; set; }

        public Exception Exception { get; set; }

        public List<string> Messages { get; set; }

        public string GetMessage()
        {
            return this.Messages.Any() ? this.Messages.Aggregate((x, y) => x + "\r\n" + y) : null;
        }
    }
}
