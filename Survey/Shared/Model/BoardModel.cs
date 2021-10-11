using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.Shared.Model
{
    public class BoardModel
    {

        public int Id { get; set; }
        public User OwnerUser { get; set; }
        // todo kitolto user tipust letrehozni atnevezni
        //public ICollection<User> surveyFillingUser { get; set; }
        public ICollection<CardModel> Cards { get; set; }


    }
}
