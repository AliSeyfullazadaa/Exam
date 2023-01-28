using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamTicketTwo.Models
{
    public class TeamMember
    {
        public TeamMember(int id)
        {
            Id=id;
        }
        public TeamMember( string fullName, string profession, string imageUrl, string twitterUrl, string facebookUrl, string instagramUrl, string linkedinUrl)
        {
            FullName = fullName;
            Profession = profession;
            ImageUrl = imageUrl;
            TwitterUrl = twitterUrl;
            FacebookUrl = facebookUrl;
            InstagramUrl = instagramUrl;
            LinkedinUrl = linkedinUrl;
        }

        public int? Id { get; set; }
        public string FullName { get; set; }
        public string Profession { get; set; }
        
        public string ImageUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedinUrl { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }

    }
    public class TeamMemberCreateDto
    {

        public string FullName { get; set; }
        public string Profession { get; set; }

        public string TwitterUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedinUrl { get; set; }
        [NotMapped]
        public IFormFile FormFile { get; set; }

    }
}
