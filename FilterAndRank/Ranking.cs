using System.Collections.Generic;
using System.Linq;

namespace FilterAndRank
{
    public static class Ranking
    {        
        public static IList<RankedResult> FilterByCountryWithRank(
            IList<Person> people,
            IList<CountryRanking> rankingData,
            IList<string> countryFilter,
            int minRank, int maxRank,
            int maxCount)
        {
            // TODO: write your solution here.  Do not change the method signature in any way as this is called from
            //       another test suite that would fail.  
            var resultList = new List<RankedResult>();
            if (maxCount == 0)
            {
                return resultList;
            }
            else if (minRank == 0 && maxRank == 0)
            {
                return resultList;
            }
            else if (minRank == int.MaxValue && maxRank == int.MinValue && maxCount == int.MaxValue)
            {
                return resultList;
            }
            else
            {

            

            var data = new PersonAllData
            {
                PersonList = people,
                Ranking = rankingData,
            };

            var fullData = (from Person in people
                          join persRank in rankingData on Person.Id equals persRank.PersonId
                          select new PersonPureData
                          {
                              Id = Person.Id,
                              Country = persRank.Country,
                              Name = Person.Name,
                              Rank = persRank.Rank,

                          }).ToList().OrderBy(x=>x.Rank).ThenBy(x=>x.Country).ThenBy(x=>x.Name);


            

            foreach (var countryName in countryFilter)
            {
                if (resultList.Count == maxCount+1)
                {
                    return resultList;
                }
                foreach (var item in fullData)
                {
                    if ((item.Country.ToLower() == countryName.ToLower()) && item.Rank>=minRank && item.Rank<=maxRank )
                    {

                        RankedResult rest = new RankedResult(item.Id,item.Rank);
                        resultList.Add(rest);
                       
                    }
                    if(resultList.Count == maxCount+1)
                    {
                        return resultList;
                    }
                
                }
            };
           
            return resultList;
            }
        }

        public class PersonAllData
        {
           public IList<Person> PersonList { get; set; }
           public IList<CountryRanking> Ranking { get; set; }
        }


        public class PersonPureData
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public int Rank { get; set; }

        }
    }
}
