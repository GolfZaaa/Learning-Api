﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace TestApi.Models
{
    public partial class Certificate
    {
        public int Id { get; set; }
        public string CertificateCode { get; set; }
        public DateTime? CertificateDate { get; set; }
        public int? UserCode { get; set; }
        public string Status { get; set; }
        public string SignName { get; set; }
        public string ProjectCode { get; set; }

    }
}