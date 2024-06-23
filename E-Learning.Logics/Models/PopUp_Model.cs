using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E_Learning.Logics.Models
{
    public class PopUp_Model
    {
        [JsonPropertyName("pageIndex")]
        public int PageIndex { get; set; } = 1;
        [JsonPropertyName("pageSize")]


        public int PageSize { get; set; }
        [JsonPropertyName("searchValue")]


        public string SearchValue { get; set; } = string.Empty;
        [JsonPropertyName("tableName")]


        public string TableName { get; set; } = "";
        [JsonPropertyName("searchColumn")]
        public string SearchColumn {  get; set; } = string.Empty;

        [JsonPropertyName("searchType")]
        public string SearchType { get; set; } = "";
    }
}
