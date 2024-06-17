using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.DTO_s
{
    public class RequestResponseDTO
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
