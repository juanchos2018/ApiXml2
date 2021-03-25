using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class ClsDataComprobante
    {
        public string ruc { get; set; }
        public string anio { get; set; }
        public string TipoDcumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Serie { get; set; }
        public bool RbtnTermico { get; set; }

    }
}