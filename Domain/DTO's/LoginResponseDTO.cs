using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.DTO_s
{
    public class LoginResponseDTO
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
