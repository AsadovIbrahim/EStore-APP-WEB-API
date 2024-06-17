using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Domain.DTO_s
{
    public class GetAllCategoryRequestDTO
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
