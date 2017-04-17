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
            words = words.OrderByDescending(x => x.Occurances).ToList();
            this.Words = Mapper.Map<Word[], List<WordDTO>>(words.ToArray()).ToList();
            string JsonObject = JsonConvert.SerializeObject(this.Words);
            JArray obj = JArray.Parse(JsonObject);
            JArray labels = new JArray();
            JArray data = new JArray();
            JArray estimatedData = new JArray();
            long maxOccurances = obj.First()["Occurances"].Value<long>();
            int Rank = 1;
            foreach (JObject currObj in obj)
            {
                labels.Add(currObj["WordString"]);
                data.Add(currObj["Occurances"]);
                estimatedData.Add((long)Math.Ceiling((double)maxOccurances/Rank));
                Rank++;
            }
            this.WordsJSON = labels.ToString(Formatting.None);
            this.OccurancesJSON = data.ToString(Formatting.None);
            this.EstimatedOccurancesJSON = estimatedData.ToString(Formatting.None);
        } 

        public List<WordDTO> Words { get; set; }
        public string WordsJSON { get; set; }
        public string OccurancesJSON { get; set; }
        public string EstimatedOccurancesJSON { get; set; }
    }

    public class WordDTO
    {
        public int Id { get; set; }
        public string WordString { get; set; }
        public long Occurances{ get; set; }
    }
}
