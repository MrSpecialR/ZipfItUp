using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipfItUp.Models;
using Newtonsoft.Json;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Newtonsoft.Json.Linq;

namespace ZipfItUp.DataTranfer
{
    public class WordInfo
    {
        
        public WordInfo(List<Word> words)
        {
           // Mapper.Initialize(x => x.CreateMap<Word, WordDTO>());
           this.Words = Mapper.Map<Word[], List<WordDTO>>(words.ToArray()).ToList();
           // this.WordsJSON = JsonConvert.SerializeObject(this.Words);
            string JsonObject = JsonConvert.SerializeObject(this.Words);
            JArray obj = JArray.Parse(JsonObject);
            JArray labels = new JArray();
            JArray data = new JArray();
            foreach (JObject currObj in obj)
            {
                labels.Add(currObj["WordString"]);
                data.Add(currObj["Occurances"]);
            }
            this.WordsJSON = labels.ToString(Formatting.None);
            this.OccurancesJSON = data.ToString(Formatting.None);
        } 

        public List<WordDTO> Words { get; set; }
        public string WordsJSON { get; set; }
        public string OccurancesJSON { get; set; }
    }

    public class WordDTO
    {
        public int Id { get; set; }
        public string WordString { get; set; }
        public long Occurances{ get; set; }
    }
}
