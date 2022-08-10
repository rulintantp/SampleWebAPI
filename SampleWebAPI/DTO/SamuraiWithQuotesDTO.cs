using SampleWebAPI.Domain;

namespace SampleWebAPI.DTO
{
    public class SamuraiWithQuotesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<QuoteDTO> Quotes { get; set; }
    }
}
