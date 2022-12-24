using Workwise.API.Data.Interface;
using Workwise.API.Models;

namespace Workwise.API.Data
{
    public class CompanyRepository : ICompanyRepository
    {
        public IEnumerable<Company> GetAllCompanies()
        {
            List<Company> model = new()
            {
                new Company()
                {
                    Id = 1,
                    Name = "Facebook Inc.",
                    ImageUrl = @"/images/cmp-icon.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                },
                new Company()
                {
                    Id = 1,
                    Name = "Google Inc.",
                    ImageUrl = @"/images/cmp-icon1.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                },
                new Company()
                {
                    Id = 1,
                    Name = "Pinterest",
                    ImageUrl = @"/images/cmp-icon2.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                },
                new Company()
                {
                    Id = 1,
                    Name = "Microsoft Inc.",
                    ImageUrl = @"/images/cmp-icon3.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                },
                new Company()
                {
                    Id = 1,
                    Name = "Line Inc.",
                    ImageUrl = @"/images/cmp-icon4.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                },
                new Company()
                {
                    Id = 1,
                    Name = "Linked In",
                    ImageUrl = @"/images/cmp-icon5.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                },
                new Company()
                {
                    Id = 1,
                    Name = "Apple Inc.",
                    ImageUrl = @"/images/cmp-icon6.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                },
                new Company()
                {
                    Id = 1,
                    Name = "Samsung Inc.",
                    ImageUrl = @"/images/cmp-icon7.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                },
                new Company()
                {
                    Id = 1,
                    Name = "Oppo",
                    ImageUrl = @"/images/cmp-icon8.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                },
                new Company()
                {
                    Id = 1,
                    Name = "Game loft",
                    ImageUrl = @"/images/cmp-icon9.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                },
                new Company()
                {
                    Id = 1,
                    Name = "Android Inc.",
                    ImageUrl = @"/images/cmp-icon10.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                },
                new Company()
                {
                    Id = 1,
                    Name = "Oracle",
                    ImageUrl = @"/images/cmp-icon11.png",
                    EstablishedDate = new DateTime(2004, 2, 1)
                }
            };

            return model;
        }
    }
}