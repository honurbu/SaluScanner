using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaluScanner.SharedLibrary.DTOs
{
    public class ErrorDto
    {
        public List<string> Errors { get; private set; }

        public ErrorDto()
        {
            Errors = new List<String>();
        }

        public ErrorDto(string error)
        {
            this.Errors.Add(error);
        }

        public ErrorDto(List<string> errors)
        {
            this.Errors=errors;
        }
    }
}
