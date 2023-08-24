﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.Logics.Models
{
    public class FilterKabupatenBody
    {
        [JsonPropertyName("provinsi")]
        public string? provinsi {  get; set; }
        [JsonPropertyName("kabupaten")]
        public string? kabupaten { get; set; }
    }
}
